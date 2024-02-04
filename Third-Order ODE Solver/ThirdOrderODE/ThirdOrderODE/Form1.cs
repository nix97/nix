using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;
using org.mariuszgromada.math.mxparser;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace ThirdOrderODE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void butExample_Click(object sender, EventArgs e)
        {
            if (rbExample1.Checked) {
                tbFunction.Text = "exp(t)+y-x+z"; //Func Y'''
                tb_a.Text = "0"; //value a =
                tb_b.Text = "2"; //value b =
                tb_yt0.Text = "1"; //value y(t0) = 
                tb_yt01.Text = "1"; //value y'(t0) =
                tb_yt02.Text = "0"; //value y''(t0) =;
                tb_M.Text = "50";
                cbExact.Checked = true;
                tbExactY.Text = "1/2*t*exp(t)+cos(t)+1/2*sin(t)";
                tbExactY2.Text = "1/2*exp(t)+1/2*t*exp(t)-sin(t)+1/2*cos(t)";
                tbExactY3.Text = "exp(t)+1/2*t*exp(t)-cos(t)-1/2*sin(t)";
            }
            else //Example 2
            {
                tbFunction.Text = "(t^2*z-3*t*x+4*y+5*t^2*ln(t)+9*t^3)/t^3"; //Func Y''' ; ln(t) is natural logarithm, if logarithm base 10(use log10(t) on mXparser) 
                tb_a.Text = "1"; //value a =
                tb_b.Text = "2"; //value b =
                tb_yt0.Text = "0"; //value y(t0) = 
                tb_yt01.Text = "1"; //value y'(t0) =
                tb_yt02.Text = "3"; //value y''(t0) =;
                tb_M.Text = "100"; //h=(2-1)/10=0.1
                cbExact.Checked = false;
                tbExactY.Text = ""; //have no exact solution
                tbExactY2.Text = "";
                tbExactY3.Text = "";
                
            }
        }

        private void butRun_Click(object sender, EventArgs e)
        {
            int M, k, sm;
            double a, b, step, Ymax, Ymax1, Ymax2, Ymax3, Chk;
            double h, yt0, yt1, yt2;
            double[] t3 = new double[10001];
            double[] Y = new double[10001]; //Y[k]
            double[] Y2 = new double[10001]; //Y'[k]
            double[] Y3 = new double[10001]; //Y''[k]
            double[] YExact = new double[10001]; //Y[k] Exact
            double[] Y2Exact = new double[10001]; //Y'[k] Exact
            double[] Y3Exact = new double[10001]; //Y''[k] Exact
            double f1, f2, f3, f4, g1, g2, g3, g4, h1, h2, h3, h4; //slope
            double[] RelErrorY = new double[10001]; //Relative Error Y
            double[] RelErrorY2 = new double[10001]; //Relative Error Y'
            double[] RelErrorY3 = new double[10001]; //Relative Error Y''

            double[] Tkm = new double[10001]; //tk exact smooth graph
            double[] YkmExact = new double[10001];//Yk exact smooth graph
            double[] Y2kmExact = new double[10001];//Y' exact smooth graph
            double[] Y3kmExact = new double[10001];//Y'' exact smooth graph
            string Func, FuncY, t2, x2, y2, z2, result, title, line; //Y'''=H(t,y,x,z) where x=y' and z=y''


            try
            {
                a = double.Parse(tb_a.Text);
                b = double.Parse(tb_b.Text);
                M = int.Parse(tb_M.Text);

                yt0 = double.Parse(tb_yt0.Text); //y(t0)
                yt1 = double.Parse(tb_yt01.Text); //y'(t0)
                yt2 = double.Parse(tb_yt02.Text); //y''(t0)

                if (M > 10000)
                {
                    MessageBox.Show("Maximum of M is 10000 !!!");
                    return;
                };

                if (a > b)
                {
                    MessageBox.Show("Value of a must be lower than b !!!");
                    return;
                };

                h = (b - a) / M;  //data tipe of a,b is double,if use int (value h is zero)

                Y[0] = yt0;
                Y2[0] = yt1;
                Y3[0] = yt2;
                t3[0] = a;
                t3[M] = b;


                //t,y,x,z need close {} or block for separate another block that use same t,y,x,z
                {
                    //Check input Y'''=... (if wrong typing)
                    Argument t = new Argument("t", 2);
                    Argument y = new Argument("y", 2);
                    Argument x = new Argument("x", 2);
                    Argument z = new Argument("z", 2);

                    Func = tbFunction.Text;

                    Expression expres = new Expression(Func,t,y,x,z);
                    Chk = (expres.calculate());

                    if (double.IsNaN(Chk))
                    {
                        MessageBox.Show("Something wrong with the input Y ' ' ' !!!");
                        return;
                    }
                }

                for (k = 0; k < M; k++)
                {

                    t3[k] = a + k * h;

                    Func = tbFunction.Text;

                    f1 = Y2[k];
                    g1 = Y3[k];

                    //h1
                    t2 = t3[k].ToString();
                    y2 = Y[k].ToString();
                    x2 = Y2[k].ToString();
                    z2 = Y3[k].ToString();

                    Argument t_arg = new Argument("t", t2);
                    Argument y_arg = new Argument("y", y2);
                    Argument x_arg = new Argument("x", x2);
                    Argument z_arg = new Argument("z", z2);

                    Expression express = new Expression(Func, t_arg, y_arg, x_arg, z_arg);
                    h1 = express.calculate();

                    //f2
                    f2 = Y2[k] + 0.5 * h * g1;

                    //g2
                    g2 = Y3[k] + 0.5 * h * h1;

                    //h2
                    t2 = (t3[k] + 0.5 * h).ToString();
                    y2 = (Y[k] + 0.5 * h * f1).ToString();
                    x2 = (Y2[k] + 0.5 * h * g1).ToString();
                    z2 = (Y3[k] + 0.5 * h * h1).ToString();

                    Argument t_arg2 = new Argument("t", t2);
                    Argument y_arg2 = new Argument("y", y2);
                    Argument x_arg2 = new Argument("x", x2);
                    Argument z_arg2 = new Argument("z", z2);

                    Expression express2 = new Expression(Func, t_arg2, y_arg2, x_arg2, z_arg2);
                    h2 = express2.calculate();

                    //f3
                    f3 = Y2[k] + 0.5 * h * g2;

                    //g3
                    g3 = Y3[k] + 0.5 * h * h2;

                    //h3
                    t2 = (t3[k] + 0.5 * h).ToString();
                    y2 = (Y[k] + 0.5 * h * f2).ToString();
                    x2 = (Y2[k] + 0.5 * h * g2).ToString();
                    z2 = (Y3[k] + 0.5 * h * h2).ToString();

                    Argument t_arg3 = new Argument("t", t2);
                    Argument y_arg3 = new Argument("y", y2);
                    Argument x_arg3 = new Argument("x", x2);
                    Argument z_arg3 = new Argument("z", z2);

                    Expression express3 = new Expression(Func, t_arg3, y_arg3, x_arg3, z_arg3);
                    h3 = express3.calculate();

                    //f4
                    f4 = Y2[k] + h * g3;

                    //g4
                    g4 = Y3[k] + h * h3;

                    //h4
                    t2 = (t3[k] + h).ToString();
                    y2 = (Y[k] + h * f3).ToString();
                    x2 = (Y2[k] + h * g3).ToString();
                    z2 = (Y3[k] + h * h3).ToString();

                    Argument t_arg4 = new Argument("t", t2);
                    Argument y_arg4 = new Argument("y", y2);
                    Argument x_arg4 = new Argument("x", x2);
                    Argument z_arg4 = new Argument("z", z2);

                    Expression express4 = new Expression(Func, t_arg4, y_arg4, x_arg4, z_arg4);
                    h4 = express4.calculate();

                    //Result Y[k],Y'[k],Y''[k]
                    Y[k + 1] = Y[k] + h / 6 * (f1 + 2 * f2 + 2 * f3 + f4);  //Y[k]
                    Y2[k + 1] = Y2[k] + h / 6 * (g1 + 2 * g2 + 2 * g3 + g4); //Y'[k]
                    Y3[k + 1] = Y3[k] + h / 6 * (h1 + 2 * h2 + 2 * h3 + h4); //Y''[k]

                }

                //Calculate Exact and Display Relative Error 
                if (cbExact.Checked == true)
                {
                    //Exact Y(t)
                    if (rbY.Checked == true)
                    {
                        for (k = 0; k <= M; k++)
                        {
                            t2 = t3[k].ToString();
                            FuncY = tbExactY.Text;
                            Argument t_arg = new Argument("t", t2);
                            Expression express = new Expression(FuncY, t_arg);
                            YExact[k] = express.calculate();
                            RelErrorY[k] = Math.Abs(YExact[k] - Y[k]) / Math.Abs(YExact[k]) * 100; //Relative Error Y[k] in %(percent) or call Percent Relative Error
                        }
                        RelErrorY[0] = 0;

                        //Display result Yk
                        result = "";
                        for (k = 0; k <= M; k++)
                        {
                            result = result + k.ToString() + "        " + String.Format("{0:0.0000}", t3[k]) + "        " + String.Format("{0:0.00000000000000E+00}", Y[k]) + "        " + String.Format("{0:0.00000000000000E+00}", YExact[k]) + "        " + String.Format("{0:0.00000000000000E+00}", RelErrorY[k]) + Environment.NewLine;
                        }

                        title = " k          tk                      Yk RK4                                      Yk Exact                             Relative Error(%)" + Environment.NewLine;
                       
                    }
                    else if (rbY2.Checked == true)  //Exact Y'(t)
                    {

                        for (k = 0; k <= M; k++)
                        {
                            t2 = t3[k].ToString();
                            FuncY = tbExactY2.Text;
                            Argument t_arg = new Argument("t", t2);
                            Expression express = new Expression(FuncY, t_arg);
                            Y2Exact[k] = express.calculate();
                            RelErrorY2[k] = Math.Abs(Y2Exact[k] - Y2[k]) / Math.Abs(Y2Exact[k]) * 100; //Relative Error Y'[k] in %(percent) 
                        }

                        RelErrorY2[0] = 0;

                        //Display result Y'k
                        result = "";
                        for (k = 0; k <= M; k++)
                        {
                            result = result + k.ToString() + "        " + String.Format("{0:0.0000}", t3[k]) + "        " + String.Format("{0:0.00000000000000E+00}", Y2[k]) + "        " + String.Format("{0:0.00000000000000E+00}", Y2Exact[k]) + "        " + String.Format("{0:0.00000000000000E+00}", RelErrorY2[k]) + Environment.NewLine;
                        }

                        title = " k          tk                         Y ' k RK4                                Y ' k Exact                          Relative Error (%)" + Environment.NewLine;
                    }
                    else //Y''[k]
                    {
                        for (k = 0; k <= M; k++)
                        {
                            t2 = t3[k].ToString();
                            FuncY = tbExactY3.Text;
                            Argument t_arg = new Argument("t", t2);
                            Expression express = new Expression(FuncY, t_arg);
                            Y3Exact[k] = express.calculate();
                            RelErrorY3[k] = Math.Abs(Y3Exact[k] - Y3[k]) / Math.Abs(Y3Exact[k]) * 100; //Relative Error Y''[k] in %(percent) 
                        }
                        RelErrorY3[0] = 0;
                        //Display result Y''k
                        result = "";
                        for (k = 0; k <= M; k++)
                        {
                            result = result + k.ToString() + "        " + String.Format("{0:0.0000}", t3[k]) + "        " + String.Format("{0:0.00000000000000E+00}", Y3[k]) + "        " + String.Format("{0:0.00000000000000E+00}", Y3Exact[k]) + "        " + String.Format("{0:0.00000000000000E+00}", RelErrorY3[k]) + Environment.NewLine;
                        }

                    title = " k          tk                      Y ' ' k RK4                             Y ' ' k Exact                          Relative Error (%)" + Environment.NewLine;
                        

                    }

                }
                else  //Numeric RK4 only
                {

                    //Result Y(t),Y'(t),Y''(t)

                    result = ""; //empty string at initial

                    for (k = 0; k <= M; k++)
                    {
                        result = result + k.ToString() + "        " + String.Format("{0:0.0000}", t3[k]) + "        " + String.Format("{0:0.00000000000000E+00}", Y[k]) + "        " + String.Format("{0:0.00000000000000E+00}", Y2[k]) + "        " + String.Format("{0:0.00000000000000E+00}", Y3[k]) + Environment.NewLine;
                    }

                    
                    title = " k          tk                                Yk                                          Y ' k                                    Y ' ' k  " + Environment.NewLine;

                }
                line = "=====================================================================================" + Environment.NewLine;

                tbResult.Clear();
                tbResult.Text = title + line + result;


                //Graph
                var series1 = new Series("Y RK4"); //Yk RK4
                var series2 = new Series("Y Exact"); //Yk Exact
                var series3 = new Series("Y ' RK4"); //Y'k RK4
                var series4 = new Series("Y ' Exact"); //Y'k Exact
                var series5 = new Series("Y ' ' RK4"); //Y''k RK4
                var series6 = new Series("Y ' ' Exact"); //Y''k Exact

                chart1.Series.Clear();
                chart1.ChartAreas[0].AxisX.Title = "t";
                chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif",12);
                chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif",12);


                if (cbExact.Checked) //Exact & Numeric RK4
                {
                    sm = 100; //smooth
                    step = (b - a) / sm;

                    if (rbY.Checked)
                    {
                        //Yk RK4
                        for (k = 0; k <= M; k++)
                        {
                            series1.Points.AddXY(t3[k], Y[k]);
                        };

                        //Yk exact

                        Tkm[0] = a;
                        for (k = 0; k <= sm; k++)
                        {
                            Tkm[k + 1] = Tkm[k] + step;
                            t2 = (Tkm[k]).ToString();
                            FuncY = tbExactY.Text;
                            Argument t_arg = new Argument("t", t2);
                            Expression express = new Expression(FuncY, t_arg);
                            YkmExact[k] = express.calculate();
                            series2.Points.AddXY(Tkm[k], YkmExact[k]);
                        };


                        //Ymax to display purpose only
                        Ymax = Y[0];
                        for (k = 0; k <= M; k++)
                        {
                            if (Y[k] > Ymax)
                                Ymax = Y[k];
                        };

                        chart1.ChartAreas[0].AxisY.Title = "Y RK4 & Y Exact";
                        chart1.ChartAreas[0].AxisY.Maximum = Math.Ceiling(Ymax); //display max only

                    }
                    else if (rbY2.Checked)   // Y' RK4 & Y' Exact
                    {
                        //Y'k RK4

                        for (k = 0; k <= M; k++)
                        {
                            series3.Points.AddXY(t3[k], Y2[k]);
                        };

                        //Ymax to display purpose only
                        Ymax = Y2[0];
                        for (k = 0; k <= M; k++)
                        {
                            if (Y2[k] > Ymax)
                                Ymax = Y2[k];
                        };

                        //Y'k exact

                        sm = 100; //smooth
                        step = (b - a) / sm;
                        Tkm[0] = a;
                        for (k = 0; k <= sm; k++)
                        {
                            Tkm[k + 1] = Tkm[k] + step;
                            t2 = (Tkm[k]).ToString();
                            FuncY = tbExactY2.Text;
                            Argument t_arg = new Argument("t", t2);
                            Expression express = new Expression(FuncY, t_arg);
                            Y2kmExact[k] = express.calculate();
                            series4.Points.AddXY(Tkm[k], Y2kmExact[k]);
                        };

                        chart1.ChartAreas[0].AxisY.Title = "Y ' RK4 & Y ' Exact";

                        chart1.ChartAreas[0].AxisY.Maximum = Math.Ceiling(Ymax); //display max only

                    }
                    else // Y'' RK4 & Y'' Exact

                    {
                        //Y''k RK4

                        for (k = 0; k <= M; k++)
                        {
                            series5.Points.AddXY(t3[k], Y3[k]);
                        };

                        //Ymax to display purpose only
                        Ymax = Y3[0];
                        for (k = 0; k <= M; k++)
                        {
                            if (Y3[k] > Ymax)
                                Ymax = Y3[k];
                        };

                        //Y''k exact
                        sm = 100; //smooth
                        step = (b - a) / sm;
                        Tkm[0] = a;
                        for (k = 0; k <= sm; k++)
                        {
                            Tkm[k + 1] = Tkm[k] + step;
                            t2 = (Tkm[k]).ToString();
                            FuncY = tbExactY3.Text;
                            Argument t_arg = new Argument("t", t2);
                            Expression express = new Expression(FuncY, t_arg);
                            Y3kmExact[k] = express.calculate();
                            series6.Points.AddXY(Tkm[k], Y3kmExact[k]);
                        };

                        chart1.ChartAreas[0].AxisY.Title = "Y ' ' RK4 & Y ' ' Exact";

                        chart1.ChartAreas[0].AxisY.Maximum = Math.Ceiling(Ymax); //display max only

                    };
                }
                else  //RK4 only
                {
                    if (rbRK4Y.Checked)
                    {
                        //Yk RK4
                        for (k = 0; k <= M; k++)
                        {
                            series1.Points.AddXY(t3[k], Y[k]);
                        };

                        //Ymax to display purpose only
                        Ymax = Y[0];
                        for (k = 0; k <= M; k++)
                        {
                            if (Y[k] > Ymax)
                                Ymax = Y[k];
                        };


                        chart1.ChartAreas[0].AxisY.Title = "Y RK4";
                        chart1.ChartAreas[0].AxisY.Maximum = Math.Ceiling(Ymax); //display max only

                    }
                    else if (rbRK4Y2.Checked) //Y'k RK4
                    {
                        for (k = 0; k <= M; k++)
                        {
                            series3.Points.AddXY(t3[k], Y2[k]);
                        }
                        //Ymax to display purpose only
                        Ymax = Y2[0];
                        for (k = 0; k <= M; k++)
                        {
                            if (Y2[k] > Ymax)
                                Ymax = Y2[k];
                        };

                        chart1.ChartAreas[0].AxisY.Title = "Y ' RK4";
                        chart1.ChartAreas[0].AxisY.Maximum = Math.Ceiling(Ymax); //display max only

                    }
                    else if (rbRK4Y3.Checked) //Y''k RK4
                    {
                        for (k = 0; k <= M; k++)
                        {
                            series5.Points.AddXY(t3[k], Y3[k]);
                        }

                        //Ymax to display purpose only
                        Ymax = Y3[0];
                        for (k = 0; k <= M; k++)
                        {
                            if (Y3[k] > Ymax)
                                Ymax = Y3[k];
                        };

                        chart1.ChartAreas[0].AxisY.Title = "Y ' ' RK4";
                        chart1.ChartAreas[0].AxisY.Maximum = Math.Ceiling(Ymax); //display max only

                    }
                    else //All (Y,Y',Y'')
                    {
                        //Yk RK4
                        for (k = 0; k <= M; k++)
                        {
                            series1.Points.AddXY(t3[k], Y[k]);
                        };

                        //Ymax to display purpose only
                        Ymax1 = Y[0];
                        for (k = 0; k <= M; k++)
                        {
                            if (Y[k] > Ymax1)
                                Ymax1 = Y[k];
                        };

                        Ymax2 = Y2[0];
                        for (k = 0; k <= M; k++)
                        {
                            if (Y2[k] > Ymax2)
                                Ymax2 = Y2[k];
                        };

                        Ymax3 = Y3[0];
                        for (k = 0; k <= M; k++)
                        {
                            if (Y3[k] > Ymax3)
                                Ymax3 = Y3[k];
                        };

                        //Max of Ymax1,Ymax2,Ymax3
                        if (Ymax1 > Ymax2)
                            Ymax = Ymax1;
                        else
                            Ymax = Ymax2;

                        if (Ymax3 > Ymax)
                            Ymax = Ymax3;


                        //Y'k RK4
                        for (k = 0; k <= M; k++)
                        {
                            series3.Points.AddXY(t3[k], Y2[k]);
                        };

                        for (k = 0; k <= M; k++)
                        {
                            series5.Points.AddXY(t3[k], Y3[k]);
                        };

                        chart1.ChartAreas[0].AxisY.Maximum = Math.Ceiling(Ymax); //display max only
                        chart1.ChartAreas[0].AxisY.Title = "Y , Y ' & Y ' ' (RK4)";


                    }
                }


                series1.BorderWidth = 2; //Yk RK4
                series1.Color = Color.Red;

                series2.BorderWidth = 2; //Yk exact
                series2.Color = Color.Black;
                
                series3.BorderWidth = 2; //Y'k RK4
                series3.Color = Color.Green;
                
                series4.BorderWidth = 2; //Y'k exact
                series4.Color = Color.Blue;
                
                series5.BorderWidth = 2; //Y''k RK4
                series5.Color = Color.Purple;
                
                series6.BorderWidth = 2; //Y''k exact
                series6.Color = Color.Orange;

                chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;

                chart1.ChartAreas[0].AxisX.Maximum = b;
                chart1.ChartAreas[0].AxisX.Minimum = a;


                //Yk RK4,Yk Exact
                series1.ChartType = SeriesChartType.Point; //Yk RK4
                series2.ChartType = SeriesChartType.Line; //Yk Exact
                series3.ChartType = SeriesChartType.Point; //Y'k RK4
                series4.ChartType = SeriesChartType.Line; //Y'k Exact
                series5.ChartType = SeriesChartType.Point; //Y''k RK4
                series6.ChartType = SeriesChartType.Line; //Y''k Exact

                chart1.Series.Add(series1);
                chart1.Series.Add(series2);
                chart1.Series.Add(series3);
                chart1.Series.Add(series4);
                chart1.Series.Add(series5);
                chart1.Series.Add(series6);

                //zoom
                ChartArea CA = chart1.ChartAreas[0];  // quick reference
                CA.AxisX.ScaleView.Zoomable = true;
                CA.CursorX.AutoScroll = true;
                CA.CursorX.IsUserSelectionEnabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Something wrong with the input !!!");
                return;
            }
          
                        
        }
   

                

        private void butAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("|======== Third-Order ODE Solver ========|" + Environment.NewLine + Environment.NewLine +
                "(Third-Order Ordinary Differential Equation Solver)" + Environment.NewLine + Environment.NewLine +

                "Version 1.0 - build dec 2, 2023." + Environment.NewLine +
                       "Created by Lukas Setiawan." + Environment.NewLine +
                       "Copyright (c) 2023. All Rights Reserved." + Environment.NewLine +
                       "Visit www.metodenumeriku.blogspot.com." + Environment.NewLine +
                       "FB search: Metode Numerik-Plus Programnya." + Environment.NewLine +
                       "e-mail: lukassetiawan@yahoo.com." + Environment.NewLine + Environment.NewLine +
                       "My other works :" + Environment.NewLine +
                       "https://bitbucket.org/nixz97/nix/downloads/" + Environment.NewLine + Environment.NewLine +
                       "Accept donations for software development."
           );

            /*
            DialogResult result;
            string text;
            text = ("|======== PDEs Solver ========|" + Environment.NewLine + Environment.NewLine +
                "(Partial Differential Equations Solver)" + Environment.NewLine + Environment.NewLine +

                "Version 1.0 - build mar 15, 2023." + Environment.NewLine +
                       "Created by Lukas Setiawan." + Environment.NewLine +
                       "Copyright (c) 2023. All Rights Reserved." + Environment.NewLine +
                       "Visit www.metodenumeriku.blogspot.com." + Environment.NewLine +
                       "FB search: Metode Numerik-Plus Programnya." + Environment.NewLine +
                       "e-mail: lukassetiawan@yahoo.com." + Environment.NewLine + Environment.NewLine +
                       "My other works :" + Environment.NewLine +
                       "https://bitbucket.org/nixz97/nix/downloads/" + Environment.NewLine + Environment.NewLine +
                       "Accept donations for software development.");

            
            result = MessageBox.Show(text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                Close();
            }
            */

                }

                private void butClear_Click(object sender, EventArgs e)
        {
            tbFunction.Clear();
            tb_a.Clear();
            tb_b.Clear();
            tb_yt0.Clear();
            tb_yt01.Clear();
            tb_yt02.Clear();
            tb_M.Clear();
            tbExactY.Clear();
            tbExactY2.Clear();
            tbExactY3.Clear();
            tbResult.Clear();
            cbExact.Checked = false;

            chart1.Series.Clear();

        }

        private void butFormula_Click(object sender, EventArgs e)
        {
            FormFormula formFml = new FormFormula();
            formFml.Show();//both form active
            //formFml.ShowDialog();  //form 2 only,form 1 lock
        }
    }
}
