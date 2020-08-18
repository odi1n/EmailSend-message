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

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(file)) return;

            JavaScriptSerializer jss = new JavaScriptSerializer();
            var json = jss.Deserialize<Data>(File.ReadAllText(file));
            textBox1.Text = json.Smtp;
            numericUpDown1.Value = json.Port;
            textBox2.Text = json.Email;
            textBox3.Text = json.Password;
            textBox4.Text = json.EmailSend;
            textBox5.Text = json.EmailRecipient;
            textBox6.Text = json.Subject;
            richTextBox1.Text = json.Body;
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
                Port = (int)numericUpDown1.Value,
                Email = textBox2.Text,
                Password = textBox3.Text,
                EmailSend = textBox4.Text,
                EmailRecipient = textBox5.Text,
                Subject = textBox6.Text,
                Body = richTextBox1.Text,
            };

            JavaScriptSerializer jss = new JavaScriptSerializer();
            var json = jss.Serialize(data);
            File.WriteAllText(file, json);

            MessageBox.Show("Файл был успешно сгенерирован", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }

        private class Data
        {
            public string Smtp { get; set; } = "smtp.mail.ru";
            public int Port { get; set; } = 25;
            public string Email { get; set; }
            public string Password { get; set; }
            public string EmailSend { get; set; }
            public string EmailRecipient { get; set; }
            /// <summary>
            /// тема
            /// </summary>
            public string Subject { get; set; }
            /// <summary>
            /// текст темы
            /// </summary>
            public string Body { get; set; }
        }

        
    }
}
