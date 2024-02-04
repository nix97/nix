using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using org.mariuszgromada.math.mxparser;
using System.Windows.Forms.DataVisualization.Charting;





namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }







        private void butRun_Click(object sender, EventArgs e)
        {



            Double a, b, h,step;
            int k, m,sm;
            string t2, x2, y2, hasil;


            Double tVal, xVal, yVal, f1, f2, f3, f4, g1, g2, g3, g4;
            Double XExact, YExact,Xmin,Ymax,Chk,Chk2;

            Double[] Tk = new Double[10001];
            Double[] Tkm = new Double[10001]; //exact smooth graph
            Double[] Xk = new Double[10001];
            Double[] Yk = new Double[10001];
            Double[] XkExact = new Double[10001];
            Double[] YkExact = new Double[10001];
            Double[] XkmExact = new Double[10001];//exact smooth graph
            Double[] YkmExact = new Double[10001];//exact smooth graph
            Double[] XRelEr = new Double[10001];
            Double[] YRelEr = new Double[10001];


        try
            {
            a = Double.Parse(atb.Text); //atb=a textboxt
            b = Double.Parse(btb.Text);
            m = int.Parse(mtb.Text);

            Xk[0] = Double.Parse(xt0tb.Text);
            Yk[0] = Double.Parse(yt0tb.Text);

                { //t,x,y need close/block for separate another block that use same t,x,y

                    //Check input x' or y' or x(t) or y(t) if wrong typing
                    Argument t = new Argument("t", 2);
                    Argument x = new Argument("x", 2);
                    Argument y = new Argument("y", 2);

                    Expression expres = new Expression(xEqu.Text, t, x, y);
                    Chk = (expres.calculate());


                    Argument x4 = new Argument("x", 2);
                    Expression expres2 = new Expression(yEqu.Text, t, x, y);
                    Chk2 = (expres2.calculate());

                    if ((Double.IsNaN(Chk)) || (Double.IsNaN(Chk2)))
                    {
                        MessageBox.Show("Something wrong with input !!!");
                        return;

                    }
                }


                //check typing correct on Exact x(t) & y(t)
                {

                    Argument t = new Argument("t", 2);

                        Expression expres = new Expression(AnX.Text, t);
                        Chk = (expres.calculate());


                        Expression expres2 = new Expression(AnY.Text, t);
                        Chk2 = (expres2.calculate());

                        if ((Double.IsNaN(Chk)) || (Double.IsNaN(Chk2)))
                        {
                            MessageBox.Show("Something wrong with input !!!");
                            return;

                        }
                    }


                



                if (m > 10000)
                {
                    MessageBox.Show("Maximum m is 10000!!!");
                    return;
                }


                h = (b - a) / m;
                Tk[0] = a;

                //RK4 numeric

                for (k = 0; k < m; k++)
                {


                    {
                        tVal = Tk[k];
                        xVal = Xk[k];
                        yVal = Yk[k];


                        t2 = (tVal.ToString());
                        x2 = (xVal.ToString());
                        y2 = (yVal.ToString());


                        Argument t = new Argument("t", t2);
                        Argument x = new Argument("x", x2);
                        Argument y = new Argument("y", y2);

                        Expression expres = new Expression(xEqu.Text, t, x, y);  // x=f(t,x,y)
                                                                                 //f1[k] = (expres.calculate());
                        f1 = (expres.calculate());

                        //result.Text = f1[k].ToString(); tes aja


                        //g1 y=g(t,x,y)
                        Expression expres2 = new Expression(yEqu.Text, t, x, y);  // y=g(t,x,y)
                        g1 = (expres2.calculate());

                    }

                    //tiap f1,g1 dst ada dalam 1 block statement untuk syarat  Argument t,x,y tidak boleh sama dalam 1 block padahal input fungsi f=f(t,x,y), jadi dipisah.

                    //f2 ,g2
                    {
                        //f2
                        tVal = Tk[k] + h / 2;
                        xVal = Xk[k] + h / 2 * f1;
                        yVal = Yk[k] + h / 2 * g1;

                        t2 = (tVal.ToString());
                        x2 = (xVal.ToString());
                        y2 = (yVal.ToString());


                        Argument t = new Argument("t", t2);
                        Argument x = new Argument("x", x2);
                        Argument y = new Argument("y", y2);


                        Expression expres = new Expression(xEqu.Text, t, x, y);
                        f2 = (expres.calculate());

                        Expression expres2 = new Expression(yEqu.Text, t, x, y);
                        g2 = (expres2.calculate());


                    }

                    {
                        //f3
                        tVal = Tk[k] + (h / 2);
                        xVal = Xk[k] + h / 2 * f2;
                        yVal = Yk[k] + h / 2 * g2;
                        t2 = (tVal.ToString());
                        x2 = (xVal.ToString());
                        y2 = (yVal.ToString());
                        Argument t = new Argument("t", t2);
                        Argument x = new Argument("x", x2);
                        Argument y = new Argument("y", y2);
                        Expression expres = new Expression(xEqu.Text, t, x, y);
                        f3 = (expres.calculate());

                        Expression expres2 = new Expression(yEqu.Text, t, x, y);
                        g3 = (expres2.calculate());

                    }


                    //f4 ,g4
                    {
                        tVal = Tk[k] + h;
                        xVal = Xk[k] + h * f3;
                        yVal = Yk[k] + h * g3;
                        t2 = (tVal.ToString());
                        x2 = (xVal.ToString());
                        y2 = (yVal.ToString());
                        Argument t = new Argument("t", t2);
                        Argument x = new Argument("x", x2);
                        Argument y = new Argument("y", y2);
                        Expression expres = new Expression(xEqu.Text, t, x, y);
                        f4 = (expres.calculate());

                        Expression expres2 = new Expression(yEqu.Text, t, x, y);
                        g4 = (expres2.calculate());

                    }

                    Tk[k + 1] = a + h * (k + 1);
                    Xk[k + 1] = Xk[k] + h * (f1 + 2 * f2 + 2 * f3 + f4) / 6;
                    Yk[k + 1] = Yk[k] + h * (g1 + 2 * g2 + 2 * g3 + g4) / 6;


                }


                //Exact or Analysis

                for (k = 0; k <= m; k++)
                {

                    tVal = Tk[k];
                    t2 = (tVal.ToString());

                    Argument t = new Argument("t", t2);
                    Expression expres = new Expression(AnX.Text, t);  // x=f(t,x,y)
                    XExact = (expres.calculate());

                    Expression expres2 = new Expression(AnY.Text, t);  // y=g(t,x,y)
                    YExact = (expres2.calculate());

                    Tk[k + 1] = a + h * (k + 1);
                    XkExact[k] = XExact;
                    YkExact[k] = YExact;

                    //result.Text = YkExact[k].ToString(); tes aja

                    //Relative error (%)
                    XRelEr[k] = (Math.Abs((XkExact[k] - Xk[k]) / XkExact[k])) * 100;
                    YRelEr[k] = (Math.Abs((YkExact[k] - Yk[k]) / YkExact[k])) * 100;



                }



                //result

                rtbResult.Clear();
                hasil = "";

                if (ExactCb.Checked == true)  //xk Exact & RK4
                {
                    if (rbx.Checked == true)
                    {  //x

                        for (k = 0; k <= m; k++)
                        {

                            if (rbDeci.Checked == true)//decimal
                            {
                                hasil = hasil + k.ToString() + "         " + String.Format("{0:0.000}", Tk[k]) + "             " + String.Format("{0:0.00000000000000}", XkExact[k]) + "             " + String.Format("{0:0.00000000000000}", Xk[k]) + "             " + String.Format("{0:0.00000000000000}", XRelEr[k]) + "\n";
                                rtbResult.Text = "k             tk                          xk Exact                                  xk RK4                        Relative Error ( % )" + "\n" + hasil;

                            }


                            else
                            { //exponent 
                                hasil = hasil + k.ToString() + "         " + String.Format("{0:0.000}", Tk[k]) + "             " + XkExact[k].ToString("E15") + "             " + Xk[k].ToString("E15") + "             " + XRelEr[k].ToString("E15") + "\n";
                                rtbResult.Text = "k             tk                                  xk Exact                                                  xk RK4                                   Relative Error ( % )" + "\n" + hasil;

                            }
                        }
                    }

                    else // rby.checked,  yk exact & yk RK4
                    {
                        for (k = 0; k <= m; k++)
                        {

                            if (rbDeci.Checked == true)//yk decimal
                            {
                                hasil = hasil + k.ToString() + "         " + String.Format("{0:0.000}", Tk[k]) + "             " + String.Format("{0:0.00000000000000}", YkExact[k]) + "             " + String.Format("{0:0.00000000000000}", Yk[k]) + "             " + String.Format("{0:0.00000000000000}", YRelEr[k]) + "\n";
                                rtbResult.Text = "k             tk                          yk Exact                                  yk RK4                        Relative Error ( % )" + "\n" + hasil;

                            }
                            else
                            { //yk format exponent 
                              //hasil = hasil + k.ToString() + "         " + String.Format("{0:0.000}", Tk[k]) + "             " + String.Format("{0:0.00000000000000}", XkExact[k]) + "             " + String.Format("{0:0.00000000000000}", Xk[k]) + "             " + String.Format("{0:0.00000000000000}", XRelEr[k]) + "\n";
                              //rtbResult.Text = "k             tk                          xk Exact                                  xk RK4                        Relative Error ( % )" + "\n" + hasil;
                                hasil = hasil + k.ToString() + "         " + String.Format("{0:0.000}", Tk[k]) + "             " + YkExact[k].ToString("E15") + "             " + Yk[k].ToString("E15") + "             " + YRelEr[k].ToString("E15") + "\n";

                                rtbResult.Text = "k             tk                                  yk Exact                                                  yk RK4                                   Relative Error ( % )" + "\n" + hasil;

                            }
                        }

                    }
                }
                else //xk RK4 & yk RK4 only (no exact) 
                {
                    hasil = "";
                    for (k = 0; k <= m; k++)
                    {
                        if (rbDeci.Checked == true)
                        {
                            //Decimal format
                            hasil = hasil + k.ToString() + "         " + String.Format("{0:0.000}", Tk[k]) + "             " + String.Format("{0:0.00000000000000}", Xk[k]) + "             " + String.Format("{0:0.00000000000000}", Yk[k]) + "\n";// + "               "+Yk[j].ToString("E") + "\n";
                            rtbResult.Text = "k             tk                            xk RK4                                    yk RK4" + "\n" + hasil;
                        }                                                                                                                                     //rtbResult.Text = "k             tk                                xk                                            yk" + "\n" + hasil;


                        else
                        {
                            hasil = hasil + k.ToString() + "         " + String.Format("{0:0.000}", Tk[k]) + "             " + Xk[k].ToString("E15") + "             " + Yk[k].ToString("E15") + "\n";
                            rtbResult.Text = "k             tk                                   xk RK4                                                yk RK4" + "\n" + hasil;
                        }
                    }

                }


                //Graphic RK4


                /*
                if (ExactCb.Checked == true)
                {
                    if (rbx.Checked == true)
                    {
                        lgd = "xcv";
                        // var series = new Series("Xk RK4"); //Xk RK4

                        var series = new Series(lgd); //Xk RK4

                        var series2 = new Series("Xk Exact"); //Xk Exact RK4
                        var series3 = new Series("Yk RK4"); //Yk RK4
                        var series4 = new Series("Yk Exact"); //Yk Exact RK4


                    }


                }*/




                var series = new Series("Xk RK4"); //Xk RK4
                var series2 = new Series("Xk Exact"); //Xk Exact RK4
                var series3 = new Series("Yk RK4"); //Yk RK4
                var series4 = new Series("Yk Exact"); //Yk Exact RK4

                chart1.Series.Clear();


                if (ExactCb.Checked == true)
                {  //Exact
                    sm = 100; //smooth
                    step = (b - a) / sm;


                    if (rbx.Checked == true)
                    { //xk exact
                        Tkm[0] = a;
                        for (k = 0; k <= sm; k++)
                        {
                            Tkm[k + 1] = Tkm[k] + step;
                            x2 = (Tkm[k]).ToString();
                            Argument t = new Argument("t", x2);
                            Expression ex = new Expression(AnX.Text, t);
                            XkmExact[k] = (ex.calculate());
                            series2.Points.AddXY(Tkm[k], XkmExact[k]);
                        }


                        for (k = 1; k <= m; k++)
                        {
                            series.Points.AddXY(Tk[k], Xk[k]);
                        }

                        chart1.ChartAreas[0].AxisX.Title = "tk";
                        chart1.ChartAreas[0].AxisY.Title = "xk RK4 / xk Exact";
                        chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 13);
                        chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 13);
                    

                    }
                    else
                    {  //yk exact    

                        Tkm[0] = a;
                        for (k = 0; k <= sm; k++)
                        {
                            Tkm[k + 1] = Tkm[k] + step;
                            x2 = (Tkm[k]).ToString();
                            Argument t = new Argument("t", x2);
                            Expression ex = new Expression(AnY.Text, t);
                            YkmExact[k] = (ex.calculate());
                            series4.Points.AddXY(Tkm[k], YkmExact[k]);
                        }

                        for (k = 0; k <= m; k++)
                        {
                            series3.Points.AddXY(Tk[k], Yk[k]);
                        }
                        chart1.ChartAreas[0].AxisX.Title = "tk";
                        chart1.ChartAreas[0].AxisY.Title = "yk RK4 / yk Exact";
                        chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 13);
                        chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 13);


                    }
                }
                else  //RK4 only(xk RK4 & yk RK4)
                {
                    //xk RK4
                    for (k = 0; k <= m; k++)
                    {
                        series.Points.AddXY(Tk[k], Xk[k]);
                        series3.Points.AddXY(Tk[k], Yk[k]);

                    }
                    chart1.ChartAreas[0].AxisX.Title = "tk";
                    chart1.ChartAreas[0].AxisY.Title = "xk RK4 / yk RK4";
                    chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 13);
                    chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 13);
                }


                //xk & yk RK4 
                series.BorderWidth = 2; //xk
                series.Color = Color.Red;
                series3.BorderWidth = 2; //yk
                series3.Color = Color.Black;

                //xk Exact & yk Exact
                series2.BorderWidth = 2; // xk Exact
                series2.Color = Color.Blue;
                series4.BorderWidth = 2;  //yk Exact
                series4.Color = Color.Green;

                chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;


                chart1.ChartAreas[0].AxisX.Maximum = b;
                chart1.ChartAreas[0].AxisX.Minimum = a;

                //display Ymax
                if (Xk[m] > Yk[m])
                    Ymax = Xk[m];
                else
                    Ymax = Yk[m];
                chart1.ChartAreas[0].AxisY.Maximum = Math.Ceiling(Ymax); //display max only

                //display Xmin
                if (Xk[0] < Yk[0])
                    Xmin = Xk[0];
                else
                    Xmin = Yk[0];

                chart1.ChartAreas[0].AxisY.Minimum = Math.Floor(Xmin);

                //Xk,Yk RK4
                series.ChartType = SeriesChartType.Point;
                series3.ChartType = SeriesChartType.Point;

                chart1.Series.Add(series);
                chart1.Series.Add(series3);

                //Xk Exact
                series2.ChartType = SeriesChartType.Line;
                series4.ChartType = SeriesChartType.Line;

                chart1.Series.Add(series2);
                chart1.Series.Add(series4);
                
        }
        catch (Exception){
                MessageBox.Show("Something wrong with input !!!");
                return;
        }

            
    }

        private void btClear_Click(object sender, EventArgs e)
        {
            xEqu.Clear();
            yEqu.Clear();
            atb.Clear(); 
            btb.Clear();
            mtb.Clear();
            xt0tb.Clear();
            yt0tb.Clear();
            rtbResult.Clear();
            ExactCb.Checked = false;
            AnX.Clear();
            AnY.Clear();

            chart1.Series.Clear();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void butAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("|======== Systems of ODE Solver ========|" + Environment.NewLine + Environment.NewLine +
                        "Version 1.0 - build feb 9, 2022." + Environment.NewLine +
                        "Created by Lukas Setiawan." + Environment.NewLine +
                        "Copyright (c) 2022. All Rights Reserved." + Environment.NewLine +
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



        
    
