import type { TreeNode } from "@speckle/viewer";
import type { StudyModel } from "../types/study/StudyModel";
import type { StudyType } from "../types/study";
import type { SpeckleViewer } from "./SpeckleViewer"
import { createLegendService, type IModelLegendsService } from "./ModelLegendsService";

export type SpeckleModelParams<T extends StudyType = StudyType> = {
  modelUrl: string;
  viewer: SpeckleViewer;
  studyModel: StudyModel<T>;
  resourceUrl: string;
  treeNode: TreeNode
}

export class SpeckleModel<T extends StudyType = StudyType> {
  private _studyModel: StudyModel<T>;
  private _viewer: SpeckleViewer;
  private _modelUrl: string;
  private _resourceUrl: string
  private _treeNode: TreeNode
  private _legendService: IModelLegendsService

  get studyModel(): StudyModel<T> {
    return this._studyModel;
  }
  get modelId(): string {
    return this._studyModel.id;
  }
  get modelUrl(): string {
    return this._modelUrl;
  }
  get resourceUrl(): string {
    return this._resourceUrl
  }
  get treeNode(): TreeNode {
    return this._treeNode
  }
  get legendService(): IModelLegendsService {
    return this._legendService
  }

  constructor({
    modelUrl,
    viewer,
    studyModel,
    resourceUrl,
    treeNode
  }: SpeckleModelParams<T>) {
    this._studyModel = studyModel;
    this._viewer = viewer;
    this._modelUrl = modelUrl;
    this._resourceUrl = resourceUrl;
    this._treeNode = treeNode

    this._legendService = createLegendService(this)
  }
}