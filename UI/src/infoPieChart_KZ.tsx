import { bindValue, useValue } from "cs2/api";
import { VanillaComponentResolver } from "vanillaComponentResolver";
import { UIBindings, ChartTypes, ModSettingsDefaults } from "uiConstants";
import styles from "barChart.module.scss";
import { BarChart } from "barChart";

// Bindings for chart settings.
const bindingChartType        = bindValue<number >(UIBindings.GroupName, UIBindings.ChartType,        ModSettingsDefaults.ChartType);
const bindingPieChartSize     = bindValue<number >(UIBindings.GroupName, UIBindings.PieChartSize,     ModSettingsDefaults.PieChartSize);
const bindingPieChartHoleSize = bindValue<number >(UIBindings.GroupName, UIBindings.PieChartHoleSize, ModSettingsDefaults.PieChartHoleSize);
const bindingBarChartHeight   = bindValue<number >(UIBindings.GroupName, UIBindings.BarChartHeight,   ModSettingsDefaults.BarChartHeight);
const bindingChartAnimation   = bindValue<boolean>(UIBindings.GroupName, UIBindings.ChartAnimation,   ModSettingsDefaults.ChartAnimation);

// Define a new info pie chart function with the same parameters as the original info pie chart function.
export const InfoPieChart_KZ =
(
    {
        title,
        colors,
        labels,
        data,
        ignoreZero = !1,
        usePercentageValue = !1,
        customLegend,
        className
    }: any
) =>
{
    // Chart setting values from bindings.
    const valueChartType:        number  = useValue(bindingChartType);
    const valuePieChartSize:     number  = useValue(bindingPieChartSize);
    const valuePieChartHoleSize: number = useValue(bindingPieChartHoleSize);
    const valueBarChartHeight:   number  = useValue(bindingBarChartHeight);
    const valueChartAnimation:   boolean = useValue(bindingChartAnimation);

    // Function to join classes.
    const joinClasses = (...classes: any) =>
    {
        return classes.join(' ')
    }

    // Create array of objects where each object has a value and a background color.
    // This is WZ in index.js, but WZ is not exported, so it is recreated here.
    const combineDataAndColors_WZ = (chartData: any, colors: any) => chartData.values.map((function (dataValue: number, index: number)
    {
        return {
            value: dataValue,
            background: colors[index]
        }
    }));

    // Compute value multiplier based on whether or not using percentage value.
    const valueMultiplier = usePercentageValue && data.total > 0 ? 100 / data.total : 1;
    
    // Return the new info pie chart.
    // This mostly duplicates InfoPieChart (i.e. KZ from index.js, see below).
    // Where InfoPieChart displays only a pie chart (UZ), this displays has one of three charts depending on chart type.
    //      Pie chart is the game's pie chart (UZ) where size, innerRadius, and animate props are replaced with chart settings.
    //      Bar chart is this mod's bar chart with height and animate props from chart settings.
    //      No chart is simply a small space to separate the section title from the legend.
    // InfoPieChart includes the chart legend, which is duplicated here from the base game and is always included.
    return (
        <div className={joinClasses(VanillaComponentResolver.instance.InfoPieChartClasses_HZ.infoPiechart, className)}>
            {title && (<VanillaComponentResolver.instance.InfoRowTooltipRow_jI uppercase={!0} left={title} />)}
            <div className={VanillaComponentResolver.instance.InfoPieChartClasses_HZ.content}>
                {
                    valueChartType === ChartTypes.PieChart &&
                    (
                        <div className={VanillaComponentResolver.instance.InfoPieChartClasses_HZ.pie}>
                            <VanillaComponentResolver.instance.PieChart_UZ
                                data={combineDataAndColors_WZ(data, colors)}
                                size={valuePieChartSize}
                                innerRadius={valuePieChartHoleSize / 100}
                                animate={valueChartAnimation}
                            />
                        </div>
                    )
                }
                {
                    valueChartType === ChartTypes.BarChart &&
                    (
                        <div className={styles.bar}>
                            <BarChart
                                data={data.values}
                                colors={colors}
                                height={valueBarChartHeight}
                                animate={valueChartAnimation}
                            />
                        </div>
                    )
                }
                {
                    valueChartType === ChartTypes.NoChart &&
                    (
                        <div className={VanillaComponentResolver.instance.InfoViewSpaceClasses_iZ.smallSpace} />
                    )
                }
                <div className={VanillaComponentResolver.instance.InfoPieChartClasses_HZ.legends}>
                    {
                        customLegend ||
                        (
                            data.values.filter(((dataValue: number) => !ignoreZero || dataValue > 0)).map((dataValue: number, index: number) =>
                            {
                                return (
                                    <div className={VanillaComponentResolver.instance.InfoPieChartClasses_HZ.legend}>
                                        <VanillaComponentResolver.instance.ColorLegend_WM
                                            color={colors[index]}
                                            label={labels[index]}
                                        />
                                        <VanillaComponentResolver.instance.LocalizedNumber_Gc
                                            value={dataValue * valueMultiplier}
                                            unit={usePercentageValue ? "percentageSingleFraction" : "integer"}
                                        />
                                    </div>
                                );
                            })
                        )
                    }
                </div>
            </div>
        </div>
    );

    // Here is the formatted InfoPieChart KZ from index.js as of game version 1.1.7f1:
    //
    //  var KZ =
    //  (
    //      {
    //      title: e,
    //      colors: t,
    //      labels: n,
    //      data: s,
    //      ignoreZero: a=!1,
    //      usePercentageValue: i=!1,
    //      customLegend: o,
    //      className: r
    //      }
    //  ) =>
    //  {
    //      const l = i && s.total > 0 ? 100 / s.total : 1;
    //      return (0, z.jsxs)("div",
    //      {
    //          className: Zu()(HZ.infoPiechart, r),
    //          children:
    //          [
    //              e && (0, z.jsx)(jI,
    //              {
    //                  uppercase: !0,
    //                  left: e
    //              }),
    //              (0, z.jsxs)("div",
    //              {
    //                  className: HZ.content,
    //                  children:
    //                  [
    //                      (0, z.jsx)("div",
    //                      {
    //                          className: HZ.pie,
    //                          children: (0, z.jsx)(UZ,
    //                          {
    //                              data: WZ(s, t),
    //                              innerRadius: .6,
    //                              size: 125
    //                          })
    //                      }),
    //                      (0, z.jsx)("div",
    //                      {
    //                          className: HZ.legends,
    //                          children: o || s.values.filter((e => !a || e > 0)).map(((e,s) => (0,z.jsxs)("div",
    //                          {
    //                              className: HZ.legend,
    //                              children:
    //                              [
    //                                  (0, z.jsx)(WM,
    //                                  {
    //                                      color: t[s],
    //                                      label: n[s]
    //                                  }),
    //                                  (0, z.jsx)(Gc,
    //                                  {
    //                                      value: e * l,
    //                                      unit: i ? Pc.PercentageSingleFraction : Pc.Integer
    //                                  })
    //                              ]
    //                          }, s)))
    //                      })
    //                  ]
    //              })
    //          ]
    //      })
    //  };

};
