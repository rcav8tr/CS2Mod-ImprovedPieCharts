using Colossal.IO.AssetDatabase;
using Game;
using Game.Modding;
using Game.SceneFlow;

namespace ImprovedPieCharts
{
    public class Mod : IMod
    {
        // The one and only global settings for this mod.
        public static ModSettings ModSettings = null;

        public void OnLoad(UpdateSystem updateSystem)
        {
            LogUtil.Info($"{nameof(Mod)}.{nameof(OnLoad)}");

            // Set up mod settings.
            ModSettings = new ModSettings(this);
            ModSettings.RegisterInOptionsUI();
            AssetDatabase.global.LoadSettings(nameof(ImprovedPieCharts), ModSettings, new ModSettings(this));
            ModSettings.ApplyAndSave();

            // Set up all locales.
            foreach (string languageCode in Translation.instance.LanguageCodes)
            {
                GameManager.instance.localizationManager.AddSource(languageCode, new Locale(languageCode));
            }

            // Create and activate this mod's systems.
            updateSystem.UpdateAt<UISystem>(SystemUpdatePhase.UIUpdate);

            #if DEBUG
            // Create UI constant files.
            // Uncomment this only when the UI constant files need to be created or recreated.
            // Then run the mod once in the game to create the constant files.
            // Then comment this again.  The UI constants are now available to use.
            //CreateUIConstantFiles.Create();
            #endif
        }

        public void OnDispose()
        {
            LogUtil.Info($"{nameof(Mod)}.{nameof(OnDispose)}");

            // Unregister mod settings.
            ModSettings?.ApplyAndSave();
            ModSettings?.UnregisterInOptionsUI();
            ModSettings = null;
        }
    }
}
