import { TParticipant } from "src/Components/Admin/TParticipant/TParticipant";
import { TCareers } from "../Components/Admin/TCareers/TCareers";
import { TCategory } from "../Components/Admin/TCategory/TCategory";
import { TUsers } from "../Components/Admin/TUsers/TUsers";
import { TTypeContest } from "src/Components/Admin/TTypeContest/TTypeContest";
import { TYear } from "src/Components/Admin/TYear/TYear";
import { TContest } from "src/Components/Admin/TContest/TContest";

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
    path: "participants",
    title: "Participantes",
    Component: TParticipant,
  },
  {
    path: "careers",
    title: "Carreras",
    Component: TCareers,
  },
  {
    path: "typesContests",
    title: "Tipos de concurso",
    Component: TTypeContest,
  },
  {
    path: "contests",
    title: "Concursos",
    Component: TContest,
  },
  {
    path: "years",
    title: "Años de carreras",
    Component: TYear,
  },
];
