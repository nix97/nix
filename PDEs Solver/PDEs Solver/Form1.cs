using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gigasoft.ProEssentials.Enums;
using org.mariuszgromada.math.mxparser;


namespace WindowsFormsApp1
{
    public partial class FormPDEs : Form
    {
        public FormPDEs()
        {
            InitializeComponent();
        }

        //Heat Equation
        private void btRun_Click(object sender, EventArgs e)
        {

            double[] F = new double[1001];
            double[] t = new double[1001];
            double[] X = new double[1001];
            double[,] U = new double[1001, 1001];

            
            double h, r, s2, x2,k, Chk, Chk2;
            int i, j, m, n;
            string x3, hasil;


            try
            {

                double a = double.Parse(tba.Text);
                double b = double.Parse(tbb.Text);
                double c = double.Parse(tbc.Text);
                double c1 = double.Parse(tbc1.Text);
                double c2 = double.Parse(tbc2.Text);
                m = int.Parse(tbm.Text);
                n = int.Parse(tbn.Text);

                if ((m > 1000) || (n > 1000))
                {
                    MessageBox.Show("Maximum m or n  is 1000 !!!");
                    return;
                }

                h = a / (n - 1);
                k = b / (m - 1);
                r = (c * c * k) / (h * h);
                s2 = 1 - 2 * r;


                for (i = 2; i <= n - 1; i++)
                {
                    x2 = h * (i - 1);
                    x3 = (x2.ToString());
                    Argument x = new Argument("x", x3);
                    Expression expres = new Expression(tbFx.Text, x);
                    F[i] = (expres.calculate());

                }


                //Check input F(x) kalau ada salah ketik
                Argument x4 = new Argument("x", -2);
                Expression expres2 = new Expression(tbFx.Text, x4);
                Chk = (expres2.calculate());

                Argument x5 = new Argument("x", 2);
                Expression expres3 = new Expression(tbFx.Text, x5);
                Chk2 = (expres3.calculate());

                if ((Double.IsNaN(Chk)) || (Double.IsNaN(Chk2)))
                {
                    MessageBox.Show("Something wrong with input F(x) !!!");
                    return;

                }


                for (j = 1; j <= m; j++)
                {
                    U[1, j] = c1;
                    U[n, j] = c2;
                }

                for (i = 2; i <= n - 1; i++)
                {
                    U[i, 1] = (F[i]);
                }

                for (j = 2; j <= m; j++)
                {
                    for (i = 2; i <= n - 1; i++)
                    {
                        U[i, j] = s2 * U[i, j - 1] + r * (U[i - 1, j - 1] + U[i + 1, j - 1]);
                    }
                }


                for (j = 1; j <= m; j++)
                {
                    t[j] = k * (j - 1);
                }

                for (i = 1; i <= n; i++)
                {
                    X[i] = h * (i - 1);

                }


                // Decimal
           
                if (rbDecimal.Checked) {

                    hasil = "";
                    for (j = 1; j <= m; j++)
                    {
                        for (i = 1; i <= n; i++)
                        {
                            hasil = hasil + i.ToString() + "         " + j.ToString() + "          " + String.Format("{0:0.000}", X[i]) + "            " + String.Format("{0:0.000}", t[j]) + "            " + String.Format("{0:0.000000000000000}", U[i, j]) + "\n";
                        }
                    }

                    rtbResult.Clear();
                    rtbResult.Text = "i            j            x i                   t j                            U ( i , j )" + "\n\n" + hasil;
                }
                else{ //format exponent
                    hasil = "";
                    for (j = 1; j <= m; j++)
                    {
                        for (i = 1; i <= n; i++)
                        {
                            hasil = hasil + i.ToString() + "         " + j.ToString() + "          " + String.Format("{0:0.000}", X[i]) + "            " + String.Format("{0:0.000}", t[j]) + "            " + String.Format("{0:0.00000000000000E+00}", U[i, j]) + "\n";
                        }
                    }

                    rtbResult.Clear();
                    rtbResult.Text = "i            j            x i                   t j                           U ( i , j )" + "\n\n" + hasil;
                }


                //Plot Graph 3D
                Pe3do1.PeFunction.Reset();
                Pe3do1.PePlot.PolyMode = Gigasoft.ProEssentials.Enums.PolyMode.SurfacePolygons;
                Pe3do1.PeConfigure.PrepareImages = true;

                // Set the amount of data and pass data
                Pe3do1.PeData.Subsets = m; //z axis
                Pe3do1.PeData.Points = n; //x axis


                for (j = 1; j <= m; j++)
                {
                    for (i = 1; i <= n; i++)
                    {
                        Pe3do1.PeData.X[j - 1, i - 1] = (float)X[i];  //x axis
                        Pe3do1.PeData.Z[j - 1, i - 1] = (float)t[j];  //z axis
                        Pe3do1.PeData.Y[j - 1, i - 1] = (float)U[i, j]; //y axis
                    }
                }


                Pe3do1.PePlot.ViewingHeight = 30;
                Pe3do1.PePlot.DegreeOfRotation = 35;
                Pe3do1.PePlot.Method = Gigasoft.ProEssentials.Enums.ThreeDGraphPlottingMethod.Four;


                Pe3do1.PeString.MainTitle = "";
                Pe3do1.PeString.SubTitle = "";

                Pe3do1.PeString.XAxisLabel = "x (x-axis)";
                Pe3do1.PeString.ZAxisLabel = "t (z-axis)";
                Pe3do1.PeString.YAxisLabel = "U (y-axis)";

                Pe3do1.PeConfigure.RenderEngine = RenderEngine.Direct3D;
                Pe3do1.PeColor.BitmapGradientMode = true;
                Pe3do1.PeColor.QuickStyle = QuickStyle.DarkNoBorder;


                Pe3do1.PeFunction.ReinitializeResetImage();
                Pe3do1.Refresh();

            }
            catch (Exception)
            {

                MessageBox.Show("Something wrong with input(check again) !!!");
                return;
            }
        }


