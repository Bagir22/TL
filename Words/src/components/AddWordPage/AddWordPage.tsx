import { Box, Button, TextField, Typography } from '@mui/material';
import { CustomBackButton } from '../CustomBackButton/CustomBackButton.tsx';
import { useNavigate } from 'react-router';
import { useWords } from '../../hooks/useWords.ts';
import { useState } from 'react';

export const AddWordPage = () => {
  const navigate = useNavigate();
  const { addWord } = useWords();

  const [formData, setFormData] = useState({
    russian: '',
    english: '',
  });
  const [errors, setErrors] = useState({
    russian: '',
    english: '',
  });

  const validateForm = () => {
    const newErrors = {
      russian: '',
      english: '',
    };

    if (!formData.russian.trim()) {
      newErrors.russian = 'Введите слово на русском';
    }

    if (!formData.english.trim()) {
      newErrors.english = 'Введите перевод на английском';
    }

    setErrors(newErrors);
    return !newErrors.russian && !newErrors.english;
  };

  const handleBackClick = () => {
    navigate(-1);
  };

  const handleSaveClick = () => {
    if (validateForm()) {
      addWord(formData.russian.trim(), formData.english.trim());
      navigate('/dictionary');
    }
  };

  const handleCancelClick = () => {
    navigate(-1);
  };

  const handleInputChange =
    (field: keyof typeof formData) =>
    (e: React.ChangeEvent<HTMLInputElement>) => {
      setFormData((prev) => ({ ...prev, [field]: e.target.value }));
      if (errors[field]) {
        setErrors((prev) => ({ ...prev, [field]: '' }));
      }
    };

  const isFormValid = formData.russian.trim() && formData.english.trim();

  return (
    <Box sx={{ minHeight: '100vh', p: 3 }}>
      <Box sx={{ display: 'flex', alignItems: 'center', gap: 2 }}>
        <CustomBackButton onClick={handleBackClick} size="large" />
        <Typography variant="h3" sx={{ color: '#2c2c40' }}>
          Добавление слова
        </Typography>
      </Box>

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
        <Typography variant="h4" sx={{ color: '#1a1a25' }}>
          Словарное слово
        </Typography>
        <Box sx={{ display: 'flex', alignItems: 'center', mt: 3 }}>
          <Typography variant="subtitle1" sx={{ color: '#2c2c40' }}>
            Слово на русском языке
          </Typography>
          <TextField
            label="Слово на русском"
            type="text"
            value={formData.russian}
            onChange={handleInputChange('russian')}
            sx={{ ml: 6.5 }}
            size="small"
            error={!!errors.russian}
            helperText={errors.russian}
            required
          />
        </Box>
        <Box sx={{ display: 'flex', alignItems: 'center', mt: 2 }}>
          <Typography variant="subtitle1" sx={{ color: '#2c2c40' }}>
            Перевод на английский язык
          </Typography>
          <TextField
            label="Слово на английском"
            type="text"
            value={formData.english}
            onChange={handleInputChange('english')}
            sx={{ ml: 2 }}
            size="small"
            error={!!errors.english}
            helperText={errors.english}
            required
          />
        </Box>
      </Box>

      <Box sx={{ mt: 3, display: 'flex' }}>
        <Button
          variant="contained"
          size="large"
          onClick={handleSaveClick}
          disabled={!isFormValid}
        >
          Сохранить
        </Button>
        <Button
          variant="outlined"
          size="large"
          sx={{ ml: 1 }}
          onClick={handleCancelClick}
        >
          Отменить
        </Button>
      </Box>
    </Box>
  );
};
