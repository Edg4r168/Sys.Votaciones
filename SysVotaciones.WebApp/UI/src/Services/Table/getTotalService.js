import { ERRORS, baseUrl } from "src/consts/consts";

export const getTotalService = async ({ table }) => {
  try {
    const response = await fetch(`${baseUrl}/${table}/total`);
    const res = await response.json();

    if (!res.ok) return { success: false, total: 0 };

    return { success: true, total: res.total };
  } catch (error) {
    throw new Error(ERRORS.actions.errorFetch);
  }
};
