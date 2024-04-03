import { useEffect, useState } from "react";
import { EditIcon, TrashIcon } from "src/Components/Icons";
import { ModalPortal } from "src/Components/Modal/Modal";
import { FormEdit } from "./FormEdit";
import { FormDelete } from "./FormDelete";
import { TABLE_ACTIONS } from "src/consts/consts";
import { useTable } from "src/hooks/useTable";
import { Row } from "src/Components/Table/Row";

export function ListOfCategories() {
  const [showModal, setShowModal] = useState({ isOpen: false, action: null });
  const { loading, entries, getEntry } = useTable();
  // const [visibleRows, setVisibleRows] = useState(0);

  // useEffect(() => {
  //   let timer;
  //   if (visibleRows < entries.length - 2) {
  // Si aún hay filas por mostrar, configuramos un temporizador para mostrar la siguiente fila después de un breve retraso
  //   timer = setTimeout(() => {
  //     setVisibleRows((preVisibleRows) => preVisibleRows + 1);
  //   }, 100);
  // }

  // Limpiamos el temporizador cuando todos las filas se hayan mostrado
  //   return () => clearTimeout(timer);
  // }, [visibleRows, entries]);

  const handleOnClose = () => {
    setShowModal({ action: null, isOpen: false });
  };

  const handleOnEdit = (id) => {
    setShowModal({ isOpen: true, action: TABLE_ACTIONS.edit });
    getEntry(id);
  };

  const handleOnDelete = (id) => {
    setShowModal({ isOpen: true, action: "delete" });
    getEntry(id);
  };

  const buttonCursor = {
    cursor: loading ? "progress" : "",
  };

  return (
    <>
      {entries.length !== 0 ? (
        entries.map((category) => {
          if (category == null) return;
          // const isVisible = index < visibleRows;
          // console.log(index < visibleRows);
          // console.log({ index, visibleRows });

          return (
            <Row key={category.id}>
              <td>{category.id}</td>
              <td>{category.name}</td>
              <td className="actions center">
                <button
                  style={buttonCursor}
                  onClick={() => handleOnDelete(category.id)}
                >
                  <TrashIcon />
                </button>

                <button
                  style={buttonCursor}
                  onClick={() => handleOnEdit(category.id)}
                >
                  <EditIcon />
                </button>
              </td>
            </Row>
          );
        })
      ) : (
        <tr>
          <td colSpan="3">No se encontraron resultados</td>
        </tr>
      )}

      <ModalPortal
        onClose={handleOnClose}
        openModal={
          showModal.action === TABLE_ACTIONS.edit ? showModal.isOpen : false
        }
      >
        <FormEdit onCancel={handleOnClose} onSubmit={handleOnClose} />
      </ModalPortal>

      <ModalPortal
        onClose={handleOnClose}
        openModal={
          showModal.action === TABLE_ACTIONS.delete ? showModal.isOpen : false
        }
        height={200}
        width={300}
      >
        <FormDelete onSubmit={handleOnClose} />
      </ModalPortal>
    </>
  );
}
