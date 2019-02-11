using System;
using System.IO;


namespace MyLittleIRAF
{
    public class TableReader
    {
        public TableReader()
        {
        }
        public static double[][] LoadTable(string path)
        {
            StreamReader sr = new StreamReader(path);
                
                
                    string line;
                    int n = 0;
                    int m = 0;
                    char[] delimiterChars = { ' ', '\t', ',' };

                    while ((line=sr.ReadLine()) != null)
                    {
                        n++;
                    }

                    sr = new StreamReader(path);
                    m = sr.ReadLine().Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries).Length;

                    sr = new StreamReader(path);

                    double[][] array = new double[n][];


                    for(int i = 0; i <array.Length; i++)
                    {
                        array[i] = new double[m];
                        string[] str_mas = sr.ReadLine().Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                        for (int j = 0; j < str_mas.Length; j++)
                        {
                            array[i][j] = Convert.ToDouble(str_mas[j]);
                        }

                    }
            return array;

        }

        public static void SaveTable(string path, double[][] table)
        {
            try
            {
                using(StreamWriter sw = new StreamWriter(path))
                {
                    for (int i = 0; i < table.Length; i++)
                    {
                        sw.WriteLine(Convert.ToString(table[i][0]) + "\t" + Convert.ToString(table[i][1]));
                    }
                }
                
            } catch
            {
                Console.WriteLine("Error");
            }
        }
    }
}
