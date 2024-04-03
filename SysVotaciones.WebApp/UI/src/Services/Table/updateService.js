import { ERRORS, baseUrl } from "src/consts/consts";

export const updateService = async ({ entry, table }) => {
  try {
    const response = await fetch(`${baseUrl}/${table}/update`, {
      method: "PATCH",
      headers: {
        "Content-type": "application/json",
      },
      body: JSON.stringify(entry),
    });
    const res = await response.json();
    if (!res.ok) return { success: false, message: ERRORS.actions.errorUpdate };

    return { success: true, message: ERRORS.actions.successUpdate };
  } catch (error) {
    throw new Error(ERRORS.actions.errorUpdate);
  }
};
