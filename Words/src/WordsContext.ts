import { createContext } from 'react';

export type Word = {
  id: string;
  russian: string;
  english: string;
};

export type WordsContextType = {
  words: Word[];
  addWord: (russian: string, english: string) => void;
  editWord: (id: string, russian: string, english: string) => void;
  deleteWord: (id: string) => void;
  lastResult?: { correct: number; total: number };
  setLastResult?: (result: { correct: number; total: number }) => void;
};

export const WordsContext = createContext<WordsContextType | undefined>(
  undefined
);
