/* eslint-disable react/prop-types */
import { useEffect, useState } from "react";

export const Notification = ({
  duration = 500,
  onAnimationEnd = null,
  type = "",
  children,
}) => {
  const [isVisible, setIsvisible] = useState(true);

  useEffect(() => {
    const timer = setTimeout(() => {
      setIsvisible(false);
    }, duration);

    const timerEnd = setTimeout(() => {
      onAnimationEnd && onAnimationEnd();
    }, duration + 500);

    // Limpiar el temporizador al desmontar el componente
    return () => {
      clearTimeout(timer);
      clearTimeout(timerEnd);
    };
  }, [duration, onAnimationEnd]);

  return (
    <span
      className={`notification mgs-${type} ${isVisible ? "" : "slideUp"}`}
      onTransitionEnd={() => setIsvisible(false)}
    >
      {children}
    </span>
  );
};
