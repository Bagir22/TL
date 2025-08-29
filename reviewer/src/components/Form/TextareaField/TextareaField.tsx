import styles from './TextareaField.module.css';
import { useEffect, useRef } from 'react';

type TextareaFieldProps = {
  placeholder?: string;
  value: string;
  onChange: (value: string) => void;
  required?: boolean;
};

function TextareaField({
  placeholder,
  value,
  onChange,
  required = false,
}: TextareaFieldProps) {
  const textareaRef = useRef<HTMLTextAreaElement>(null);

  useEffect(() => {
    if (textareaRef.current) {
      textareaRef.current.style.height = 'auto';
      textareaRef.current.style.height =
        textareaRef.current.scrollHeight + 'px';
    }
  }, [value]);

  return (
    <textarea
      ref={textareaRef}
      placeholder={placeholder}
      value={value}
      onChange={(e) => onChange(e.target.value)}
      className={styles.textarea}
      required={required}
    />
  );
}

export default TextareaField;
