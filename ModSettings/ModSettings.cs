using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.UI;

namespace ImprovedPieCharts
{
    [FileLocation(nameof(ImprovedPieCharts))]
    [SettingsUIGroupOrder(GroupChartSettings, GroupAbout)]
    [SettingsUIShowGroupName(GroupChartSettings, GroupAbout)]
    public class ModSettings : ModSetting
    {
        // Group constants.
        public const string GroupChartSettings = "ChartSettings";
        public const string GroupAbout = "About";

        public ModSettings(IMod mod) : base(mod)
        {
            LogUtil.Info($"{nameof(ModSettings)}.{nameof(ModSettings)}");

            SetDefaults();
        }
        
        /// <summary>
        /// Set a default value for every setting that has a value that can change.
        /// </summary>
        public override void SetDefaults()
        {
            // It is important to set a default for every value.
            ChartType           = ModSettingsDefaults.ChartType;
            PieChartSize        = ModSettingsDefaults.PieChartSize;
            PieChartHoleSize    = ModSettingsDefaults.PieChartHoleSize;
            BarChartHeight      = ModSettingsDefaults.BarChartHeight;
            ChartAnimation      = ModSettingsDefaults.ChartAnimation;

            // Save settings.
            ApplyAndSave();
        }

        // Chart types.
        public enum ChartTypes
        {
            PieChart,
            BarChart,
            NoChart
        }

        [SettingsUISection(GroupChartSettings)]
        [SettingsUIMultilineText]
        public string ChartSettingsDescription {  get { return "TBD"; } }
        
        // Chart type.
        [SettingsUISection(GroupChartSettings)]
        public ChartTypes ChartType { get; set; }
        [SettingsUIHidden]
        public int ChartTypeAsInt { get { return (int)ChartType; } }

        // Pie chart size.
        [SettingsUISection(GroupChartSettings)]
        [SettingsUISlider(min = 50, max = 200, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        [SettingsUIDisableByCondition(typeof(ModSettings), nameof(DisablePieChartSize))]
        public int PieChartSize { get; set; }
        private bool DisablePieChartSize() { return ChartType != ChartTypes.PieChart; }

        // Pie chart hole size.
        [SettingsUISection(GroupChartSettings)]
        [SettingsUISlider(min = 20, max = 90, step = 1, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUIDisableByCondition(typeof(ModSettings), nameof(DisablePieChartHoleSize))]
        public int PieChartHoleSize { get; set; }
        private bool DisablePieChartHoleSize() { return ChartType != ChartTypes.PieChart; }

        // Bar chart height.
        [SettingsUISection(GroupChartSettings)]
        [SettingsUISlider(min = 10, max = 50, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        [SettingsUIDisableByCondition(typeof(ModSettings), nameof(DisableBarChartHeight))]
        public int BarChartHeight { get; set; }
        private bool DisableBarChartHeight() { return ChartType != ChartTypes.BarChart; }

        // Chart animation.
        [SettingsUISection(GroupChartSettings)]
        [SettingsUIDisableByCondition(typeof(ModSettings), nameof(DisableChartAnimation))]
        public bool ChartAnimation { get; set; }
        private bool DisableChartAnimation() { return ChartType == ChartTypes.NoChart; }

        // Button to reset settings.
        [SettingsUISection(GroupChartSettings)]
        [SettingsUIButton()]
        public bool ResetSettings { set { SetDefaults(); } }

        // Display mod version in settings.
        [SettingsUISection(GroupAbout)]
        public string ModVersion { get { return ModAssemblyInfo.Version; } }


        /// <summary>
        /// Handle when any setting changes.
        /// </summary>
        public override void Apply()
        {
            base.Apply();

            // Send all chart settings to UI.
            UISystem.SendAllChartSettingsToUI();
        }
    }
}
