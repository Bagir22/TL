import styles  from './AboutButton.module.css'

type AboutButtonProps = {
  fromCurrency: string;
  toCurrency: string;
  type?: 'submit' | 'button';
  onClick?: () => void;
}

export const AboutButton = ({fromCurrency, toCurrency, type = 'button', onClick}: AboutButtonProps) => {
  return (
    <button type={type} className={styles.aboutButton} onClick={onClick}>
      <span>{fromCurrency}/{toCurrency}: about ⬇</span>
    </button>
  )
}