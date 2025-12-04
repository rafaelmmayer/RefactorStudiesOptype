import type { StudyType } from '@/types/study';

export function loadInputsComponent(type: StudyType) {
  return import(`@/studies/${type}/InputsComponent.vue`);
}

export function loadOutputsComponent(type: StudyType) {
  return import(`@/studies/${type}/OutputsComponent.vue`);
}