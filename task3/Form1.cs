using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task3
{
    public partial class Form1 : Form
    {
        private List<Account> accounts = new List<Account>();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            // читаем файл в строку
            listBox1.Items.Clear();
            string[] fileText = System.IO.File.ReadAllLines(filename);
            foreach (string line in fileText) {
                string[] sline = line.Split(' ');
               
                listBox1.Items.Add(new Account(Convert.ToInt32(sline[0]), sline[1], Convert.ToInt32(sline[2]), Convert.ToInt32(sline[3])).toString());
                accounts.Add(new Account(Convert.ToInt32(sline[0]), sline[1], Convert.ToInt32(sline[2]), Convert.ToInt32(sline[3])));
            }
            MessageBox.Show("Файл открыт");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                return; 
            }
            if (accounts[listBox1.SelectedIndex].isActive == 1)
            {
                accounts[listBox1.SelectedIndex].isActive = 0;
            }
            else
            {
                accounts[listBox1.SelectedIndex].isActive = 1;
            }
            listBox1.Items.Clear();
            foreach (Account a in accounts)
            {
                listBox1.Items.Add(a.toString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double sum = 0;
            foreach (Account a in accounts)
            {
                if (a.balance >= 0) sum += a.balance;
            }
            textBox1.Text = Convert.ToString(sum);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double sum = 0;
            foreach (Account a in accounts)
            {
                if (a.balance <= 0) sum += a.balance;
            }
            textBox2.Text = Convert.ToString(sum);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "") return;

            if (comboBox1.Text == "Номер") {
                accounts = accounts.OrderBy(b => b.number).ToList();
            }
            if (comboBox1.Text == "Сумма")
            {
                accounts = accounts.OrderBy(b => b.balance).ToList();
            }
            if (comboBox1.Text == "Состояние")
            {
                accounts = accounts.OrderByDescending(b => b.isActive).ToList();
            }

            listBox1.Items.Clear();
            foreach (Account a in accounts)
            {
                listBox1.Items.Add(a.toString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            if (comboBox2.Text == "") return;

            if (comboBox2.Text == "Номер")
            {
                foreach (Account a in accounts)
                {
                    if(a.number == Convert.ToInt32(textBox3.Text)) listBox2.Items.Add(a.toString());
                }
            }
            if (comboBox2.Text == "Сумма")
            {
                foreach (Account a in accounts)
                {
                    if (a.balance == Convert.ToInt32(textBox3.Text)) listBox2.Items.Add(a.toString());
                }
            }
            if (comboBox2.Text == "Состояние")
            {
                foreach (Account a in accounts)
                {
                    string isActiveToStr = (a.isActive == 1) ? "Активен" : "Заблокирован";
                    if (isActiveToStr == textBox3.Text) listBox2.Items.Add(a.toString());
                }
            }
            if (comboBox2.Text == "Банк")
            {
                foreach (Account a in accounts)
                {
                    if (a.bankName == textBox3.Text) listBox2.Items.Add(a.toString());
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    class Account
    {
        public Account(int number, string bankName, int balance, int isActive)
        {
            this.number = number;
            this.bankName = bankName;
            this.balance = balance;
            this.isActive = isActive;
        }
        public int number { get; set; }
        public string bankName { get; set; }
        public int balance { get; set; }
        public int isActive { get; set; }
        public string toString() 
        {
            string isActiveToStr = (isActive == 1) ? "Активен" : "Заблокирован";
            return number + " " + bankName + " " + balance + " " + isActiveToStr;
        }

    }
}
