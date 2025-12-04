import type { Study, StudyModel, StudyModelType } from "../types/study";

export type StudyModelDb = Omit<StudyModel, 'study'> & { studyId: string }

class StudyRepository {
  private readonly studies = new Map<string, Study>([
    [
      "7005f455d2194371bccccffbade1c681",
      { 
        id: '7005f455d2194371bccccffbade1c681', 
        name: 'SP-0200-LN-CD10',
        type: 'parede_e_fundacao',
        inputs: {
          tipoDeBloco: ''
        }
      }
    ],
    [
      "02579415e04a474781cae7499b64ee8b",
      { 
        id: '02579415e04a474781cae7499b64ee8b', 
        name: 'Rua da Paz',
        type: 'alvenaria',
        inputs: {
          tipoDeBloco: 'Tipo 1'
        } 
      }
    ],
    [
      "43ba61cf1dbe4431acd52fe45298593f",
      { 
        id: '02579415e04a474781cae7499b64ee8b', 
        name: 'Liber Jaçanã',
        type: 'parede_de_concreto',
        inputs: {
          tipoDeBloco: ''
        }
      }
    ]
  ])

  public getStudies(): Array<Study> {
    return Array.from(this.studies.values())
  }

  public getStudy(id: string): Study | undefined {
    return this.studies.get(id)
  }
}

class StudyModelRepository {
  private readonly models = new Map<string, StudyModelDb>([
    [
      "a8daddf7b0",
      {
        id: 'a8daddf7b0',
        type: 'input',
        studyId: '7005f455d2194371bccccffbade1c681',
        speckleProjectId: '39e49d34ff'
      }
    ],
    [
      "e2a3580746",
      {
        id: 'e2a3580746',
        type: 'output',
        studyId: '7005f455d2194371bccccffbade1c681',
        speckleProjectId: '39e49d34ff'
      }
    ],
    [
      "e8a4c25f7f",
      {
        id: 'e8a4c25f7f',
        type: 'input',
        studyId: '02579415e04a474781cae7499b64ee8b',
        speckleProjectId: '6690d5548e'
      }
    ],
    [
      "c90a9e67e3",
      {
        id: 'c90a9e67e3',
        type: 'output',
        studyId: '02579415e04a474781cae7499b64ee8b',
        speckleProjectId: '6690d5548e'
      }
    ],
    [
      "b40d39577f",
      {
        id: 'b40d39577f',
        type: 'input',
        studyId: '43ba61cf1dbe4431acd52fe45298593f',
        speckleProjectId: '27d43048b2'
      }
    ],
    [
      "ebc3f67ce4",
      {
        id: 'ebc3f67ce4',
        type: 'output',
        studyId: '43ba61cf1dbe4431acd52fe45298593f',
        speckleProjectId: '27d43048b2'
      }
    ]
  ])

  public getModels(): Array<StudyModelDb> {
    return Array.from(this.models.values())
  }

  public getModel(id: string): StudyModelDb | undefined {
    return this.models.get(id)
  }

  public getModelByStudyId(
    studyId: string, type: StudyModelType 
  ): StudyModelDb | undefined {
    return Array.from(this.models.values()).find(
      model => model.studyId === studyId && model.type === type
    )
  }

  public getModelsByStudyId(
    studyId: string
  ): StudyModelDb[] {
    return Array.from(this.models.values()).filter(
      model => model.studyId === studyId
    )
  }
}

class AppDbContext {
  public studies: StudyRepository
  public models: StudyModelRepository

  constructor() {
    this.studies = new StudyRepository()
    this.models = new StudyModelRepository()
  }
} 

export const db = new AppDbContext()