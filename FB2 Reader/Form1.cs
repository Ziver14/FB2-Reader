using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace FB2_Reader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofg = new OpenFileDialog();
            ofg.Filter = "FB2 Files (*.fb2)|*.fb2|All Files (*.*)|*.*";

            if (ofg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Читаем файл
                    string filePath = ofg.FileName;

                    if (!File.Exists(filePath))
                    {
                        MessageBox.Show("Файл не существует.");
                        return;
                    }

                    string fileContent = File.ReadAllText(filePath);

                    // Парсим содержимое FB2 файла
                    ParseFB2File(fileContent);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при чтении файла: " + ex.Message);
                }
            }
        }

        private void ParseFB2File(string fileContent)
        {
            try
            {
                // Создаем XmlDocument и загружаем содержимое файла
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(fileContent);

                // Получаем все параграфы текста из файла FB2
                XmlNodeList paragraphs = doc.GetElementsByTagName("p");

                // Очищаем ListBox перед добавлением новых элементов
                lstBox.Items.Clear();

                // Добавляем каждый параграф в ListBox
                foreach (XmlNode paragraph in paragraphs)
                {
                    lstBox.Items.Add(paragraph.InnerText);
                }
            }
            catch (Exception ex)
            {
                // Обрабатываем ошибки
                MessageBox.Show("Ошибка при чтении файла: " + ex.Message);
            }
        }
    }
}
