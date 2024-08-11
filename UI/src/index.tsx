import { ModRegistrar } from "cs2/modding";
import { VanillaComponentResolver } from "vanillaComponentResolver";
import { InfoPieChart_KZ } from "infoPieChart_KZ";

const register: ModRegistrar = (moduleRegistry) =>
{
    // Initialize vanilla component resolver.
    VanillaComponentResolver.instance.Initialize();

    // Replace InfoPieChart with the info pie chart from this mod.
    // Because InfoPieChart is overridden (i.e. not appended or extended),
    // this mod WILL conflict with any other mod that also overrides InfoPieChart.
    moduleRegistry.override("game-ui/common/charts/pie-chart/info-piechart.tsx", "InfoPieChart", InfoPieChart_KZ);

    // Registration is complete.
    console.log("ImprovedPieCharts registration complete.");
}

export default register;