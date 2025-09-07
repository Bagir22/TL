import ArrowBackIosNewIcon from '@mui/icons-material/ArrowBackIosNew';
import { useNavigate } from 'react-router';
import { Button } from '@mui/material';

type CustomBackButtonProps = {
  type?: 'button' | 'submit';
  onClick?: () => void;
  size?: 'small' | 'medium' | 'large';
  variant?: 'text' | 'outlined' | 'contained';
};

export const CustomBackButton = ({
  type = 'button',
  onClick,
  size = 'medium',
  variant = 'outlined',
}: CustomBackButtonProps) => {
  const navigate = useNavigate();

  const handleClick = () => {
    if (onClick) {
      onClick();
    } else {
      navigate(-1);
    }
  };

  return (
    <Button
      type={type}
      onClick={handleClick}
      variant={variant}
      size={size}
      sx={{
        padding: '8px',
        borderRadius: '8px',
        borderColor: 'primary.main',
        color: 'primary.main',
        '&:hover': {
          backgroundColor: 'primary.main',
          color: 'white',
          borderColor: 'primary.main',
        },
      }}
    >
      <ArrowBackIosNewIcon fontSize={size} />
    </Button>
  );
};
