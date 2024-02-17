using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestTaskDigitalDesign
{
    public partial class TestTaskForm : Form
    {
        public TestTaskForm()
        {
            InitializeComponent();
        }
        static string exePath = AppDomain.CurrentDomain.BaseDirectory;
        static string TempFilePath = Path.Combine(exePath, @"Resource\Result.txt"); 
        // Адрес промежуточного файла

        private void FillDataGridView(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    dataGridView1.Rows.Clear();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        if (values.Length == 2)
                            dataGridView1.Rows.Add(values); 
                        
                    }
                }
                MessageBox.Show("Данные успешно загружены в DataGridView");
            }
            catch (Exception ex)
            {
                // Обработка ошибок, если они возникнут
                MessageBox.Show("Ошибка при загрузке данных в таблицу: " + ex.Message);
            }
        }
        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Установка свойств диалогового окна
                openFileDialog.Title = "Выберите файл";
                openFileDialog.Filter = "Текстовые файлы|*.txt";

                DialogResult result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Получение выбранного файла
                    string selectedFilePath = openFileDialog.FileName;
                    Counter.CountFileWords(selectedFilePath); 
                    FillDataGridView(TempFilePath);
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Создание диалогового окна выбора выходного файла
            
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Выберите выходной файл";
                saveFileDialog.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";

                DialogResult result = saveFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Выполнение операции над файлами и сохранение результата в выходной файл
                    ProcessFiles(TempFilePath, saveFileDialog.FileName);
                }
            }
        }

        private void ProcessFiles(string inputPath, string outputPath)
        {
            try
            {
                // Чтение содержимого входного файла
                string content = File.ReadAllText(inputPath);

                // Запись содержимого в выходной файл
                File.WriteAllText(outputPath, content);

                // Отображение результата операции
                MessageBox.Show("Операция выполнена успешно. Результат сохранен в " + outputPath);
            }
            catch (Exception ex)
            {
                // Обработка ошибок, если они возникнут
                MessageBox.Show("Ошибка при выполнении операции: " + ex.Message);
            }
        }
    }
}
