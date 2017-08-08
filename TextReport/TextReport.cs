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

        List<Tuple<string, int>> columnContent = new List<Tuple<string, int>>();

        private const char cellHorizontalLine = '─';
        private const string cellVerticalLine = "│";

        public static string Path { get; set; }

        private int tableSize;

        private string separateLine;

        const int dataWidth = 12;
        const int intWidth = 10;
        const int stringWidth = 16;

        public TextReport()
        {
            SetMainColumn();
            SetSeparateLine();

            Path = @"Звіт.txt";

            using (StreamWriter report = File.CreateText(Path)) report.WriteLine(separateLine);

            PrintTextReport(mainColumn);
        }

        public TextReport(string path)
        {
            SetMainColumn();
            SetSeparateLine();

            Path = @path;

            using (StreamWriter report = File.CreateText(@path)) report.WriteLine(separateLine);

            PrintTextReport(mainColumn);
        }

        private void SetMainColumn()
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
        }

        private void SetSeparateLine()
        {
            for (int i = 0; i < mainColumn.Count; i++) tableSize = tableSize + mainColumn[i].Item2;
            separateLine = new string(cellHorizontalLine, tableSize + mainColumn.Count + 1);
        }

        public void SetAndPrintInfo(string text)
        {
            string[] columnStrings = text.Split(';');

            for (int i = 0; i < columnStrings.Length; i++)
                columnContent.Add(new Tuple<string, int>(columnStrings[i], mainColumn[i].Item2));

            PrintTextReport(columnContent);
        }

        private void PrintTextReport(List<Tuple<string, int>> column)
        {            
            bool allowPrint = true;

            string row;
            string columText = "";

            while (allowPrint == true)
            {
                row = cellVerticalLine;
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
                            column.Insert(i, new Tuple<string, int>(SetEmptyString(width), width));
                        }

                    }
                    else
                    {
                        allowPrint = true;
                        columText = CorrectStringPart(column, i);
                        RemoveUsedWords(column,columText, i);
                    }
                    row += AlignCentre(columText, column[i].Item2) + cellVerticalLine;
                }
                using (StreamWriter report = File.AppendText(Path)) report.WriteLine(row);
            }
            using (StreamWriter report = File.AppendText(Path)) report.WriteLine(separateLine);

            columnContent.Clear();

        }

        private string CorrectStringPart(List<Tuple<string, int>> column ,int index)
        {
            string[] words = column[index].Item1.Split(' ');
            int width = column[index].Item2;
            string content;
            if (words[0].Length > width)
                content = WordTransmit(column,index, words[0].Substring(0, width - 1));

            else content = words[0]; 

            for (int i = 1; i < words.Length; i++)
                if (content.Length + words[i].Length + 1 < width) content += " " + words[i];
                else break;
            return content;
        }

        private string WordTransmit(List<Tuple<string, int>> column, int index, string word)
        {
            string text = column[index].Item1.Substring(column[index].Item2 - 1);
            int width = column[index].Item2;
            column.Remove(new Tuple<string, int>(column[index].Item1, column[index].Item2));
            column.Insert(index, new Tuple<string, int>(word + " -" +  text, width));
            return word + "-";
        }

        private void RemoveUsedWords(List<Tuple<string, int>> column, string usedPart, int index)
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

        private string SetEmptyString(int width)
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

    

