// AUTO-GENERATED. DO NOT EDIT.

import type { Outputs as AlvenariaOutputs } from '@/studies/alvenaria/Outputs';
import type { Outputs as ParedeDeConcretoOutputs } from '@/studies/parede_de_concreto/Outputs';
import type { Outputs as ParedeEFundacaoOutputs } from '@/studies/parede_e_fundacao/Outputs';
import type { StudyType } from './StudyType';

export type StudyOutputsMap = {
  'alvenaria': AlvenariaOutputs;
  'parede_de_concreto': ParedeDeConcretoOutputs;
  'parede_e_fundacao': ParedeEFundacaoOutputs;
};

export type OutputsForStudyType<T extends StudyType> = T extends keyof StudyOutputsMap
  ? StudyOutputsMap[T]
  : never;
