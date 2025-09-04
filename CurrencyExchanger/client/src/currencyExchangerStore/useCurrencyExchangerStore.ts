import { useContext } from "react";
import { CurrencyExchangerContext } from './currencyExchangerContext.ts';

export const useCurrencyExchangerStore = () => useContext(CurrencyExchangerContext);
