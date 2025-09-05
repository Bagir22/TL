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

export const CheckKnowledge = () => {
  const navigate = useNavigate();

  const handleBackClick = () => {
    navigate(-1);
  };

  const handleCheckClick = () => {
    return;
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
        {' '}
        Слово: 1 из 5
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
            label="Слово на русском"
            type="text"
            sx={{ ml: 6.5 }}
            size="small"
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
            <Select label="Слово">
              <MenuItem value={10}>Ten</MenuItem>
              <MenuItem value={20}>Twenty</MenuItem>
              <MenuItem value={30}>Thirty</MenuItem>
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
