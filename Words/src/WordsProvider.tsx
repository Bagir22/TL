import React, { useEffect, useState } from 'react';
import { WordsContext, type Word } from './WordsContext.ts';
import { generateGuid } from './utils/generateGuid.ts';

export const WordsProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [words, setWords] = useState<Word[]>(() => {
    try {
      const saved = localStorage.getItem('words');
      return saved ? JSON.parse(saved) : [];
    } catch {
      return [];
    }
  });
  const [lastResult, setLastResult] = useState<{
    correct: number;
    total: number;
  }>();

  useEffect(() => {
    localStorage.setItem('words', JSON.stringify(words));
  }, [words]);

  const addWord = (russian: string, english: string) => {
    const newWord: Word = { id: generateGuid(), russian, english };
    setWords((prev) => [...prev, newWord]);
  };

  const editWord = (id: string, russian: string, english: string) => {
    setWords((prev) =>
      prev.map((word) =>
        word.id === id ? { ...word, russian, english } : word
      )
    );
  };

  const deleteWord = (id: string) => {
    setWords((prev) => prev.filter((word) => word.id !== id));
  };

  return (
    <WordsContext.Provider
      value={{
        words,
        addWord,
        editWord,
        deleteWord,
        lastResult,
        setLastResult,
      }}
    >
      {children}
    </WordsContext.Provider>
  );
};
