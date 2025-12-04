import { registerLegendService, LegendType, ModelLegendsService, type ElementsGroup } from "@/speckle/ModelLegendsService";

export class AlvenariaLegendsService extends ModelLegendsService<'alvenaria'> {

  @LegendType("Dimensionamento")
  getDimensionamento(): Array<ElementsGroup> {
    console.log('alvenaria Dimensionamento')
    return [];
  }
}

registerLegendService(AlvenariaLegendsService, 'alvenaria');