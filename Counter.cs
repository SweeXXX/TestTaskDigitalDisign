using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestTaskDigitalDesign
{
    internal class Counter
    {
        public static void CountFileWords(string fileIn )
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string inputFilePath = fileIn;
            string outputFilePath = Path.Combine(exePath, @"Resource\Result.txt");
            if (!File.Exists(inputFilePath))
            {
                MessageBox.Show("Указанный входной файл не существует.");
                return;
            }

            try
            {
                Dictionary<string, int> wordCounts = CountWords(inputFilePath);
                var sortedWordCounts = wordCounts.OrderByDescending(pair => pair.Value);
                
                // Запись результатов в промежуточный файл
                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    foreach (var pair in sortedWordCounts)
                    {
                        writer.WriteLine($"{pair.Key,-20}\t{pair.Value}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при загрузке файла: {ex.Message}");
            }
        }

        static Dictionary<string, int> CountWords(string filePath)
        {
            Dictionary<string, int> wordCounts = new Dictionary<string, int>();

            string text = File.ReadAllText(filePath);

            // Разделение текста на слова с использованием регулярного выражения
            string pattern = @"\b\w+\b";
            MatchCollection matches = Regex.Matches(text, pattern);

            // Подсчет количества употреблений каждого слова
            foreach (Match match in matches)
            {
                string word = match.Value.ToLower();
                if (wordCounts.ContainsKey(word))
                {
                    wordCounts[word]++;
                }
                else
                {
                    wordCounts[word] = 1;
                }
            }

            return wordCounts;
        }
    }
}
