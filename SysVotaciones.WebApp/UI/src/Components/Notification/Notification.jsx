import { useEffect, useState } from "react";

export const Notification = ({
  duration = 500,
  onTimeout = null,
  children,
}) => {
  const [isVisible, setIsvisible] = useState(true);

  useEffect(() => {
    const timer = setTimeout(() => {
      setIsvisible(false);
      onTimeout && onTimeout();
    }, duration);

    return () => clearTimeout(timer);
  }, [duration, onTimeout]);

  return isVisible ? <span className="success-message">{children}</span> : null;
};
