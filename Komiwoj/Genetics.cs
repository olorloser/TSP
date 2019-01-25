using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komiwojazer
{
    static class Genetics
    {
        public static Specimen InversionMutation(Specimen osobnik, Random random)
        {
            int k1 = 1 + random.Next(osobnik.body.GetLength(0) - 2); //Nie chce 'stawiac kreski' przed pierwszym i po ostatnim elemencie
            int k2 = 1 + random.Next(osobnik.body.GetLength(0) - 2);
            while (k1 == k2) //Jeżeli wylosuje w tym samym miejscu to losuj dalej
            {
                k2 = 1 + random.Next(osobnik.body.GetLength(0) - 2);
            }
            int k1temp = Math.Min(k1, k2);
            int k2temp = Math.Max(k1, k2);
            k1 = k1temp;
            k2 = k2temp;
            //if (k1 < k2)
            //{
                for(int i = k1, j = k2; i < j; i++,j--)
                {
                    int TempGene = osobnik.body[i];
                    osobnik.body[i] = osobnik.body[j];
                    osobnik.body[j] = TempGene;
                }
            //}
            //else
            //{
            //    for (int i = k2, j = k1; i < j; i++, j--)
            //    {
            //       int TempGene = osobnik.body[i];
            //        osobnik.body[i] = osobnik.body[j];
            //        osobnik.body[j] = TempGene;
            //    }
            //}
            osobnik.rate = osobnik.rateSelf();
            return osobnik;
        }

        public static Specimen OXCrossOver(Specimen osobnik1, Specimen osobnik2, Random random)
        {
            int k1 = 1 + random.Next(osobnik1.body.GetLength(0) - 2); //Nie chce 'stawiac kreski' przed pierwszym i po ostatnim elemencie
            int k2 = 1 + random.Next(osobnik1.body.GetLength(0) - 2);
            Queue<int> usedValues = new Queue<int>();
            Specimen Offspring = new Specimen(osobnik1.body.GetLength(0), osobnik1.data);

            while (k1 == k2) //Jeżeli wylosuje w tym samym miejscu to losuj dalej
            {
                k2 = 1 + random.Next(osobnik1.body.GetLength(0) - 2);
            }

            if(k1 > k2)
            {
                int tempK = k1;
                k1 = k2;
                k2 = tempK;
            }

            for (int i = k1; i <= k2; i++) //Przepisuje ciało
            {
                Offspring.body[i] = osobnik1.body[i];
                usedValues.Enqueue(Offspring.body[i]);
            }
            //Lista (FIFO)dostepnych wartosci
            Queue<int> aviableValues = new Queue<int>();
            //int[] tempArray = new int[Offspring.body.GetLength(0) - (k2 - k1)];

            for (int i = k2+1; i < Offspring.body.GetLength(0); i++)
            {
                //Jezeli Osobnik2.body[i] nie nalezy do listy uzytych wartosci
                //Spushuj do FIFO
                if(!(usedValues.Contains(osobnik2.body[i])))
                {
                    aviableValues.Enqueue(osobnik2.body[i]);
                }
            }

            for (int i = 0; i < k2+1; i++)
            {
                //Jezeli Osobnik2.body[i] nie nalezy do listy uzytych wartosci
                //Spushuj do FIFO
                if (!(usedValues.Contains(osobnik2.body[i])))
                {
                    aviableValues.Enqueue(osobnik2.body[i]);
                }
            }
            //////////////////////

            for (int i = k2+1; i < Offspring.body.GetLength(0); i++)
            {
                //Offspring.body[i] = Pierwszy z FIFO
                Offspring.body[i] = aviableValues.Dequeue();
            }

            for (int i = 0; i < k1; i++)
            {
                //Offspring.body[i] = Pierwszy z FIFO
                Offspring.body[i] = aviableValues.Dequeue();
            }
            Offspring.rate = Offspring.rateSelf();
            return Offspring;
        }
    }
}
