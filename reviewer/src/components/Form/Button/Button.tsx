import styles from './Button.module.css';

type FormButtonProps = {
  children: React.ReactNode;
  type?: 'button' | 'submit';
  onClick?: () => void;
};

function Button({ children, type = 'button', onClick }: FormButtonProps) {
  return (
    <div className={styles.buttonBox}>
      <button type={type} onClick={onClick} className={styles.submitBtn}>
        {children}
      </button>
    </div>
  );
}

export default Button;
