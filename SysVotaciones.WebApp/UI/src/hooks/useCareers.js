import { useEffect, useState } from "react";
import { getCareersService } from "../Services/getCareersService";

export function useCareers() {
  const [careers, setCareers] = useState([]);

  useEffect(() => {
    getCareersService().then((careers) => {
      setCareers(careers);
    });
  }, []);

  return { careers };
}
