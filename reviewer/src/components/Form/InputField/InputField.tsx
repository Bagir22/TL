import styles from './InputField.module.css';

type InputFieldProps = {
  label: string;
  type?: string;
  placeholder?: string;
  value: string;
  onChange: (value: string) => void;
  required?: boolean;
};

function InputField({
  label,
  type = 'text',
  placeholder,
  value,
  onChange,
  required = false,
}: InputFieldProps) {
  return (
    <div className={styles.inputBox}>
      <label className={styles.label}>{label}</label>
      <input
        type={type}
        placeholder={placeholder}
        className={styles.input}
        value={value}
        onChange={(e) => onChange(e.target.value)}
        required={required}
      />
    </div>
  );
}

export default InputField;
