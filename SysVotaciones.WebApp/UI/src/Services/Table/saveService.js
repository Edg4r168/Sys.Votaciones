import { ERRORS, baseUrl } from "src/consts/consts";
import { schemes } from "src/utils/mappedObject";

export const saveService = async ({ entry, table }) => {
  try {
    const response = await fetch(`${baseUrl}/${table}/save`, {
      method: "POST",
      headers: {
        "Content-type": "application/json",
      },
      body: JSON.stringify(entry),
    });
    const res = await response.json();

    if (!res.ok)
      return {
        success: false,
        current: null,
        message: ERRORS.actions.errorSave,
      };

    const current = schemes[table](res.data);

    return {
      success: true,
      current,
      message: ERRORS.actions.successSave,
    };
  } catch (error) {
    throw new Error(ERRORS.actions.errorSave);
  }
};
