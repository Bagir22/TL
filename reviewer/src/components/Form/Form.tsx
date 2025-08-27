import styles from './Form.module.css';
import { type FormEvent, useState } from 'react';
import type { ReviewData } from '../../types/ReviewData.ts';
import InputField from './InputField/InputField.tsx';
import TextareaField from './TextareaField/TextareaField.tsx';
import Button from './Button/Button.tsx';
import RatingPart from './RatingPart/RatingPart.tsx';

type FormProps = {
  onAddReview: (review: ReviewData) => void;
};

const ratingCategories = [
  'Чистенько',
  'Сервис',
  'Скорость',
  'Место',
  'Культура речи',
];

function Form({ onAddReview }: FormProps) {
  const [ratings, setRatings] = useState<Record<string, number>>({});
  const [name, setName] = useState('');
  const [comment, setComment] = useState('');

  const handleRatingChange = (label: string, value: number) => {
    setRatings((prev) => ({ ...prev, [label]: value }));
  };

  const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const allRated = ratingCategories.every((category) => ratings[category]);
    if (!allRated) {
      alert('Пожалуйста, оцените все категории');

      return;
    }

    const values = Object.values(ratings);
    const average =
      values.reduce((total, value) => total + value, 0) / values.length;

    const newReview: ReviewData = {
      guid: crypto.randomUUID(),
      name,
      comment: comment,
      rating: Number(average.toFixed(1)),
    };

    onAddReview(newReview);

    setName('');
    setComment('');
    setRatings({});
  };

  return (
    <div className={styles.container}>
      <h5 className={styles.title}>
        Помогите нам сделать процесс бронирования лучше
      </h5>

      {ratingCategories.map((category) => (
        <RatingPart
          key={category}
          label={category}
          value={ratings[category]}
          onChange={(value) => handleRatingChange(category, value)}
        />
      ))}

      <form className={styles.form} onSubmit={handleSubmit}>
        <InputField
          label={`*Имя`}
          placeholder={`Как вас зовут?`}
          value={name}
          onChange={setName}
          required={true}
        />

        <TextareaField
          placeholder="Напишите, что понравилось, что было непонятно"
          value={comment}
          onChange={setComment}
        />

        <Button type="submit">Отправить</Button>
      </form>
    </div>
  );
}

export default Form;
