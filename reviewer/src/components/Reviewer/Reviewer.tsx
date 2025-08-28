import Form from '../Form/Form.tsx';
import styles from './Reviewer.module.css';
import type { ReviewData } from '../../types/ReviewData.ts';
import Reviews from '../Reviews/Reviews.tsx';
import { useLocalStorage } from '../../hooks/useLocalStorage.ts';

function Reviewer() {
  const [reviews, setReviews] = useLocalStorage<ReviewData[]>('reviews', []);

  const addReview = (newReview: ReviewData) => {
    setReviews(existing => [...existing, newReview]);
  };

  return (
    <div className={styles.wrapper}>
      <Form onAddReview={addReview} />
      <Reviews reviews={reviews} />
    </div>
  );
}

export default Reviewer;
