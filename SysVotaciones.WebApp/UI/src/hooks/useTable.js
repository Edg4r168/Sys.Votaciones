import { useContext, useRef, useState } from "react";
import { deleteService } from "src/Services/Table/deleteService";
import { getService } from "src/Services/Table/getService";
import { saveService } from "src/Services/Table/saveService";
import { searchService } from "src/Services/Table/searchService";
import { updateService } from "src/Services/Table/updateService";
import { initialOffset } from "src/consts/consts";
import { TableContext } from "src/context/tableContext";

export function useTable() {
  const context = useContext(TableContext);

  if (context === undefined)
    throw new Error("useTable must be used whitin a TableProvider");

  const {
    entries,
    loading,
    messages,
    entry,
    prevEntries,
    total,
    table,
    setPrevEntries,
    setEntry,
    setMessages,
    setEntries,
    setLoading,
    offset,
    setTotal,
  } = context;

  const getPaginated = () => {
    setLoading(true);
    setMessages({ error: null, success: null });

    getService({ offset: offset.current, table })
      .then(({ data, message }) => {
        if (!data) return setMessages({ error: message, success: null });

        setEntries((prev) => [...prev, ...data]);
        setPrevEntries((prev) => [...prev, ...data]);

        offset.current += initialOffset;
      })
      .catch((err) => {
        setMessages({ error: err.message, success: null });
      })
      .finally(() => setLoading(false));
  };

  const remove = async (id) => {
    setLoading(true);
    setMessages({ error: null, success: null });
    setTotal((prevTotal) => prevTotal - 1);

    const newEntris = entries.filter((entry) => {
      const stateId = entry?.id ?? entry?.studentCode;
      return stateId !== id;
    });
    setEntries(newEntris);

    try {
      const { success, message } = await deleteService({ id, table });

      if (success) {
        setPrevEntries(newEntris);
        return setMessages({ error: null, success: message });
      }
      setMessages({ error: message, success: null });
      setEntries(prevEntries);
    } catch (err) {
      setMessages({ error: err.message, success: null });
      setEntries(prevEntries);
      setTotal((prevTotal) => prevTotal + 1);
    } finally {
      setLoading(false);
    }
  };

  const save = (entry) => {
    setLoading(true);
    setMessages({ error: null, success: null });
    setTotal((prevTotal) => prevTotal + 1);

    saveService({ entry, table })
      .then(({ success, current, message }) => {
        if (!success) return setMessages({ error: message, success: null });

        const newEntries = [...entries];
        newEntries.unshift(current);

        setEntries(newEntries);
        setPrevEntries(newEntries);
        setMessages({ error: null, success: message });

        offset.current++;
      })
      .catch((err) => {
        setMessages({ error: err.message, success: null });
        setTotal((prevTotal) => prevTotal - 1);
      })
      .finally(() => setLoading(false));
  };

  const update = (entry) => {
    setLoading(true);
    setMessages({ error: null, success: null });

    // Indice del registro a actualizar
    const id = entry?.id ?? entry?.studentCode;

    const entryIndex = entries.findIndex((entryState) => {
      const stateId = entryState?.id ?? entryState?.studentCode;

      return stateId === id;
    });

    if (entryIndex < 0) {
      setMessages({ error: "El registro no se encontró.", success: null });
      return setLoading(false);
    }

    const newEntries = [
      ...entries.slice(0, entryIndex), // registros anteriores hasta el índice del registro que queremos actualizar.
      {
        ...entry,
      },
      ...entries.slice(entryIndex + 1), // registros restantes después del registro que estamos actualizando.
    ];

    setEntries(newEntries);

    updateService({ entry, table })
      .then(({ success, message }) => {
        if (!success) {
          setPrevEntries(prevEntries);
          setMessages({ error: message, success: null });
          return;
        }

        setPrevEntries(newEntries);
        setMessages({ error: null, success: message });
      })
      .catch((err) => {
        setEntries(prevEntries);
        setMessages({ error: err.message, success: null });
      })
      .finally(() => setLoading(false));
  };

  const getEntry = (id) => {
    const newEntry = entries.find((entry) => {
      const currentId = entry?.id ?? entry?.studentCode;

      return currentId === id;
    });
    setEntry(newEntry);
  };

  const search = ({ keyWord }) => {
    setLoading(true);
    setMessages({ error: null, success: null });

    searchService({ keyWord, table })
      .then(({ data }) => {
        setEntries(data);
      })
      .catch((err) => {
        setMessages({ error: err.message, success: null });
      })
      .finally(() => setLoading(false));
  };

  return {
    entries,
    entry,
    messages,
    loading,
    total,
    prevEntries,
    offset,
    setEntries,
    setTotal,
    remove,
    search,
    save,
    update,
    getEntry,
    getPaginated,
  };
}
