import { useState, useEffect } from 'react';
import { Currency } from '../domain/Currency.ts';

export const useCurrencyInfo = (currencyCode: string | null) => {
  const [currencyInfo, setCurrencyInfo] = useState<Currency | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!currencyCode) {
      setCurrencyInfo(null);
      setError(null);
      setLoading(false);

      return;
    }

    const fetchCurrencyInfo = async () => {
      setLoading(true);
      setError(null);

      try {
        const response = await fetch(`http://localhost:5081/currency/${currencyCode}`);

        if (!response.ok) {
          throw new Error(`Server returned ${response.status}`);
        }

        const currencyData = await response.json();
        setCurrencyInfo(currencyData);
      } catch (err) {
        setError('COULD NOT GET DATA FROM SERVER');
        setCurrencyInfo(null);
      } finally {
        setLoading(false);
      }
    };

    fetchCurrencyInfo();
  }, [currencyCode]);

  return { data: currencyInfo, loading: loading, error: error };
};