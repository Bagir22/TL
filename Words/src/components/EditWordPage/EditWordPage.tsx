import { Box, Button, TextField, Typography } from '@mui/material';
import { CustomBackButton } from '../CustomBackButton/CustomBackButton.tsx';
import { useNavigate, useParams } from 'react-router';
import { useWords } from '../../hooks/useWords.ts';
import { type ChangeEvent, useEffect, useState } from 'react';

export const EditWordPage = () => {
  const navigate = useNavigate();
  const { id } = useParams<{ id: string }>();
  const { words, editWord } = useWords();

  const wordToEdit = words.find((w) => w.id === id);

  const [formData, setFormData] = useState({
    russian: '',
    english: '',
  });

  useEffect(() => {
    if (wordToEdit) {
      setFormData({
        russian: wordToEdit.russian,
        english: wordToEdit.english,
      });
    }
  }, [wordToEdit]);

  const handleSaveClick = () => {
    if (id && formData.russian.trim() && formData.english.trim()) {
      editWord(id, formData.russian.trim(), formData.english.trim());
      navigate('/dictionary');
    }
  };

  const handleInputChange =
    (field: keyof typeof formData) => (e: ChangeEvent<HTMLInputElement>) => {
      setFormData((prev) => ({ ...prev, [field]: e.target.value }));
    };

  const handleBackClick = () => {
    navigate(-1);
  };

  const handleCancelClick = () => {
    navigate(-1);
  };

  return (
    <Box sx={{ minHeight: '100vh', p: 3 }}>
      <Box sx={{ display: 'flex', alignItems: 'center', gap: 2 }}>
        <CustomBackButton onClick={handleBackClick} size="large" />
        <Typography variant="h3" sx={{ color: '#2c2c40' }}>
          Редаткирование слова
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
            label="Слово на русском*"
            type="text"
            value={formData.russian}
            onChange={handleInputChange('russian')}
            sx={{ ml: 6.5 }}
            size="small"
          />
        </Box>
        <Box sx={{ display: 'flex', alignItems: 'center', mt: 2 }}>
          <Typography variant="subtitle1" sx={{ color: '#2c2c40' }}>
            Перевод на английский язык
          </Typography>
          <TextField
            label="Слово на английском*"
            type="text"
            value={formData.english}
            onChange={handleInputChange('english')}
            sx={{ ml: 2 }}
            size="small"
          />
        </Box>
      </Box>
      <Box sx={{ mt: 3, display: 'flex' }}>
        <Button
          variant="contained"
          size="large"
          onClick={handleSaveClick}
          disabled={!formData.russian.trim() || !formData.english.trim()}
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
