import Form from "../Form/Form.tsx";
import styles from "./Reviewer.module.css";

function Reviewer ()  {
    return (
        <div className={styles.wrapper}>
            <Form/>
            {/*<Reviews/>*/}
        </div>
    )
}

export default Reviewer;