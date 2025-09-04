import styles from './CurrencyField.module.css';
import { Currency } from '../../domain/Currency.ts';
import { ChangeEvent } from 'react';

type CurrencyFieldViewProps = {
  amount: number;
  selectedCurrency?: string;
  currencies: Currency[];
  readonly?: boolean;
  onAmountChange: (value: number) => void;
  onCurrencyChange: (currency: string) => void;
};

export const CurrencyFieldView = ({ amount, selectedCurrency, currencies, readonly = false, onAmountChange,  onCurrencyChange }: CurrencyFieldViewProps) => {
  const handleAmountChange = (e: ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    if (value === '') {
      onAmountChange(0);
    } else {
      onAmountChange(Number(value));
    }
  };

  return (
    <div className={styles.container}>
      <input
        className={`${styles.value} ${readonly ? styles.readonly : ""}`}
        type="number"
        step="0.01"
        min="0"
        value={amount === 0 ? '' : amount.toString()}
        onChange={handleAmountChange}
        readOnly={readonly}
        placeholder="0"
      />
      <select
        className={styles.currencyCode}
        value={selectedCurrency}
        onChange={(e) => onCurrencyChange(e.target.value)}
      >
        {currencies.map((currency) => (
          <option key={currency.code} value={currency.code}>
            {currency.code}
          </option>
        ))}
      </select>
    </div>
  );
};