        //Heat Equation
        private void btClear_Click(object sender, EventArgs e)
        {
            tbFx.Clear();
            tba.Clear();
            tbb.Clear();
            tbc.Clear();
            tbc1.Clear();
            tbc2.Clear();
            tbm.Clear();
            tbn.Clear();
            rtbResult.Clear();
            Pe3do1.Refresh();
        }



        //Wave Eq
        private void btRunWave_Click(object sender, EventArgs e)
        {

            int i, j,m,n;
            double h, k, r, r2, r22, s1, s2, x2, ChkF, ChkG;
             

            double[] F = new double[1001];
            double[] G = new double[1001];

            double[,]U = new double[1001,1001];
            double[] t = new double[1001];
            double[] X = new double[10001];

            string x3, hasil;

       
            try
            {

                double a= double.Parse(tbWaveA.Text);
                double b = double.Parse(tbWaveB.Text);
                double c = double.Parse(tbWaveC.Text);
                m = int.Parse(tbWaveM.Text);
                n = int.Parse(tbWaveN.Text);

                if ((m > 1000) || (n > 1000))
                {
                    MessageBox.Show("Maximum m or n  is 1000 !!!");
                    return;
                }


                h = a / (n - 1);
                k = b / (m - 1);
                r = (c * k) / h;
                r2 = r * r;
                r22 = (r * r) / 2;
                s1 = 1 - (r * r);
                s2 = 2 - (2 * r * r);

                for (i = 2; i <= n - 1; i++)
                {
                    x2 = h * (i - 1);
                    x3 = (x2.ToString());
                    Argument x6 = new Argument("x", x3);
                    Expression expres = new Expression(tbWaveF.Text, x6);
                    F[i] = (expres.calculate());

                    Argument x7 = new Argument("x", x3);
                    Expression expres4 = new Expression(tbWaveG.Text, x7);
                    G[i] = (expres4.calculate());

                 }

                //Check input F(x) benar
                Argument x9 = new Argument("x", "0.234"); //0.234 bil acak sembarang untuk cek aja 
                Expression expres5 = new Expression(tbWaveF.Text, x9);
                ChkF = (expres5.calculate());
                if (Double.IsNaN(ChkF)) //
                {
                    MessageBox.Show("Something wrong with input F(x) !!!");
                    return;
                }

                //Check input G(x) benar
                Argument x10 = new Argument("x", "0.234"); //0.234 bil acak sembarang untuk cek aja 
                Expression expres6 = new Expression(tbWaveG.Text, x10);
                ChkG = (expres6.calculate());
                if (Double.IsNaN(ChkG)) //
                {
                    MessageBox.Show("Something wrong with input G(x) !!!");
                    return;
                }


                for (j = 1; j <= m; j++)
                {
                    U[1,j] = 0;
                    U[n,j] = 0;
                }


                for (i = 2; i <= n - 1; i++)
                {
                    U[i,1] = (float)F[i];
                    U[i,2] = s1 *(float)F[i] + k * (float)G[i] + r22 * ((float)F[i + 1] + (float)F[i - 1]);
                }

                for (j = 3; j <= m; j++)
                {
                    for (i = 2; i <= n - 1; i++)
                    {
                        U[i,j] = s2 * U[i,j - 1] + r2 * (U[i - 1,j - 1] + U[i + 1,j - 1]) - U[i,j - 2];
                    }
                }


                for (j = 1; j <= m; j++)
                {
                    t[j] = k * (j - 1);
                }

                for (i = 1; i <= n; i++)
                {
                    X[i] = h * (i - 1);
                }

                // Decimal
                if (rbWaveDec.Checked)
                {

                    hasil = "";
                    for (j = 1; j <= m; j++)
                    {
                        for (i = 1; i <= n; i++)
                        {
                            hasil = hasil + i.ToString() + "         " + j.ToString() + "          " + String.Format("{0:0.000}", X[i]) + "            " + String.Format("{0:0.000}", t[j]) + "          " + String.Format("{0:0.000000000000000}", U[i, j]) + "\n";
                        }
                    }

                    rtbResultWave.Clear();
                    rtbResultWave.Text = "i            j              x i                  t j                        U ( i , j )" + "\n\n" + hasil;
                }
                else
                { //format exponent
                    hasil = "";
                    for (j = 1; j <= m; j++)
                    {
                        for (i = 1; i <= n; i++)
                        {
                            hasil = hasil + i.ToString() + "         " + j.ToString() + "          " + String.Format("{0:0.000}", X[i]) + "            " + String.Format("{0:0.000}", t[j]) + "           " + string.Format("{0:0.00000000000000E+00}", U[i, j]) + "\n";

                            //hasil = hasil + i.ToString() + "         " + j.ToString() + "          " + String.Format("{0:0.000}", X[i]) + "            " + String.Format("{0:0.000}", t[j]) + "           " + U[i, j].ToString("E") + "\n";
                            //string.Format("{0:0.##E+00}", dValue);
                        }
                    }

                    rtbResultWave.Clear();
                    rtbResultWave.Text = "i            j              x i                  t j                           U ( i , j )" + "\n\n" + hasil;
                }


                //Plot Graph 3D
                pe3do2.PeFunction.Reset();
                pe3do2.PePlot.PolyMode = Gigasoft.ProEssentials.Enums.PolyMode.SurfacePolygons;
                pe3do2.PeConfigure.PrepareImages = true;

                // Set the amount of data and pass data
                pe3do2.PeData.Subsets = m;
                pe3do2.PeData.Points = n;


                for (j = 1; j <= m; j++)
                {
                    for (i = 1; i <= n; i++)
                    {
                        pe3do2.PeData.X[j - 1, i - 1] = (float)X[i];
                        pe3do2.PeData.Z[j - 1, i - 1] = (float)t[j];
                        pe3do2.PeData.Y[j - 1, i - 1] = (float)U[i, j];

                    }
                }


                pe3do2.PePlot.ViewingHeight = 30;
                pe3do2.PePlot.DegreeOfRotation = 35;
                pe3do2.PePlot.Method = Gigasoft.ProEssentials.Enums.ThreeDGraphPlottingMethod.Four;
            

                pe3do2.PeString.MainTitle = "";
                pe3do2.PeString.SubTitle = "";

                pe3do2.PeString.XAxisLabel = "x (x-axis)";
                pe3do2.PeString.ZAxisLabel = "t (z-axis)";
                pe3do2.PeString.YAxisLabel = "U (y-axis)";

                pe3do2.PeConfigure.RenderEngine = RenderEngine.Direct3D;
                pe3do2.PeColor.BitmapGradientMode = true;
                pe3do2.PeColor.QuickStyle = QuickStyle.DarkNoBorder;


                pe3do2.PeFunction.ReinitializeResetImage();
                pe3do2.Refresh();

            }
            catch (Exception)
            {

                MessageBox.Show("Something wrong with input(check again) !!!");
                return;
            }
            
        }

