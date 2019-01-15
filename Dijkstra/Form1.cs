using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dijkstra
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int[,] graph = null;
        int n;

        int minDistance(int[] dist, bool[] sptSet)
        {
            int min = int.MaxValue, min_index = -1;
            for (int v = 0; v < n; v++)
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }
            return min_index;
        }

        void printSolution(int[] dist, int n)
        {
            String s = "Vertex - Distance from Source" + Environment.NewLine;
            for (int i = 0; i < n; i++)
                s += (i + "  -  " + dist[i] + Environment.NewLine);

            txtLog.Text += s;
        }

        void dijkstra(int src)
        {
            int[] dist = new int[n]; 
            bool[] sptSet = new bool[n];
            for (int i = 0; i < n; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }
            dist[src] = 0;
            for (int count = 0; count < n - 1; count++)
            {
                int u = minDistance(dist, sptSet);
                sptSet[u] = true;
                for (int v = 0; v < n; v++)
                    if ( !sptSet[v] 
                        && graph[u, v] != 0 
                        && dist[u] != int.MaxValue 
                        && dist[u] + graph[u, v] < dist[v])
                        dist[v] = dist[u] + graph[u, v];
            }
            printSolution(dist, n);
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if (graph == null)
                {
                    n = Convert.ToInt32(txtNum.Text);
                    graph = new int[n, n];
                    txtLog.Text = "n = " + n + Environment.NewLine;
                    txtLog.Text += "Please Enter i j size: " + Environment.NewLine;
                    txtNum.Clear();
                }
                else
                {
                    string[] s = txtNum.Text.Trim().Split(' ');
                    txtNum.Clear();
                    int i = Convert.ToInt32(s[0]);
                    int j = Convert.ToInt32(s[1]);
                    int size = Convert.ToInt32(s[2]);
                    graph[i, j] = size;
                    graph[j, i] = size;
                    txtLog.Text += i + " -> " + j + " = " + size + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDijkstra_Click(object sender, EventArgs e)
        {
            int src;
            try
            {
                src = Convert.ToInt32(txtNum.Text);
            }
            catch (Exception ex)
            {
                src = 0;
            }
            txtLog.Text = "source = " + src + Environment.NewLine;
            dijkstra(src);
        }

    }
}
