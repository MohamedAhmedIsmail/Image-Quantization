using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Processing
{
    class PixelGraph
    {
        public struct Edge
        {
            public PixelRGB P1;
            public PixelRGB P2;
            public short Weight;
        }
        public List<Edge> Edges { get; set; }
        public PixelGraph()
        {
            Edges = new List<Edge>();
        }
        public PixelGraph(List<int> L)
        {
            Edges = new List<Edge>();
            for (int i = 0; i < L.Count; i++)
            {
                for (int j = i+1; j < L.Count; j++)
                {
                    Edge E;
                    E.P1.red = (byte)(L[i] >> 16);
                    E.P1.green = (byte)(L[i] >> 8);
                    E.P1.blue = (byte)(L[i]);
                    E.P2.red = (byte)(L[j] >> 16);
                    E.P2.green = (byte)(L[j] >> 8);
                    E.P2.blue = (byte)(L[j]);
                    E.Weight = (short)Math.Sqrt((E.P1.red - E.P2.red) * (E.P1.red - E.P2.red) + (E.P1.green - E.P2.green) * (E.P1.green - E.P2.green) + (E.P1.blue - E.P2.blue) * (E.P1.blue - E.P2.blue));
                    Edges.Add(E);
                }
            }
        }
    }
}
