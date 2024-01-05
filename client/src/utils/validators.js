export const validationInfo = [
  {
    name: "email",
    isValid: true,
    info: "Podano nie poprawny email!",
  },
  {
    name: "nickname",
    isValid: true,
    info: "Podana nazwa jest nie poprawna. Poprawna nazwa powinna składać się z od 3 do 15 znaków.",
  },
  {
    name: "password",
    isValid: true,
    info: "Podane hasło jest nie poprawne. Odopowiednie hasło powinno składać się z minimum 8 znaków, zawierać 1 wielką literę, 1 cyfrę oraz 1 znak specjalny.",
  },
];

export const handleValidation = (values) => {
  const updatedValidationMessages = validationInfo.map((field) => {
    const name = field.name;
    const value = values[name];
    if (value !== undefined) {
      return {
        ...field,
        isValid:
          name === "nickname"
            ? validateNickname(value)
            : name === "email"
            ? validateEmail(value)
            : validatePassword(value),
      };
    } else {
      return {
        ...field,
        isValid: true,
      };
    }
  });

  return updatedValidationMessages;
};

export const handleIsValid = (validationInfo) => {
  return validationInfo.every((field) => field.isValid);
};

export const validateNickname = (nickname) => {
  return nickname.length > 2 && nickname.length < 32;
};

export const validateEmail = (email) => {
  const emailRegex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
  return emailRegex.test(email);
};

export const validatePassword = (password) => {
  const passwordRegex = /^(?=.*[A-Z])(?=.*[!@#$%^&*])(?=.*[0-9]).{8,}$/;
  return passwordRegex.test(password);
};
