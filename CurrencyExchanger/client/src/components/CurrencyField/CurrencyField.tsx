import { CurrencyFieldView } from "./CurrencyField.view";
import { useCallback } from "react";
import { useCurrencyExchangerStore } from "../../currencyExchangerStore/useCurrencyExchangerStore.ts";
import { useCurrencies, useFromCurrency, useToCurrency, useFromAmount } from "../../hooks/useCurrencies";
import {useToAmount} from '../../hooks/useToAmount.ts';

type CurrencyFieldProps = {
  type: "from" | "to";
  readonly?: boolean;
};

export const CurrencyField = ({ type, readonly = false }: CurrencyFieldProps) => {
  const store = useCurrencyExchangerStore();

  const fromAmount = useFromAmount();
  const toAmount = useToAmount();
  const fromCurrency = useFromCurrency();
  const toCurrency = useToCurrency();
  const currencies = useCurrencies();

  const amount = type === "from" ? fromAmount : toAmount;
  const selectedCurrency = type === "from" ? fromCurrency : toCurrency;

  const onAmountChange = useCallback((newAmount: number) => {
    if (readonly) return;

    const state = store.getSnapshot();

    if (type === "from") {
      if (state.fromAmount !== newAmount) {
        store.set({
          ...state,
          fromAmount: newAmount,
        });
      }
    }
  }, [readonly, store, type]);

  const onCurrencyChange = useCallback((newCurrency: string) => {
    const state = store.getSnapshot();
    const currentCurrency = type === "from" ? state.fromCurrency : state.toCurrency;

    if (currentCurrency !== newCurrency) {
      store.set({
        ...state,
        [type === "from" ? "fromCurrency" : "toCurrency"]: newCurrency,
      });
    }
  }, [store, type]);

  return (
    <CurrencyFieldView
      amount={amount || 0}
      selectedCurrency={selectedCurrency}
      currencies={currencies}
      readonly={readonly}
      onAmountChange={onAmountChange}
      onCurrencyChange={onCurrencyChange}
    />
  );
};