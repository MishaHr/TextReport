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
        List<Tuple<string, int>> mainColumn = new List<Tuple<string, int>>();

        List<string> columnContent = new List<string>();

        const int dataWidth = 12;
        const int intWidth = 10;
        const int stringWidth = 16;

        int sum;

        string separateLine;

        public TextReport()
        {
            mainColumn.Add(new Tuple<string, int>("ID", intWidth));
            mainColumn.Add(new Tuple<string, int>("Назва товару", stringWidth));
            mainColumn.Add(new Tuple<string, int>("Категорiя", stringWidth));
            mainColumn.Add(new Tuple<string, int>("Дата виготовлення", dataWidth));
            mainColumn.Add(new Tuple<string, int>("Термiн придатностi", stringWidth));
            mainColumn.Add(new Tuple<string, int>("Кількість одиниць", intWidth));
            mainColumn.Add(new Tuple<string, int>("Ціна за одиницю", intWidth));
            mainColumn.Add(new Tuple<string, int>("Постачальник", stringWidth));
            mainColumn.Add(new Tuple<string, int>("Телефон постачальника", stringWidth));
            mainColumn.Add(new Tuple<string, int>("Дата поставки", dataWidth));
            mainColumn.Add(new Tuple<string, int>("Номер складу", stringWidth));
            mainColumn.Add(new Tuple<string, int>("Короткий опис", stringWidth));
            mainColumn.Add(new Tuple<string, int>("Поле для приміток", stringWidth));


            for (int i = 0; i < mainColumn.Count; i++) sum = sum + mainColumn[i].Item2;
            separateLine = new string('-', sum + mainColumn.Count + 1);
        }

        public void SetInfo(string text)
        {
            string[] columnStrings = text.Split(';', '\n');

            for (int i = 0; i < columnStrings.Length; i++)
                columnContent.Add(columnStrings[i]);
        }

        public void PrintTextReport()
        {
            using (StreamWriter report = File.CreateText(@"C:\Users\Misha\Desktop\ЗвітТест.txt")) report.WriteLine(separateLine);

            bool allowPrint = true;

            string row;
            string columText = "";

            while (allowPrint == true)
            {
                row = "|";
                allowPrint = false;
                for (int i = 0; i < mainColumn.Count; i++)
                {
                    if (mainColumn[i].Item1.Length <= mainColumn[i].Item2)
                    {
                        columText = AlignCentre(mainColumn[i].Item1, mainColumn[i].Item2);

                        int width = mainColumn[i].Item2;

                        if (!IsEmptyString(mainColumn[i].Item1))
                        {
                            mainColumn.Remove(new Tuple<string, int>(mainColumn[i].Item1, mainColumn[i].Item2));
                            mainColumn.Insert(i, new Tuple<string, int>(EmptyString(width), width));
                        }

                    }
                    else
                    {
                        allowPrint = true;
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

            mainColumn.Remove(new Tuple<string, int>(mainColumn[index].Item1, mainColumn[index].Item2));
            mainColumn.Insert(index, new Tuple<string, int>(subString, width));

        }

        private string AlignCentre(string text, int width)
        {
            return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
        }

        private string EmptyString(int width)
        {
            return new string(' ', width);
        }

        private bool IsEmptyString(string text)
        {
            for (int i = 0; i < text.Length; i++)
                if (text[i] != ' ') return false;

            return true;
        }
    }
}

    

