import { createContext, useEffect, useRef, useState } from "react";
import { getService } from "src/Services/Table/getService";
import { getTotalService } from "src/Services/Table/getTotalService";
import { amountEntries, initialOffset } from "src/consts/consts";

export const TableContext = createContext();

// const MokUsers = [
//   {
//     studentCode: "sl0045",
//     careerYearId: 1,
//     careerYear: "2",
//     careerId: 1,
//     career: "Software",
//   },
//   {
//     studentCode: "pa24003",
//     careerYearId: 1,
//     careerYear: "2",
//     careerId: 1,
//     career: "Software",
//   },
//   {
//     studentCode: "ljiin8",
//     careerYearId: 1,
//     careerYear: "2",
//     careerId: 1,
//     career: "Software",
//   },
//   {
//     studentCode: "lii",
//     careerYearId: 1,
//     careerYear: "2",
//     careerId: 1,
//     career: "Software",
//   },
// ];

export function TableProvider({ children, table }) {
  const [entries, setEntries] = useState([]);
  const [prevEntries, setPrevEntries] = useState([]);
  const [entry, setEntry] = useState({});
  const [total, setTotal] = useState(0);
  const [messages, setMessages] = useState({
    error: null,
    success: null,
  });
  const [loading, setLoading] = useState(false);
  const offset = useRef(initialOffset);

  useEffect(() => {
    getService({ offset: 0, amount: amountEntries, table })
      .then(({ data, message }) => {
        if (!data) return setMessages({ error: message, success: null });

        setEntries(data);
        setPrevEntries(data);
      })
      .catch((err) => {
        setMessages({ error: err.message, success: null });
      });
  }, []);

  useEffect(() => {
    getTotalService({ table }).then(({ total }) => {
      setTotal(total);
    });
  }, []);

  return (
    <TableContext.Provider
      value={{
        entries,
        loading,
        messages,
        entry,
        offset,
        prevEntries,
        total,
        table,
        setPrevEntries,
        setEntry,
        setMessages,
        setEntries,
        setLoading,
        setTotal,
      }}
    >
      {children}
    </TableContext.Provider>
  );
}
