using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komiwojazer
{
    class DataReader
    {
        public int amountOfLines = 0;
        public int[,] matrice; //na private?
        public int[,] toTable(String path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    line = sr.ReadLine(); // Pierwsze zczytanie liczby lini by ustalic wielkosc tablicy dwuwymiarowej
                    Int32.TryParse(line, out amountOfLines);
                    int[,] OutputTable = new int[amountOfLines, amountOfLines];
                    int index1 = 0;
                    int index2 = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        String[] linia = line.Split(' ');
                        foreach (String liczba in linia)
                        {
                            if (!string.IsNullOrWhiteSpace(liczba))
                            {
                                OutputTable[index1, index2] = Int32.Parse(liczba);
                                if (index1 != index2)
                                {
                                    OutputTable[index2, index1] = OutputTable[index1, index2];
                                }
                                index1++;
                            }
                        }
                        index1 = 0;
                        index2++;
                    }
                    return OutputTable;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public int distance(int[,] table, int x, int y)
        {
            return table[x, y];
        }

        public int distance2(int x, int y)
        {
            return matrice[x, y];
        }

        public DataReader(string path)
        {
            matrice = toTable(path);
        }
    }
}
