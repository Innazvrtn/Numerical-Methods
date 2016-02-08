using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form3 : Form
    {

        public Form3(Form1 f3)
        {
            InitializeComponent();
            dataGridView1.Rows.Clear();
           
            int j = 0; 
          foreach (double element  in f3.xkk )
        {
            this.dataGridView1.Rows.Add();
            dataGridView1.Rows[j].Cells[0].Value = (String.Format("{0:0.000}", element));
            j++;
        }
          j = 0; 
          foreach (double element in f3.YY)
          {
             // this.dataGridView1.Rows.Add();
              dataGridView1.Rows[j].Cells[1].Value = (String.Format("{0:0.000}", element));
              j++;
          }
          j = 0; 
          foreach (double element in f3.Yt)
          {
              //this.dataGridView1.Rows.Add();
              dataGridView1.Rows[j].Cells[2].Value = (String.Format("{0:0.000}", element));
              j++;
          }
          j = 0; 
          foreach (double element in f3.D)
          {
             // this.dataGridView1.Rows.Add();
              dataGridView1.Rows[j].Cells[3].Value = (String.Format("{0:0.000}", element));
              j++;
          }
          j = 0; 
          foreach (double element in f3.proch)
          {
             // this.dataGridView1.Rows.Add();
              dataGridView1.Rows[j].Cells[4].Value = (String.Format("{0:0.000}", element));
              j++;
          }
          j = 0; 
          foreach (double element in f3.Step)
          {
             // this.dataGridView1.Rows.Add();
              dataGridView1.Rows[j].Cells[5].Value = (String.Format("{0:0.0000}", element));
              j++;
          }
          j = 0; 
          foreach (double element in f3.G)
          {
             // this.dataGridView1.Rows.Add();
              dataGridView1.Rows[j].Cells[6].Value = (String.Format("{0:0.00E+00}", element));
              j++;
          }
          j = 0; 
          foreach (double element in f3.ALF)
          {
             // this.dataGridView1.Rows.Add();
              dataGridView1.Rows[j].Cells[7].Value = (String.Format("{0:0.000000}", element));
              j++;
          }
          j = 0;
          foreach (double element in f3.KK)
          {
             // this.dataGridView1.Rows.Add();
              dataGridView1.Rows[j].Cells[8].Value = (String.Format("{0:0}", element));
              j++;
          }

          
          j = 0;
          foreach (double element in f3.xFail)
          {
               this.dataGridView2.Rows.Add();
               dataGridView2.Rows[j].Cells[0].Value = (String.Format("{0:0.000}", element));
              j++;
          }
          j = 0;
          foreach (double element in f3.Fail)
          {
              // this.dataGridView1.Rows.Add();
              dataGridView2.Rows[j].Cells[1].Value = (String.Format("{0:0.000}", element));
              j++;
          }
          j = 0;
          foreach (double element in f3.KFail)
          {
              // this.dataGridView1.Rows.Add();
              dataGridView2.Rows[j].Cells[2].Value = (String.Format("{0:0}", element));
              j++;
          }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4(this);
            f.Show();
        }
    }
}
