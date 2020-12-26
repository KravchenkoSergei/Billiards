using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Billiards
{
    public partial class Form1 : Form
    {
        List<Ball> balls = new List<Ball>();
        List<PictureBox> pbballs;
        List<Point> points;
        List<PictureBox> holes;
        public Form1()
        {
            InitializeComponent();
        }

        public void pbHole3_Click(EventHandler e)
        {

        }

        private void pbBall0_Click(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double dt = timer1.Interval / 1000.0;
            foreach(Ball b in balls)
            {
                b.Move(dt);
                for (int i = 0; i < (points.Count - 1); i++)
                {
                    if (b.IsCollision(points[i], points[i + 1]))
                        b.Collide(points[i], points[i + 1]);
                }
            }
            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i+1; j < balls.Count; j++)
                {
                
                    if (balls[i] != balls[j])
                    {
                        if(Ball.IsCollision(balls[i], balls[j]))
                        {
                            Ball.Collide(balls[i], balls[j]);
                        }
                    }
                }

                for (int j = 0; j < holes.Count; j++)
                {
                    if (balls[i].IsCollision(new Point(holes[j].Location.X, holes[j].Location.Y + holes[j].Height / 2), new Point(holes[j].Location.X + holes[j].Width, holes[j].Location.Y + holes[j].Height / 2)))
                    {
                        if (i != 0)
                        {
                            balls.RemoveAt(i);
                            pbballs[i].Visible = false;
                            pbballs.RemoveAt(i);
                        }
                    }
                }
                if (balls == new List<Ball> { })
                {
                    MessageBox.Show("You win!");
                }
            }



  
            for (int i = 0; i < balls.Count; i++)
            {
                pbballs[i].Location = new Point((int)balls[i].R.X - pbballs[i].Width / 2, (int)balls[i].R.Y - pbballs[i].Height / 2);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pbballs = new List<PictureBox>()
            {
                pbBall0, pbBall1, pbBall2, pbBall3, pbBall4, pbBall5, pbBall6, pbBall7, pbBall8, pbBall9, pbBall10, pbBall11, pbBall11, pbBall12, pbBall13, pbBall14, pbBall15
            };
            for (int i = 0; i < 16; i++)
            {
                    balls.Add(new Ball() { M = 5, D = pbballs[i].Width, R = new Vector(pbballs[i].Left, pbballs[i].Top), V = new Vector(0, 0) });
            }
            points = new List<Point>()
            {
                pbWall1.Location, new Point(pbWall1.Location.X + pbWall1.Width, pbWall1.Location.Y + pbWall1.Width), new Point(pbWall1.Location.X + pbWall1.Width, pbWall1.Location.Y - pbWall1.Width), new Point(pbWall1.Location.X, pbWall1.Height),
                pbWall2.Location, new Point(pbWall2.Location.X + pbWall2.Height, pbWall2.Location.Y + pbWall2.Height), new Point(pbWall2.Location.X + pbWall2.Width, pbWall2.Location.Y + pbWall2.Height), new Point(pbWall2.Location.X + pbWall2.Width, pbWall2.Location.Y + pbWall2.Location.Y),
                pbWall3.Location, new Point(pbWall3.Location.X, pbWall3.Location.Y + pbWall3.Height), new Point(pbWall3.Location.X + pbWall3.Width - pbWall3.Height, pbWall3.Location.Y + pbWall3.Height), new Point(pbWall3.Location.X + pbWall3.Width, pbWall2.Location.Y),
                new Point(pbWall4.Location.X + pbWall4.Width, pbWall4.Location.Y), new Point(pbWall4.Location.X, pbWall4.Location.Y + pbWall4.Width), new Point(pbWall4.Location.X, pbWall4.Location.Y + pbWall4.Height - pbWall4.Width), new Point(pbWall4.Location.X + pbWall4.Width, pbWall4.Location.Y + pbWall4.Height),
                new Point(pbWall5.Location.X + pbWall5.Width, pbWall5.Location.Y + pbWall5.Height), new Point(pbWall5.Location.X + pbWall5.Width - pbWall5.Height, pbWall5.Location.Y), pbWall5.Location, new Point(pbWall5.Location.X, pbWall5.Location.Y + pbWall5.Height),
                new Point(pbWall6.Location.X + pbWall6.Width, pbWall6.Location.Y + pbWall6.Height), new Point(pbWall6.Location.X + pbWall6.Width, pbWall6.Location.Y), new Point(pbWall6.Location.X + pbWall6.Height, pbWall6.Location.Y), new Point(pbWall6.Location.X, pbWall6.Location.Y + pbWall6.Height)
            };
            holes = new List<PictureBox>()
            {
                pbHole1, pbHole2, pbHole3, pbHole4, pbHole5, pbHole6
            };
        }

        private void pbBall0_MouseClick(object sender, MouseEventArgs e)
        {

            balls[0].V = -(new Vector(new Point(pbBall0.Location.X + pbBall0.Width / 2, pbBall0.Location.Y + pbBall0.Height / 2), new Point(e.Location.X + pbBall0.Location.X, e.Location.Y + pbBall0.Location.Y))) * 30;
            timer1.Start();
        }
    }
}
