import { ButtonGreen, ButtonRed } from "src/Components/Layout/Buttons";
import { Form } from "../Forms/Form";
import { ButtonsGroup, FormGroup } from "../Forms/FormGroup";
import { useForm } from "src/hooks/useForm";
import { useTable } from "src/hooks/useTable";

export function FormSave({ onCancel, onSubmit }) {
  const { save } = useTable();
  const { values, onChange } = useForm({
    name: "",
  });

  const handleOnSubmit = (e) => {
    e.preventDefault();
    save(values);
    onSubmit && onSubmit();
  };

  return (
    <Form onSubmit={handleOnSubmit}>
      <FormGroup>
        <label htmlFor="name">Nombre</label>
        <input
          name="name"
          id="name"
          type="text"
          value={values.name}
          onChange={onChange}
        />
      </FormGroup>

      <ButtonsGroup>
        <ButtonGreen>Aceptar</ButtonGreen>
        <ButtonRed type="button" onClick={() => onCancel && onCancel()}>
          Canelar
        </ButtonRed>
      </ButtonsGroup>
    </Form>
  );
}