        private void btRunTool_Click(object sender, EventArgs e)
        {
            string x2, y2, z2;
            double ResultV, ChkF;

            try
            {


                //cek V(x,y,z)
                Argument x9 = new Argument("x", "0.234"); //0.234 bil acak sembarang untuk cek aja 
                Argument y9 = new Argument("y", "0.234"); //0.234 bil acak sembarang untuk cek aja 
                Argument z9 = new Argument("z", "0.234"); //0.234 bil acak sembarang untuk cek aja 

                Expression expres5 = new Expression(tbV.Text, x9, y9, z9);
                ChkF = (expres5.calculate());
                if (Double.IsNaN(ChkF)) //
                {
                    MessageBox.Show("Something wrong with input V(x,y,z) !!!");
                    return;
                }

                x2 = tbx.Text;
                y2 = tby.Text;
                z2 = tbz.Text;

                Argument x = new Argument("x", x2);
                Argument y = new Argument("y", y2);
                Argument z = new Argument("z", z2);

                Expression expres = new Expression(tbV.Text, x, y, z);
                ResultV = (expres.calculate());

                if (Double.IsNaN(ResultV))
                {
                    MessageBox.Show("Something wrong with input(check again) !!!");
                    return;
                }

                                
                lbResult.Text = "V = " + ResultV.ToString();
            }


            catch (Exception)
            {
                MessageBox.Show("Something wrong with input(check again) !!!");
                return;
            }

        }

        private void btClearTool_Click(object sender, EventArgs e)
        {
                tbV.Clear();
                tbx.Clear();
                tby.Clear();
                tbz.Clear();
                lbResult.Text = "V = ?";
         }

