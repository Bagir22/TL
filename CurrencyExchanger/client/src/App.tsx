import CurrencyExchanger from './components/CurrencyExchanger/CurrencyExchanger.tsx';
import styles from './AppView.module.css';

const App = () => {
  return (
    <div className={styles.container}>
      < CurrencyExchanger />
    </div>
  );
};

export default App;
