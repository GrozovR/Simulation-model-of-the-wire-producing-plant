using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory
{
    public partial class Form1 : Form
    {
        C_Factory Factory;

        public Form1()
        {
            Factory = new C_Factory(this);
            InitializeComponent();
        }

        public void Write_Text1(string str)
        {
            textBox1.Text = str;
        }
        public void Write_Text2(string str)
        {
            textBox2.Text = str;
        }
        public void Write_Text4(string str)
        {
            textBox4.Text = str;
        }
        public void Write_Text5(string str)
        {
            textBox5.Text = str;
        }
        public void Write_Text6(string str)
        {
            textBox6.Text = str;
        }
        public void Write_Text7(string str)
        {
            textBox7.Text = str;
        }
        public void Write_Text8(string str)
        {
            textBox8.Text = str;
        }
        public void Write_Text9(string str)
        {
            textBox9.Text = str;
        }
        public void Write_Text10(string str)
        {
            textBox10.Text = str;
        }
        public void Write_Text11(string str)
        {
            textBox11.Text = str;
        }
        public void Write_Text12(string str)
        {
            textBox12.Text = str;
        }
        public void Write_Text13(string str)
        {
            textBox13.Text = str;
        }
        public void Write_Text14(string str)
        {
            textBox14.Text = str;
        }
        public void Write_Text15(string str)
        {
            textBox15.Text = str;
        }
        public void Write_Text16(string str)
        {
            textBox16.Text = str;
        }
        public void Write_Text18(string str)
        {
            textBox18.Text = str;
        }
        public void Write_Text17(string str)
        {
            textBox17.Text = str;
        }
        public void Write_Text19(string str)
        {
            textBox19.Text = str;
        }
        public void Write_Text20(string str)
        {
            textBox20.Text = str;
        }
        public void Write_Text21(string str)
        {
            textBox21.Text = str;
        }
        public void Write_Text22(string str)
        {
            textBox22.Text = str;
        }
        public void Write_Text23(string str)
        {
            textBox23.Text = str;
        }
        public void Write_Text24(string str)
        {
            textBox24.Text = str;
        }
        public void Write_Text25(string str)
        {
            textBox25.Text = str;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int days = int.Parse(textBox3.Text);
            int minutes = days*24*60;
            for (int i = 0; i < minutes; i++)
            {
                Factory.iterate(); 
            }
            Factory.statist();

                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Factory.iterate();
            Factory.statist();
        }

    }
}
