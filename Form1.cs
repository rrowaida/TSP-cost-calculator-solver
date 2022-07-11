using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;//use to generate chart 
using System.IO;//use for reading input files

namespace TSP_cost_calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static class GlobalData
        {
            public static string contents = "";
        };

        public static class ListTest
        {
            public static List<int> list_ind = new List<int>();
            public static List<double> list_coorx = new List<double>();
            public static List<double> list_coory = new List<double>();
        };

        public bool finding(int[] a, int e, int l)
        {
            int f=0;
            for(int i=0; i<l; i++)
            {
                if(a[i]==e)
                    f=1;
            }

            if(f==1)
                return true;
            else
                return false;
        }

        public void init_gen(int[] ar, int l)
        {
            int k;
            bool v;

            Random rand = new Random();
            

            ar[0] = rand.Next(0, l);
            for (int i = 1; i < l; i++)
            {
                k = rand.Next(0, l);

                v = finding(ar, k, i);

                if (v == true)
                    i = i - 1;
                else
                    ar[i] = k;
            }
        }

        public double Eval(double[,] dist, int[] t, int l)
        {
            double c = 0;

            for (int i = 0; i < l - 1; i++)
            {
                int k = t[i];
                c = c + dist[t[i],t[i+1]];
            }
            c=c+dist[t[l-1],t[0]];
            return c;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //chart1.Update();
            int size = -1;
            string filename = "";
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                filename = openFileDialog1.FileName;
                try
                {
                    string text = File.ReadAllText(filename);
                    size = text.Length;
                }
                catch (IOException)
                {
                    MessageBox.Show("unreadable file");
                }

                GlobalData.contents = File.ReadAllText(@filename);
            }

            int len = GlobalData.contents.Length;

            string[] words = GlobalData.contents.Split(' ');
            int i = 0;
            double[] f_data;
            f_data = new double[len];

            int v = 0;

            string check = "NODE_COORD_SECTION";
            richTextBox1.Text = "";
            GlobalData.contents = "";

            foreach (string word in words)
            {
                if (word.Contains(check))
                    v = 1;
                if (v == 1)
                    GlobalData.contents = GlobalData.contents + " " + word;
            }

            System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(GlobalData.contents, @"(\d+\.?\d*|\.\d+)");

            string[] MatchList = new string[matches.Count];

            // add each match
            int c = 0;
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                MatchList[c] = match.ToString();
                c++;
            }

            len = MatchList.Length;

            int value;
            for (i = 1; i < len; i++)
            {
                if (i % 3 == 1)
                {
                    int.TryParse(MatchList[i], out value);
                    ListTest.list_ind.Add(value);

                }

                if (i % 3 == 2)
                {
                    ListTest.list_coorx.Add(Convert.ToDouble(MatchList[i]));

                }

                if (i % 3 == 0)
                {
                    ListTest.list_coory.Add(Convert.ToDouble(MatchList[i]));
                }
            }

            chart1.Show();
            len = ListTest.list_coorx.Count;

            for (i = 0; i < len; i++)
            {
                chart1.Series["nodes"].Points.AddXY(ListTest.list_coorx[i], ListTest.list_coory[i]);
            }
            chart1.Series["nodes"].ChartType = SeriesChartType.Point;
            chart1.Series["nodes"].Color = Color.Red;
 
        }

        private void graphViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int len = GlobalData.contents.Length;

            string[] words = GlobalData.contents.Split(' ');
            int i = 0;
            double[] f_data;
            f_data = new double[len];

            int v = 0;

            string check = "NODE_COORD_SECTION";
            richTextBox1.Text = "";
            GlobalData.contents = "";

            foreach (string word in words)
            {
                if (word.Contains(check))
                    v = 1;
                if (v == 1)
                    GlobalData.contents = GlobalData.contents + " " + word;
            }
            
            System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(GlobalData.contents, @"(\d+\.?\d*|\.\d+)");

            string[] MatchList = new string[matches.Count];

            // add each match
            int c = 0;
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                MatchList[c] = match.ToString();
                c++;
            }

            len = MatchList.Length;
            int value;
            for (i = 1; i < len; i++)
            {
                if (i % 3 == 1)
                {
                    int.TryParse(MatchList[i], out value);
                    //GlobalData.ind[j] = value;
                    ListTest.list_ind.Add(value);
                    //j++;
                }

                if (i % 3 == 2)
                {
                    //GlobalData.xcoor[k] = Convert.ToDouble(MatchList[i]);
                    ListTest.list_coorx.Add(Convert.ToDouble(MatchList[i]));
                    //k++;
                }

                if (i % 3 == 0)
                {
                    //GlobalData.ycoor[l] = Convert.ToDouble(MatchList[i]);
                    ListTest.list_coory.Add(Convert.ToDouble(MatchList[i]));
                    //l++;
                }
            }

            chart1.Show();
            len = ListTest.list_coorx.Count;
            richTextBox1.Text = "";
            //richTextBox1.Text = richTextBox1.Text + len;
            for (i = 0; i < len; i++)
            {
                //richTextBox1.Text = richTextBox1.Text + ListTest.list_ind[i] + " " + ListTest.list_coorx[i] + " " + ListTest.list_coory[i] + "\n";
                //richTextBox1.Text = richTextBox1.Text + GlobalData.ind[i] + " " + GlobalData.xcoor[i] + " " + GlobalData.ycoor[i] + "\n";
            }
            
            for (i = 0; i < len; i++)
            {
                chart1.Series["nodes"].Points.AddXY
                                (ListTest.list_coorx[i], ListTest.list_coory[i]);
                chart1.Series["edges"].Points.AddXY
                                (ListTest.list_coorx[i], ListTest.list_coory[i]);
            }


            chart1.Series["nodes"].ChartType = SeriesChartType.Point;
            chart1.Series["nodes"].Color = Color.Red;

            chart1.Series["edges"].ChartType = SeriesChartType.FastLine;
            chart1.Series["edges"].Color = Color.Blue;

            len =len / 2;
            double [,] Distance = new double[len, len];            

            for (i = 0; i < len; i++)
            {
                double x = ListTest.list_coorx[i];
                double y = ListTest.list_coorx[i];


                for (int j = 0; j < len; j++)
                {
                    Distance[i,j] = Math.Sqrt(Math.Pow(x - ListTest.list_coorx[j], 2) + Math.Pow(y - ListTest.list_coory[j], 2));
                }
            }

            int[] tour = new int[len];

            init_gen(tour, len);


            //foreach(int el in tour)richTextBox1.Text = richTextBox1.Text + len +  "\t" + el + "\n";

            double cost = 0;

            for (i = 0; i < len - 1; i++)
            {
                richTextBox1.Text = richTextBox1.Text + tour[i] + " to " + tour[i+1] + " -- " + Distance[tour[i], tour[i+1]] + "\n";
            }

            richTextBox1.Text = richTextBox1.Text + tour[len-1] + " to " + tour[0] + " -- " + Distance[tour[len-1], tour[0]] + "\n";
            cost = Eval(Distance, tour, len);

            textBox1.Text = textBox1.Text + cost;

        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a Travelling Salesman Problem(TSP) solver which is created using Artificial Immune System(AIS)", "Software information");
        }

        private void showCostToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}
