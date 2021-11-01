using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace Numerical24
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //MRK();
            DrawGraph(zedGraphControl1);
        }

        private double Function(double x, double y)
        {
            //y'+2y=x^2
            //y' =f(x, y) = x^2 - 2y
            // y(0) = 1;

            return x * x - 2 * y;
        }

        


        private double[,] MRK()
        {
            // [a, b], h
            // y(0) = 1;
            double a = 0;
            double b = 1;
            double h = 0.1;
            double num = (b - a) / h + 1;
            int size = (int) num;
            double[, ] points = new double[size, 2];
            points[0, 1] = 1;

            for(int i=0; i<size; i++)
            {
                points[i, 0] = a + i * h;
            }
            for(int i=1; i<size; i++)
            {
                double k1 = Function(points[i-1, 0], points[i-1, 1]);
                double k2 = Function((points[i - 1, 0]+h/2), (points[i - 1, 1]+ h*k1/2));
                double k3 = Function((points[i - 1, 0] + h / 2), (points[i - 1, 1] + h * k2 / 2));
                double k4 = Function((points[i - 1, 0]+h), (points[i - 1, 1]+h*k3));
                points[i, 1] = points[i-1, 1]+ h /6*(k1+2*k2+2*k3+k4);
            }

            for(int i=0; i<size; i++)
            {
                richTextBox1.Text += points[i, 0].ToString() + "        " + points[i, 1].ToString();
                richTextBox1.Text += Environment.NewLine;
            }
            return points;

        }


        private void DrawGraph(ZedGraphControl zedGraph)
        {


                GraphPane pane = zedGraph.GraphPane;

                pane.CurveList.Clear();

                // Создадим список точек для кривой f1(x)
                PointPairList f1_list = new PointPairList();

                double[,] points = new double[11, 2];
                points = MRK();
                int i = 0;
                for (double x = 0; x <= 1; x += 0.1)
                {
                    f1_list.Add(points[i, 0], points[i, 1]);
                    i++;
                }


                LineItem f1_curve = pane.AddCurve("Function ", f1_list, Color.Teal, SymbolType.Circle);

                pane.XAxis.Min = -0.2;
                pane.XAxis.Max = 1.2;


                pane.YAxis.Min = -0.2;
                pane.YAxis.Max = 1.2;

                zedGraph.AxisChange();

                // Обновляем график
                zedGraph.Invalidate();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
