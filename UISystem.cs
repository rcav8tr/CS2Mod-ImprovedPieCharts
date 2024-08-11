using Colossal.Serialization.Entities;
using Colossal.UI.Binding;
using Game;
using Game.UI;

namespace ImprovedPieCharts
{
    public partial class UISystem : UISystemBase
    {
        // C# to UI bindings for chart settings.
        private static ValueBinding<int >_bindingChartType;
        private static ValueBinding<int >_bindingPieChartSize;
        private static ValueBinding<int >_bindingPieChartHoleSize;
        private static ValueBinding<int >_bindingBarChartHeight;
        private static ValueBinding<bool>_bindingChartAnimation;

        /// <summary>
        /// Do one-time initialization of the system.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();

            LogUtil.Info($"{nameof(UISystem)}.{nameof(OnCreate)}");

            // Add C# to UI bindings for chart settings.
            AddBinding(_bindingChartType        = new ValueBinding<int >(UIBindings.GroupName, UIBindings.ChartType,        Mod.ModSettings.ChartTypeAsInt));
            AddBinding(_bindingPieChartSize     = new ValueBinding<int >(UIBindings.GroupName, UIBindings.PieChartSize,     Mod.ModSettings.PieChartSize));
            AddBinding(_bindingPieChartHoleSize = new ValueBinding<int >(UIBindings.GroupName, UIBindings.PieChartHoleSize, Mod.ModSettings.PieChartHoleSize));
            AddBinding(_bindingBarChartHeight   = new ValueBinding<int >(UIBindings.GroupName, UIBindings.BarChartHeight,   Mod.ModSettings.BarChartHeight));
            AddBinding(_bindingChartAnimation   = new ValueBinding<bool>(UIBindings.GroupName, UIBindings.ChartAnimation,   Mod.ModSettings.ChartAnimation));
        }

        /// <summary>
        /// Called by the game when a GameMode is about to be loaded.
        /// </summary>
        protected override void OnGamePreload(Purpose purpose, GameMode mode)
        {
            base.OnGamePreload(purpose, mode);

            // Before loading a game, send all chart settings to UI.
            if (mode == GameMode.Game)
            {
                SendAllChartSettingsToUI();
            }
        }

        /// <summary>
        /// Send all chart settings to UI.
        /// </summary>
        public static void SendAllChartSettingsToUI()
        {
            if (_bindingChartType != null && Mod.ModSettings != null)
            {
                // UI accepts chart type as a number.
                _bindingChartType       .Update(Mod.ModSettings.ChartTypeAsInt  );
                _bindingPieChartSize    .Update(Mod.ModSettings.PieChartSize    );
                _bindingPieChartHoleSize.Update(Mod.ModSettings.PieChartHoleSize);
                _bindingBarChartHeight  .Update(Mod.ModSettings.BarChartHeight  );
                _bindingChartAnimation  .Update(Mod.ModSettings.ChartAnimation  );
            }
        }
    }
}
