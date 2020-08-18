using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;
using System.Web.Script.Serialization;
using System.Net;

namespace EmailSend
{
    public partial class Form1 : Form
    {
        private string path = "setting.json";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var file = OpenFile();
            if (file == null)
            {
                var info = MessageBox.Show("Проверьте существование файла настроек 'setting.json'", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (info == DialogResult.OK)
                    this.Close();
            }
            else
            {
                if (file.Email.Length == 0 || file.EmailRecipient.Length == 0 || file.EmailSend.Length == 0 || file.Body.Length == 0 ||
                    file.Subject.Length == 0 || file.Password.Length == 0 || file.Smtp.Length == 0)
                {
                    if (MessageBox.Show("Проверьте данные в файле 'setting.json', не все данные были указаны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                        this.Close();
                }
                else
                {
                    var info = ProcessFile(file);
                    if (info == "ERROR")
                    {
                        var message = MessageBox.Show($"Не удалось отправить сообщение, возникла ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (message == DialogResult.OK)
                            this.Close();
                    }
                    else if (info != "OK")
                    {
                        var message = MessageBox.Show($"Проверьте все ли указанные данные верны. \nОшибка: {info}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (message == DialogResult.OK)
                            this.Close();
                    }
                    
                }
            }
            this.Close();
        }

        private Data OpenFile()
        {
            if (!File.Exists(path)) { return null; }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            var json = jss.Deserialize<Data>(File.ReadAllText(path));
            return json;
        }

        private class Data
        {
            public string Smtp { get; set; }
            public int Port { get; set; }
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

        private string ProcessFile(Data data)
        {
            MailAddress From = new MailAddress(data.EmailSend );
            MailAddress To = new MailAddress(data.EmailRecipient );
            var msg = new MailMessage(From, To)
            {
                Body = data.Body,
                Subject = data.Subject
            };

            var smtpClient = new SmtpClient(data.Smtp, data.Port )
            {
                Credentials = new NetworkCredential(data.Email, data.Password),
                EnableSsl = true
            };
            try
            {
                smtpClient.Send(msg);
                return "OK";
            }
            catch (System.Net.Mail.SmtpException e)
            {
                return e.Message;
            }
            catch
            {
                return "ERROR";
            }
        }
    }
}
