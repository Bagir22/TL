import styles from "./Reviews.module.css";
import type {ReviewData} from "../../types/ReviewData.ts";
import defaultAvatar from "../../assets/images/customerIcon.jpg";

type ReviewsDataProps = {
    reviews: ReviewData[]
}

function Reviews({reviews}: ReviewsDataProps) {
    return(
        <div className={styles.container}>
            {reviews.map((review) => (
                <div key={review.id} className={styles.reviewCard}>
                    <img
                        src={defaultAvatar}
                        alt="Customer Icon"
                        className={styles.customerIcon}
                    />
                    <div className={styles.reviewText}>
                        <h4 className={styles.name}>{review.name}</h4>
                        <p className={styles.comment}>{review.comment}</p>
                    </div>

                    <p className={styles.reviewRating}>{review.rating}/5</p>
                </div>
            ))}
        </div>
    )
}

export default Reviews;