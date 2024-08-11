import { getModule } from "cs2/modding";
import { Theme } from "cs2/bindings";

// Define props used by code modules.
type InfoRowTooltipRowProps =
{
    icon?: any,
    left?: any,
    right?: any,
    uppercase?: boolean,
    subRow?: boolean
}

type PieChartPieChartProps =
{
    data: any,
    size?: number,
    startAngle?: number,
    defaultBackground?: string,
    total?: number,
    className?: string,
    labelOffset?: number,
    innerRadius?: number,
    labelAngleThreshold?: number,
    animate?: boolean
}

type ColorLegendProps =
{
    color?: string,
    label?: string,
    className?: any,
    children?: any
}

type LocalizedNumberProps =
{
    value: number,
    unit?: any,
    signed?: boolean
}

export class VanillaComponentResolver
{
    // Define instances.
    private static _instance: VanillaComponentResolver = new VanillaComponentResolver();
    public static get instance(): VanillaComponentResolver { return this._instance }

    // Define cached code modules from index.js.
    // The trailing letters are the minified variable names.
    private cachedInfoRowTooltipRow_jI: any;
    private cachedPieChart_UZ: any;
    private cachedColorLegend_WM: any;
    private cachedLocalizedNumber_Gc: any;
    private cachedSpringAnimation_qK: any;

    // Define cached SCSS modules from index.js.
    private cachedInfoPieChartClasses_HZ: any;
    private cachedInfoViewSpaceClasses_iZ: any;
    
    // Initialize cached modules.
    public Initialize()
    {
        // Initialize code modules.
        this.cachedInfoRowTooltipRow_jI = getModule("game-ui/game/components/selected-info-panel/shared-components/info-row/info-row.tsx", "TooltipRow");
        this.cachedPieChart_UZ          = getModule("game-ui/common/charts/pie-chart/pie-chart.tsx", "PieChart");
        this.cachedColorLegend_WM       = getModule("game-ui/common/charts/legends/color-legend.tsx", "ColorLegend");
        this.cachedLocalizedNumber_Gc   = getModule("game-ui/common/localization/localized-number.tsx", "LocalizedNumber");
        this.cachedSpringAnimation_qK   = getModule("game-ui/common/animations/spring-hooks.tsx", "useSpringAnimation");

        // Initialize SCSS modules.
        this.cachedInfoPieChartClasses_HZ  = getModule("game-ui/common/charts/pie-chart/info-piechart.module.scss", "classes");
        this.cachedInfoViewSpaceClasses_iZ = getModule("game-ui/game/components/infoviews/active-infoview-panel/components/infoview-panel-space.module.scss", "classes");
    }

    // Provide access to cached code modules.
    public get InfoRowTooltipRow_jI(): (props: InfoRowTooltipRowProps) => JSX.Element { return this.cachedInfoRowTooltipRow_jI; }
    public get PieChart_UZ():          (props: PieChartPieChartProps) => JSX.Element { return this.cachedPieChart_UZ; }
    public get ColorLegend_WM():       (props: ColorLegendProps) => JSX.Element { return this.cachedColorLegend_WM; }
    public get LocalizedNumber_Gc():   (props: LocalizedNumberProps) => JSX.Element { return this.cachedLocalizedNumber_Gc; }
    public get SpringAnimation_qK()    { return this.cachedSpringAnimation_qK; }

    // Provide access to cached SCSS modules.
    public get InfoPieChartClasses_HZ():   Theme | any { return this.cachedInfoPieChartClasses_HZ; }
    public get InfoViewSpaceClasses_iZ():  Theme | any { return this.cachedInfoViewSpaceClasses_iZ; }
}