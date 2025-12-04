import type { Study } from "./Study";
import type { StudyType } from "./StudyType";

export type StudyModelType = 'input' | 'output';

export type StudyModel<T extends StudyType = StudyType> = {
  id: string;
  study: Study<T>;
  type: StudyModelType
  speckleProjectId: string
}