using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainCreator
{
    class LineData
    {
        public point Start { get; set; }  //Starting Point
        public point End { get; set; }    //Ending Point

        public point Middle
        {
            get
            {
                point midPoint=new point();
                midPoint.x = (Start.x + End.x) / 2;
                midPoint.height = (Start.height + End.height) / 2;
                return midPoint;
            }
        }
    }

    class point
    {
        public float x { get; set; }
        public float height { get; set; }
    }
}
