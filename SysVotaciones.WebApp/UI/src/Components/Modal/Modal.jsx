import { createPortal } from "react-dom";
import "./index.css";
import { CloseIcon } from "../Icons";

export function Modal({
  children,
  onClose,
  openModal,
  height = "80vh",
  width = "600px",
}) {
  return (
    <>
      {openModal ? (
        <section className="modal">
          <article
            className="modal-content"
            style={{ height: `${height}px`, maxWidth: `${width}px` }}
          >
            <button className="btn-close" onClick={() => onClose && onClose()}>
              <CloseIcon />
            </button>
            {children}
          </article>
        </section>
      ) : null}
    </>
  );
}

export function ModalPortal({ children, onClose, openModal, height, width }) {
  return createPortal(
    <Modal
      onClose={onClose}
      openModal={openModal}
      height={height}
      width={width}
    >
      {children}
    </Modal>,
    document.getElementById("modal-root")
  );
}
