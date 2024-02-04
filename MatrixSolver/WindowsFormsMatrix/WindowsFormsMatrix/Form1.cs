using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using lglib;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //private int additionalmemory;

        public Form1()
        {
            InitializeComponent();
        }

        //Multiplication

        private void btActive_Click(object sender, EventArgs e)
        {
            //nix2
            int m, n, q, z;

            try
            {
                m = int.Parse(tbM.Text);
                n = int.Parse(tbN.Text);
                q = int.Parse(tbQ.Text);

                 if ((m > 100) || (n > 100) || (q > 100))
                {
                    MessageBox.Show("Maximum m or n or q is 100 !!!");
                    return;
                }
          
                dataGV.Rows.Clear();
                dataGV.Refresh();

                if (n > q)
                    z = n;
                else z = q;

                dataGV.ColumnCount = 2 * z + 5; //view grid only
                dataGV.RowCount = 2 * m + q + 5;

                dataGV.Rows[0].HeaderCell.Value = ("Matrix A");
                dataGV.Rows[m + 1].HeaderCell.Value = ("Matrix B");

                //start input
                dataGV[0, 0].Value = "A start from here";
                dataGV[0, m + 1].Value = "B start from here";
            }

            catch (Exception)
            {
                MessageBox.Show("Something wrong with the input !!!");
                return;
            }


        }

        private void btExample_Click(object sender, EventArgs e)
        {
            
            int m, n, q, m2, z;


            tbM.Text = "3"; //example A ordo 3 x 2 ; B ordo 2 x 3 hasil C ordo 3 x 3
            tbN.Text = "2";
            tbQ.Text = "3";

            m = int.Parse(tbM.Text);
            n = int.Parse(tbN.Text);
            q = int.Parse(tbQ.Text);

            m2 = m + 1; //posisi matrix B

            if (n > q)
                z = n;
            else z = q;

            dataGV.ColumnCount = 2 * z + 5; //view grid only
            dataGV.RowCount = 2 * m + q + 5;

            dataGV.Rows[0].HeaderCell.Value = ("Matrix A");
            dataGV.Rows[m + 1].HeaderCell.Value = ("Matrix B");


            //Example Matrix A (3 x 2)
            dataGV[0, 0].Value = 1;  //dataGV[col 0,row 0]
            dataGV[1, 0].Value = -2;
            dataGV[0, 1].Value = 3;
            dataGV[1, 1].Value = 4;
            dataGV[0, 2].Value = 7;
            dataGV[1, 2].Value = -5;

            //Example Matrix B (2 x 3)
            dataGV[0, m2].Value = 3;
            dataGV[0, m2 + 1].Value = -7;
            dataGV[1, m2].Value = -3;
            dataGV[1, m2 + 1].Value = 6;
            dataGV[2, m2].Value = 8;
            dataGV[2, m2 + 1].Value = -9;

        }

        private void btRun_Click(object sender, EventArgs e)
        {
            int i, j, k, m, n, q, m2, z;


            Double[,] A = new Double[101, 101];
            Double[,] B = new Double[101, 101];
            Double[,] C = new Double[101, 101];

            try
            {

                m = int.Parse(tbM.Text);
                n = int.Parse(tbN.Text);
                q = int.Parse(tbQ.Text);

                if (n > q)
                    z = n;
                else z = q;

                dataGV.ColumnCount = 2 * z + 5; 
                dataGV.RowCount = 2 * m + q + 5;

                
                if ((m > 100) || (n > 100) || (q > 100))
                {
                    MessageBox.Show("Maximum m or n or q is 100 !!!");
                    return;
                }

                m2 = m + 1; //posisi matrix B
                z = m + n + 2;


                dataGV.ColumnCount = 2 * z + 5; 
                dataGV.RowCount = 2 * m + q + 5;
                                
                //A
                for (i = 0; i <= m - 1; i++)
                {
                    for (j = 0; j <= n - 1; j++)
                    {
                        A[i + 1, j + 1] = Double.Parse(dataGV[j, i].Value.ToString());
                    }
                }

                
                //B
                for (i = 0; i <= n - 1; i++)
                {
                    for (j = 0; j <= q - 1; j++)
                    {
                        B[i + 1, j + 1] = Double.Parse(dataGV[j, i + m2].Value.ToString());
                    }
                }

                
                //C ( C = A x B )
                for (i = 1; i <= m; i++)
                {
                    for (k = 1; k <= q; k++)
                    {
                        C[i, k] = 0;
                        for (j = 1; j <= n; j++)
                        {
                            C[i, k] = C[i, k] + A[i, j] * B[j, k];
                        }
                    }
                }
                
                //Result (Display Matrix C)
                for (i = 0; i <= m - 1; i++)
                {
                    for (k = 0; k <= q - 1; k++)
                    {
                        dataGV[k, i + m + n + 2].Value = C[i + 1, k + 1].ToString();

                    }
                }


                dataGV.Rows[m + n + 2].HeaderCell.Value = ("Matrix C");
            }

            catch (Exception)
            {
                MessageBox.Show("Something wrong with the input !!!");
                return;
            }
        }

        private void btClear_Click(object sender, EventArgs e)
        {

            tbM.Clear();
            tbN.Clear();
            tbQ.Clear();

            dataGV.Rows.Clear();
            dataGV.Refresh();

        }

        //End Multiplication

        private void btAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("|======== Matrix Solver ========|" + Environment.NewLine + Environment.NewLine +
                          "Version 1.0 - build jan 23, 2023." + Environment.NewLine +
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

        private void Form1_Load(object sender, EventArgs e)
        {
            tbM.Focus();
        }

        //SVD
        private void butExample_Click(object sender, EventArgs e)
        {

            int m, n;
            double[,] a = new double[101, 101];
            double[] w = new double[101];
            double[,] u = new double[101, 101];
            double[,] vt = new double[101, 101];

            tbSVDm.Text = "3"; 
            tbSVDn.Text = "2";

            m = int.Parse(tbSVDm.Text);
            n = int.Parse(tbSVDn.Text);

            dgvSVD.ColumnCount = 2 * n + 5; //view grid only
            dgvSVD.RowCount = 4 * m + 5;


            dgvSVD.Rows[0].HeaderCell.Value = ("Matrix A");
            dgvSVD[0, 0].Value = 1; // 
            dgvSVD[1, 0].Value = -2;
            dgvSVD[0, 1].Value = 3;
            dgvSVD[1, 1].Value = 4;
            dgvSVD[0, 2].Value = 7;
            dgvSVD[1, 2].Value = -5;


            //keterangan: Jika dikalikan: U x W x VT = A  (Sesuai dekomposisinya); 

        }

        private void butActive_Click(object sender, EventArgs e)
        {
            int m, n;

            try
            {

                m = int.Parse(tbSVDm.Text);
                n = int.Parse(tbSVDn.Text);

                if ((m > 100) || (n > 100))
                {
                    MessageBox.Show("Maximum m or n is 100 !!!");
                    return;
                }

                dgvSVD.Rows.Clear();
                dgvSVD.Refresh();

                //alglib.svd.rmatrixsvd


                dgvSVD.ColumnCount = 2 * n + 5; //view grid only
                dgvSVD.RowCount = 2 * m + 5;

                dgvSVD.Rows[0].HeaderCell.Value = ("Matrix A");

                //start input
                dgvSVD[0, 0].Value = "A start from here";
            }

            catch (Exception)
            {
                MessageBox.Show("Something wrong with the input !!!");
                return;
            }

        }

        private void butRun_Click(object sender, EventArgs e)
        {
            int i, j, m, n;
            double[,] a = new double[101, 101];
            double[] w = new double[101];
            double[,] u = new double[101, 101];
            double[,] vt = new double[101, 101];

            try
            {

                m = int.Parse(tbSVDm.Text);
                n = int.Parse(tbSVDn.Text);

                if ((m > 100) || (n > 100))
                {
                    MessageBox.Show("Maximum m or n is 100 !!!");
                    return;
                }

                dgvSVD.ColumnCount = 2 * n + 5; //view grid only
                dgvSVD.RowCount = 4 * m + 5;

                //Matrix a
                for (i = 0; i <= m - 1; i++)
                {
                    for (j = 0; j <= n - 1; j++)
                    {
                        a[i, j] = double.Parse(dgvSVD[j, i].Value.ToString()); //index dimulai [0,0]
                    }
                }

                alglib.svd.rmatrixsvd(a, m, n, 2, 2, 2, ref w, ref u, ref vt, null); //keterangan parameter 2,2,2 lihat di public class svd(file linalg.cs) lib alglib
                                                                                    
                //A = U x W x VT (dekomposisi matrix A ); VT is V Transpose
                //A(mxn) = U(mxm) x W(mxn) x VT(nxn)

                //Result Matrix u 
                dgvSVD.Rows[m + 1].HeaderCell.Value = ("Matrix U");

                for (i = 0; i <= m - 1; i++)
                {
                    for (j = 0; j <= m - 1; j++)
                    {
                        dgvSVD[j, m + 1 + i].Value = u[i, j].ToString();
                    }
                }

                //Result W (matrix diagonal m x n (artinya hanya punya value di diagonal ,sisanya nol(0) 
                dgvSVD.Rows[2 * m + 2].HeaderCell.Value = ("Matrix W");


                for (i = 0; i <= m - 1; i++)
                {
                    for (j = 0; j <= n - 1; j++)
                    {
                        if (i == j)
                        {
                            dgvSVD[j, 2 * m + i + 2].Value = w[i].ToString();
                        }
                        else
                        {
                            dgvSVD[j, 2 * m + i + 2].Value = 0;

                        }
                    }
                }

                //Result vt
                dgvSVD.Rows[3 * m + 3].HeaderCell.Value = ("Matrix VT");

                for (i = 0; i <= n - 1; i++)
                {
                    for (j = 0; j <= n - 1; j++)
                    {
                        dgvSVD[j, 3 * m + 3 + i].Value = vt[i, j].ToString();
                    }
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
            tbSVDm.Clear();
            tbSVDn.Clear();

            dgvSVD.Rows.Clear();
            dgvSVD.Refresh();
        }
        //End SVD


        //Eigenpairs
        private void btEigenExample_Click(object sender, EventArgs e)
        {

            int n;
            double[,] a = new double[101, 101];
            double[] w = new double[101];
            double[,] u = new double[101, 101];
            double[,] vt = new double[101, 101];

            tbEigenN.Text = "3"; //ordo 3 x 3
            tbMaxIter.Text = "50";

            n = int.Parse(tbEigenN.Text);

            dgvEigen.ColumnCount = 2 * n + 5; //view grid only
            dgvEigen.RowCount = 4 * n + 5;

            dgvEigen.Rows[0].HeaderCell.Value = ("Matrix A");
            dgvEigen[0, 0].Value = 3;  
            dgvEigen[1, 0].Value = -1;
            dgvEigen[2, 0].Value = 0;  
            dgvEigen[0, 1].Value = -1;
            dgvEigen[1, 1].Value = 2;
            dgvEigen[2, 1].Value = -1;
            dgvEigen[0, 2].Value = 0;
            dgvEigen[1, 2].Value = -1;
            dgvEigen[2, 2].Value = 3;

        }

        private void btEigenActive_Click(object sender, EventArgs e)
        {
            int n, maxIter;
            try
            {

                n = int.Parse(tbEigenN.Text);
                maxIter = int.Parse(tbMaxIter.Text);

            
                if (n > 100)
                {
                    MessageBox.Show("Maximum n is 100 !!!");
                    return;
                }
                if (maxIter > 100)
                {
                    MessageBox.Show("Maximum of max iteration is 100 !!!");
                    return;
                }

                dgvEigen.Rows.Clear();
                dgvEigen.Refresh();

                dgvEigen.ColumnCount = 2 * n + 5; //view grid only
                dgvEigen.RowCount = 2 * n + 5;

                dgvEigen.Rows[0].HeaderCell.Value = ("Matrix A");

                //start input
                dgvEigen[0, 0].Value = "A start from here";
                
            }
            
            catch (Exception)
            {
                MessageBox.Show("Something wrong with the input !!!");
                return;
            }
        }

        private void btEigenClear_Click(object sender, EventArgs e)
        {
            tbEigenN.Clear();
            tbMaxIter.Clear();

            dgvEigen.Rows.Clear();
            dgvEigen.Refresh();
        }


        private void btEigenRun_Click(object sender, EventArgs e)
        {
            int n, j, i, k, maxIter;
            double C1, lambda, MaxElement;

            double[] Y = new double[101];
            double[] X = new double[101];
            double[,] a = new double[101, 101];

            try
            {
                n = int.Parse(tbEigenN.Text);
                maxIter = int.Parse(tbMaxIter.Text);

                for (j = 0; j < n; j++)
                    X[j] = 1;


                //matrix A
                for (i = 0; i < n; i++)
                    for (j = 0; j < n; j++)
                        a[i, j] = double.Parse(dgvEigen[j, i].Value.ToString());

                lambda = 0;

                for (i = 0; i < maxIter; i++)
                {
                    {
                        for (k = 0; k < n; k++)
                        {
                            Y[k] = 0;
                            for (j = 0; j < n; j++)
                                Y[k] = Y[k] + a[k, j] * X[j];
                        }


                        MaxElement = 0;
                        for (j = 0; j < n; j++)
                        {
                            if (Math.Abs(Y[j]) > Math.Abs(MaxElement))
                                MaxElement = Y[j];
                        }

                        C1 = MaxElement;
                        for (k = 0; k < n; k++)
                            Y[k] = (1 / C1) * Y[k];

                        for (k = 0; k < n; k++)
                            X[k] = Y[k];
                        lambda = C1;
                    }

                    //eigenvalue λ 1 ot λ max
                    dgvEigen.Rows[n + 1].HeaderCell.Value = ("λ 1 ( Lambda 1 ) = ");
                    dgvEigen[0, n + 1].Value = lambda;

                    //eigenvector X 1
                    dgvEigen.Rows[n + 3].HeaderCell.Value = ("X 1 ( Vector of λ 1 ) = ");
                    for (k = 0; k < n; k++)
                        dgvEigen[0, n + 3 + k].Value = X[k];

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something wrong with the input !!!");
                return;
            }

        }
        //End Eigenpairs


     
        //Determinant using LU Decomposition

        private void btDetExam_Click(object sender, EventArgs e)
        {

            int n;
    
            tbDetN.Text = "3"; //ordo 3 x 3

            n = int.Parse(tbDetN.Text);

            dgvDet.ColumnCount = 2 * n + 5; //view grid only
            dgvDet.RowCount = 4 * n + 5;

            dgvDet.Rows[0].HeaderCell.Value = ("Matrix A");
            dgvDet[0, 0].Value = 5;  
            dgvDet[1, 0].Value = -2;
            dgvDet[2, 0].Value = 8;  
            dgvDet[0, 1].Value = -7;
            dgvDet[1, 1].Value = 0;
            dgvDet[2, 1].Value = 4;
            dgvDet[0, 2].Value = 6;
            dgvDet[1, 2].Value = -5;
            dgvDet[2, 2].Value = 3;

        }

        private void btDetAct_Click(object sender, EventArgs e)
        {
            int n;
             try
            {
                n = int.Parse(tbDetN.Text);

                if (n > 100)
                {
                    MessageBox.Show("Maximum n is 100 !!!");
                    return;
                }


                dgvDet.Rows.Clear();
                dgvDet.Refresh();

                dgvDet.ColumnCount = 2 * n + 5; //view grid only
                dgvDet.RowCount = 2 * n + 5;


                dgvDet.Rows[0].HeaderCell.Value = ("Matrix A");

                //start input
                dgvDet[0, 0].Value = "A start from here";

            }


            catch (Exception)
            {
                MessageBox.Show("Something wrong with the input !!!");
                return;
            }
        }

        private void btDetClr_Click(object sender, EventArgs e)
        {
            ResultDet.Text = "Determinant =  ?";
            tbDetN.Clear();
            
            dgvDet.Rows.Clear();
            dgvDet.Refresh();

        }

        private void btDetRun_Click(object sender, EventArgs e)
        {
            int i,j,k,n,l;
     
            double ratio, det;

            double[,] a = new double[101, 101];


            try
            {

                n = int.Parse(tbDetN.Text);

                //matrix A
                for (i = 0; i < n; i++)
                    for (j = 0; j < n; j++)
                        a[i, j] = double.Parse(dgvDet[j, i].Value.ToString());

                for (i = 0; i < n; i++)
                    for (j = i + 1; j < n; j++)
                    {
                        ratio = a[j, i] / a[i, i];
                        for (k = 0; k < n; k++)
                            a[j, k] = a[j, k] - ratio * a[i, k];
                    }

                det = 1;

                for (l = 0; l < n; l++)
                    det = det * a[l, l];

                ResultDet.Text = "Determinant = " + det.ToString();
            

            }
            catch (Exception)
            {
                MessageBox.Show("Something wrong with the input !!!");
                return;
            }

        }
        //End Determinant


        //Inverse
        private void btExpInv_Click(object sender, EventArgs e)
        {

            int n;

            tbInvN.Text = "3"; //ordo 3 x 3

            n = int.Parse(tbInvN.Text);

            dgvInv.ColumnCount = 2 * n + 5; //view grid only
            dgvInv.RowCount = 4 * n + 5;

            dgvInv.Rows[0].HeaderCell.Value = ("Matrix A");
            dgvInv[0, 0].Value = 3;  
            dgvInv[1, 0].Value = 4;
            dgvInv[2, 0].Value = 5;  
            dgvInv[0, 1].Value = 1;
            dgvInv[1, 1].Value = 6;
            dgvInv[2, 1].Value = -8;
            dgvInv[0, 2].Value = 3;
            dgvInv[1, 2].Value = 2;
            dgvInv[2, 2].Value = 0;

        }

        private void btClrInv_Click(object sender, EventArgs e)
        {
            tbInvN.Clear();

            dgvInv.Rows.Clear();
            dgvInv.Refresh();

        }

        private void btActInv_Click(object sender, EventArgs e)
        {
            int n;
            try
            {
                n = int.Parse(tbInvN.Text);

                if (n > 100)
                {
                    MessageBox.Show("Maximum n is 100 !!!");
                    return;
                }


                dgvInv.Rows.Clear();
                dgvInv.Refresh();

                dgvInv.ColumnCount = 2 * n + 5; //view grid only
                dgvInv.RowCount = 2 * n + 5;


                dgvInv.Rows[0].HeaderCell.Value = ("Matrix A");

                //start input
                dgvInv[0, 0].Value = "A start from here";

            }


            catch (Exception)
            {
                MessageBox.Show("Something wrong with the input !!!");
                return;
            }
        }

        private void btRunInv_Click(object sender, EventArgs e)
        {
            int i, n, j, k;
            double pv, y;

            try
            {

                n = int.Parse(tbInvN.Text);
                double[,] a = new double[101, 101];


                //matrix A
                for (i = 0; i < n; i++)
                    for (j = 0; j < n; j++)
                        a[i, j] = double.Parse(dgvInv[j, i].Value.ToString());

                //inverse operation
                for (i = 0; i < n; i++)
                {
                    pv = a[i, i];
                    a[i, i] = 1;
                    for (j = 0; j < n; j++)
                        a[i, j] = a[i, j] / pv;
                    for (k = 0; k < n; k++)
                    {
                        if (k != i)
                        {
                            y = a[k, i];
                            a[k, i] = 0;
                            for (j = 0; j < n; j++)
                                a[k, j] = a[k, j] - y * a[i, j];

                        }
                    }
                }


                //Result inverse A
                dgvInv.Rows[n + 1].HeaderCell.Value = ("Inverse ( A )");

                for (i = 0; i < n; i++)
                    for (j = 0; j < n; j++)
                        dgvInv[j, n + i + 1].Value = a[i, j];
            }

            catch (Exception)
            {
                MessageBox.Show("Something wrong with the input !!!");
                return;
            }

        }
        //End Inverse

        //Linear System
        private void btExpLinear_Click(object sender, EventArgs e)
        {

            int i,n;

            tbLinearN.Text = "3"; //ordo 3 x 3

            n = int.Parse(tbLinearN.Text);

            dgvLinear.ColumnCount = 2 * n + 5; //view grid only
            dgvLinear.RowCount = 4 * n + 5;

            
            for (i= 0;i< n;i++)
            {
               dgvLinear.Rows[i].HeaderCell.Value ="Equation-"+(i+1).ToString();
               dgvLinear.Columns[i].HeaderText = "A "+(i+1).ToString()+" X "+ (i+1).ToString();
            }

            dgvLinear.Columns[n].HeaderText = "B" ;
            
            dgvLinear[0, 0].Value = 2;  
            dgvLinear[1, 0].Value = -3;
            dgvLinear[2, 0].Value = 1;
            dgvLinear[3, 0].Value = 11; //B1

            dgvLinear[0, 1].Value = 1;
            dgvLinear[1, 1].Value = 2;
            dgvLinear[2, 1].Value = -2;
            dgvLinear[3, 1].Value = -9; //B2

            dgvLinear[0, 2].Value = 3;
            dgvLinear[1, 2].Value = 2;
            dgvLinear[2, 2].Value = -3;
            dgvLinear[3, 2].Value = -10; //B3

        }

      
        private void btClearLinear_Click(object sender, EventArgs e)
        {

            tbLinearN.Clear();
            dgvLinear.Rows.Clear();
            dgvLinear.Columns.Clear();
            dgvLinear.Refresh();
        }

        private void btActLinear_Click(object sender, EventArgs e)
        {

            int i, n;
            try
            {
                n = int.Parse(tbLinearN.Text);

                if (n > 100)
                {
                    MessageBox.Show("Maximum n is 100 !!!");
                    return;
                }


                dgvLinear.Rows.Clear();
                dgvLinear.Refresh();

                dgvLinear.ColumnCount = 2 * n + 5; //view grid only
                dgvLinear.RowCount = 2 * n + 5;

                for (i = 0; i < n; i++)
                {
                    dgvLinear.Rows[i].HeaderCell.Value = "Equation-" + (i + 1).ToString();
                    dgvLinear.Columns[i].HeaderText = "A " + (i + 1).ToString() + " X " + (i + 1).ToString();
                }

                dgvLinear.Columns[n].HeaderText = "B i"; //from 1 to n


            }


            catch (Exception)
            {
                MessageBox.Show("Something wrong with the input !!!");
                return;
            }

        }

        private void btRunLinear_Click(object sender, EventArgs e)
        {
          int  i, j, k, n;
          double Ratio;
          double [,] a = new double [101,101];
          double[] X = new double[101];

            try
            {
                n = int.Parse(tbLinearN.Text);

                for (i = 0; i < n; i++)
                   for (j = 0; j <= n; j++)
                       a[i, j] = double.Parse(dgvLinear[j, i].Value.ToString());
                
                

                for (i = 0; i < n; i++)
                {
                    if (a[i, i] == 0)
                    {
                        MessageBox.Show("Division by zero (Undefined), if possible try to reorder position of equations,so on the diagonal of matrix A, no zero value !!!");
                        return;
                    }


                    for (j = 0; j < n; j++)
                    {
                        if (i != j)
                        {
                            Ratio = a[j, i] / a[i, i];
                            for (k = 0; k <= n; k++)
                                a[j, k] = a[j, k] - Ratio * a[i, k];

                        }
                    }
                }

                X[1] = 0;

                for (i = 0; i < n; i++)
                {
                    X[i] = a[i, n] / a[i, i];
                    dgvLinear.Rows[i+n+1].HeaderCell.Value = "X " + (i + 1).ToString()+" = ";
                    dgvLinear[0, n + 1 + i].Value = X[i];
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Something wrong with the input !!!");
                return;
            }

        }
        //End Linear System

    }
}

