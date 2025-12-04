import { 
  Viewer,
  DefaultViewerParams,
  UrlHelper,
  SpeckleLoader,

  ViewerEvent,
  LoaderEvent,

  MeasurementsExtension,

  ViewModes,
  ViewMode,
  CameraController,
  SelectionExtension,
} from "@speckle/viewer";
import { SpeckleModel } from "./SpeckleModel";
import type { StudyModel } from "../types/study/StudyModel";
import { getAppConfig, type AppConfig } from "@/config";

export type SpeckleViewerParams = {
  container: HTMLElement
  token: string
}

export type SpeckleViewerInitParams = {
  onLoadComplete: () => void;
  onFilteringStateSet: () => void;
  onPointerMove: (e: PointerEvent) => void;
}

export type SpeckleViewerLoadObjectParams = {
  studyModel: StudyModel;
  onLoadProgress: (progress: number) => void;
}

export class SpeckleViewer  {
  private configs: AppConfig
  private viewer: Viewer
  private token: string
  private isInitialized: boolean = false
  private loadingUrl: string | null = null

  private mainModel: SpeckleModel | null = null
  private models = new Map<string, SpeckleModel>()

  constructor({ container, token }: SpeckleViewerParams) {
    const viewerParams = DefaultViewerParams;
		viewerParams.showStats = false;
		viewerParams.verbose = true;

    this.viewer = new Viewer(container, viewerParams)
    this.token = token
    this.configs = getAppConfig()
  }

  public async init({ 
    onLoadComplete 
  }: SpeckleViewerInitParams): Promise<void> {
    if (this.isInitialized) {
      return
    }

    const viewerParams = DefaultViewerParams;
		viewerParams.showStats = false;
		viewerParams.verbose = true;
    
    await this.viewer.init();
    this.viewer.on(ViewerEvent.LoadComplete, () => {
      this.loadingUrl = null
      onLoadComplete()
    });

    this.viewer.createExtension(CameraController);

    this.viewer.createExtension(SelectionExtension);

    const measurementsExt = this.viewer.createExtension(MeasurementsExtension);
		measurementsExt.options.type = 1;
		measurementsExt.options.chain = false;

    const viewModes = this.viewer.createExtension(ViewModes);
		viewModes.setViewMode(ViewMode.DEFAULT, { edges: false });

    this.isInitialized = true
  }

  public async unloadAll() {
    await this.viewer.unloadAll()
    this.models.clear()
  }

  public async unLoadModel(modelId: string) {
    const model = this.models.get(modelId)

    if (!model) {
      return 
    }

    await this.viewer.unloadObject(model.resourceUrl)
    this.models.delete(modelId)
  }

  public async loadMainStudyModel(params: SpeckleViewerLoadObjectParams): Promise<SpeckleModel | null> {
    const model = await this.loadStudyModel(params)

    if (model) {
      this.mainModel = model
    }

    return model
  }

  public async loadStudyModel({
    studyModel,
    onLoadProgress
  }: SpeckleViewerLoadObjectParams): Promise<SpeckleModel | null> {
    const tModel = this.models.get(studyModel.id)

    if (tModel) {
      return tModel
    }

    const modelUrl = `${this.configs.speckleUrl}/projects/${studyModel.speckleProjectId}/models/${studyModel.id}`;

    const urls = await UrlHelper.getResourceUrls(modelUrl, this.token);

    if (urls.length <= 0) {
      throw new Error("Nenhuma url foi encontrada para esse modelo");
    }

    const resourceUrl = urls[0]!

    this.loadingUrl = resourceUrl

    const loader = new SpeckleLoader(
      this.viewer.getWorldTree(),
      resourceUrl,
      this.token,
    );

    loader.on(LoaderEvent.LoadProgress, (event) => {
      onLoadProgress(event.progress);
    });

    await this.viewer.loadObject(loader, true);

    const modelTree = this.viewer.getWorldTree().findSubtree(resourceUrl)

    if (!modelTree) {
      return null
    } 

    const model = new SpeckleModel({ modelUrl, viewer: this, studyModel, resourceUrl, treeNode: modelTree })

    this.models.set(model.modelId, model)

    return model
  }

  public async cancelLoading() {
    if (this.loadingUrl) {
      try {
        await this.viewer.cancelLoad(this.loadingUrl, true)
      } catch (err) {
        console.log(err)
      } finally {
        this.loadingUrl = null
      }
    }
  }

  public getLegends(type: string) {
    for (const m of Array.from(this.models.values())){
      const ls = m.legendService.getLegends(type)
    }
  }

  public getLegendsTypes() {
    const set = new Set<string>();

    for (const m of this.models.values()) {
      for (const t of m.legendService.getLegendTypes()) {
        set.add(t);
      }
    }

    return Array.from(set);
  }

  public isStudyModelLoaded(modelId: string) {
    return this.models.has(modelId)
  }

  public async dispose() {
    await this.unloadAll()
    this.viewer.dispose()
    this.isInitialized = false
  }
}