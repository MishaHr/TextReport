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
        static string Path { get; set; }

        static List<Tuple<string, int>> mainColumn = new List<Tuple<string, int>>();

        List<Tuple<string, int>> columnContent = new List<Tuple<string, int>>();

        const int dataWidth = 12;
        const int intWidth = 10;
        const int stringWidth = 16;

        int tableSize;

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


            for (int i = 0 ; i < mainColumn.Count; i++) tableSize = tableSize + mainColumn[i].Item2;
            separateLine = new string('-', tableSize + mainColumn.Count + 1);

            using (StreamWriter report = File.CreateText(@"C:\Users\Misha\Desktop\ЗвітТест.txt")) report.WriteLine(separateLine);

            PrintTextReport(mainColumn);
        }

        public void SetInfo(string text)
        {
            string[] columnStrings = text.Split(';');

            for (int i = 0; i < columnStrings.Length; i++)
                columnContent.Add(new Tuple<string, int>(columnStrings[i], mainColumn[i].Item2));

            PrintTextReport(columnContent);
        }

        public void PrintTextReport(List<Tuple<string, int>> column)
        {            
            bool allowPrint = true;

            string row;
            string columText = "";

            while (allowPrint == true)
            {
                row = "|";
                allowPrint = false;
                for (int i = 0; i < column.Count; i++)
                {
                    if (column[i].Item1.Length <= column[i].Item2)
                    {
                        columText = AlignCentre(column[i].Item1, column[i].Item2);

                        int width = column[i].Item2;

                        if (!IsEmptyString(column[i].Item1))
                        {
                            column.Remove(new Tuple<string, int>(column[i].Item1, column[i].Item2));
                            column.Insert(i, new Tuple<string, int>(EmptyString(width), width));
                        }

                    }
                    else
                    {
                        allowPrint = true;
                        columText = CorrectStringPart(column[i].Item1, column[i].Item2);
                        RemoveUsedPart(column,columText, i);
                    }
                    row += AlignCentre(columText, column[i].Item2) + '|';
                }
                using (StreamWriter report = File.AppendText(@"C:\Users\Misha\Desktop\ЗвітТест.txt")) report.WriteLine(row);
            }
            using (StreamWriter report = File.AppendText(@"C:\Users\Misha\Desktop\ЗвітТест.txt")) report.WriteLine(separateLine);

            columnContent.Clear();

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

        private void RemoveUsedPart(List<Tuple<string, int>> column, string usedPart, int index)
        {
            string subString = column[index].Item1.Remove(0, usedPart.Length + 1);
            int width = column[index].Item2;

            column.Remove(new Tuple<string, int>(column[index].Item1, column[index].Item2));
            column.Insert(index, new Tuple<string, int>(subString, width));

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

    

