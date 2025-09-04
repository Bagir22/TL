import { useFromCurrency, useFromAmount, useToCurrency, useCurrencyName } from './useCurrencies';
import { useToAmount } from "./useToAmount";

export const useCurrencyResult = () => {
  const fromCurrency = useFromCurrency();
  const toCurrency = useToCurrency();
  const fromAmount = useFromAmount();
  const toAmount = useToAmount();

  const fromCurrencyName = useCurrencyName(fromCurrency);
  const toCurrencyName = useCurrencyName(toCurrency);

  const roundedFromAmount = parseFloat(fromAmount.toFixed(2));
  const roundedToAmount = parseFloat(toAmount.toFixed(2));

  return {
    fromCurrency: fromCurrencyName,
    toCurrency: toCurrencyName,
    fromAmount: roundedFromAmount,
    toAmount: roundedToAmount,
    datetime: new Date(),
  };
};