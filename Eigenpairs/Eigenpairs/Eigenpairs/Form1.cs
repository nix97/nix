using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eigenpairs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void butExample_Click(object sender, EventArgs e)
        {

            int i, n;
        
            

            tb_n.Text = "4";
            tb_epsilon.Text = "1e-07";

            n = int.Parse(tb_n.Text);
         
            dgv.ColumnCount = 3 * n; //view grid only
            dgv.RowCount = 5 * n;

            for (i = 1; i <= n; i++)
            {
                dgv.Rows[i - 1].HeaderCell.Value = ("Row-") + i.ToString();
                dgv.Columns[i - 1].HeaderCell.Value = ("Col-") + i.ToString();
            }

          
            //Example Matrix A (4 x 4)
            //row 1
            dgv[0, 0].Value = 8; //dgv[col 0,row 0]; matrix A[1,1]
            dgv[1, 0].Value = -1;
            dgv[2, 0].Value = 3;
            dgv[3, 0].Value = -1;

            //row 2
            dgv[0, 1].Value = -1;
            dgv[1, 1].Value = 6;
            dgv[2, 1].Value = 2;
            dgv[3, 1].Value = 0;

            dgv[0, 2].Value = 3;
            dgv[1, 2].Value = 2;
            dgv[2, 2].Value = 9;
            dgv[3, 2].Value = 1;

            dgv[0, 3].Value = -1;
            dgv[1, 3].Value = 0;
            dgv[2, 3].Value = 1;
            dgv[3, 3].Value = 7;



            

        }

        private void butRun_Click(object sender, EventArgs e)
        {
            int i, j, k, p, q, n;
            double epsilon, Sum, RMS, T, Theta, C, S;
            double[,] A = new double[101, 101];
            double[,] V = new double[101, 101];
            double[] XP = new double[101];
            double[] XQ = new double[101];
            double[] YP = new double[101];
            double[] YQ = new double[101];
            bool state;

          
            try { 

            n = int.Parse(tb_n.Text);
            epsilon = double.Parse(tb_epsilon.Text);

            if (epsilon > 0.1)
                {
                    MessageBox.Show("Maximum of epsilon is 0.1 !!!");
                    return;
                }
                    
            if (epsilon <1e-15)
                {
                  MessageBox.Show("Minimum of epsilon is 1e-15 !!!");
                  return;
                }
           
                
             if (n > 100)
                {
                    MessageBox.Show("Maximum of n is 100 !!!");
                    return;
                }





            dgv.ColumnCount = 3 * n; //view grid only
            dgv.RowCount = 12 * n;

            //matrix A
            for (i = 0; i < n; i++)
                for (j = 0; j < n; j++)
                    A[i+1, j+1] = double.Parse(dgv[j, i].Value.ToString());

            //Matrix Identity
            for (j = 1; j <= n; j++)
            {
                for (k = 1; k <= n; k++)
                {
                    if (j != k)
                        V[j, k] = 0;
                    else
                        V[j, k] = 1;
                }
            }
            
                       

            state = true;
            while (state==true)
            
            {
                
                
                
                Sum = 0;
                for (j = 1; j <= n; j++)
                {
                    Sum = Sum + A[j, j] * A[j, j];
                }

                RMS = Math.Sqrt(Sum / n);
                T = RMS;

                
                for (p = 1; p <= n - 1; p++)
                {
                    for (q = p + 1; q <= n; q++)
                    {
                        if (Math.Abs((A[p, q] / T)) > epsilon)

                        {
                            Theta = (A[q, q] - A[p, p]) / (2 * A[p, q]);
                            T = Math.Sign(Theta) / (Math.Abs(Theta) + Math.Sqrt(Theta * Theta + 1));
                            C = 1 / Math.Sqrt(T * T + 1);
                            S = C * T;

                            for (j = 1; j <= n; j++)
                            {
                                XP[j] = A[j, p];
                                XQ[j] = A[j, q];
                            }

                            A[p, q] = 0;
                            A[q, p] = 0;
                            A[p, p] = C * C * XP[p] + S * S * XQ[q] - 2 * C * S * XQ[p];
                            A[q, q] = S * S * XP[p] + C * C * XQ[q] + 2 * C * S * XQ[p];

                            for (j = 1; j <= n; j++)
                            {

                                if ((j != p) && (j != q))
                                {
                                    A[j, p] = C * XP[j] - S * XQ[j];
                                    A[p, j] = A[j, p];
                                    A[j, q] = C * XQ[j] + S * XP[j];
                                    A[q, j] = A[j, q];
                                }
                            }

                            for (j = 1; j <= n; j++)
                            {
                                YP[j] = V[j, p];
                                YQ[j] = V[j, q];
                            }
                           
                            for (j = 1; j <= n; j++)
                            {
                                V[j, p] = C * YP[j] - S * YQ[j];
                                V[j, q] = S * YP[j] + C * YQ[j];
                            }
                            state = true;
                          
                        }
                        else state = false;
                       
                    }

                }



            }
            //Result
            dgv.Rows[n + 1].HeaderCell.Value = ("Result :");


            //Eigenvalues or Lambda(λ)

            dgv.Rows[n + 2].HeaderCell.Value = ("Eigenvalues( λ ) :");
            dgv.Rows[n + 3].HeaderCell.Value = ("(diagonal elements)");

                for (i = 1; i <= n; i++)
            {
                for (j = 1; j <= n; j++)
                {
                    if (i != j) dgv[j - 1, i + n + 1].Value = 0;
                    else
                        dgv[j - 1, i + n + 1].Value = A[j, i].ToString();
                }
            }

            //Eigenvectora or V (need to transpose)
            dgv.Rows[2*n + 3].HeaderCell.Value = ("Eigenvectors( V ) :");
            dgv.Rows[2 * n + 4].HeaderCell.Value = ("(columns elements)");

                for (i = 1; i <= n; i++)
            {
                   for (j = 1; j <= n; j++)
                   {
                    dgv[j - 1, 2* n + 2+i].Value = V[i, j].ToString();
                   }
            }

                

                
            }
            catch (Exception)
            {
                MessageBox.Show("Something wrong with the input !!!");
                return;
            }


        }

      private void butView_Click(object sender, EventArgs e)
        {
            int i, n;
            double epsilon;

            try
            {
                dgv.Rows.Clear();
                dgv.Refresh();
                n = int.Parse(tb_n.Text);
              
                epsilon = double.Parse(tb_epsilon.Text);

                if (epsilon > 0.1)
                {
                    MessageBox.Show("Maximum of epsilon is 0.1 !!!");
                    return;
                }

                if (epsilon < 1e-15)
                {
                    MessageBox.Show("Minimum of epsilon is 1e-15 !!!");
                    return;
                }


                if (n > 100)
                {
                    MessageBox.Show("Maximum of n is 100 !!!");
                    return;
                }

                dgv.ColumnCount = 3 * n; //view grid only
                dgv.RowCount = 5 * n;

                for (i = 1; i <= n; i++)
                {
                    dgv.Rows[i - 1].HeaderCell.Value = ("Row-") + i.ToString();
                    dgv.Columns[i - 1].HeaderCell.Value = ("Col-") + i.ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something wrong with the input !!!");
                return;
            }
        }

        private void butClear_Click(object sender, EventArgs e)
        {
            tb_n.Clear();
            tb_epsilon.Clear();
           
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            dgv.Refresh();
        }

        private void butAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("|======== Eigenpairs Solver ========|" + Environment.NewLine + Environment.NewLine +
                          "Version 1.0 - build Oct 15, 2023." + Environment.NewLine +
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
    }
}
