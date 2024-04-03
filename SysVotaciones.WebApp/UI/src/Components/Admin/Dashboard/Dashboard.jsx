import "./index.css";
import { DropDownMenu } from "src/Components/DropDownMenu";
import { TableProvider } from "src/context/tableContext";
import { SideBar } from "./SideBar";
import { Match } from "src/Components/Match/Match";
import { SECTIONS_DASHBOARD } from "src/consts/sections";
import { TABLES } from "src/consts/tables";

export function Dashboard({ params }) {
  const currentSection = params?.section;

  return (
    <section className="dashboard">
      <header className="header-dashboard">
        <DropDownMenu />
      </header>

      <SideBar currentSection={currentSection} />

      <main className="border">
        {SECTIONS_DASHBOARD.map(({ path, Component }) => {
          if (Component == null) return null;

          return (
            <Match
              key={path}
              path={path}
              component={
                <TableProvider table={TABLES[path]}>
                  <Component />
                </TableProvider>
              }
            />
          );
        })}
      </main>
    </section>
  );
}
