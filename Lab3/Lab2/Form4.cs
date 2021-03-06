﻿using System;
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
    public partial class Form4 : Form
    {
        public Form4(Form3 f4)
        {
            InitializeComponent();
            GraphPane pane = zedGraph.GraphPane;

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();

            // Создадим список точек
           PointPairList list = new PointPairList();
           PointPairList list2 = new PointPairList();
          // PointPairList list2 = new PointPairList();
            // Заполняем список точек

            // Создадим кривую с названием "Функція", 
            // которая будет рисоваться голубым цветом (Color.Blue),
            // Опорные точки выделяться не будут (SymbolType.None)
            LineItem myCurve = pane.AddCurve("E(xk)/Y(xk)*100", list, Color.Blue, SymbolType.None);
            LineItem myCurve2 = pane.AddCurve("Y", list2, Color.Red, SymbolType.None);
          //  LineItem myCurve1 = pane.AddCurve("E(xk)/Y(xk)*100", list2, Color.Red, SymbolType.None);
           
            for (int i = 0; i < f4.dataGridView1.RowCount-1; i++)
            {
                list.Add(Convert.ToDouble( f4.dataGridView1[0, i].Value),Convert.ToDouble( f4.dataGridView1[4, i].Value));
                list2.Add(Convert.ToDouble(f4.dataGridView1[0, i].Value), Convert.ToDouble(f4.dataGridView1[2, i].Value));
                //list2.Add(Convert.ToDouble(f4.dataGridView1[0, i].Value), Convert.ToDouble(f4.dataGridView1[1, i].Value));
            }
         
            zedGraph.AxisChange();
            pane.Title.Text = "Графік";
            // Обновляем график
            zedGraph.Invalidate();
        }
        }
    
}
