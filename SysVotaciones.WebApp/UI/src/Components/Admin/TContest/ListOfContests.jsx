import { useState } from "react";
import { EditIcon, TrashIcon } from "src/Components/Icons";
import { ModalPortal } from "src/Components/Modal/Modal";
import { FormEdit } from "./FormEdit";
import { FormDelete } from "./FormDelete";
import { TABLE_ACTIONS } from "src/consts/consts";
import { useTable } from "src/hooks/useTable";
import { Row } from "src/Components/Table/Row";

export function ListOfContests() {
  const [showModal, setShowModal] = useState({ isOpen: false, action: null });
  const { loading, entries, getEntry } = useTable();

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
        entries.map((entry) => {
          if (entry == null) return;

          return (
            <Row key={entry.id}>
              <td>{entry.id}</td>
              <td>{entry.name}</td>
              <td style={{ maxWidth: "60px", overflow: "hidden" }}>
                {entry.description}
              </td>
              <td>{entry.state === 0 ? "Abierto" : "Cerrado"}</td>
              <td>{entry.typeContest}</td>
              <td className="actions center">
                <button
                  style={buttonCursor}
                  onClick={() => handleOnDelete(entry.id)}
                >
                  <TrashIcon />
                </button>

                <button
                  style={buttonCursor}
                  onClick={() => handleOnEdit(entry.id)}
                >
                  <EditIcon />
                </button>
              </td>
            </Row>
          );
        })
      ) : (
        <tr>
          <td></td>
          <td>No se encontraron resultados</td>
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