        private void btRunLap_Click(object sender, EventArgs e)
        {

            int i, j, m, n;
            double ChkF1, ChkF2,ChkF3,ChkF4,Tol,Count,w, ave, h, x2, y2, relax;
            
            double[] F1 = new double[1001];
            double[] F2 = new double[1001];
            double[] F3 = new double[1001];
            double[] F4 = new double[1001];

            double[,] U = new double[1001, 1001];
            double[] X = new double[1001];
            double[] Y = new double[10001];

            string x3,y3, hasil;



            try
            {

                double a = double.Parse(tbLa.Text);
                double b = double.Parse(tbLb.Text);
                m = int.Parse(tbLm.Text);
                n = int.Parse(tbLn.Text);

                if ((m > 1000) || (n > 1000))
                {
                    MessageBox.Show("Maximum m or n  is 1000 !!!");
                    return;
                }


                h = a / (n - 1);

                //Check input F1(x) benar
                Argument x9 = new Argument("x", "0.234"); //0.234 bil acak sembarang untuk cek aja 
                Argument x10 = new Argument("y", "0.234"); //0.234 bil acak sembarang untuk cek aja 

                Expression expres5 = new Expression(tbF1.Text, x9);
                Expression expres6 = new Expression(tbF2.Text, x9);
                Expression expres7 = new Expression(tbF3.Text, x10);
                Expression expres8 = new Expression(tbF4.Text, x10);

                ChkF1 = (expres5.calculate());
                if (Double.IsNaN(ChkF1)) //
                {
                    MessageBox.Show("Something wrong with input F1(x) !!!");
                    return;
                }

                ChkF2 = (expres6.calculate());
                if (Double.IsNaN(ChkF2)) //
                {
                    MessageBox.Show("Something wrong with input F2(x) !!!");
                    return;
                }

                ChkF3 = (expres7.calculate());
                if (Double.IsNaN(ChkF3)) //
                {
                    MessageBox.Show("Something wrong with input F3(y) !!!");
                    return;
                }

                ChkF4 = (expres8.calculate());
                if (Double.IsNaN(ChkF4)) //
                {
                    MessageBox.Show("Something wrong with input F4(y) !!!");
                    return;
                }

                for (i = 0; i <= n; i++)
                {
                    x2 = h * (i - 1);
                    y2 = h * (i - 1);

                    x3 = (x2.ToString());
                    y3 = (y2.ToString());

                    Argument x = new Argument("x", x3);
                    Expression expres = new Expression(tbF1.Text, x);
                    F1[i] = (expres.calculate());

                    Argument x4 = new Argument("x", x3);
                    Expression expres2 = new Expression(tbF2.Text, x4);
                    F2[i] = (expres2.calculate());

                    Argument y = new Argument("y", y3);
                    Expression expres3 = new Expression(tbF3.Text, y);
                    F3[i] = (expres3.calculate());

                    Argument y4 = new Argument("y", y3);
                    Expression expres4 = new Expression(tbF4.Text, y4);
                    F4[i] = (expres4.calculate());


                }
             
                ave = (a * (F1[0] + F2[0]) + b * (F3[0] + F4[0])) / (2 * a + 2 * b);


                for (i = 2; i <= n - 1; i++)
                {
                    for (j = 2; j <= m - 1; j++)
                    {
                        U[i,j] = ave;
                    }
                }


                for (j = 1; j <= m; j++)
                {
                    U[1,j] = F3[j];
                    U[n,j] = F4[j];
                }


                for (i = 1; i <= n; i++)
                {
                    U[i,1] = F1[i];
                    U[i,m] = F2[i];
                }

                U[1,1] = (U[1,2] + U[2,1]) / 2;
                U[1,m] = (U[1,m - 1] + U[2,m]) / 2;
                U[n,1] = (U[n - 1,1] + U[n,2]) / 2;
                U[n,m] = (U[n - 1,m] + U[n,m - 1]) / 2;

                w = 4 / (2 + Math.Sqrt(4 - Math.Exp(Math.Log10(Math.Cos(Math.PI / (n - 1)) + Math.Cos(Math.PI / (m - 1))) * 2)));
                Tol = 1;
                Count = 0;

                while ((Tol > 0.001) && (Count <= 25))
                {
                    Tol = 0.0;
                    for (j = 2; j <= m - 1; j++)
                    {
                        for (i = 2; i <= n - 1; i++)
                        {
                            relax = (w * (U[i,j + 1] + U[i,j - 1] + U[i + 1,j] + U[i - 1,j] - 4.0 * U[i,j]) / 4.0);
                            U[i,j] = U[i, j] + relax;
                            Tol = Math.Max(Tol, Math.Abs(relax));
                        }

                    }
                    Count = Count + 1;
                }

                for (j = 1; j <= m; j++)
                {
                    Y[j] = h * (j - 1);
                }

                for (i = 1; i <= n; i++)
                {
                    X[i] = h * (i - 1);
                }


                // Decimal

                if (rbLapDe.Checked)
                {

                    hasil = "";
                    for (j = 1; j <= m; j++)
                    {
                        for (i = 1; i <= n; i++)
                        {
                            hasil = hasil + i.ToString() + "         " + j.ToString() + "          " + String.Format("{0:0.000}", X[i]) + "            " + String.Format("{0:0.000}", Y[j]) + "            " + String.Format("{0:0.000000000000000}", U[i, j]) + "\n";
                        }
                    }

                    rtbLaplace.Clear();
                    rtbLaplace.Text = "i            j            x i                   y j                         U ( i , j )" + "\n\n" + hasil;
                }
                else
                { //format exponent
                    hasil = "";
                    for (j = 1; j <= m; j++)
                    {
                        for (i = 1; i <= n; i++)
                        {
                            hasil = hasil + i.ToString() + "         " + j.ToString() + "          " + String.Format("{0:0.000}", X[i]) + "            " + String.Format("{0:0.000}", Y[j]) + "            " + String.Format("{0:0.00000000000000E+00}", U[i, j]) + "\n";
                        }
                    }

                    rtbLaplace.Clear();
                    rtbLaplace.Text = "i            j            x i                   y j                         U ( i , j )" + "\n\n" + hasil;
                }


                //Plot Graph 3D
           
                pe3do3.PeFunction.Reset();
                pe3do3.PePlot.PolyMode = Gigasoft.ProEssentials.Enums.PolyMode.SurfacePolygons;
                pe3do3.PeConfigure.PrepareImages = true;

                // Set the amount of data and pass data
                pe3do3.PeData.Subsets = m;
                pe3do3.PeData.Points = n;



                for (j = 1; j <= m; j++)             
                {
                    for (i = 1; i <= n; i++)
                    {
                       
                        pe3do3.PeData.X[j- 1, i - 1] = (float)X[i];
                        pe3do3.PeData.Z[j - 1, i - 1] = (float)Y[j];
                        pe3do3.PeData.Y[j - 1, i - 1] = (float)U[i, j];
                                                        
                    }
                }

              
                pe3do3.PePlot.ViewingHeight = 30;
                pe3do3.PePlot.DegreeOfRotation = 35;
                pe3do3.PePlot.Method = Gigasoft.ProEssentials.Enums.ThreeDGraphPlottingMethod.Four;


                pe3do3.PeString.MainTitle = "";
                pe3do3.PeString.SubTitle = "";

                pe3do3.PeString.XAxisLabel = "x (x-axis)";
                pe3do3.PeString.ZAxisLabel = "y (z-axis)";
                pe3do3.PeString.YAxisLabel = "U (y-axis)";

                pe3do3.PeConfigure.RenderEngine = RenderEngine.Direct3D;
                pe3do3.PeColor.BitmapGradientMode = true;
                pe3do3.PeColor.QuickStyle = QuickStyle.DarkNoBorder;


                pe3do3.PeFunction.ReinitializeResetImage();
                pe3do3.Refresh();
                
            }
            catch (Exception)
            {

                MessageBox.Show("Something wrong with input(check again) !!!");
                return;
            }
        }

        private void btClearLap_Click(object sender, EventArgs e)
        {

            tbF1.Clear();
            tbF2.Clear();
            tbF3.Clear();
            tbF4.Clear();
            tbLa.Clear();
            tbLb.Clear();
            tbLm.Clear();
            tbLn.Clear();
            rtbLaplace.Clear();
            pe3do3.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("|======== PDEs Solver ========|" + Environment.NewLine + Environment.NewLine +
                "(Partial Differential Equations Solver)" + Environment.NewLine + Environment.NewLine+

                "Version 1.1 - build mar 17, 2023." + Environment.NewLine +
                       "Created by Lukas Setiawan." + Environment.NewLine +
                       "Copyright (c) 2023. All Rights Reserved." + Environment.NewLine +
                       "Visit www.metodenumeriku.blogspot.com." + Environment.NewLine +
                       "FB search: Metode Numerik-Plus Programnya." + Environment.NewLine +
                       "e-mail: lukassetiawan@yahoo.com." + Environment.NewLine + Environment.NewLine +
                       "My other works :" + Environment.NewLine +
                       "https://bitbucket.org/nixz97/nix/downloads/" + Environment.NewLine + Environment.NewLine +
                       "Accept donations for software development."
           );
        }

        private void btClearWave_Click(object sender, EventArgs e)
        {
            tbWaveF.Clear();
            tbWaveG.Clear();
            tbWaveA.Clear();
            tbWaveB.Clear();
            tbWaveC.Clear();
            tbWaveM.Clear();
            tbWaveN.Clear();
            rtbResultWave.Clear();
            pe3do2.Refresh();

        }

