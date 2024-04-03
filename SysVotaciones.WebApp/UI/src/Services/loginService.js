import { ERRORS, baseUrl } from "../consts/consts";

export const loginService = async ({ studentCode, password }) => {
  try {
    const response = await fetch(`${baseUrl}/Student/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ studentCode, password }),
    });

    const res = await response.json();

    if (!res.ok)
      return { token: null, message: ERRORS.login.invalidCredentials };

    return { token: res.token, message: "" };
  } catch (error) {
    throw new Error(ERRORS.login.loginError);
  }
};
