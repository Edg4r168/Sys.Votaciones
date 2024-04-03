import { baseUrl } from "../consts/consts";

export const getCareerYearsService = async () => {
  try {
    const response = await fetch(`${baseUrl}/Year`);
    const res = await response.json();
    console.log(res);

    if (!res.ok) return [];

    return res.data;
  } catch (error) {
    return [];
  }
};
