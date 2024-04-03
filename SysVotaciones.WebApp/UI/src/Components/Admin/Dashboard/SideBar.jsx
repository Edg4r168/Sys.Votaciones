import { UsersIcon } from "src/Components/Icons";
import { Link } from "wouter";
import { SECTIONS_DASHBOARD } from "src/consts/sections";

const ICONS = {
  categories: <UsersIcon />,
  users: <UsersIcon />,
  participants: <UsersIcon />,
};

export function SideBar({ currentSection }) {
  const isSelected = (section) =>
    currentSection === section ? "isSelected" : "";

  return (
    <aside className="sidebar border">
      <nav className="navigation">
        {SECTIONS_DASHBOARD.map((setion) => {
          const Icon = ICONS[setion.path];
          return (
            <Link
              key={setion.title}
              className={isSelected(setion.path)}
              to={`/${setion.path}`}
            >
              {Icon}
              {setion.title}
            </Link>
          );
        })}
      </nav>
    </aside>
  );
}
