using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.IO;

namespace EmailGenerate
{
    public partial class Form1 : Form
    {
        private string file = "setting.json";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0) { MessageBox.Show("Вы не указали smtp", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (textBox2.Text.Length == 0) { MessageBox.Show("Вы не указали логин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (textBox3.Text.Length == 0) { MessageBox.Show("Вы не указали пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (textBox4.Text.Length == 0) { MessageBox.Show("Вы не указали e-mail отправителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (textBox5.Text.Length == 0) { MessageBox.Show("Вы не указали e-mail получателя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (textBox6.Text.Length == 0) { MessageBox.Show("Вы не указали тему сообщения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (richTextBox1.Text.Length == 0) { MessageBox.Show("Вы не указали текст сообщения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }


            Data data = new Data()
            {
                Smtp = textBox1.Text,
                Email = textBox2.Text,
                Password = textBox3.Text,
                EmailSend = textBox4.Text,
                EmailRecipient = textBox5.Text,
                MessTema = textBox6.Text,
                MessText = richTextBox1.Text,
            };

            JavaScriptSerializer jss = new JavaScriptSerializer();
            var json = jss.Serialize(data);
            File.WriteAllText(file, json);

            MessageBox.Show("Файл был успешно сгенерирован", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }

        private class Data
        {
            public string Smtp { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string EmailSend { get; set; }
            public string EmailRecipient { get; set; }
            public string MessTema { get; set; }
            public string MessText { get; set; }
        }
    }
}
