import { useFromAmount, useCurrentPrice } from "./useCurrencies";

export const useToAmount = () => {
  const fromAmount = useFromAmount();
  const currentPrice = useCurrentPrice() || 0;

  return fromAmount * currentPrice;
};