import { useContext } from 'react';
import { WordsContext } from '../WordsContext';

export const useWords = () => {
  const context = useContext(WordsContext);
  if (!context) {
    throw new Error('useWords must be used with WordsProvider');
  }

  return context;
};
