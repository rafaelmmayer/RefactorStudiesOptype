import { registerLegendService, LegendType, ModelLegendsService, type ElementsGroup } from "@/speckle/ModelLegendsService";

export class ParedeConcretoLegendsService extends ModelLegendsService<'parede_de_concreto'> {
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
    // Agora você tem acesso tipado aos inputs!
    // this.model.studyModel.study.inputs.tipoDeBloco <- autocomplete funcionando!
    return [];
  }

  @LegendType("Material")
  getMaterial(): Array<ElementsGroup> {
    console.log('parede_de_concreto Material')
    return [];
  }
}

registerLegendService(ParedeConcretoLegendsService, 'parede_de_concreto');