import type { StudyType } from "./StudyType";
import type { InputsForStudyType } from "./StudyInputs";
import type { OutputsForStudyType } from "./StudyOutputs";

export type Study<T extends StudyType = StudyType> = {
  id: string;
  name: string;
  type: T;
  inputs: InputsForStudyType<T>
  outputs?: OutputsForStudyType<T>
}