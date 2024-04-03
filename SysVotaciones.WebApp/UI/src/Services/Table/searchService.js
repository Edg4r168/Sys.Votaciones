import { ERRORS, baseUrl } from "src/consts/consts";
import { schemes } from "src/utils/mappedObject";

export const searchService = async ({ keyWord, table }) => {
  try {
    const response = await fetch(
      `${baseUrl}/${table}/search?keyword=${keyWord}`
    );
    const res = await response.json();

    if (!res.ok) return { data: [], message: ERRORS.actions.errorFetch };

    const mappedData = schemes[table](res.data);

    return { data: mappedData, message: "" };
  } catch (error) {
    throw new Error(ERRORS.actions.errorFetch);
  }
};
