export const validateLoginForm = ({ email, password }) => {
    const isEmailValid = validateEmail(email);
    const isPasswordValid = validatePassword(password);

    return isEmailValid && isPasswordValid;
};
  
export const validateRegisterForm = ({ email, password, username }) => {
    return (
        validateEmail(email) &&
        validatePassword(password) &&
        validateUsername(username)
    );
};

export const validateUsername = (username) => {
    return username.length > 2 && username.length < 13;
};

export const validateEmail = (email) => {
    const emailRegex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
    return emailRegex.test(email);
};

export const validatePassword = (password) => {
    return password.length > 5 && password.length < 13;
};