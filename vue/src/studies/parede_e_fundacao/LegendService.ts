import { registerLegendService, LegendType, ModelLegendsService, type ElementsGroup } from "@/speckle/ModelLegendsService";

export class ParedeFundacaoLegendsService extends ModelLegendsService<'parede_e_fundacao'> {
  override getLegendTypes(): string[] {
    if (this.model.studyModel.type === 'input') {
      return ['Dimensionamento']
    } else {
      return ['Dimensionamento', 'Material']
    }
  }

  @LegendType("Dimensionamento")
  getDimensionamentoParede(): Array<ElementsGroup> {
    console.log('parede_de_concreto Dimensionamento')
    return [];
  }

  @LegendType("Material")
  getMaterial(): Array<ElementsGroup> {
    console.log('parede_de_concreto Material')
    return [];
  }
}

registerLegendService(ParedeFundacaoLegendsService, 'parede_e_fundacao')