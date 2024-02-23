import { useState, useEffect } from "react";
import { getCareerYearsService } from "../Services/getCareerYearsService";

export function useCareerYears() {
  const [careerYears, setCareerYears] = useState([]);

  useEffect(() => {
    getCareerYearsService().then((careerYears) => {
      setCareerYears(careerYears);
    });
  }, []);

  return { careerYears };
}
