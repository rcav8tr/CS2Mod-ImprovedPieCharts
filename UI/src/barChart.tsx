import { CSSProperties, useRef, useEffect } from "react";
import { VanillaComponentResolver } from "vanillaComponentResolver";

// Define bar chart component with only the props needed.
export const BarChart = 
(
    {
        data,
        colors,
        height,
        animate = !0
    }: any
) =>
{
    // Create a ref for the canvas.
    const canvasRef: any = useRef(null);

    // Get spring animation value.
    const springAnimationValue = VanillaComponentResolver.instance.SpringAnimation_qK({ tension: 30, friction: 10 }, 1, (animate ? 0 : 1), !0);

    // Compute total.
    const total: number = data.reduce(((e: number, t: number) => e + t), 0)

    // Draw the bar chart on the canvas.
    useEffect(() =>
    {
        // Get the canvas.
        const canvas: HTMLCanvasElement | null = canvasRef.current;
        if (canvas === null) return;

        // Get canvas size in pixels.
        // Canvas width seems to always be 300 pixels at the time this is executed (i.e. the default width for a canvas).
        // The canvas width seems to get resized automatically based on both the info view content and the game's Text Scaling.
        // The graphics drawn on the canvas at 300 pixels seem to get automatically stretched when the canvas is resized.
        // Therefore, the canvas resizing is not a problem.
        const canvasWidth: number = canvas.width;
        const canvasHeight: number = canvas.height;

        // Get canvas 2D rendering context.
        const context2d: CanvasRenderingContext2D | null = canvas.getContext("2d");
        if (context2d == null) return;

        // Clear the entire canvas.
        context2d.clearRect(0, 0, canvasWidth, canvasHeight);

        // Save context state.
        context2d.save();

        // Draw bar chart only if total is not zero.
        if (total > 0)
        {
            // Do each data point.
            let start: number = 0;
            for (let i = 0; i < data.length; i++)
            {
                // Do this data point only if greater than zero.
                if (data[i] > 0)
                {
                    // Do this data point only if width in pixels is greater than zero.
                    const width: number = Math.trunc(canvasWidth * data[i] / total * springAnimationValue);
                    if (width > 0)
                    {
                        // Draw the bar for this data point starting from the end of the previous bar.
                        context2d.fillStyle = colors[i];
                        context2d.fillRect(start, 0, width, canvasHeight);

                        // Compute the starting location for the next data point.
                        start += width;
                    }
                }
            }
        }
        else
        {
            // When total is zero, fill entire canvas with default background, which is similar to what pie chart does.
            // Default background color is copied from parameters of PieChart (i.e. UZ in index.js).
            context2d.fillStyle = "rgba(255, 255, 255, 0.2)";
            context2d.fillRect(0, 0, canvasWidth, canvasHeight);
        }

        // Restore context state.
        context2d.restore();
    }, [data, colors, height, springAnimationValue]);

    // Set bar chart size.
    const barChartStyle: Partial<CSSProperties> =
    {
        width: "100%",
        height: height + "px"
    }

    // Make canvas same size as parent div.
    const canvasStyle: Partial<CSSProperties> =
    {
        width:  "100%",
        height: "100%"
    }

    // Bar chart is simply a div to specify size and a canvas upon which to draw.
    // All the work of drawing on the canvas is performed above in the useEffect.
    return (
        <div style={barChartStyle}>
            <canvas style={canvasStyle} ref={canvasRef}></canvas>
        </div>
    );
}
