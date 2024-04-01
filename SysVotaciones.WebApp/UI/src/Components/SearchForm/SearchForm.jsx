import { useTable } from "src/hooks/useTable";
import { SearchIcon } from "../Icons";
import { ButtonSearch } from "../Layout/Buttons";
import "./index.css";

// const SEARCH_FILTERS = [];

export function SearchForm({ searchBy }) {
  const { loading, setEntries, search, prevEntries } = useTable();

  const handleOnSearch = (e) => {
    const keyWord = e.target.value.toLowerCase();

    if (keyWord === "") return setEntries(prevEntries);

    const newEntries = prevEntries.filter((entry) => {
      const keyWordToCompare = entry[searchBy]?.toLowerCase();

      return keyWordToCompare?.startsWith(keyWord);
    });

    setEntries(newEntries);
  };

  const handleOnSubmit = (e) => {
    e.preventDefault();

    const data = new FormData(e.target);
    const keyWord = data.get("keyword");

    search({ keyWord });
  };

  const styleCursor = {
    cursor: loading ? "progress" : "",
  };

  return (
    <>
      <form className="form-search-categories" onSubmit={handleOnSubmit}>
        <div className="container-textInputSearch">
          <SearchIcon />
          <input
            style={styleCursor}
            name="keyword"
            type="text"
            className="inputSearch"
            placeholder="Buscar..."
            onChange={handleOnSearch}
          />
          {/* <select name="filters">
            <option value="1">Nombre</option>
          </select> */}
        </div>
        <ButtonSearch style={styleCursor}>Buscar</ButtonSearch>
      </form>
    </>
  );
}
