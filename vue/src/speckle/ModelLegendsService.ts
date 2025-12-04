import "reflect-metadata";
import type { SpeckleModel } from "./SpeckleModel";
import type { StudyType } from "@/types/study";

const LEGEND_TYPE_MAP_KEY = "legend:typeMap";

type LegendServiceConstructor<T extends StudyType> = new (model: SpeckleModel<T>) => ModelLegendsService<T>;

export const LegendRegistry: Record<string, LegendServiceConstructor<any>> = {};

// Função para registrar um LegendService
export function registerLegendService<T extends StudyType>(
  constructor: LegendServiceConstructor<T>,
  type: T
) {
  LegendRegistry[type] = constructor as any;
}

export function createLegendService<T extends StudyType>(model: SpeckleModel<T>) {
  debugger
  const type = model.studyModel.study.type;

  const Target = LegendRegistry[type];
  if (!Target) {
    throw new Error("Serviço não encontrado: " + type);
  }

  return new Target(model);
}

export function LegendType(typeKey: string) {
  return function (target: any, propertyKey: string) {
    const ctor = target.constructor;

    let map: Map<string, string> =
      Reflect.getMetadata(LEGEND_TYPE_MAP_KEY, ctor) ||
      new Map<string, string>();

    map.set(typeKey, propertyKey);

    Reflect.defineMetadata(LEGEND_TYPE_MAP_KEY, map, ctor);
  };
}

export type ElementsGroup = {
  name: string;
  elementsIds: string[];
};

export interface IModelLegendsService {
  getLegends(key: string): Array<ElementsGroup>;
  getLegendTypes(): string[];
}

export abstract class ModelLegendsService<T extends StudyType = StudyType> implements IModelLegendsService {
  protected model: SpeckleModel<T>;

  constructor(model: SpeckleModel<T>) {
    this.model = model;
  }

  getLegends(typeKey: string): Array<ElementsGroup> {
    const ctor = this.constructor as any;

    const typeMap: Map<string, string> | undefined =
      Reflect.getMetadata(LEGEND_TYPE_MAP_KEY, ctor);

    if (!typeMap) return [];

    const methodName = typeMap.get(typeKey);
    if (!methodName) return [];

    const fn = (this as any)[methodName];
    if (typeof fn !== "function") return [];

    return fn.call(this);
  }

  getLegendTypes(): string[] {
    const ctor = this.constructor as any;

    const typeMap: Map<string, string> | undefined =
      Reflect.getMetadata(LEGEND_TYPE_MAP_KEY, ctor);

    if (!typeMap) return [];

    return Array.from(typeMap.keys());
  }
}