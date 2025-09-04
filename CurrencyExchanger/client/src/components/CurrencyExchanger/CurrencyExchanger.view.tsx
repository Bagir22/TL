import { CurrencyInfo } from '../CurrencyInfo/CurrencyInfo.tsx';
import { AboutButton }  from '../AboutButton/AboutButton.tsx';
import { CurrencyChart } from '../CurrencyChart/CurrencyChart.tsx';
import { CurrencyField } from '../CurrencyField/CurrencyField.tsx';
import { ResultField } from '../ResultField/ResultField.view.tsx';
import { useCurrencyInfo } from '../../hooks/useCurrencyInfo.ts';
import { useCurrencyResult } from '../../hooks/useCurrencyResult.ts';
import { useState, useTransition } from 'react';
import { useFromCurrency, useToCurrency } from '../../hooks/useCurrencies.ts';
import styles from './CurrencyExchanger.module.css'

export const CurrencyExchangerView = () => {
  const fromCurrency = useFromCurrency();
  const toCurrency = useToCurrency();
  const currencyResult = useCurrencyResult();
  const [isInfoVisible, setIsInfoVisible] = useState(false);
  const [, startTransition] = useTransition();

  const fromCurrencyInfoResult = useCurrencyInfo(fromCurrency);
  const toCurrencyInfoResult = useCurrencyInfo(toCurrency);

  const changeInfoVisibility = () => {
    startTransition(() => {
      setIsInfoVisible(!isInfoVisible);
    });
  };

  const isLoading = fromCurrencyInfoResult?.loading || toCurrencyInfoResult?.loading;
  const hasError = fromCurrencyInfoResult?.error || toCurrencyInfoResult?.error;

  return (
    <div className={styles.container}>
      <ResultField currencyResult={currencyResult} />

      <div className={styles.currenciesManagement}>
        <div className={styles.currenciesInput}>
          <CurrencyField type="from" />
          <CurrencyField type="to" readonly />
        </div>

        <CurrencyChart/>
      </div>

      <div className={styles.buttonWithLine}>
        <div className={styles.lineContainer}>
          <div className={styles.line}></div>
          <AboutButton
            fromCurrency={fromCurrency}
            toCurrency={toCurrency}
            onClick={changeInfoVisibility}
          />
        </div>
      </div>

      <div className={`${styles.currenciesInfoContainer} ${isInfoVisible ? styles.visible : ''}`}>
          {!isLoading && !hasError && fromCurrencyInfoResult?.data && (
            <CurrencyInfo currency={fromCurrencyInfoResult.data} />
          )}

          {!isLoading && !hasError && toCurrencyInfoResult?.data && (
            <CurrencyInfo currency={toCurrencyInfoResult.data} />
          )}
        </div>
    </div>
  );
};