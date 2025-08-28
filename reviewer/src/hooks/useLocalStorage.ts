import { useState } from "react";

export function useLocalStorage<T>(key: string, initialValue: T) {
  const [value, setValue] = useState<T>(() => {
    const saved = localStorage.getItem(key);

    return saved ? JSON.parse(saved) : initialValue;
  });

  const updateValue = (newValue: T | ((val: T) => T)) => {
    setValue(prev => {
      const result = newValue instanceof Function ? newValue(prev) : newValue;
      localStorage.setItem(key, JSON.stringify(result));

      return result;
    });
  };

  return [value, updateValue] as const;
}