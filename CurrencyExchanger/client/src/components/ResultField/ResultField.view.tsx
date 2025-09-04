import { CurrencyResult } from '../../domain/CurrencyResult.ts';
import styles from './ResultField.module.css';

type ResultFieldProps = {
  currencyResult: CurrencyResult;
}

export const ResultField = ({ currencyResult }: ResultFieldProps) => {

  return(
    <div className={styles.wrapper}>
      <p className={styles.fromCurrency}>{currencyResult.fromAmount} {currencyResult.fromCurrency} is</p>
      <p className={styles.toCurrency}>{currencyResult.toAmount} {currencyResult.toCurrency}</p>
      <p className={styles.date}>
        {currencyResult.datetime.toLocaleString("en-US", {
          weekday: "short",
          year: "numeric",
          month: "short",
          day: "numeric",
          hour: "2-digit",
          minute: "2-digit",
          hour12: false,
          timeZone: "UTC",
        })} UTC
      </p>
    </div>
  )
};