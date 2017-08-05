using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextReport
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "1;Samsung Galaxy S8;Смартфон;21.05.2016;необмежено;12;25.230;Samsung;380(44)3905333;12.06.2017;" +
                          "Склад №3;5.8, Super AMOLED, (2960х1440), 4 x 2.3 ГГц + 4 x 1.7 ГГц;Нормальний телефон";
            string text1 = "2;HP ProBook 450 G4;Ноутбут;18.10.2016;необмежено;7;21.780;Hewlett Packard;380(44)7508255;" +
                            "24.06.2017;Склад №2;15.6 (1920x1080) Full HD, матовый, Intel Core i7-7500U, 2.7-3.5 ГГц;крутий комп";

            TextReport rep = new TextReport();
            rep.SetInfoAndPrint(text);
            rep.SetInfoAndPrint(text1);

        }

    }
}

