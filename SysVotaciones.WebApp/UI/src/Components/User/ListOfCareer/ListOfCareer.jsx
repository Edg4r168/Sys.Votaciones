import React from "react";

const MemoizedListOfCareer = ({ careers = [] }) => {
  return (
    <>
      {careers.map((career) => (
        <option key={career.id} value={career.id}>
          {career.name}
        </option>
      ))}
    </>
  );
};

const ListOfCareer = React.memo(MemoizedListOfCareer);
export default ListOfCareer;
