using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextReport
{
    class TextReport
    {
        List<Tuple<string, int, int>> mainColumn = new List<Tuple<string, int, int>>();

        List<string> columnContent = new List<string>();

        const int dataWidth = 12;
        const int intWidth = 10;
        const int stringWidth = 16;

        int sum;

        string separateLine;

        public TextReport()
        {
            mainColumn.Add(new Tuple<string, int, int>("ID", intWidth, 1));
            mainColumn.Add(new Tuple<string, int, int>("Назва товару", stringWidth, 2));
            mainColumn.Add(new Tuple<string, int, int>("Категорiя", stringWidth, 3));
            mainColumn.Add(new Tuple<string, int, int>("Дата виготовлення", dataWidth, 4));
            mainColumn.Add(new Tuple<string, int, int>("Термiн придатностi", stringWidth, 5));
            mainColumn.Add(new Tuple<string, int, int>("Кількість одиниць", intWidth, 6));
            mainColumn.Add(new Tuple<string, int, int>("Ціна за одиницю", intWidth, 7));
            mainColumn.Add(new Tuple<string, int, int>("Постачальник", stringWidth, 8));
            mainColumn.Add(new Tuple<string, int, int>("Телефон постачальника", stringWidth, 9));
            mainColumn.Add(new Tuple<string, int, int>("Дата поставки", dataWidth, 10));
            mainColumn.Add(new Tuple<string, int, int>("Номер складу", stringWidth, 11));
            mainColumn.Add(new Tuple<string, int, int>("Короткий опис", stringWidth, 12));
            mainColumn.Add(new Tuple<string, int, int>("Поле для приміток", stringWidth, 13));


            for (int i = 0; i < mainColumn.Count; i++) sum = sum + mainColumn[i].Item2;
            separateLine = new string('-', sum + mainColumn.Count + 1);
        }

        public void SetInfo(string text)
        {
            for (int i = 0, j = 0, step = 0; i < text.Length; i++)
            {
                if (text[i] == ';')
                {
                    columnContent.Add(text.Substring(step, i - step));
                    j++;
                    step += i - step + 2;
                }
                else if (text[i] == '\n')
                {
                    columnContent.Add(text.Substring(step, i - step));
                }
            }

        }

        public void PrintTextReport()
        {
            using (StreamWriter report = File.CreateText(@"C:\Users\Misha\Desktop\ЗвітТест.txt")) report.WriteLine(separateLine);

            bool allowPrint = true;  //ознака кінця друку//

            string row;
            string columText = "";

            while (allowPrint == true)
            {
                row = "|";    //поточний рядок для друку робимо порожнім//
                allowPrint = false;  //якщо ця змінна ніде не перетвориться в "true" значить виконання while закінчиться
                for (int i = 0; i < mainColumn.Count; i++)
                {
                    if (mainColumn[i].Item1.Length <= mainColumn[i].Item2)
                    {
                        columText = AlignCentre(mainColumn[i].Item1, mainColumn[i].Item2);

                        //заповнюємо пропусками в кількості mainColumn[i].Item2
                        int width = mainColumn[i].Item2;
                        int id = mainColumn[i].Item3;

                        mainColumn.Remove(new Tuple<string, int, int>(mainColumn[i].Item1, mainColumn[i].Item2, mainColumn[i].Item3));
                        mainColumn.Insert(i, new Tuple<string, int, int>(EmptyString(width), width, id));

                    }
                    else
                    {
                        allowPrint = true; // означає, що виконання while повинно продовжуватись 
                        columText = CorrectStringPart(mainColumn[i].Item1, mainColumn[i].Item2);
                        RemoveUsedPart(columText, i);
                    }

                    row += AlignCentre(columText, mainColumn[i].Item2) + '|';
                }
                using (StreamWriter report = File.AppendText(@"C:\Users\Misha\Desktop\ЗвітТест.txt")) report.WriteLine(row);
            }
            using (StreamWriter report = File.AppendText(@"C:\Users\Misha\Desktop\ЗвітТест.txt")) report.WriteLine(separateLine);
        }

        private string CorrectStringPart(string text, int width)
        {
            string[] words = text.Split(' ');
            string content = words[0];
            for (int i = 1; i < words.Length; i++)
                if (content.Length + words[i].Length + 1 < width) content += " " + words[i];
                else break;
            return content;
        }

        private void RemoveUsedPart(string usedPart, int index)
        {
            string subString = mainColumn[index].Item1.Remove(0, usedPart.Length + 1);
            int width = mainColumn[index].Item2;
            int id = mainColumn[index].Item3;

            mainColumn.Remove(new Tuple<string, int, int>(mainColumn[index].Item1, mainColumn[index].Item2, mainColumn[index].Item3));
            mainColumn.Insert(index, new Tuple<string, int, int>(subString, width, id));

        }

        private string AlignCentre(string text, int width)
        {
            if (string.IsNullOrEmpty(text)) return new string(' ', width);
            else return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
        }

        private string EmptyString(int width)
        {
            return new string(' ', width);
        }
    }
}

    

