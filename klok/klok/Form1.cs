using System;
using System.Drawing;
using System.Windows.Forms;

namespace klok
{
    public partial class Form1 : Form
    {
        private bool drawCircle = false;
        public Form1()
        {
            InitializeComponent();
        }

        // redraw the form each time the timer ticks (calls OnPaint)
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        // set drawcircle to true so it can start painting
        private void button1_Click(object sender, EventArgs e)
        {
            drawCircle = true;
            timer1.Start();
        }
        /// <summary>
        /// Paint set values of an analogue clock
        /// </summary>
        /// <algo>
        /// 1. calculate all dimensions
        /// 2. draw the circle
        /// 3. calculate angle for each number
        /// 4. create a smaller radius within the circle
        /// 5. calculate correct points for the numbers
        /// 6. draw the numbers 1 - 12 on their correct points
        /// -- hands --
        /// 1. calculate and draw the hands for each second
        /// 2. calculate and draw the minute hands
        /// 3. calculate and draw the hour hands
        /// </algo>
        protected override void OnPaint(PaintEventArgs e)
        {
            clock klok = new clock();
            base.OnPaint(e);

            if (drawCircle)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // 1. calculate dimensions
                int margin = 60;
                int diameter = Math.Min(ClientSize.Width, ClientSize.Height) - margin;
                int radius = diameter / 2;
                int centerX = ClientSize.Width / 2;
                int centerY = ClientSize.Height / 2;

                // 2. draw the circle
                using (Pen pen = new Pen(Color.Black, 3))
                {
                    g.DrawEllipse(pen, centerX - radius, centerY - radius, diameter, diameter);
                }
                // 3. numbers
                clock.drawnumbers(g, centerX, centerY, radius);
                // ---- hands ---- 

                DateTime now = DateTime.Now;

                // 1. seconds hand
                clock.DrawHand(g, centerX, centerY, (now.Second * 6 - 90), radius - 40, Color.Red, 2);

                // 2. minute hand
                // including seconds makes the movement smoother (optional)
                double minAngle = (now.Minute * 6 + now.Second * 0.1 - 90);
                clock.DrawHand(g, centerX, centerY, minAngle, radius - 75, Color.Blue, 4);

                // 3. hour hand
                // including minutes makes the hour hand move gradually between numbers (optional)
                double hourAngle = (now.Hour % 12 * 30 + now.Minute * 0.5 - 90);
                clock.DrawHand(g, centerX, centerY, hourAngle, radius - 150, Color.Green, 6);

                // center pin (the small circle in the middle
                g.FillEllipse(Brushes.Yellow, centerX - 5, centerY - 5, 10, 10);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
