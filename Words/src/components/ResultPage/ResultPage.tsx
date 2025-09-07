import { Button, Box, Typography, Card, CardContent } from '@mui/material';
import CheckCircleOutlineIcon from '@mui/icons-material/CheckCircleOutline';
import CloseOutlinedIcon from '@mui/icons-material/CloseOutlined';
import MenuBookOutlinedIcon from '@mui/icons-material/MenuBookOutlined';
import { useWords } from '../../hooks/useWords.ts';
import { useNavigate } from 'react-router';

export const ResultPage = () => {
  const navigate = useNavigate();
  const { lastResult } = useWords();

  const correct = lastResult?.correct ?? 0;
  const total = lastResult?.total ?? 0;
  const wrong = total - correct;

  const handleStartClick = () => {
    navigate('/');
  };

  const handleCheclKnowledgeClick = () => {
    navigate('/check');
  };

  return (
    <Box>
      <Typography variant="h3" sx={{ color: '#2c2c40' }}>
        Результат проверки знаний
      </Typography>
      <Card sx={{ display: 'flex', mt: 3, maxWidth: 300 }}>
        <CardContent sx={{ width: '100%' }}>
          <Typography
            variant="subtitle1"
            sx={{
              color: '#1638d6',
              fontWeight: 600,
              mb: 2,
              textAlign: 'left',
            }}
          >
            Ответы
          </Typography>
          <Box
            sx={{
              display: 'flex',
              justifyContent: 'space-between',
              alignItems: 'center',
              width: '100%',
              mt: 1,
            }}
          >
            <Box sx={{ display: 'flex', alignItems: 'center' }}>
              <CheckCircleOutlineIcon color="success" />
              <Typography variant="body1" sx={{ ml: 1 }}>
                Правильные
              </Typography>
            </Box>
            <Typography variant="body1" sx={{ fontWeight: 600 }}>
              {correct}
            </Typography>
          </Box>
          <Box
            sx={{
              display: 'flex',
              justifyContent: 'space-between',
              alignItems: 'center',
              width: '100%',
              mt: 1,
            }}
          >
            <Box sx={{ display: 'flex', alignItems: 'center' }}>
              <CloseOutlinedIcon color="error" />
              <Typography variant="body1" sx={{ ml: 1 }}>
                Ошибочные
              </Typography>
            </Box>
            <Typography variant="body1" sx={{ fontWeight: 600 }}>
              {wrong}
            </Typography>
          </Box>
          <Box
            sx={{
              display: 'flex',
              justifyContent: 'space-between',
              alignItems: 'center',
              width: '100%',
              mt: 1,
            }}
          >
            <Box sx={{ display: 'flex', alignItems: 'center' }}>
              <MenuBookOutlinedIcon color="secondary" />
              <Typography variant="body1" sx={{ ml: 1 }}>
                Всего слов
              </Typography>
            </Box>
            <Typography variant="body1" sx={{ fontWeight: 600 }}>
              {total}
            </Typography>
          </Box>
        </CardContent>
      </Card>
      <Box sx={{ display: 'flex', mt: 3 }}>
        <Button variant="contained" onClick={handleCheclKnowledgeClick}>
          Проверить знания еще раз
        </Button>
        <Button variant="outlined" onClick={handleStartClick} sx={{ ml: 1 }}>
          Вернуться в начало
        </Button>
      </Box>
    </Box>
  );
};
