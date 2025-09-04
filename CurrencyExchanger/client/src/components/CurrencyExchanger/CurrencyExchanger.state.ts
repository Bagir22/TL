import { useEffect, useState } from "react";
import { useCurrencyExchangerStore } from "../../currencyExchangerStore/useCurrencyExchangerStore.ts";
import { usePrices } from "../../hooks/usePrices";

export const useCurrencyExchangerState = () => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const store = useCurrencyExchangerStore();

  useEffect(() => {
    const fetchInitialData = async () => {
      try {
        setLoading(true);
        setError(null);

        const currenciesResponse = await fetch("http://localhost:5081/currency");
        if (!currenciesResponse.ok) {
          setError(`Server error: ${currenciesResponse.status}`);

          return;
        }

        const currenciesData: { code: string; name: string, description: string, symbol: string }[] = await currenciesResponse.json();

        if (currenciesData.length > 0) {
          store.set({
            ...store.getSnapshot(),
            currencies: currenciesData,
            fromCurrency: currenciesData[0].code,
            toCurrency: currenciesData[0].code,
          });
        }
      } catch {
        setError("COULD NOT GET DATA FROM SERVER");
      } finally {
        setLoading(false);
      }
    };

    fetchInitialData();
  }, [store]);

  const { prices } = usePrices();

  useEffect(() => {
    if (prices.length === 0) {
      return;
    }

    const latestPrice = prices[prices.length - 1].price;
    store.set({
      ...store.getSnapshot(),
      currentPrice: latestPrice,
    });
  }, [prices, store]);

  return { loading, error };
};
