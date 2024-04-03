export const baseUrl = "http://localhost:5217";

export const ERRORS = {
  login: {
    invalidCredentials:
      "Por favor, verifica tus credenciales e intenta nuevamente.",
    loginError:
      "No se pudo completar el inicio de sesión debido a un error. Por favor, inténtelo de nuevo más tarde.",
  },
  register: {
    registerError:
      "No se pudo completar el registro de su cuenta debido a un error. Por favor, inténtelo de nuevo más tarde",
  },
  actions: {
    errorDelete: "No se pudo borrar el registro debido a un error inesperado",
    successDelete: "El registro se ha borrado correctamente",
    errorUpdate:
      "No se pudo actualizar el registro debido a un error inesperado",
    successUpdate: "El registro se ha actualizado correctamente",
    errorSave: "No se pudo guardar el registro debido a un error inesperado",
    successSave: "El registro se ha guardado correctamente",
    errorFetch: "Error al obetener los datos",
  },
};

export const TABLE_ACTIONS = {
  edit: "edit",
  delete: "delete",
};

export const amountEntries = 4;
export const initialOffset = amountEntries;
