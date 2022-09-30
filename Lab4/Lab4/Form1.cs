using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab4
{
    public partial class Form1 : Form
    {
        List<int> list = new List<int> { };
        List<int> colorList=new List<int> {};
        int kol = 0, height = 0, ymax, xmax;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "ПОСТРОИТЬ";
            button2.Text = "ЗАПИСАТЬ";
            button3.Text = "СЧИТАТЬ";
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawLine(new Pen(Color.Black, 2.0f), 100, 350, 1100, 350);
            g.DrawLine(new Pen(Color.Black, 2.0f), 100, 100, 100, 600);
            xmax = (int)1000 / (2 * kol - 1);
            float wight1 = 1000 / (float)(2 * kol - 1);
            float height1 = 250 / (float)ymax;
            SolidBrush brush = new SolidBrush(Color.Black);
            SolidBrush color1 = new SolidBrush(Color.Black);
            Font drawFont=new Font("Time new Roman",11);
            SizeF stringSize = new SizeF();
            float y = 0, x = 100;
            for (int i = 0; i < kol; i++)
            {
                string measureString = list[i].ToString();
                stringSize = g.MeasureString(measureString, drawFont);
                y = list[i] * height1;
                color1.Color = Color.FromArgb(colorList[i*3], colorList[i * 3+1], colorList[i * 3+2]);
                float m = x + wight1 / 2 - stringSize.Width / 2;
                if (y < 0)
                {
                    g.FillRectangle(color1, (int)x, 350, (float)wight1, (int)(-1 * y));
                    g.DrawString(list[i].ToString(), drawFont, brush,m,350-y );
                }
                else
                {
                    g.FillRectangle(color1, (int)x, 350 - y, (float)wight1, (int)y);
                    g.DrawString(list[i].ToString(), drawFont, brush, m, 333 - y);
                }
                x = wight1 * 2 + x;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            kol = rand.Next(4, 25);
            list.Clear();
            colorList.Clear();
            for (int i = 0; i < kol; i++)
            {
                height = rand.Next(-100, 100);
                list.Add(height);
                colorList.Add(rand.Next(0, 225));
                colorList.Add(rand.Next(0, 225));
                colorList.Add(rand.Next(0, 225));
            }
            ymax = (list.Max());
            if (ymax < Math.Abs(list.Min()))
                ymax = Math.Abs(list.Min());
            Invalidate();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            StreamWriter write = new StreamWriter("2.txt");
            string s1;
            int j = 0;
            for (int i=0;i<list.Count();i++)
            {
                s1 = list[i].ToString() + " " + colorList[j].ToString() + " " + colorList[j + 1].ToString() + " " + colorList[j + 2].ToString();
                j = j + 3;
                write.WriteLine(s1);
            }
            write.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string s1;
            list.Clear();
            colorList.Clear();
            StreamReader read = new StreamReader("1.txt");
            while ((s1=read.ReadLine())!=null)
            {
                kol = 0;
                foreach (string s in s1.Split(' '))
                {
                    if (kol == 0)
                        list.Add(Convert.ToInt32(s));
                    else
                        colorList.Add(Convert.ToInt32(s));
                    kol++;
                }
            }
            read.Close();
            if (list.Count() != 0)
            {
                ymax = (list.Max());
                if (ymax < Math.Abs(list.Min()))
                    ymax = Math.Abs(list.Min());
            }
            kol = list.Count();
            Invalidate();
        }

    }
}

