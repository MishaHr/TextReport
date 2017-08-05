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
            string text1 = "1;Samsung Galaxy S8;Смартфон;21.05.2016;необмежено;12;25.230;Samsung;380(44)3905333;12.06.2017;" +
                          "Склад №3;5.8, Super AMOLED, (2960х1440), 4 x 2.3 ГГц + 4 x 1.7 ГГц;Нормальний телефон";
            string text2 = "1;Samsung Galaxy S8;Смартфон;21.05.2016;необмежено;12;25.230;Samsung;380(44)3905333;12.06.2017;" +
                          "Склад №3;5.8, Super AMOLED, (2960х1440), 4 x 2.3 ГГц + 4 x 1.7 ГГц;Нормальний телефон";
            string text3 = "1;Samsung Galaxy S8;Смартфон;21.05.2016;необмежено;12;25.230;Samsung;380(44)3905333;12.06.2017;" +
                          "Склад №3;5.8, Super AMOLED, (2960х1440), 4 x 2.3 ГГц + 4 x 1.7 ГГц;Нормальний телефон";

            TextReport rep = new TextReport();
            rep.SetInfo(text);
            rep.SetInfo(text1);
            rep.SetInfo(text2);
            rep.SetInfo(text3);

        }

    }
}

