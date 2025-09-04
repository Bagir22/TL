import { useCurrencyExchangerStore } from "../currencyExchangerStore/useCurrencyExchangerStore.ts";
import { useStore } from "../store";

export const useCurrencies = () => useStore(useCurrencyExchangerStore(), s => s.currencies);
export const useFromCurrency = () => useStore(useCurrencyExchangerStore(), s => s.fromCurrency);
export const useToCurrency = () => useStore(useCurrencyExchangerStore(), s => s.toCurrency);
export const useFromAmount = () => useStore(useCurrencyExchangerStore(), s => s.fromAmount);
export const useCurrentPrice = () => useStore(useCurrencyExchangerStore(), s => s.currentPrice);

export const useCurrencyName = (currencyCode: string) => {
  const currencies = useCurrencies();
  const currency = currencies.find(c => c.code === currencyCode);

  return currency?.name || currencyCode;
};