// This entire file is only for creating UI constant files when in DEBUG.
#if DEBUG

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImprovedPieCharts
{
    /// <summary>
    /// Create the two files that hold constants used by both C# and UI.
    /// The two files are created from data to ensure the constants are the same in both places.
    /// </summary>
    public static class CreateUIConstantFiles
    {
        // Shortcut for UI constants dictionary.
        // Dictionary key is the constant name.
        // Dictionary value is the constant value.
        private class UIConstants : Dictionary<string, string> { }

        // Define UI bindings.
        private const string UIBindingsComment = "Constants for UI bindings.";
        private const string UIBindingsClassName = "UIBindings";
        private static readonly UIConstants _uiBindings = new UIConstants()
        {
            { "GroupName",          ModAssemblyInfo.Name    },
            { "ChartType",          "ChartType"             },
            { "PieChartSize",       "PieChartSize"          },
            { "PieChartHoleSize",   "PieChartHoleSize"      },
            { "BarChartHeight",     "BarChartHeight"        },
            { "ChartAnimation",     "ChartAnimation"        }
        };


        // Define pie chart types.
        private const string ChartTypesComment = "Chart types.";
        private const string ChartTypesClassName = "ChartTypes";
        private const int PieChart = (int)ModSettings.ChartTypes.PieChart;
        private const int BarChart = (int)ModSettings.ChartTypes.BarChart;
        private const int NoChart  = (int)ModSettings.ChartTypes.NoChart;


        // Define constants for setting default values.
        private const string SettingDefaultsComment = "Defaults for mod settings.";
        private const string SettingDefaultsClassName = "ModSettingsDefaults";
        private const ModSettings.ChartTypes ChartType = ModSettings.ChartTypes.PieChart;
        private const int PieChartSize = 125;
        private const int PieChartHoleSize = 60;
        private const int BarChartHeight = 25;
        private const bool ChartAnimation = true;


        // Shortcut for translation keys list.
        // Entry is used for constant name and constant value suffix.
        private class TranslationKeys : List<string> { }

        // Define constants for translation keys.
        private const string TranslationKeyComment = "Translation keys.";
        private const string TranslationKeyClassName = "UITranslationKey";
        private static readonly TranslationKeys _translationKeyModInfo = new TranslationKeys()
        {
            "Title",
            "Description",
        };

        private static readonly UIConstants _translationKeySettings = new UIConstants()
        {
            { "SettingTitle",                       Mod.ModSettings.GetSettingsLocaleID()                                               },


            { "SettingGroupChartSettings",          Mod.ModSettings.GetOptionGroupLocaleID(ModSettings.GroupChartSettings)              },
            { "SettingChartSettingsDescription",    Mod.ModSettings.GetOptionLabelLocaleID(nameof(ModSettings.ChartSettingsDescription))},

            { "SettingChartTypeLabel",              Mod.ModSettings.GetOptionLabelLocaleID(nameof(ModSettings.ChartType))               },
            { "SettingChartTypeDesc",               Mod.ModSettings.GetOptionDescLocaleID (nameof(ModSettings.ChartType))               },

            { "SettingChartTypePieChart",           Mod.ModSettings.GetEnumValueLocaleID(ModSettings.ChartTypes.PieChart)               },
            { "SettingChartTypeBarChart",           Mod.ModSettings.GetEnumValueLocaleID(ModSettings.ChartTypes.BarChart)               },
            { "SettingChartTypeNoChart",            Mod.ModSettings.GetEnumValueLocaleID(ModSettings.ChartTypes.NoChart)                },

            { "SettingPieChartSizeLabel",           Mod.ModSettings.GetOptionLabelLocaleID(nameof(ModSettings.PieChartSize))            },
            { "SettingPieChartSizeDesc",            Mod.ModSettings.GetOptionDescLocaleID (nameof(ModSettings.PieChartSize))            },

            { "SettingPieChartHoleSizeLabel",       Mod.ModSettings.GetOptionLabelLocaleID(nameof(ModSettings.PieChartHoleSize))        },
            { "SettingPieChartHoleSizeDesc",        Mod.ModSettings.GetOptionDescLocaleID (nameof(ModSettings.PieChartHoleSize))        },

            { "SettingBarChartHeightLabel",         Mod.ModSettings.GetOptionLabelLocaleID(nameof(ModSettings.BarChartHeight))          },
            { "SettingBarChartHeightDesc",          Mod.ModSettings.GetOptionDescLocaleID (nameof(ModSettings.BarChartHeight))          },

            { "SettingChartAnimationLabel",         Mod.ModSettings.GetOptionLabelLocaleID(nameof(ModSettings.ChartAnimation))          },
            { "SettingChartAnimationDesc",          Mod.ModSettings.GetOptionDescLocaleID (nameof(ModSettings.ChartAnimation))          },

            { "SettingResetSettingsLabel",          Mod.ModSettings.GetOptionLabelLocaleID(nameof(ModSettings.ResetSettings))           },
            { "SettingResetSettingsDesc",           Mod.ModSettings.GetOptionDescLocaleID (nameof(ModSettings.ResetSettings))           },


            { "SettingGroupAbout",                  Mod.ModSettings.GetOptionGroupLocaleID(ModSettings.GroupAbout)                      },
            
            { "SettingModVersionLabel",             Mod.ModSettings.GetOptionLabelLocaleID(nameof(ModSettings.ModVersion))              },
            { "SettingModVersionDesc",              Mod.ModSettings.GetOptionDescLocaleID (nameof(ModSettings.ModVersion))              },
        };


        /// <summary>
        /// Create the two UI constant files.
        /// One file for C# and one file for UI.
        /// </summary>
        public static void Create()
        {
            // Write the C# file to the same folder as this source code file.
            string contentsCS = ConstructFileContents(true);
            string sourceCodePath = GetSourceCodePath();
            File.WriteAllText(Path.Combine(sourceCodePath, "UIConstants.cs"), contentsCS);

            // Write the UI file to the UI/src folder.
            // Assumes this source code file is in a folder, so first need to go up one directory.
            string contentsUI = ConstructFileContents(false);
            string uiPath = Path.Combine(Directory.GetParent(sourceCodePath).FullName, "UI", "src");
            File.WriteAllText(Path.Combine(uiPath, "uiConstants.ts"), contentsUI);
        }

        /// <summary>
        /// Construct the contents of the C# (CS) or UI file.
        /// </summary>
        private static string ConstructFileContents(bool typeCS)
        {
            StringBuilder sb = new StringBuilder();

            // Include do not modify instructions.
            sb.AppendLine($"// DO NOT MODIFY THIS FILE.");
            sb.AppendLine($"// This entire file was automatically generated by class {nameof(CreateUIConstantFiles)}.");
            sb.AppendLine($"// Make any needed changes in class {nameof(CreateUIConstantFiles)}.");
            sb.AppendLine();

            // Include namespace for C# file.
            if (typeCS)
            {
                sb.AppendLine($"namespace {ModAssemblyInfo.Name}");
                sb.AppendLine("{");
            }

            // Include UI bindings.
            sb.Append(StartClass(typeCS, UIBindingsComment, UIBindingsClassName));
            sb.Append(GetConstantsContent(typeCS, _uiBindings));
            sb.Append(EndClass(typeCS));

            // For UI, include chart types.
            if (!typeCS)
            {
                sb.AppendLine();
                sb.Append(StartClass(typeCS, ChartTypesComment, ChartTypesClassName));
                sb.AppendLine($"    public static {nameof(PieChart)}: number = {PieChart};");
                sb.AppendLine($"    public static {nameof(BarChart)}: number = {BarChart};");
                sb.AppendLine($"    public static {nameof(NoChart )}: number = {NoChart };");
                sb.Append(EndClass(typeCS));
            }

            // Include class for setting defaults.
            sb.AppendLine();
            sb.Append(StartClass(typeCS, SettingDefaultsComment, SettingDefaultsClassName));
            if (typeCS)
            {
                sb.AppendLine($"        public const ModSettings.ChartTypes {nameof(ChartType)} = ModSettings.ChartTypes.{ChartType};");
                sb.AppendLine($"        public const int {nameof(PieChartSize)} = {PieChartSize};");
                sb.AppendLine($"        public const int {nameof(PieChartHoleSize)} = {PieChartHoleSize};");
                sb.AppendLine($"        public const int {nameof(BarChartHeight)} = {BarChartHeight};");
                sb.AppendLine($"        public const bool {nameof(ChartAnimation)} = {ChartAnimation.ToString().ToLower()};");
            }
            else
            {
                sb.AppendLine($"    public static {nameof(ChartType)}: number = {(int)ChartType};");
                sb.AppendLine($"    public static {nameof(PieChartSize)}: number = {PieChartSize};");
                sb.AppendLine($"    public static {nameof(PieChartHoleSize)}: number = {PieChartHoleSize};");
                sb.AppendLine($"    public static {nameof(BarChartHeight)}: number = {BarChartHeight};");
                sb.AppendLine($"    public static {nameof(ChartAnimation)}: boolean = {ChartAnimation.ToString().ToLower()};");
            }
            sb.Append(EndClass(typeCS));

            // Include translation keys only for C#.
            // The mod's UI displays no text, so no translations are needed in the UI.
            if (typeCS)
            {
                sb.AppendLine();
                sb.Append(StartClass(typeCS, TranslationKeyComment, TranslationKeyClassName));
                sb.Append(GetConstantsContent(typeCS, _translationKeyModInfo));
                sb.AppendLine();
                sb.Append(GetConstantsContent(typeCS, _translationKeySettings));
                sb.Append(EndClass(typeCS));
            }

            // End namespace for C# file.
            if (typeCS)
            {
                sb.AppendLine("}");
            }

            // Return the contents.
            return sb.ToString();
        }

        /// <summary>
        /// Get the content for the start of a class.
        /// </summary>
        private static string StartClass(bool typeCS, string classComment, string className)
        {
            StringBuilder sb = new StringBuilder();
            if (typeCS)
            {
                sb.AppendLine("    // " + classComment);
                sb.AppendLine("    public class " + className);
                sb.AppendLine("    {");
            }
            else
            {
                sb.AppendLine("// " + classComment);
                sb.AppendLine("export class " + className);
                sb.AppendLine("{");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Get the content for the end of a class.
        /// </summary>
        private static string EndClass(bool typeCS)
        {
            StringBuilder sb = new StringBuilder();
            if (typeCS)
            {
                sb.AppendLine("    }");
            }
            else
            {
                sb.AppendLine("}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Get the content for a comment and constants.
        /// </summary>
        private static string GetConstantsContent(bool typeCS, UIConstants constants)
        {
            string indentation = (typeCS ? "        " : "    ");
            StringBuilder sb = new StringBuilder();
            foreach (var key in constants.Keys)
            {
                if (typeCS)
                {
                    sb.AppendLine($"{indentation}public const string {key} = \"{constants[key]}\";");
                }
                else
                {
                    sb.AppendLine($"{indentation}public static {key}: string = \"{constants[key]}\";");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Get the translation key content for a comment and constants.
        /// </summary>
        private static string GetConstantsContent(bool typeCS, TranslationKeys keys)
        {
            string indentation = (typeCS ? "        " : "    ");
            StringBuilder sb = new StringBuilder();
            foreach (var key in keys)
            {
                if (typeCS)
                {
                    sb.AppendLine($"{indentation}public const string {key} = \"{ModAssemblyInfo.Name}.{key}\";");
                }
                else
                {
                    sb.AppendLine($"{indentation}public static {key}: string = \"{ModAssemblyInfo.Name}.{key}\";");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Get the full path of this C# source code file.
        /// </summary>
        private static string GetSourceCodePath([System.Runtime.CompilerServices.CallerFilePath] string sourceFile = "")
        {
            return Path.GetDirectoryName(sourceFile);
        }
    }
}

#endif
