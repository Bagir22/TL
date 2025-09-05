import { Box, Button, TextField, Typography } from '@mui/material';
import { CustomBackButton } from '../CustomBackButton/CustomBackButton.tsx';
import { useNavigate } from 'react-router';

export const AddWord = () => {
  const navigate = useNavigate();

  const handleBackClick = () => {
    navigate(-1);
  };

  function handleDictionaryClick() {
    navigate(-1);
  }

  function handleCancelClick() {
    navigate(-1);
  }

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
            sx={{ ml: 6.5 }}
            size="small"
          />
        </Box>
        <Box sx={{ display: 'flex', alignItems: 'center', mt: 2 }}>
          <Typography variant="subtitle1" sx={{ color: '#2c2c40' }}>
            Перевод на английский язык
          </Typography>
          <TextField
            label="Слово на английском"
            type="text"
            sx={{ ml: 2 }}
            size="small"
          />
        </Box>
      </Box>
      <Box sx={{ mt: 3, display: 'flex' }}>
        <Button
          variant="contained"
          size="large"
          onClick={handleDictionaryClick}
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
