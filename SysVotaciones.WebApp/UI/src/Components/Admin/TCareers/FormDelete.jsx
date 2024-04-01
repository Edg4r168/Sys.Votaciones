import { ButtonGreen } from "src/Components/Layout/Buttons";
import { Form } from "../Forms/Form";
import { FormGroup } from "../Forms/FormGroup";
import { useTable } from "src/hooks/useTable";
import { useKeyUp } from "src/hooks/useKeyUp";

export function FormDelete({ onSubmit }) {
  const { remove, entry } = useTable();

  const handleOnSubmit = (e) => {
    e?.preventDefault();

    remove(entry.id);
    onSubmit && onSubmit();
  };

  useKeyUp(handleOnSubmit);

  return (
    <Form onSubmit={handleOnSubmit}>
      <FormGroup>
        <p className="warning-message">
          ¿Estas seguro de borrar este registro?
        </p>
      </FormGroup>

      <ButtonGreen>Aceptar</ButtonGreen>
    </Form>
  );
}
