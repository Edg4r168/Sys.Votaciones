import { Form } from "../Forms/Form";
import { ButtonsGroup, FormGroup } from "../Forms/FormGroup";
import { useForm } from "src/hooks/useForm";
import { ButtonGreen, ButtonRed } from "src/Components/Layout/Buttons";
import { useTable } from "src/hooks/useTable";

export function FormEdit({ onCancel, onSubmit }) {
  const { entry, update } = useTable();
  const { values, onChange } = useForm(entry);

  const handleOnSubmit = (e) => {
    e.preventDefault();

    update(values);
    onSubmit && onSubmit();
  };

  return (
    <>
      <Form onSubmit={handleOnSubmit}>
        <FormGroup>
          <label htmlFor="id">Id</label>
          <input name="id" id="id" type="text" value={values?.id} disabled />
        </FormGroup>

        <FormGroup>
          <label htmlFor="careerYear">Año de carrera</label>
          <input
            name="careerYear"
            id="careerYear"
            type="text"
            value={values.careerYear}
            onChange={onChange}
          />
        </FormGroup>

        <ButtonsGroup>
          <ButtonGreen>Actualizar</ButtonGreen>
          <ButtonRed type="button" onClick={() => onCancel && onCancel()}>
            Canelar
          </ButtonRed>
        </ButtonsGroup>
      </Form>
    </>
  );
}
