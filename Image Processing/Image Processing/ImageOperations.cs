using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using Priority_Queue;
namespace Image_Processing
{
    struct PixelRGB
    {
        public byte red;
        public byte green;
        public byte blue;
    }
    struct Edge
    {
        public int V1;      // Parent node
        public int V2;      // destination node
        public float Weight;  // weight of edge
    }
    class VertexParent:FastPriorityQueueNode
    {
        // 
        public VertexParent()
        { }
        public VertexParent(int vertex, int? parent)
        {
            V = vertex;
            P = parent;
        }
        public int V { get; set; }          // current vertix
        public int? P { get; set; }         // parent vertix

    }
    class ImageOperations
    {
        private static float CalcWeight(VertexParent V1, VertexParent V2)
        {
            byte red1, red2;
            byte green1, green2;
            byte blue1, blue2;
            red1 = (byte)(V1.V >> 16);
            red2 = (byte)(V2.V >> 16);
            green1 = (byte)(V1.V >> 8);
            green2 = (byte)(V2.V >> 8);
            blue1 = (byte)(V1.V);
            blue2 = (byte)(V2.V);
            return (float)Math.Sqrt((red2 - red1) * (red2 - red1) + (green2 - green1) * (green2 - green1) + (blue2 - blue1) * (blue2 - blue1));
        }
        private static float CalcWeight(int V1, int V2)
        {
            byte red1, red2;
            byte green1, green2;
            byte blue1, blue2;
            red1 = (byte)(V1 >> 16);
            red2 = (byte)(V2 >> 16);
            green1 = (byte)(V1 >> 8);
            green2 = (byte)(V2 >> 8);
            blue1 = (byte)(V1);
            blue2 = (byte)(V2);
            return (float)Math.Sqrt((red2 - red1) * (red2 - red1) + (green2 - green1) * (green2 - green1) + (blue2 - blue1) * (blue2 - blue1));
        }
        public static List<int> GetDistinctPixels(PixelRGB[,] ImageMatrix)
        {
            int width = ImageMatrix.GetLength(1);
            int height = ImageMatrix.GetLength(0);
            int r, g, b;
            HashSet<int> S = new HashSet<int>();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    r = ImageMatrix[y, x].red;
                    g = ImageMatrix[y, x].green;
                    b = ImageMatrix[y, x].blue;
                    S.Add((r << 16) + (g << 8) + b);
                }
            }
            List<int> L = S.ToList();
            return L;
        }
        public static PixelRGB[,] GetPixelsFromImage(string path)
        {
            Bitmap bm = new Bitmap(path);
            int width = bm.Width;
            int height = bm.Height;
            PixelRGB[,] ImageMatrix = new PixelRGB[height, width];
            BitmapData bmd = bm.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bm.PixelFormat);
            unsafe
            {
                bool format8 = false;
                bool format24 = false;
                bool format32 = false;
                byte* p = (byte*)bmd.Scan0;
                int nWidth = 0;
                if (bmd.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    format8 = true;
                    nWidth = width;
                }
                else if (bmd.PixelFormat == PixelFormat.Format24bppRgb)
                {
                    format24 = true;
                    nWidth = 3 * width;
                }
                else
                {
                    format32 = true;
                    nWidth = 4 * width;
                }
                int nOffset = bmd.Stride - nWidth;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (format8)
                        {
                            ImageMatrix[y, x].red = p[0];
                            ImageMatrix[y, x].green = p[0];
                            ImageMatrix[y, x].blue = p[0];
                            p++;
                        }
                        else
                        {
                            ImageMatrix[y, x].red = p[2];
                            ImageMatrix[y, x].green = p[1];
                            ImageMatrix[y, x].blue = p[0];
                            if (format24)
                                p += 3;
                            else if (format32)
                                p += 4;
                        }
                    }
                    p += nOffset;
                }
            }
            bm.UnlockBits(bmd);
            return ImageMatrix;
        }
        public static Bitmap ToBitmap(PixelRGB[,] ImageMatrix)
        {
            int width = ImageMatrix.GetLength(1);
            int height = ImageMatrix.GetLength(0);
            Bitmap target = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData bmd = target.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, target.PixelFormat);
            unsafe
            {
                byte* p = (byte*)bmd.Scan0;
                int nOffset = bmd.Stride - 3 * width;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        p[2] = ImageMatrix[y,x].red;
                        p[1] = ImageMatrix[y, x].green;
                        p[0] = ImageMatrix[y, x].blue;
                        p += 3;
                    }
                    p += nOffset;
                }
            }
            target.UnlockBits(bmd);
            return target;
        }
        public static List<Edge> PrimMST(List<int> D)
        {
            List<Edge> MSTList = new List<Edge>();
            // final list contains the MST 

            FastPriorityQueue < VertexParent > FP= new FastPriorityQueue<VertexParent>(D.Count);
            // Priority queue sorts the priority of edges' weights each time .

            VertexParent [] VP = new VertexParent[D.Count];
            // array holding each node and it's parent node.

            VP[0] = new VertexParent(D[0], null); // initializing the first node in the MST.

            FP.Enqueue(VP[0], 0);  // inserting the first node into the priority queue.

            float w;  
            for (int i = 1; i < D.Count; i++)
            {
                // intializing all the weights with OO value.
                VP[i]=new VertexParent(D[i],null);
                FP.Enqueue(VP[i],int.MaxValue);  
            }
            while (FP.Count != 0)
            {
                VertexParent Top = FP.Dequeue();   // get the minimum priority 
                if (Top.P != null)        // if it is not the starting node.
                {
                    Edge E;
                    E.V1 = Top.V;
                    E.V2 = (int)(Top.P);
                    E.Weight = (float)(Top.Priority);
                    MSTList.Add(E);     // add the minimum weight to the MST.
                }
                foreach (var unit in FP)     // modify the priority each time .
                {
                    w = CalcWeight(unit, Top);  // calculates the weight between the current node and the top node.
                    if (w < unit.Priority)
                    {
                        unit.P = Top.V;
                        FP.UpdatePriority(unit, w); 
                    }
                }
            }

            return MSTList;
        }
        public static List<HashSet<int>> Cluster(List<Edge> MST, int k) //cluster Samiir
        {
            float maxweight = 0;
            int maxindex = 0;
            for(int j=0;j<k-1;j++) //loop Sala7
            {
                maxweight = 0;
                maxindex = 0;
                for (int i = 0; i < MST.Count; i++)
                {
                    if (MST[i].Weight >= maxweight)
                    {
                        maxweight = MST[i].Weight;
                        maxindex = i;
                    }
                }
                Edge e;
                e.V1 = MST[maxindex].V1;
                e.V2 = MST[maxindex].V2;
                e.Weight = 0;
                MST[maxindex] = e; //fatyet Som3a
            }
            List<HashSet<int>> clusters = new List<HashSet<int>>();
            HashSet<int> cut = new HashSet<int>();
            HashSet<int> S = new HashSet<int>();
            for (int i = 0; i < MST.Count; i++)
            {
                
                if (MST[i].Weight != 0)
                {
                    S.Add(MST[i].V1);
                    S.Add(MST[i].V2);
                }
                else
                {
                    cut.Add(MST[i].V1);
                    cut.Add(MST[i].V2);
                    if (S.Count != 0)
                    {
                        HashSet<int> SCopy = new HashSet<int>();
                        foreach (var unit in S)
                        {
                            SCopy.Add(unit);
                        }
                        clusters.Add(SCopy);
                    }
                    S.Clear();
                }
            }
            if (S.Count != 0)
            {
                clusters.Add(S);
            }
            bool found=false;
            foreach (var vertex in cut)
            {
                found = false;
                foreach (var c in clusters)
                {

                    if (c.Contains(vertex))
                    {
                        found = true;
                        break;
                    }
                    
                }
                if (!found)
                {
                    HashSet<int> unique=new HashSet<int>();
                    unique.Add(vertex);
                    clusters.Add(unique);
                }
            }
            return clusters;
        }
        private static void DFS(int cur, ref HashSet<int> visited, ref Dictionary<int, List<int>> neighbours, ref HashSet<int> cluster)
        {
            visited.Add(cur);
            cluster.Add(cur);
            foreach (var neighbour in neighbours[cur])
            {
                if (!visited.Contains(neighbour))
                    DFS(neighbour, ref visited, ref neighbours, ref cluster);
            }
        }


        public static List<HashSet<int>> Cluster2(List<Edge> MST, int k)
        {
            List<HashSet<int>> clusters = new List<HashSet<int>>();
            HashSet<int> visited = new HashSet<int>();
            float maxweight = 0;
            int maxindex = 0;
            for (int j = 0; j < k - 1; j++)
            {
                maxweight = 0;
                maxindex = 0;
                for (int i = 0; i < MST.Count; i++)
                {
                    if (MST[i].Weight > maxweight)
                    {
                        maxweight = MST[i].Weight;
                        maxindex = i;
                    }
                }
                Edge e;
                e.V1 = MST[maxindex].V1;
                e.V2 = MST[maxindex].V2;
                e.Weight = 0;
                MST[maxindex] = e;
            }
            Dictionary<int, List<int>> neighbours = new Dictionary<int, List<int>>();
            foreach (var edge in MST)
            {
                if (edge.Weight != 0)
                {
                    if (neighbours.ContainsKey(edge.V1))
                    {
                        neighbours[edge.V1].Add(edge.V2);
                    }
                    else
                    {
                        List<int> l = new List<int>();
                        l.Add(edge.V2);
                        neighbours.Add(edge.V1, l);
                    }// v2
                    if (neighbours.ContainsKey(edge.V2))
                    {
                        neighbours[edge.V2].Add(edge.V1);
                    }
                    else
                    {
                        List<int> l = new List<int>();
                        l.Add(edge.V1);
                        neighbours.Add(edge.V2, l);
                    }
                }
                else
                {
                    if (!neighbours.ContainsKey(edge.V1))
                    {
                        List<int> l = new List<int>();
                        neighbours.Add(edge.V1, l);
                    }
                    if (!neighbours.ContainsKey(edge.V2))
                    {
                        List<int> l = new List<int>();
                        neighbours.Add(edge.V2, l);
                    }
                }
            }
            int q = 0;
            foreach (var vertex in neighbours)
            {
                if (!visited.Contains(vertex.Key))
                {
                    HashSet<int> h = new HashSet<int>();
                    DFS(vertex.Key, ref visited, ref neighbours, ref h);
                    clusters.Add(h);
                    q++;
                }
            }
            int[]averages = new int[k];
            int r;
            int g;
            int b;
            
           /* bool movement = true;
            for(int mov=0;mov<3;mov++)
            {
                //movement = false;
                q = 0;
                foreach (var set in clusters)
                {
                    r = 0;
                    g = 0;
                    b = 0;
                    foreach (var unit in set)
                    {
                        r = (r + (byte)(unit >> 16));
                        g = (g + (byte)(unit >> 8));
                        b = (b + (byte)(unit));
                    }
                    r = (r / set.Count);
                    g = (g / set.Count);
                    b = (b / set.Count);
                    averages[q] = (r << 16) + (g << 8) + (b);
                    q++;
                }
                int index = 0;
                float minweight = float.MaxValue;
                float _weight;
                for (int i = 0; i < k; i++)
                {
                    for (int l=0;l<clusters[i].Count;l++)
                    {
                        minweight = float.MaxValue;
                        int unit = clusters[i].ElementAt(l);
                        for (int j = 0; j < k; j++)
                        { 
                            _weight = CalcWeight(unit, averages[j]);
                            if (_weight < minweight)
                            {
                                minweight = _weight;
                                index = j;
                            }
                        }
                        if (index != i)
                        {
                           // movement = true;
                            clusters[i].Remove(unit);
                            clusters[index].Add(unit);
                        }
                    }
                }
            }*/
            return clusters;
        }
        public static Dictionary<int, int> Palette(ref List<HashSet<int>> Cluster)
        {
            Dictionary<int, int> resultPalette = new Dictionary<int, int>();
            int r;
            int g;
            int b;
            int value=0;
            foreach (var set in Cluster)
            {
                r = 0;
                g = 0;
                b = 0;
                foreach (var unit in set)
                {
                    r = (r + (byte)(unit >> 16));
                    g = (g + (byte)(unit >> 8));
                    b = (b + (byte)(unit));
                }
                r = (r / set.Count);
                g = (g / set.Count);
                b = (b / set.Count);
                value = (r << 16) + (g << 8) + (b);
                foreach (var unit in set)
                {
                    resultPalette.Add(unit, value);
                }
            }
            return resultPalette;
        }
        public static void Quantize(ref PixelRGB[,] ImageMatrix, ref Dictionary<int, int> Palette)
        {
            int width = ImageMatrix.GetLength(1);
            int height = ImageMatrix.GetLength(0);
            int r, g, b;
            int key = 0;
            int value=0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    r = ImageMatrix[y, x].red;
                    g = ImageMatrix[y, x].green;
                    b = ImageMatrix[y, x].blue;
                    key = (r << 16) + (g << 8) + b;
                    value = Palette[key];
                    ImageMatrix[y, x].red = (byte)(value >> 16);
                    ImageMatrix[y, x].green = (byte)(value >> 8);
                    ImageMatrix[y, x].blue = (byte)(value);
                }
            }
        }
    }

}
