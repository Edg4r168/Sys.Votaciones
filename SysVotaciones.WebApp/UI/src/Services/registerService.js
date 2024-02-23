import { ERRORS, baseUrl } from "../consts.js";

export const registerService = async (student) => {
  try {
    const response = await fetch(`${baseUrl}/Student/save`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(student),
    });

    const res = await response.json();

    if (!res.ok) return false;

    return true;
  } catch (error) {
    throw new Error(ERRORS.register.registerError);
  }
};
