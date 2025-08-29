import styles from './Reviews.module.css';
import type { ReviewData } from '../../types/ReviewData.ts';
import ReviewCard from '../ReviewCard/ReviewCard.tsx';

type ReviewsDataProps = {
  reviews: ReviewData[];
};

function Reviews({ reviews }: ReviewsDataProps) {
  return (
    <div className={styles.container}>
      {reviews.map((review) => (
        <ReviewCard key={review.guid} review={review} />
      ))}
    </div>
  );
}

export default Reviews;
