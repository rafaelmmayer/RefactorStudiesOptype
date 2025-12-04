// AUTO-GENERATED. DO NOT EDIT.

import type { Inputs as AlvenariaInputs } from '@/studies/alvenaria/Inputs';
import type { Inputs as ParedeDeConcretoInputs } from '@/studies/parede_de_concreto/Inputs';
import type { Inputs as ParedeEFundacaoInputs } from '@/studies/parede_e_fundacao/Inputs';
import type { StudyType } from './StudyType';

export type StudyInputsMap = {
  'alvenaria': AlvenariaInputs;
  'parede_de_concreto': ParedeDeConcretoInputs;
  'parede_e_fundacao': ParedeEFundacaoInputs;
};

export type InputsForStudyType<T extends StudyType> = T extends keyof StudyInputsMap
  ? StudyInputsMap[T]
  : never;
