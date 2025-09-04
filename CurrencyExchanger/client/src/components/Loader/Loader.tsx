import styles from './Loader.module.css';

export const Loader = () => {
  return (
    <div className={styles.loaderContainer}>
      <p className={styles.loaderText}>LOADING...</p>
      <div className={styles.circularBars}>
        {[...Array(12)].map((_, index) => (
          <div key={index} className={styles.circularBar}></div>
        ))}
      </div>
    </div>
  );
};

