import {
  Box,
  Button,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  TextField,
  Typography,
} from '@mui/material';
import { CustomBackButton } from '../CustomBackButton/CustomBackButton.tsx';
import { useNavigate } from 'react-router';
import { useWords } from '../../hooks/useWords.ts';
import { useEffect, useState } from 'react';

export const CheckKnowledgePage = () => {
  const navigate = useNavigate();
  const { words, setLastResult } = useWords();

  const [currentIndex, setCurrentIndex] = useState(0);
  const [selectedAnswer, setSelectedAnswer] = useState('');
  const [correctAnswers, setCorrectAnswers] = useState(0);
  const [options, setOptions] = useState<string[]>([]);

  const currentWord = words[currentIndex];

  useEffect(() => {
    if (currentWord) {
      const otherWords = words.filter((w) => w.id !== currentWord.id);

      const randomOther = [...otherWords]
        .sort(() => 0.5 - Math.random())
        .slice(0, 3)
        .map((w) => w.english);

      const opts = [currentWord.english, ...randomOther].sort(
        () => 0.5 - Math.random()
      );

      setOptions(opts);
    }
  }, [currentWord, words]);

  const handleBackClick = () => {
    navigate(-1);
  };

  const handleCheckClick = () => {
    if (!currentWord) return;

    let newCorrect = correctAnswers;
    if (selectedAnswer === currentWord.english) {
      newCorrect++;
    }

    if (currentIndex < words.length - 1) {
      setCurrentIndex((prev) => prev + 1);
      setSelectedAnswer('');
      setCorrectAnswers(newCorrect);
    } else {
      setLastResult?.({ correct: newCorrect, total: words.length });
      navigate('/result');
    }
  };

  return (
    <Box sx={{ minHeight: '100vh', p: 3 }}>
      <Box sx={{ display: 'flex', alignItems: 'center', gap: 2 }}>
        <CustomBackButton onClick={handleBackClick} size="large" />
        <Typography variant="h3" sx={{ color: '#2c2c40' }}>
          Проверка знаний
        </Typography>
      </Box>

      <Typography
        sx={{ display: 'flex', color: '#201f1f', fontWeight: 700, mt: 2 }}
      >
        Слово: {currentIndex + 1} из {words.length}
      </Typography>
      <Box
        sx={{
          backgroundColor: '#ffffff',
          p: 3,
          width: '90vw',
          mt: 2,
          display: 'flex',
          alignItems: 'flex-start',
          flexDirection: 'column',
          borderRadius: 2,
        }}
      >
        <Box sx={{ display: 'flex', alignItems: 'center', mt: 1 }}>
          <Typography variant="subtitle1" sx={{ color: '#2c2c40' }}>
            Слово на русском языке
          </Typography>
          <TextField
            value={currentWord.russian}
            type="text"
            sx={{ ml: 6.5 }}
            size="small"
            disabled
          />
        </Box>
        <Box sx={{ display: 'flex', alignItems: 'center', mt: 2, gap: 2 }}>
          <Typography
            variant="subtitle1"
            sx={{
              color: '#2c2c40',
              whiteSpace: 'nowrap',
              minWidth: { xs: '150px' },
            }}
          >
            Перевод на английский язык
          </Typography>
          <FormControl
            sx={{
              minWidth: { xs: '195px' },
            }}
          >
            <InputLabel>Слово</InputLabel>
            <Select
              value={selectedAnswer}
              onChange={(e) => setSelectedAnswer(e.target.value)}
              label="Слово"
              sx={{ textAlign: 'left' }}
            >
              {options.map((opt) => (
                <MenuItem key={opt} value={opt}>
                  {opt}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Box>
      </Box>
      <Button
        variant="contained"
        onClick={handleCheckClick}
        sx={{ display: 'flex', mt: 2 }}
      >
        Проверить
      </Button>
    </Box>
  );
};
