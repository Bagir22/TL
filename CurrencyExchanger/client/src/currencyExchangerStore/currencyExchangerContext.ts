import { createContext } from "react";
import { Store } from "../store";
import { Currency } from '../domain/Currency.ts';

export type CurrencyState = {
  currencies: Currency[];
  fromCurrency: string;
  toCurrency: string;
  fromAmount: number;
  currentPrice?: number;
};

const initialState: CurrencyState = {
  currencies: [],
  fromCurrency: "",
  toCurrency: "",
  fromAmount: 0,
  currentPrice: undefined,
};

export const CurrencyExchangerContext = createContext(Store.create(initialState));