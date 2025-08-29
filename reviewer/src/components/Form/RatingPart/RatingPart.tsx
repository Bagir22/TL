import styles from './RatingPart.module.css';
import EmojiButton from '../EmojiButton/EmojiButton';
import Angry from '../../../assets/images/emojies/twemoji_angry-face.svg';
import Frown from '../../../assets/images/emojies/twemoji_slightly-frowning-face.svg';
import Neutral from '../../../assets/images/emojies/twemoji_neutral-face.svg';
import Grin from '../../../assets/images/emojies/twemoji_grinning-face-with-big-eyes.svg';
import Smile from '../../../assets/images/emojies/twemoji_slightly-smiling-face.svg';

const steps = [
  { emoji: Angry, color: '#f24e1e' },
  { emoji: Frown, color: '#ff8311' },
  { emoji: Neutral, color: '#ff8311' },
  { emoji: Smile, color: '#ffc700' },
  { emoji: Grin, color: '#ffc700' },
];

type RatingPartProps = {
  label: string;
  value?: number;
  onChange: (val: number) => void;
};

function RatingPart({ label, value, onChange }: RatingPartProps) {
  const selected = value ?? null;

  const handlePick = (index: number) => {
    onChange(index + 1);
  };

  const getTrackStyle = () => {
    if (!selected) return {};

    const selectedIndex = selected - 1;
    const totalSteps = steps.length - 1;
    const percent = (selectedIndex / totalSteps) * 100;

    const selectedColor = steps[selectedIndex].color;

    return {
      background: `linear-gradient(to right, ${selectedColor} ${percent}%, #ffffff ${percent}%)`,
    };
  };

  return (
    <div className={styles.wrapper}>
      <div className={styles.track} style={getTrackStyle()}>
        {steps.map((step, i) => {
          const index = i + 1;
          const isSelected = selected === index;
          let backgroundColor;

          if (!selected) {
            backgroundColor = step.color;
          } else if (index <= selected) {
            backgroundColor = steps[selected - 1].color;
          } else {
            backgroundColor = '#ffffff';
          }

          return (
            <div key={index} className={styles.pointWrapper}>
              <EmojiButton
                emoji={step.emoji}
                isSelected={isSelected}
                backgroundColor={backgroundColor}
                onClick={() => handlePick(i)}
              />
            </div>
          );
        })}
      </div>
      <span className={styles.fieldLabel}>{label}</span>
    </div>
  );
}

export default RatingPart;
