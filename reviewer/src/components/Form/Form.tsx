import styles from "./Form.module.css";
import angry from "../../assets/images/emojies/twemoji_angry-face.svg";
import frown from "../../assets/images/emojies/twemoji_slightly-frowning-face.svg";
import neutral from "../../assets/images/emojies/twemoji_neutral-face.svg";
import smile from "../../assets/images/emojies/twemoji_slightly-smiling-face.svg";
import grin from "../../assets/images/emojies/twemoji_grinning-face-with-big-eyes.svg";

import {type FormEvent, useState} from "react";
import type {ReviewData} from "../../types/ReviewData.ts";

type FormProps = {
    onAddReview: (review: ReviewData) => void;
};

function Form({ onAddReview }: FormProps) {
    const [activeEmoji, setActiveEmoji] = useState<number | null>(null);
    const [name, setName] = useState("");
    const [review, setReview] = useState("");

    const emojies = [
        { src: angry, alt: "Angry emoji" },
        { src: frown, alt: "Frown emoji" },
        { src: neutral, alt: "Neutral emoji" },
        { src: smile, alt: "Smile emoji" },
        { src: grin, alt: "Grinning emoji" },
    ];

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        if (!activeEmoji) {
            alert("Пожалуйста, выберите оценку");
            return;
        }

        const newReview: ReviewData = {
            guid: crypto.randomUUID(),
            name,
            comment: review,
            rating: activeEmoji,
        };

        onAddReview(newReview);

        setActiveEmoji(null);
        setName("");
        setReview("");
    };

    return (
        <div className={styles.container}>
            <h5 className={styles.title}>
                Помогите нам сделать процесс бронирования лучше
            </h5>

            <div className={styles.emojiesList}>
                {emojies.map((emoji, index) => (
                    <button
                        key={index}
                        type="button"
                        className={` ${styles.emojiButton} ${activeEmoji === index + 1 ? styles.active : ""}`}
                        onClick={() => setActiveEmoji(index + 1)}
                    >
                        <img src={emoji.src} alt={emoji.alt}/>
                    </button>
                ))}
            </div>

            <form className={styles.form} onSubmit={handleSubmit}>
                <div className={styles.inputBox}>
                    <label className={styles.labelName}>*Имя</label>
                    <input
                        type="text"
                        placeholder="Как вас зовут?"
                        className={`${styles.input} ${styles.inputName}`}
                        value={name}
                        onChange={e => setName(e.target.value)}
                        required
                    />
                </div>

                <textarea
                    placeholder="Напишите, что понравилось, что было непонятно"
                    value={review}
                    onChange={e => setReview(e.target.value)}
                    className={`${styles.textarea} ${styles.textareaReview}`}
                    required
                />

                <div className={styles.buttonBox}>
                    <button type="submit" className={styles.submitBtn}>
                        Отправить
                    </button>
                </div>
            </form>
        </div>
    );
}

export default Form;
