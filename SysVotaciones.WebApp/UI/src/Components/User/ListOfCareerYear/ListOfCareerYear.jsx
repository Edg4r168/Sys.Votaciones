export const ListOfCareerYear = ({ careerYears = [] }) => {
  return (
    <>
      {careerYears.map((careerYear) => (
        <option key={careerYear.id} value={careerYear.id}>
          {careerYear.careerYear}
        </option>
      ))}
    </>
  );
};
