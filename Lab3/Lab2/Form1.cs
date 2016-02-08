using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Lab2
{
   
    public partial class Form1 : Form
    {
       public List<double> xkk = new List<double>();
       public List<double> xFail = new List<double>();
       public List<double> Fail = new List<double>();
       public List<double> KFail = new List<double>();
       public List<double> YY = new List<double>();
       public List<double> Yt = new List<double>();
       public List<double> D = new List<double>();
       public List<double> proch = new List<double>();
       public List<double> Step = new List<double>();
       public List<double> G = new List<double>();
       public List<double> ALF = new List<double>();
       public List<int> KK = new List<int>();
        double edop = 0.01;
       double h=0.1;
       double y;
        double A = 0;
        double B = 0.9;
         double function (double x,double y)
        {
             double otvet;
             otvet = (1 + Math.Pow(y, 2.0)) / (1 + Math.Pow(x, 2.0));
            // otvet = (y / x) * Math.Log(y/x);
             return otvet; 
        }
         double functionotvet(double x)
         {
             double otvet;
             otvet = (1 + x) / (1 - x);
             //otvet = x * Math.Pow(Math.E, (1.0 - x));
             return otvet;
         }
         void metodRKF(double h)// Рунге-Кутта-Фельберга 1 порядку 
        {
            dataGridView1.Rows.Clear();

            double K0,K1;
            double Ykin= 0;
            y = 1;
            int j=0;

            for (double x = A; x <= B+0.000001; x += h) {
                K0 = h * function(x, y);
                K1 = h*function(x+(h/2),y+(K0/2));
                Ykin = y + ((1 / 256.0) * (K0 + (255.0 * K1)));
                this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[j].Cells[0].Value = (String.Format("{0:0.000}", x));
                this.dataGridView1.Rows[j].Cells[1].Value = (String.Format("{0:0.0000}", y));
                this.dataGridView1.Rows[j].Cells[2].Value = (String.Format("{0:0.0000}", functionotvet(x)));
                this.dataGridView1.Rows[j].Cells[3].Value = (String.Format("{0:0.00 }", (functionotvet(x) - y) / functionotvet(x)*100));
                this.dataGridView1.Rows[j].Cells[4].Value = (String.Format("{0:0.00 }", (functionotvet(x) - y)));
                j++;
                y = Ykin;


            }
        }
         public void metodRKF4(double h)// Рунге-Кутта 4-го порядку
         {
             dataGridView1.Rows.Clear();
            
             double K0, K1,K2,K3;
             double Ykin;
             y = 1;
             int j = 0;
  
            

             for (double x = A; x <= B + 0.000001; x += h)
             {
                 
                 K0 = h * function(x, y);
                 K1 = h * function(x + (h / 2), y + (K0 / 2));
                 K2 = h * function(x + (h / 2), y + (K1/ 2));
                 K3 = h * function(x + h , y + K2 );
                 Ykin = y +(1/ 6.0 )* (K0 + 2.0*K1+2.0*K2+K3);
                
                 this.dataGridView1.Rows.Add();
                 this.dataGridView1.Rows[j].Cells[0].Value = (String.Format("{0:0.000}", x));
                 this.dataGridView1.Rows[j].Cells[1].Value = (String.Format("{0:0.0000}", y));
                 this.dataGridView1.Rows[j].Cells[2].Value = (String.Format("{0:0.0000}", functionotvet(x)));
                 this.dataGridView1.Rows[j].Cells[3].Value = (String.Format("{0:0.00E+00 }",Math.Abs( (functionotvet(x) - y) / functionotvet(x) * 100)));
                 this.dataGridView1.Rows[j].Cells[4].Value = (String.Format("{0:0.00 }", (functionotvet(x) - y)));
                 j++;
                 y = Ykin;
             }

    
          
         }
       

         double metodRKF(double H,double i,double y)// Рунге-Кутта 4 порядку  автоматичний вибір кроку
         {
             double K0, K1,K2,K3;
             double Ykin;
                 K0 = H * function(i, y);
                 K1 = H * function(i + (H / 2), y + (K0 / 2));
                 K2 = H * function(i + (H / 2), y + (K1/ 2));
                 K3 = H * function(i + H , y + K2 );
                 Ykin = y +(1/ 6.0 )* (K0 + 2.0*K1+2.0*K2+K3);
                 y = Ykin;
                 return y;
         }
        public void metodRKFavto()// Рунге-Кутта 4 порядку  автоматичний вибір кроку
         {
             double y1, yprom, y2,xpo=0;
             double H = (B - A)/1000.0;
             double Y = 1.0;
             double g=0;
             double ALFA=0;
             double MaxStep=(B-A)/4;
             double MinStep = (B - A) / 100000;
             double xk = A;
             int K = 0, Kp = 0;
             xkk.Clear();
             YY.Clear();
             Yt.Clear();
             D.Clear();
             proch.Clear();
             Step.Clear();
             G.Clear();
             ALF.Clear();
             KK.Clear();
             xFail.Clear();
             Fail.Clear();
             KFail.Clear();
              dataGridView1.Rows.Clear();
              this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[0].Cells[0].Value = (String.Format("X"));
                 this.dataGridView1.Rows[0].Cells[1].Value = (String.Format("Fail step"));
                 this.dataGridView1.Rows[0].Cells[2].Value = (String.Format("Кількість кроків"));
          int j = 1;
               while(xk<=B+0.001){
                 //xpo = xk;
            y1 = metodRKF(H, xk, Y);
             yprom = metodRKF((H / 2.0),xk+(H/2), Y);
             y2 = metodRKF((H / 2.0), xk, yprom);
            
             if (Math.Abs(y1) <= 1)
             {
                 g = Math.Abs(y1 - y2);
             }
             if (Math.Abs(y1) > 1)
             {
                 g = Math.Abs((y1 - y2) / (y1));
             }
             if (g <edop){
                 xkk.Add(xk);
                 YY.Add(Y);
                 Yt.Add(functionotvet(xk));
                 D.Add(Math.Abs((functionotvet(xk) - Y)));
                 proch.Add(Math.Abs((functionotvet(xk) - Y) / functionotvet(xk) * 100));
                 Step.Add(H);
                 G.Add(g);
                 
                 Kp++;
                 KK.Add(Kp);
                 Y = y2 - ((y1 - y2) / 15);
                
            ALFA =  Math.Pow((edop/g),0.2);
            
           if (ALFA >= 4) { ALFA = 4; }
           ALF.Add(ALFA);
            xk += H;
                 H *= ALFA;
               
             
                 if (H < MinStep) { H = MinStep; }
                 else
                 { if (H > MaxStep) H= MaxStep; }
             }
             else {
                 K++;
                
                 xFail.Add(xk);
                 Fail.Add(H);
                 KFail.Add(K);
                 this.dataGridView1.Rows.Add();
                 this.dataGridView1.Rows[j].Cells[0].Value = (String.Format("{0:0.000}", xk));
                 this.dataGridView1.Rows[j].Cells[1].Value = (String.Format("{0:0.0000}", H));
                 this.dataGridView1.Rows[j].Cells[2].Value = (String.Format("{0:0}", K));
                 j++;
                 H *= 0.5;
               
                 if (H< MinStep) { H = MinStep; };
                 xk = xpo;
               
             }
             }
             
         }

        double metodRKFmy(double H, double x, double y)//  Рунге-Кутта-Фельберга 1 порядку  автоматичний вибір кроку+
         { 
             double K0, K1;
             double Ykin;
             K0 = H * function(x, y);
                K1 = H * function(x + (H / 2), y + (K0 / 2));
                Ykin = y + ((1 / 256.0) * (K0 + (255.0 * K1)));
                 y = Ykin;
                 return y;
         }
             
        public void metodRKFavtomy()// Рунге-Кутта-Фельберга 1 порядку  автоматичний вибір кроку
        {
            double y1, yprom, y2, xpo = 0;
            double H = (B - A) / 1000.0;
            double Y = 1.0;
            double g = 0;
            double ALFA = 0;
            double MaxStep = (B - A) / 4;
            double MinStep = (B - A) / 100000;
            double xk = A;
            xkk.Clear();
            YY.Clear();
            Yt.Clear();
            D.Clear();
            proch.Clear();
            Step.Clear();
            G.Clear();
            ALF.Clear();
            KK.Clear();
            xFail.Clear();
            Fail.Clear();
            KFail.Clear();
            dataGridView1.Rows.Clear();
            int K = 0;
            this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[0].Cells[0].Value = (String.Format("X"));
            this.dataGridView1.Rows[0].Cells[1].Value = (String.Format("Fail step"));
            this.dataGridView1.Rows[0].Cells[2].Value = (String.Format("Кількість кроків"));
            int j = 1;
            int Kp = 0;
            ALFA = 0;
            g = 0;
             while (xk <= B )
            {
              //  xpo = xk;
                y1 = metodRKF(H, xk, Y);
                yprom = metodRKF((H / 2.0), xk + (H / 2.0), Y);
                y2 = metodRKF((H / 2.0), xk, yprom);

                if (Math.Abs(y1) <= 1)
                {
                    g = Math.Abs(y1 - y2);
                }
                if (Math.Abs(y1) > 1)
                {
                    g = Math.Abs(y1 - y2) / Math.Abs(y1);
                }
             
                if (g < edop)
                {
                    xkk.Add(xk);
                    YY.Add(Y);
                    Yt.Add(functionotvet(xk));
                    D.Add(Math.Abs((functionotvet(xk) - Y)));
                    proch.Add(Math.Abs((functionotvet(xk) - Y) / functionotvet(xk) * 100));
                    Step.Add(H);
                    G.Add(g);
                    
                    Kp++;
                    KK.Add(Kp);
                    Y = y2 - ((y1 - y2) / 1); ;
                 
                   ALFA = Math.Pow((edop / g), 0.5);
                    if (ALFA >= 4) { ALFA = 4; }
                    ALF.Add(ALFA);
                    xk += H;
                    H *= ALFA;
                    if (H < MinStep) { H = MinStep; }
                    else
                    { if (H > MaxStep) H = MaxStep; }
               
                    
                }
                else
                {
                    K++;
               
                    xFail.Add(xk);
                    Fail.Add(H);
                    KFail.Add(K);
                    this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[j].Cells[0].Value = (String.Format("{0:0.000}", xk));
                    this.dataGridView1.Rows[j].Cells[1].Value = (String.Format("{0:0.0000}", H));
                    this.dataGridView1.Rows[j].Cells[2].Value = (String.Format("{0:0}", K));
                    j++;
                    H *= 0.5;

                    if (H < MinStep) { H = MinStep; };
                 // xk = xpo;

                }
            }

        }
 

        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            metodRKF(h);
            Form2 f = new Form2(this);
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            metodRKF(h/5);
            Form2 f = new Form2(this);
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            metodRKF(h/21);
            Form2 f = new Form2(this);
            f.Show();
        }

        public void button4_Click(object sender, EventArgs e)
        {
            metodRKF4(h);
            Form2 f = new Form2(this);
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            metodRKF4(h/5);
            Form2 f = new Form2(this);
            f.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            metodRKF4(h / 21);
            Form2 f = new Form2(this);
            f.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {

                   dataGridView1.Rows.Clear();
            GraphPane pane = zedGraph.GraphPane;

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();
            
            double[] YValues1 = new double[1];
        double[] YValues2 = new double[1];
        double[] YValues3 = new double[1];
        double[] XValues = new double[1];
                 XValues[0]= 1;
            metodRKF(h);
                 YValues1[0] = Convert.ToDouble(dataGridView1[3,dataGridView1.RowCount-2].Value);
            metodRKF(h/5);
                 YValues2[0] = Convert.ToDouble(dataGridView1[3,dataGridView1.RowCount-2].Value);
             metodRKF(h/21);
                 YValues3[0] = Convert.ToDouble( dataGridView1[3,dataGridView1.RowCount-2].Value);
                 dataGridView1.Rows.Clear();
            // Создадим три гистограммы
            // Так как для всех гистограмм мы передаем одинаковые массивы координат по X,
            // то столбики будут группироваться в кластеры в этих точках.
            BarItem bar1 = pane.AddBar("H", XValues, YValues1, Color.Blue);
            BarItem bar2 = pane.AddBar("H/5", XValues, YValues2, Color.Red);
            BarItem bar3 = pane.AddBar("H/21", XValues, YValues3, Color.Yellow);

            // !!! Расстояния между столбиками в кластере (группами столбиков)
            pane.BarSettings.MinBarGap = 0.0f;

            // !!! Увеличим расстояние между кластерами в 2.5 раза
            pane.BarSettings.MinClusterGap = 2.5f;
        
            zedGraph.AxisChange();
            pane.Title.Text = "Гістограма";
            // Обновляем график
            zedGraph.Invalidate();

        }

        public void button8_Click(object sender, EventArgs e)
        {
           metodRKFavto();
            Form3 f = new Form3(this);
            f.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            metodRKFavtomy();
            Form3 f = new Form3(this);
            f.Show();
        }
    }
}
 