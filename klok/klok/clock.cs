using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace klok
{
    internal class clock
    {
        /// <summary>
        /// draws each hand from center of the clock to a set point as the end
        /// center point X, Center point Y, Angle for calculation, Length of the hand, Color of the hand, Width of the hand
        /// </summary>
        /// <algo>
        /// 1. convert the input angle from degrees to radians for Math functions
        /// 2. use Cosine (X) and Sine (Y) to find the endpoint of the hand
        /// 3. create a pen with the specified color and width
        /// 4. draw the line from the center (cx, cy) to the calculated endpoint (endX, endY)
        /// </algo>
        public static void DrawHand(Graphics g, int cx, int cy, double angleDeg, int length, Color color, int width)
        {
            // 1. convert degrees to radians
            double angleRad = angleDeg * Math.PI / 180;

            // 2. calculate endpoint Cosine and Sine
            float endX = (float)(cx + length * Math.Cos(angleRad));
            float endY = (float)(cy + length * Math.Sin(angleRad));

            // 3. create the pen
            using (Pen pen = new Pen(color, width))
            {
                // set styling for the hand (optional)
                pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

                // 4. draw the hand
                g.DrawLine(pen, cx, cy, endX, endY);
            }
        }
        public static void drawnumbers(Graphics g, int centerX, int centerY, int radius)
        {
            Font font = new Font("Arial", 12, FontStyle.Bold);
            for (int i = 1; i <= 12; i++)
            {
                // create a smaller circle within the bigger circle for each number
                double angle = (i * 30 - 90) * Math.PI / 180;
                int numberRadius = radius - 20;
                // calculate correct points for each number
                float posX = (float)(centerX + numberRadius * Math.Cos(angle));
                float posY = (float)(centerY + numberRadius * Math.Sin(angle));
                // measure the textsize
                SizeF textSize = g.MeasureString(i.ToString(), font);

                // calculate the center of each number, draw them centered with their correct points
                g.DrawString(i.ToString(), font, Brushes.Black, posX - (textSize.Width / 2), posY - (textSize.Height / 2));
            }
        }
    }
}