        private void btRunPoisson_Click(object sender, EventArgs e)
        {

            
            bool OK;

            int i, j, m,n,m1, m2, n1, n2,L, LL, iter;
            double a,b,c,d,h,k, tol, V, VV, Z, E, ChkF, ChkG;

            double[,] U = new double[1001, 1001];
            double[] X = new double[1001];
            double[] X2 = new double[1001];
            double[] Y = new double[10001];
            double[] Y2 = new double[10001];

            double[] parG = new double[101];
            double[] parG2 = new double[101];
            double[] parF = new double[101];
            double[] Res = new double[101];
            
            string x3,y3, hasil;

            /*
             * 
            bool OK;

            int i, j, m,n,m1, m2, n1, n2,L, LL, iter;
            double a,b,c,d,h,k, tol, V, VV, Z, E, ChkF, ChkG;

            float[,] U = new float[1001, 1001];
            float[] X = new float[1001];
            float[] X2 = new float[1001];
            float[] Y = new float[10001];
            float[] Y2 = new float[10001];

            float[] parG = new float[101];
            float[] parG2 = new float[101];
            float[] parF = new float[101];
            float[] Res = new float[101];
            
            string x3,y3, hasil;

             * */

            try
            {

                a = double.Parse(tbPoiA.Text);
                b = double.Parse(tbPoiB.Text);
                c = double.Parse(tbPoiC.Text);
                d = double.Parse(tbPoiD.Text);
                tol = double.Parse(tbTol.Text);

                m = int.Parse(tbPoiM.Text);
                n = int.Parse(tbPoiN.Text);
                iter = int.Parse(tbIter.Text);


                if ((m > 1000) || (n > 1000))
                {
                    MessageBox.Show("Maximum m or n  is 1000 !!!");
                    return;
                }

                //if (tol < 0.99e-8)
                if (tol < 1e-8)
                {
                    MessageBox.Show("Minimum Tol is 1e-8 !!!");
                    return;
                }
                if (iter > 10000)
                {
                    MessageBox.Show("Maximum iteration is 10000 !!!");
                    return;
                }


                h = (b - a) / n;
                k = (d - c) / m;

               
                //cek F(x,y) /G(x,y)
                Argument x9 = new Argument("x", "0.234"); //0.234 bil acak sembarang untuk cek aja 
                Argument y9 = new Argument("y", "0.234"); //0.234 bil acak sembarang untuk cek aja 
                Expression expres5 = new Expression(tbPoiF.Text, x9,y9);
                ChkF = (expres5.calculate());
                if (Double.IsNaN(ChkF)) //
                {
                    MessageBox.Show("Something wrong with input F(x,y) !!!");
                    return;
                }

                Argument x30 = new Argument("x", "0.234"); //0.234 bil acak sembarang untuk cek aja 
                Argument y30 = new Argument("y", "0.234"); //0.234 bil acak sembarang untuk cek aja 
                Expression expres6 = new Expression(tbPoiG.Text, x30,y30);
                ChkG = (expres6.calculate());
                if (Double.IsNaN(ChkG)) //
                {
                    MessageBox.Show("Something wrong with input G(x,y) !!!");
                    return;
                }
                

                OK = false;
                while (OK == false)
                {
                    if ((a >= b) || (c >= d))
                    {
                        MessageBox.Show("Left endpoint must be less than right endpoint !!!");
                        return;
                    }
                    else
                        OK = true;
                }


                if ((m < 3) || (n < 3))
                {
                    MessageBox.Show("m or n must be larger than 2 !!!");
                    return;
                }


                OK = false;
                while (OK == false)
                {
                    if ((m <= 2) || (n <= 2))
                    {
                        MessageBox.Show("Numbers must exceed 2 !!!");
                        break;
                    }
                    else
                        OK = true;
                }

                OK = false;
                while (OK == false)
                {
                    if (tol <= 0)
                    {
                        MessageBox.Show("Tolerance must be positive !!!");
                        break;
                    }
                    else
                        OK = true;
                }

                OK = false;
                while (OK == false)
                {
                    if (iter <= 0)
                    {
                        MessageBox.Show("Number must be a positive integer !!!");
                        break;
                    }
                    else
                        OK = true;
                }

                if (OK == true)
                { 
                    m1 = m - 1;
                    m2 = m - 2;
                    n1 = n - 1;
                    n2 = n - 2;

                    // STEP 1
                    h = (b - a) / n;
                    k = (d - c) / m;

                    
                    // STEP 2
                    for (j = 1; j <= n + 1; j++)
                    {
                        X2[j] = 0;
                    }

                    for (j = 1; j <= m + 1; j++)
                    {
                        Y2[j] = 0;
                    }

                    for (i = 1; i <= n + 1; i++)
                    {
                        for (j = 1; j <= m + 1; j++)
                        {
                            U[i, j] = 0;
                        }
                    }

                    for (i = 0; i <= n; i++)
                    {
                        X2[i + 1] = (float)(a + i * h); 
                    }

                    // STEP 3
                    for (j = 0; j <= m; j++)
                    {
                        Y2[j + 1] = (float)(c + j * k);
                    }

                    
                    //STEP 4
                    for (i = 1; i <= n1; i++)
                    {
                        parG[0] = X2[i + 1];
                        parG[1] = Y2[1];

                        x3 = parG[0].ToString();
                        y3 = parG[1].ToString();

                        Argument x5 = new Argument("x", x3);
                        Argument y5 = new Argument("y", y3);

                        Expression expres = new Expression(tbPoiG.Text, x5, y5);
                        U[i + 1, 1] = (float)(expres.calculate());


                        parG2[0] = X2[i + 1];
                        parG2[1] = Y2[m + 1];

                        x3 = parG2[0].ToString();
                        y3 = parG2[1].ToString();

                        Argument x6 = new Argument("x", x3);
                        Argument y6 = new Argument("y", y3);

                        Expression expres2 = new Expression(tbPoiG.Text, x6, y6);
                        U[i + 1, m + 1] = (float)(expres2.calculate());

                    }



                    for (j = 0; j <= m; j++)
                    {
                        parG[0] = X2[1];
                        parG[1] = Y2[j + 1];
                        x3 = parG[0].ToString();
                        y3 = parG[1].ToString();
                        Argument x5 = new Argument("x", x3);
                        Argument y5 = new Argument("y", y3);
                        Expression expres = new Expression(tbPoiG.Text, x5, y5);
                        U[1, j + 1] = (float)(expres.calculate());


                        parG2[0] = X2[n + 1];
                        parG2[1] = Y2[j + 1];
                        x3 = parG2[0].ToString();
                        y3 = parG2[1].ToString();
                        Argument x6 = new Argument("x", x3);
                        Argument y6 = new Argument("y", y3);
                        Expression expres2 = new Expression(tbPoiG.Text, x6, y6);
                        U[n + 1, j + 1] = (float)(expres2.calculate());

                    }

                    for (i = 1; i <= n1; i++)
                    {
                        for (j = 1; j <= m1; j++)
                        {
                            U[i + 1, j + 1] = 0;
                        }
                    }

                    // STEP 5
                    // use V for lambda, VV for mu
                    V = h * h / (k * k);
                    VV = 2 * (1 + V);
                    L = 1;
                    OK = false;

                    //STEP 6 long while
                    //while ((L <= 1) && (OK == false)) {  //til step 20
                    while ((L <= iter) && (OK == false))
                    {  //til step 20
                       //while (OK == false) {  //til step 20

                        parF[0] = X2[2];
                        parF[1] = Y2[m1 + 1];

                        x3 = parF[0].ToString();
                        y3 = parF[1].ToString();
                        Argument x5 = new Argument("x", x3);
                        Argument y5 = new Argument("y", y3);
                        Expression expres = new Expression(tbPoiF.Text, x5, y5);
                        Res[1] = (float)(expres.calculate());

                        //G(A,Y(M1+1))
                        parG[0] = (float)a;
                        parG[1] = Y2[m1 + 1];
                        x3 = parG2[0].ToString();
                        y3 = parG2[1].ToString();
                        Argument x6 = new Argument("x", x3);
                        Argument y6 = new Argument("y", y3);
                        Expression expres2 = new Expression(tbPoiG.Text, x6, y6);
                        Res[2] = (float)(expres2.calculate());

                        parG[0] = X2[2];
                        parG[1] = (float)d;
                        x3 = parG2[0].ToString();
                        y3 = parG2[1].ToString();
                        Argument x7 = new Argument("x", x3);
                        Argument y7 = new Argument("y", y3);
                        Expression expres3 = new Expression(tbPoiG.Text, x7, y7);
                        Res[3] = (float)(expres3.calculate());


                        Z = (-h * h * Res[1] + Res[2] + V * Res[3] + V * U[2, m2 + 1] + U[3, m1 + 1]) / VV;

                        E = Math.Abs(U[2, m1 + 1] - Z);
                        U[2, m1 + 1] = (float)Z;

                        //STEP 8
                        for (i = 2; i <= n2; i++)
                        {
                            parF[0] = X2[i + 1];
                            parF[1] = Y2[m1 + 1];


                            x3 = parF[0].ToString();
                            y3 = parF[1].ToString();
                            Argument x8 = new Argument("x", x3);
                            Argument y8 = new Argument("y", y3);
                            Expression expres4 = new Expression(tbPoiF.Text, x8, y8);
                            Res[4] = (float)(expres4.calculate());

                            //G(X(I+1),D) res[5]
                            parG[0] = i + 1;
                            parG[1] = (float)d;
                            x3 = parG[0].ToString();
                            y3 = parG[1].ToString();
                            Argument x10 = new Argument("x", x3);
                            Argument y10 = new Argument("y", y3);
                            Expression expres7 = new Expression(tbPoiG.Text, x10, y10);
                            Res[5] = (float)(expres7.calculate());


                            Z = (-h * h * Res[4] + V * Res[5] + U[i, m1 + 1] + U[i + 2, m1 + 1] + V * U[i + 1, m2 + 1]) / VV;
                            if (Math.Abs(U[i + 1, m1 + 1] - Z) > E)
                            {
                                E = Math.Abs(U[i + 1, m1 + 1] - Z);
                            }
                            U[i + 1, m1 + 1] = (float)Z;

                        }

                        //STEP 9
                        parF[0] = X2[n1 + 1];
                        parF[1] = Y2[n1 + 1];

                        x3 = parF[0].ToString();
                        y3 = parF[1].ToString();
                        Argument x2 = new Argument("x", x3);
                        Argument y2 = new Argument("y", y3);
                        Expression expres8 = new Expression(tbPoiF.Text, x2, y2);
                        Res[6] = (float)(expres8.calculate());


                        //G(B,Y(M1+1)) res[7]
                        parG[0] = (float)b;
                        parG[1] = Y2[m1 + 1];
                        x3 = parG[0].ToString();
                        y3 = parG[1].ToString();
                        Argument x11 = new Argument("x", x3);
                        Argument y11 = new Argument("y", y3);
                        Expression expres9 = new Expression(tbPoiG.Text, x11, y11);
                        Res[7] = (float)(expres9.calculate());

                        //G(X(N1+1),D) res 8
                        parG[0] = X2[n1 + 1];
                        parG[1] = (float)d;
                        x3 = parG[0].ToString();
                        y3 = parG[1].ToString();
                        Argument x12 = new Argument("x", x3);
                        Argument y12 = new Argument("y", y3);
                        Expression expres10 = new Expression(tbPoiG.Text, x12, y12);
                        Res[8] = (float)(expres10.calculate());


                        Z = (-h * h * Res[6] + Res[7] + V * Res[8] + U[n2 + 1, m1 + 1] + V * U[n1 + 1, m2 + 1]) / VV;

                        if (Math.Abs(U[n1 + 1, m1 + 1] - Z) > E)
                        {
                            E = Math.Abs(U[n1 + 1, m1 + 1] - Z);
                        }


                        U[n1 + 1, m1 + 1] = (float)Z;


                        for (LL = 2; LL <= m2; LL++)
                        { //STEP 10 LL=iter
                            j = m2 - LL + 2;
                            // STEP 11
                            //F(X(2),Y(J+1)) res[9]
                            parF[0] = X2[2];
                            parF[1] = Y2[j + 1];


                            x3 = parF[0].ToString();
                            y3 = parF[1].ToString();
                            Argument x13 = new Argument("x", x3);
                            Argument y13 = new Argument("y", y3);
                            Expression expres11 = new Expression(tbPoiF.Text, x13, y13);
                            Res[9] = (float)(expres11.calculate());

                            //G(A,Y(J+1)) Res[10]
                            parG[0] = (float)a;
                            parG[1] = Y2[j + 1];
                            x3 = parG[0].ToString();
                            y3 = parG[1].ToString();
                            Argument x14 = new Argument("x", x3);
                            Argument y14 = new Argument("y", y3);
                            Expression expres12 = new Expression(tbPoiG.Text, x14, y14);
                            Res[10] = (float)(expres12.calculate());



                            Z = (-h * h * Res[9] + Res[10] + V * U[2, j + 2] + V * U[2, j] + U[3, j + 1]) / VV;
                            if (Math.Abs(U[2, j + 1] - Z) > E)
                            {
                                E = Math.Abs(U[2, j + 1] - Z);
                            }
                            U[2, j + 1] = (float)Z;


                            //STEP 12
                            for (i = 2; i <= n2; i++)
                            {
                                //F(X(I+1),Y(J+1)) resp[11]
                                parF[0] = X2[i + 1];
                                parF[1] = Y2[j + 1];
                                x3 = parF[0].ToString();
                                y3 = parF[1].ToString();
                                Argument x25 = new Argument("x", x3);
                                Argument y25 = new Argument("y", y3);
                                Expression expres13 = new Expression(tbPoiF.Text, x25, y25);
                                Res[11] = (float)(expres13.calculate());

                                Z = (-h * h * Res[11] + U[i, j + 1] + V * U[i + 1, j + 2] + V * U[i + 1, j] + U[i + 2, j + 1]) / VV;
                                if (Math.Abs(U[i + 1, j + 1] - Z) > E)
                                {
                                    E = Math.Abs(U[i + 1, j + 1] - Z);
                                }
                                U[i + 1, j + 1] = (float)Z;
                            }

                            // STEP 13
                            //F(X(N1+1),Y(J+1)) res[12]
                            parF[0] = X2[n1 + 1];
                            parF[1] = Y2[j + 1];
                            x3 = parF[0].ToString();
                            y3 = parF[1].ToString();
                            Argument x16 = new Argument("x", x3);
                            Argument y16 = new Argument("y", y3);
                            Expression expres14 = new Expression(tbPoiF.Text, x16, y16);
                            Res[12] = (float)(expres14.calculate());

                            //G(B,Y(J+1)) res[13]
                            parG[0] = (float)b;
                            parG[1] = Y2[j + 1];

                            x3 = parG[0].ToString();
                            y3 = parG[1].ToString();
                            Argument x15 = new Argument("x", x3);
                            Argument y15 = new Argument("y", y3);
                            Expression expres15 = new Expression(tbPoiG.Text, x15, y15);
                            Res[13] = (float)(expres15.calculate());

                            Z = (-h * h * Res[12] + Res[13] + U[n2 + 1, j + 1] + V * U[n1 + 1, j + 2] + V * U[n1 + 1, j]) / VV;
                            if (Math.Abs(U[n1 + 1, j + 1] - Z) > E)
                            {
                                E = Math.Abs(U[n1 + 1, j + 1] - Z);
                            }
                            U[n1 + 1, j + 1] = (float)Z;
                        }

                        // STEP 14
                        //F(X(2),Y(2)) res[14]
                        parF[0] = X2[2];
                        parF[1] = Y2[2];

                        x3 = parF[0].ToString();
                        y3 = parF[1].ToString();
                        Argument x17 = new Argument("x", x3);
                        Argument y17 = new Argument("y", y3);
                        Expression expres16 = new Expression(tbPoiF.Text, x17, y17);
                        Res[14] = (float)(expres16.calculate());

                        //G(X(2),C) res[15]
                        parG[0] = X2[2];
                        parG[1] = (float)c;

                        x3 = parG[0].ToString();
                        y3 = parG[1].ToString();
                        Argument x18 = new Argument("x", x3);
                        Argument y18 = new Argument("y", y3);
                        Expression expres17 = new Expression(tbPoiG.Text, x18, y18);
                        Res[15] = (float)(expres17.calculate());

                        //G(A,Y(2))
                        parG[0] = (float)a;
                        parG[1] = Y2[2];

                        x3 = parG[0].ToString();
                        y3 = parG[1].ToString();
                        Argument x19 = new Argument("x", x3);
                        Argument y19 = new Argument("y", y3);
                        Expression expres18 = new Expression(tbPoiG.Text, x19, y19);
                        Res[16] = (float)(expres18.calculate());


                        Z = (-h * h * Res[14] + V * Res[15] + Res[16] + V * U[2, 3] + U[3, 2]) / VV;
                        if (Math.Abs(U[2, 2] - Z) > E)
                        {
                            E = Math.Abs(U[2, 2] - Z);
                        }

                        U[2, 2] = (float)Z;

                        // STEP 15
                        for (i = 2; i <= n2; i++)
                        {
                            //F(X(I+1),Y(2)) res[17]
                            parF[0] = X2[i + 1];
                            parF[1] = Y2[2];
                            x3 = parF[0].ToString();
                            y3 = parF[1].ToString();
                            Argument x20 = new Argument("x", x3);
                            Argument y20 = new Argument("y", y3);
                            Expression expres19 = new Expression(tbPoiF.Text, x20, y20);
                            Res[17] = (float)(expres19.calculate());

                            //G(X(I+1),C) res[18]
                            parG[0] = X2[i + 1];
                            parG[1] = (float)c;
                            x3 = parG[0].ToString();
                            y3 = parG[1].ToString();
                            Argument x21 = new Argument("x", x3);
                            Argument y21 = new Argument("y", y3);
                            Expression expres20 = new Expression(tbPoiG.Text, x21, y21);
                            Res[18] = (float)(expres20.calculate());

                            Z = (-h * h * Res[17] + V * Res[18] + U[i + 2, 2] + U[i, 2] + V * U[i + 1, 3]) / VV;
                            if (Math.Abs(U[i + 1, 2] - Z) > E)
                            {
                                E = Math.Abs(U[i + 1, 2] - Z);
                            }
                            U[i + 1, 2] = (float)Z;
                        }

                        //STEP 16
                        //F(X(N1+1),Y(2)) res[19]
                        parF[0] = X2[n1 + 1];
                        parF[1] = Y2[2];
                        x3 = parF[0].ToString();
                        y3 = parF[1].ToString();
                        Argument x22 = new Argument("x", x3);
                        Argument y22 = new Argument("y", y3);
                        Expression expres21 = new Expression(tbPoiF.Text, x22, y22);
                        Res[19] = (float)(expres21.calculate());

                        //G(X(N1+1),C) res[20]
                        parG[0] = X2[n1 + 1];
                        parG[1] = (float)c;
                        x3 = parG[0].ToString();
                        y3 = parG[1].ToString();
                        Argument x23 = new Argument("x", x3);
                        Argument y23 = new Argument("y", y3);
                        Expression expres22 = new Expression(tbPoiG.Text, x23, y23);
                        Res[20] = (float)(expres22.calculate());

                        //G(B,Y(2)) res[21]
                        parG[0] = (float)b;
                        parG[1] = Y2[2];
                        x3 = parG[0].ToString();
                        y3 = parG[1].ToString();
                        Argument x24 = new Argument("x", x3);
                        Argument y24 = new Argument("y", y3);
                        Expression expres23 = new Expression(tbPoiG.Text, x24, y24);
                        Res[21] = (float)(expres23.calculate());

                        Z = (-h * h * Res[19] + V * Res[20] + Res[21] + U[n2 + 1, 2] + V * U[n1 + 1, 3]) / VV;

                        if (Math.Abs(U[n1 + 1, 2] - Z) > E)
                        {
                            E = Math.Abs(U[n1 + 1, 2] - Z);
                        }

                        U[n1 + 1, 2] = (float)Z;



                        // STEP 17
                        if (E <= tol)
                        {
                            // Decimal

                            if (rbDecPoi.Checked)
                            {

                                hasil = "";
                                for (i = 1; i <= n1; i++)
                                {
                                    for (j = 1; j <= m1; j++)
                                    {
                                        hasil = hasil + i.ToString() + "         " + j.ToString() + "          " + String.Format("{0:0.000}", X2[i+1]) + "            " + String.Format("{0:0.000}", Y2[j+1]) + "            " + String.Format("{0:0.000000000000000}", U[i+1, j+1]) + "\n";
                                    }
                                }

                                rtbPoisson.Clear();
                                rtbPoisson.Text = "i            j               x i                   y j                          U( i , j )" + "\n\n" + hasil;
                            }
                            else
                            { //format exponent
                                hasil = "";
                                for (j = 1; j <= m1; j++)
                                {
                                    for (i = 1; i <= n1; i++)
                                    {
                                        hasil = hasil + i.ToString() + "         " + j.ToString() + "          " + String.Format("{0:0.000}", X2[i+1]) + "            " + String.Format("{0:0.000}", Y2[j+1]) + "            " + String.Format("{0:0.00000000000000E+00}", U[i+1, j+1]) + "\n";
                                    }
                                }

                                rtbPoisson.Clear();
                                rtbPoisson.Text = "i            j              x i                   y j                              U( i , j )" + "\n\n" + hasil;
                            }

                            lbMsg.Text = "Convergence occurred on iteration number : " + L.ToString();

                            //Plot Graph 3D
                            pe3do4.PeFunction.Reset();
                            pe3do4.PePlot.PolyMode = Gigasoft.ProEssentials.Enums.PolyMode.SurfacePolygons;
                            pe3do4.PeConfigure.PrepareImages = true;

                            // Set the amount of data and pass data
                            pe3do4.PeData.Subsets = m1;
                            pe3do4.PeData.Points = n1;



                            for (i = 1; i <= n1; i++)
                            {
                                for (j = 1; j <= m1; j++)
                                {
                                    pe3do4.PeData.X[j - 1, i - 1] = (float)X2[i+1];
                                    pe3do4.PeData.Z[j - 1, i - 1] = (float)Y2[j+1];
                                    pe3do4.PeData.Y[j - 1, i - 1] = (float)U[i+1,j+1];
                                }
                            }


                            pe3do4.PePlot.ViewingHeight = 30;
                            pe3do4.PePlot.DegreeOfRotation = 35;
                            pe3do4.PePlot.Method = Gigasoft.ProEssentials.Enums.ThreeDGraphPlottingMethod.Four;


                            pe3do4.PeString.MainTitle = "";
                            pe3do4.PeString.SubTitle = "";

                            pe3do4.PeString.XAxisLabel = "x (x-axis)";
                            pe3do4.PeString.ZAxisLabel = "y (z-axis)";
                            pe3do4.PeString.YAxisLabel = "U (y-axis)";

                            pe3do4.PeConfigure.RenderEngine = RenderEngine.Direct3D;
                            pe3do4.PeColor.BitmapGradientMode = true;
                            pe3do4.PeColor.QuickStyle = QuickStyle.DarkNoBorder;


                            pe3do4.PeFunction.ReinitializeResetImage();
                            pe3do4.Refresh();


                            // STEP 19
                            //OK = false;
                            OK = true;
                                  //tvmsg.setText(Integer.toString(L));
                                  //break;
                        }
                        else
                            // STEP 20
                            L = L + 1;
                  

                    }

                    // STEP 21
                    if (L > iter)
                    {
                        lbMsg.Text= "Method fails after iteration number : " + iter.ToString() + "; Increase Max Iteration !!!";
                        return;
                        
                    }
                }
            }

            catch (Exception)
            {

                MessageBox.Show("Something wrong with input(check again) !!!");
                return;
            }

        }

        private void btClearPoisson_Click(object sender, EventArgs e)
        {
            tbPoiF.Clear();
            tbPoiG.Clear();
            tbPoiA.Clear();
            tbPoiB.Clear();
            tbPoiC.Clear();
            tbPoiD.Clear();
            tbPoiM.Clear();
            tbPoiN.Clear();
            tbTol.Clear();
            tbIter.Clear();
            rtbPoisson.Clear();
            pe3do4.Refresh();
            lbMsg.Text = "It takes time...";
        }
    }


}

 
 