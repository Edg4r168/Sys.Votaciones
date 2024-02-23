import { ERRORS, baseUrl } from "src/consts";

export const loginAdService = async (admin) => {
  try {
    const response = await fetch(`${baseUrl}/Admin/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(admin),
    });

    const res = await response.json();
    console.log(res);

    if (!res.ok)
      return { token: null, message: ERRORS.login.invalidCredentials };

    return { token: res.token, message: "Te has registrado correctamente" };
  } catch (error) {
    console.log(error);
    throw new Error(ERRORS.login.loginError);
  }
};
