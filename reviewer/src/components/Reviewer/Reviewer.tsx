import Form from '../Form/Form.tsx';
import styles from './Reviewer.module.css';
import { useState } from 'react';
import type { ReviewData } from '../../types/ReviewData.ts';
import Reviews from '../Reviews/Reviews.tsx';

function Reviewer() {
  const [reviews, setReviews] = useState<ReviewData[]>(() => {
    const saved = localStorage.getItem('reviews');

    return saved ? JSON.parse(saved) : [];
  });

  const addReview = (newReview: ReviewData) => {
    setReviews((existing) => {
      const updated = [...existing, newReview];
      localStorage.setItem('reviews', JSON.stringify(updated));

      return updated;
    });
  };

  return (
    <div className={styles.wrapper}>
      <Form onAddReview={addReview} />
      <Reviews reviews={reviews} />
    </div>
  );
}

export default Reviewer;
