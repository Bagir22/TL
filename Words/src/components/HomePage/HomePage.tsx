import { Button, Container, Stack, Typography } from '@mui/material';
import { useNavigate } from 'react-router';

export const HomePage = () => {
  const navigate = useNavigate();

  const handleDictionaryClick = () => {
    navigate('/dictionary');
  };

  const handleCheclKnowledgeClick = () => {
    navigate('/check');
  };

  return (
    <Stack>
      <Typography variant="h3" sx={{ color: '#2c2c40' }}>
        Выберите режим
      </Typography>
      <Container sx={{ mt: 3 }}>
        <Button variant="contained" onClick={handleDictionaryClick}>
          Заполнить словарь
        </Button>
        <Button
          variant="outlined"
          sx={{ ml: 1 }}
          onClick={handleCheclKnowledgeClick}
        >
          Проверить знания
        </Button>
      </Container>
    </Stack>
  );
};
