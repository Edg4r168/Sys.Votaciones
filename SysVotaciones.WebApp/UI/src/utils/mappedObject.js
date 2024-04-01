import { TABLES } from "src/consts/consts";

const mappedUses = (users) => {
  if (!users.length) {
    return {
      studentCode: users.studentCode,
      careerYearId: users.oCareerYear?.id,
      careerYear: users.oCareerYear?.careerYear,
      careerId: users.oCareer?.id,
      career: users.oCareer?.name,
    };
  }

  return users.map((user) => ({
    studentCode: user.studentCode,
    careerYearId: user.oCareerYear?.id,
    careerYear: user.oCareerYear?.careerYear,
    careerId: user.oCareer?.id,
    career: user.oCareer?.name,
  }));
};

const mappedCategories = (categories) => {
  if (!categories.length) {
    return {
      id: categories.id,
      name: categories.name,
    };
  }

  return categories.map((category) => ({
    id: category.id,
    name: category.name,
  }));
};

const mappedCareers = (careers) => {
  if (!careers.length) {
    return {
      id: careers.id,
      name: careers.name,
    };
  }

  return careers.map((career) => ({
    id: career.id,
    name: career.name,
  }));
};

export const schemes = {
  [TABLES.users]: mappedUses,
  [TABLES.categories]: mappedCategories,
  [TABLES.careers]: mappedCareers,
};
