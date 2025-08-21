"use strict";

const Auth = (() => {
    const promo = document.querySelector('.promo');
    const loginForm = document.querySelector('.login-form');
    const jsonForm = document.querySelector('.json-form');

    const startBtn = document.querySelector('.promo__start-btn');
    const authBtn = document.querySelector('.header__auth-btn');
    const greeting = document.querySelector('.header__greeting');
    const usernameInput = document.querySelector('.login-form__input');
    const errorMessage = document.querySelector(".login-form__error-message");
    const loginSubmit = document.querySelector('.login-form__submit');

    let username = null;

    const showLoginForm = () => {
        promo.style.display = "none";
        jsonForm.style.display = "none";
        loginForm.style.display = "flex";
        errorMessage.style.display = "none";
        errorMessage.textContent = "";
        usernameInput.value = "";
    };

    const showPromo = () => {
        loginForm.style.display = "none";
        jsonForm.style.display = "none";
        promo.style.display = "flex";

        if (username) {
            startBtn.style.display = "block";
        } else {
            startBtn.style.display = "none";
        }
    };

    const updateHeader = () => {
        if (username) {
            greeting.textContent = `Hello, ${username}!`;
            authBtn.textContent = " Log out";
        } else {
            greeting.textContent = "";
            authBtn.textContent = "Log in";
        }
    };

    const handleAuthClick = () => {
        if (username) {
            username = null;
            updateHeader();
            showPromo();
        } else {
            showLoginForm();
        }
    };

    const handleLoginSubmit = (e) => {
        e.preventDefault();
        const value = usernameInput.value.trim();

        if (!value) {
            errorMessage.textContent = "Обязательное поле";
            errorMessage.style.display = "flex";
            return;
        }

        username = value;
        updateHeader();
        loginForm.style.display = "none";
        showPromo();
    };

    const init = () => {
        authBtn.addEventListener("click", handleAuthClick);
        loginSubmit.addEventListener("click", handleLoginSubmit);
        showPromo();
        updateHeader();
    };

    return { init };
})();

export { Auth };
