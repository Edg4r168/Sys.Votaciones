import { TCareers } from "../Components/Admin/TCareers/TCareers";
import { TCategory } from "../Components/Admin/TCategory/TCategory";
import { TUsers } from "../Components/Admin/TUsers/TUsers";

export const SECTIONS_DASHBOARD = [
  {
    path: "categories",
    title: "Categorias",
    Component: TCategory,
  },
  {
    path: "users",
    title: "Usuarios",
    Component: TUsers,
  },
  {
    path: "careers",
    title: "Carreras",
    Component: TCareers,
  },
];
