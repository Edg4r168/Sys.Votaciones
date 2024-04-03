import { ERRORS, baseUrl } from "src/consts/consts";

export const deleteService = async ({ id, table }) => {
  console.log(id);
  try {
    const response = await fetch(`${baseUrl}/${table}/delete/${id}`, {
      method: "DELETE",
    });
    const res = await response.json();

    if (!res.ok) return { success: false, message: ERRORS.actions.errorDelete };

    return { success: true, message: ERRORS.actions.successDelete };
  } catch (error) {
    throw new Error(ERRORS.actions.errorDelete);
  }
};
