using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp3
{
    class Program
    {
        static string Read_from_file(string file_path)
        {
            try
            {
                string text = "";
                using (StreamReader sr = new StreamReader(file_path, Encoding.Unicode))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null) // пока строка существует считываем текст из файла NameT в переменную string
                    {
                        text += line; 
                    }
                }
                return text;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        static private Dictionary<string, int> countLetters(string text)
        {
            var map = new Dictionary<string, int>();   // создаём словарь ключ будет string значение int
            foreach (var str in text)
            {
                if (!map.ContainsKey(str.ToString())) // если буква встречается первый раз, добавляем её в ключ и записываем ей значение 1
                {
                    map.Add(str.ToString(), 1);
                }
                else
                {
                    int k;
                    map.TryGetValue(str.ToString(), out k); // иначе увеличиваем значение 
                    k++;
                    map[str.ToString()] = k;
                }
            }
            return map;
        }

        static async void Write_to_file(string text, string file_path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(file_path, false, System.Text.Encoding.Unicode)) 
                {
                    await sw.WriteLineAsync(text); // записываем текст из переменной  string  в файл NameS
                }
                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        static void Main(string[] args)
        {
            string text = Read_from_file(@"C:\Users\Vladislav\source\repos\ConsoleApp3\NameT.txt");
            Console.WriteLine(text); // записываем считанный текст в консоль

            string t = ""; // создаём переменную,в которую будем записывать отсортированные значения
            char[] split = text.ToCharArray(); // создаём массив символов, в который загоняем наш текст

            bool flag = true;
            while (flag)
            {
                flag = false;
                for (int i = 0; i < split.Length - 1; i++)   
                    if (split[i].CompareTo(split[i + 1]) > 0) // сортируем по кодовому значению
                    {
                        char buf = split[i];
                        split[i] = split[i + 1];
                        split[i + 1] = buf;
                        flag = true;
                    }
            }

            foreach (char s in split)
            {
                t += s.ToString(); // переводим символы обратно в текст
            }

            text = "";  // приравниваем текст null

            foreach (var pair in countLetters(t))
            {
                Console.WriteLine("{0}, {1}", pair.Key, pair.Value); // выводим в консоль отсортированные элементы
                text += ("{0}, {1}", pair.Key, pair.Value);
            }

            using (var writer = new StreamWriter(@"C:\Users\Vladislav\source\repos\ConsoleApp3\NameS.txt"))   // записываем отсортированные элементы в файл вывода
            {
                foreach (var kvp in countLetters(t))
                {
                    writer.WriteLine($"{ kvp.Key}\t{kvp.Value}"); 
                }
            }
        }
    }
}

