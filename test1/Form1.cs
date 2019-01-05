using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace test1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AddButton();
        }
        /// <summary>
        /// Margin设为0 是为了减少行间距
        /// The reason why I have set the Margin property of the buttons to zero is to reduce the line gap between each row in our list control.
        /// </summary>
        private void AddButton()
        {
            Random r = new Random();
            for (int i = 0; i < 20; i++)
            {
                //var btn = new Button();
                //btn.Name = "btn" + i;
                //btn.Text = "按钮" + i;
                //btn.Height = 30;
                //btn.Margin = new Padding(0);

                listControl1.Add(i.ToString(),r.NextDouble().ToString());
            }
        }

        private void listControl1_Layout(object sender, LayoutEventArgs e)
        {
        }
    }
}
