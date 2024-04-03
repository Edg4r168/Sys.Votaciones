import { ERRORS, baseUrl, amountEntries } from "src/consts/consts";
import { schemes } from "src/utils/mappedObject";

export const getService = async ({
  offset = 0,
  amount = amountEntries,
  table,
}) => {
  try {
    const response = await fetch(
      `${baseUrl}/${table}?offset=${offset}&amount=${amount}`
    );
    const res = await response.json();

    if (!res.ok) return { data: [], message: ERRORS.actions.errorFetch };

    const mappedData = res.data.length > 0 ? schemes[table](res.data) : [];

    return { data: mappedData, message: "" };
  } catch (error) {
    throw new Error(ERRORS.actions.errorFetch);
  }
};
