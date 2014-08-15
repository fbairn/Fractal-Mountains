using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MountainCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random rand=new Random();
        List<LineData> mountain = new List<LineData>();

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            //Draw our mountain
            foreach (LineData item in mountain)
            {
                e.Graphics.DrawLine(Pens.Black, item.Start.x, 300-item.Start.height, item.End.x, 300-item.End.height);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateMountains(double.Parse(txtAdjust.Text), double.Parse(txtGamma.Text));
        }

        /// <summary>
        /// Creates fractal mountain range
        /// </summary>
        /// <param name="adjustRate">Height Adjustment. The higher the number the higher the peaks.</param>
        /// <param name="gamma">Changes the adjustRate. This will make it so as our lines get smaller so will the height changes.</param>
        public void CreateMountains(double adjustRate, double gamma)
        {
            if (gamma >= 1)
            {
                throw new Exception("Gamma must be less than 1");
            }
            mountain.Clear();
            //Add two starting points.
            mountain.Add(new LineData { Start = new point { x = 100, height = 0 }, End = new point { x = 400, height = 100 } });
            mountain.Add(new LineData { Start = new point { x = 400, height = 100 }, End = new point { x = 800, height = 0 } });

            //Splits the lines 7 times.
            //If this number is increased your mountain range will have more detail.
            //In my testing I found that 7 creates a nice looking mountain.
            //Less than 7 it comes out looking like lines. More and you won't see the detail anyway.
            for (int i = 0; i < 7; i++) 
            {
                List<LineData> newList = new List<LineData>();
                foreach (LineData item in mountain)
                {
                    //Splits a line into two new lines.
                    //Create two new lines
                    LineData oldLine = item;
                    LineData line1 = new LineData { Start = oldLine.Start, End = oldLine.Middle };
                    LineData line2 = new LineData { Start = oldLine.Middle, End = oldLine.End };
                    //Adjust the height a random amount * adjust height.
                    float heightAdjust = float.Parse(((rand.NextDouble() * adjustRate * 2) - adjustRate).ToString());
                    //Set the height for our new lines
                    line1.End.height += heightAdjust;
                    line2.Start.height += heightAdjust;
                    //Add the new lines to the collection
                    newList.Add(line1);
                    newList.Add(line2);
                }
                adjustRate = adjustRate * gamma;    //Changes the adjustRate. This will make it so as our lines get smaller so will the height changes.
                mountain = newList;
            }
            this.Invalidate();  //Causes the canvas to re-draw
        }
    }
}
