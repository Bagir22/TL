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
  IconButton,
  Menu,
  MenuItem,
  ListItemIcon,
  ListItemText,
} from '@mui/material';
import { CustomBackButton } from '../CustomBackButton/CustomBackButton.tsx';
import { useNavigate } from 'react-router';
import { useWords } from '../../hooks/useWords.ts';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import MenuIcon from '@mui/icons-material/Menu';
import { useState } from 'react';

export const DictionaryPage = () => {
  const navigate = useNavigate();
  const { words, deleteWord } = useWords();
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [selectedWordId, setSelectedWordId] = useState<string | null>(null);

  const handleBackClick = () => {
    navigate('/');
  };

  const handleAddWordClick = () => {
    navigate('/new-word');
  };

  const handleMenuOpen = (
    event: React.MouseEvent<HTMLElement>,
    wordId: string
  ) => {
    setAnchorEl(event.currentTarget);
    setSelectedWordId(wordId);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
    setSelectedWordId(null);
  };

  const handleEdit = () => {
    if (selectedWordId) {
      navigate(`/edit-word/${selectedWordId}`);
    }
    handleMenuClose();
  };

  const handleDelete = () => {
    if (selectedWordId) {
      deleteWord(selectedWordId);
    }

    handleMenuClose();
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
          <TableBody>
            {words.map((word) => (
              <TableRow key={word.id} hover>
                <TableCell sx={{ fontSize: '16px', fontWeight: 500 }}>
                  {word.russian}
                </TableCell>
                <TableCell sx={{ fontSize: '16px' }}>{word.english}</TableCell>
                <TableCell align="right">
                  <IconButton
                    onClick={(e) => handleMenuOpen(e, word.id)}
                    size="small"
                  >
                    <MenuIcon />
                  </IconButton>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>

      <Menu
        anchorEl={anchorEl}
        open={Boolean(anchorEl)}
        onClose={handleMenuClose}
      >
        <MenuItem onClick={handleEdit}>
          <ListItemIcon>
            <EditIcon fontSize="small" color="primary" />
          </ListItemIcon>
          <ListItemText>Редактировать</ListItemText>
        </MenuItem>
        <MenuItem onClick={handleDelete}>
          <ListItemIcon>
            <DeleteIcon fontSize="small" color="error" />
          </ListItemIcon>
          <ListItemText>Удалить</ListItemText>
        </MenuItem>
      </Menu>
    </Box>
  );
};
