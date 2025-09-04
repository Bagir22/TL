import { useState, useEffect, useCallback } from 'react';
import { useFromCurrency, useToCurrency } from './useCurrencies';

export type PriceInTime = {
  dateTime: string;
  price: number;
}

export const usePrices = () => {
  const [prices, setPrices] = useState<PriceInTime[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fromCurrency = useFromCurrency();
  const toCurrency = useToCurrency();

  const fetchPrices = useCallback(async () => {
    if (!fromCurrency || !toCurrency) {
      return;
    }

    setLoading(true);
    setError(null);

    try {
      const response = await fetch(
        `http://localhost:5081/prices?` +
        `PaymentCurrency=${fromCurrency}&` +
        `PurchasedCurrency=${toCurrency}&` +
        `FromDateTime=${new Date().toISOString().split('T')[0]}`
      );

      if (!response.ok) {
        throw new Error(`Server returned: ${response.status}`);
      }

      const data: { price: number; dateTime: string }[] = await response.json();

      const fiveMinutesAgoDateTime = new Date(new Date().getTime() - 5 * 60 * 1000);

      const filteredData = data
        .map(item => ({
          dateTime: item.dateTime,
          price: item.price
        }))
        .filter(item => {
          const itemTime = new Date(item.dateTime).getTime();

          return itemTime >= fiveMinutesAgoDateTime.getTime();
        })
        .sort((a, b) =>
          new Date(a.dateTime).getTime() - new Date(b.dateTime).getTime());

      setPrices(filteredData);
    } catch (err) {
      setError('Failed to fetch prices');
    } finally {
      setLoading(false);
    }
  }, [fromCurrency, toCurrency]);

  useEffect(() => {
    fetchPrices();

    const interval = setInterval(fetchPrices, 10000);

    return () => clearInterval(interval);
  }, [fetchPrices]);

  return { prices, error, loading };
};