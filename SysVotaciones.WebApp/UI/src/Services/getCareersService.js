import { baseUrl } from "../consts/consts";

export const getCareersService = async () => {
  try {
    const response = await fetch(`${baseUrl}/Career`);
    const res = await response.json();

    if (!res.ok) return [];

    return res.data;
  } catch (error) {
    return [];
  }
};
