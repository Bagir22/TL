import styles from './EmojiButton.module.css';

type EmojiButtonProps = {
  emoji: string;
  isSelected: boolean;
  backgroundColor: string;
  onClick: () => void;
};

function EmojiButton({
  emoji,
  isSelected,
  backgroundColor,
  onClick,
}: EmojiButtonProps) {
  return (
    <div
      className={`${styles.point} ${isSelected ? styles.isSelected : ''}`}
      onClick={onClick}
      style={{
        backgroundColor: backgroundColor,
      }}
    >
      {isSelected && <img src={emoji} alt="" className={styles.emoji} />}
    </div>
  );
}

export default EmojiButton;
