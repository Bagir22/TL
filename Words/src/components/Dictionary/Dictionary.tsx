import {
  Button,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
  Box,
} from '@mui/material';
import { CustomBackButton } from '../CustomBackButton/CustomBackButton.tsx';
import { useNavigate } from 'react-router';

export const Dictionary = () => {
  const navigate = useNavigate();

  const handleBackClick = () => {
    navigate('/');
  };

  const handleAddWordClick = () => {
    navigate('/new-word');
  };

  return (
    <Box sx={{ minHeight: '100vh' }}>
      <Box sx={{ p: 3 }}>
        <Box sx={{ display: 'flex', alignItems: 'center', gap: 2 }}>
          <CustomBackButton onClick={handleBackClick} size="large" />
          <Typography variant="h3" sx={{ color: '#2c2c40' }}>
            Словарь
          </Typography>
        </Box>

        <Box sx={{ display: 'flex', mt: 2 }}>
          <Button variant="contained" onClick={handleAddWordClick}>
            + Добавить слово
          </Button>
        </Box>
      </Box>

      <TableContainer
        sx={{
          px: 3,
          width: '90vw',
        }}
      >
        <Table sx={{ minWidth: '100%' }}>
          <TableHead sx={{ backgroundColor: '#aeaebf' }}>
            <TableRow>
              <TableCell sx={{ color: '#2e2f35', fontWeight: 'bold' }}>
                Слово на русском
              </TableCell>
              <TableCell sx={{ color: '#2e2f35', fontWeight: 'bold' }}>
                Перевод на английский язык
              </TableCell>
              <TableCell
                sx={{
                  color: '#2e2f35',
                  fontWeight: 'bold',
                  textAlign: 'right',
                }}
              >
                Действие
              </TableCell>
            </TableRow>
          </TableHead>
          <TableBody>{/* TODO */}</TableBody>
        </Table>
      </TableContainer>
    </Box>
  );
};
