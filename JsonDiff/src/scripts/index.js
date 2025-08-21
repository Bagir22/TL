"use strict";

import { Auth } from "./auth.js";
import { Diff } from "./diff.js";

const startBtn = document.querySelector('.promo__start-btn');
const jsonForm = document.querySelector('.json-form');
const promo = document.querySelector('.promo');
const oldJsonInput = document.querySelector('#oldJson');
const newJsonInput = document.querySelector('#newJson');
const resultPre = document.querySelector('.json-form__result');
const diffBtn = document.querySelector('.json-form__submit');

const validateJsonField = (textarea) => {
    const errorMessage = textarea.parentElement.querySelector('.json-form__error-message');
    const value = textarea.value.trim();

    if (!value) {
        errorMessage.textContent = "Обязательное поле";
        errorMessage.style.display = "block";

        return null;
    }

    try {
        const parsed = JSON.parse(value);
        errorMessage.textContent = "";
        errorMessage.style.display = "none";

        return parsed;
    } catch {
        errorMessage.textContent = "Некорректный JSON";
        errorMessage.style.display = "block";

        return null;
    }
};

const showJsonForm = () => {
    promo.style.display = 'none';
    jsonForm.style.display = 'flex';
    resultPre.style.display = 'none';

    document.querySelectorAll('.json-form__error-message').forEach(err => {
        err.style.display = 'none';
        err.textContent = "";
    });
};

const init = () => {
    Auth.init();

    startBtn.addEventListener("click", () => {
        showJsonForm();
    });

    diffBtn.addEventListener("click", (e) => {
        e.preventDefault();

        const oldObj = validateJsonField(oldJsonInput);
        const newObj = validateJsonField(newJsonInput);

        if (oldObj && newObj) {
            const diff = Diff.calculate(oldObj, newObj);
            resultPre.style.display = "block";
            resultPre.textContent = diff;
        } else {
            resultPre.style.display = "none";
        }
    });
};

init();
