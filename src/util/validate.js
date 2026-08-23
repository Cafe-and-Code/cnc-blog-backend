const validateEmail = (email) => {
  const validRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return validRegex.test(email);
};

const validatePassword = (password) => {
  const validRegex =
    /^(((?=.*[A-Za-z])(?=.*\d))|((?=.*[a-z])(?=.*[A-Z]))|((?=.*[A-Za-z])(?=.*[![\]¥"#$%&'()\-^@;:,.\\_/=~|`{+*}<>?]))|((?=.*\d)(?=.*[![\]¥"#$%&'()\-^@;:,.\\_/=~|`{+*}<>?])))[A-Za-z\d[![\]¥"#$%&'()\-^@;:,.\\_/=~|`{+*}<>?]{8,}$/;
  if (!validRegex.test(password) || password.length > 20) {
    return false;
  }
  return true;
};

const validateConfirmPassword = (confirmPassword, password) => {
  if (confirmPassword !== password) {
    return false;
  }
  return true;
};

module.exports = {
  validateEmail,
  validatePassword,
  validateConfirmPassword,
};
