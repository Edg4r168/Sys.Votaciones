import { TABLES } from "src/consts/tables";

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
  return categories;
};

const mappedParticipants = (participants) => {
  if (!participants.length) {
    return {
      id: participants.id,
      name: participants.name,
      lastName: participants.lastName,
      studentCode: participants.studentCode,
      contestId: participants.oContest?.id,
      contest: participants.oContest?.name,
    };
  }

  return participants.map((participant) => ({
    id: participant.id,
    name: participant.name,
    lastName: participant.lastName,
    studentCode: participant.studentCode,
    contestId: participant.oContest?.id,
    contest: participant.oContest?.name,
  }));
};

const mappedCareers = (careers) => {
  return careers;
};

const mappedTypesContests = (typesContests) => {
  return typesContests;
};

const mappedContests = (contests) => {
  if (!contests.length) {
    return {
      id: contests.id,
      name: contests.name,
      description: contests.description,
      state: contests.state,
      typeContestId: contests.oTypeContest?.id,
      typeContest: contests.oTypeContest?.name,
    };
  }

  return contests.map((contest) => ({
    id: contest.id,
    name: contest.name,
    description: contest.description,
    state: contest.state,
    typeContestId: contest.oTypeContest?.id,
    typeContest: contest.oTypeContest?.name,
  }));
};

const mappedYears = (years) => {
  return years;
};

export const schemes = {
  [TABLES.users]: mappedUses,
  [TABLES.categories]: mappedCategories,
  [TABLES.careers]: mappedCareers,
  [TABLES.typesContests]: mappedTypesContests,
  [TABLES.contests]: mappedContests,
  [TABLES.years]: mappedYears,
  [TABLES.participants]: mappedParticipants,
};
