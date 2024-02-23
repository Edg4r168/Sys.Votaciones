import { baseUrl } from "../consts";

export const getCareersService = async () => {
  try {
    const response = await fetch(`${baseUrl}/Career`);
    const res = await response.json();
    console.log(res);

    if (!res.ok) return [];

    return res.data;
  } catch (error) {
    return [];
  }
};
