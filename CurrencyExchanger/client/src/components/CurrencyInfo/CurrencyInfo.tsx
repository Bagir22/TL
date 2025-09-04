import { Currency } from '../../domain/Currency.ts';
import styles from './CurrencyInfo.module.css'

type CurrencyInfoProps = {
  currency: Currency;
}

export const CurrencyInfo = ({ currency }: CurrencyInfoProps) => {

  return (
    <div className={styles.wrapper}>
      <span className={styles.title}>{currency.name} - {currency.code} - {currency.symbol}</span>
      <p className={styles.description}>{currency.description}</p>
    </div>
  )
}