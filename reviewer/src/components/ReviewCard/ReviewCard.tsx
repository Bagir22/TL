import type { ReviewData } from '../../types/ReviewData.ts';
import styles from '../ReviewCard/ReviewCard.module.css';
import defaultIcon from '../../assets/images/customerIcon.jpg';

type ReviewCardProps = {
  review: ReviewData;
};

function ReviewCard({ review }: ReviewCardProps) {
  return (
    <div key={review.guid} className={styles.reviewCard}>
      <img
        src={defaultIcon}
        alt="Customer Icon"
        className={styles.customerIcon}
      />

      <div className={styles.reviewText}>
        <h4 className={styles.name}>{review.name}</h4>
        <p className={styles.comment}>{review.comment}</p>
      </div>

      <p className={styles.reviewRating}>{review.rating}/5</p>
    </div>
  );
}

export default ReviewCard;
