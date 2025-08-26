import Form from "../Form/Form.tsx";
import styles from "./Reviewer.module.css";
import {useState} from "react";
import type {ReviewData} from "../../types/ReviewData.ts";
import Reviews from "../Reviews/Reviews.tsx";

function Reviewer ()  {
    const [reviews, setReviews] = useState<ReviewData[]>([]);

    const addReview = (newReview: ReviewData) => {
        setReviews(prev => [...prev, newReview]);
    };

    return (
        <div className={styles.wrapper}>
            <Form onAddReview={addReview} />
            <Reviews reviews={reviews} />
        </div>
    )
}

export default Reviewer;