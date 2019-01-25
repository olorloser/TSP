using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komiwojazer
{
    class Specimen
    {
        public int n; //Długość tablicy; problemu rozwazanego (52 dla Berlin52)
        public int[] body; //Ciało osobnika;
        public int rate; //Ocena osobnika
        public DataReader data;

        public int[] CreateBody(Random rnd)
        {
            int[] bodyToReturn = new int[n];
            bool[] aviableValues; //Robie tablice booleanow by wiedziec ktore punkty juz wykorzystalem w konkretnej permutacji
            aviableValues = new bool[n];
            for (int i = 0; i < n; i++)
            {
                aviableValues[i] = true;
            }
            //Ustawiam wszystkie na true (możiwe do pominiecia jesli mozna ustalic domyslne wartosci przy tworzeniu)

            //Po kolei zapełniam ciało osobnika kolejnymi wierzchołkami które wyznaczą ścieżkę
            for (int i = 0; i < n; i++)
            {
                int tempIndex = rnd.Next(aviableValues.Length); //aviableValues.Length = n
                bool temp = aviableValues[tempIndex]; //Sprawdzam czy dany punkt juz wykorzystalem w sciezce
                while (temp == false) //Dopoki trafiam na wykorzystane punkty
                {
                    tempIndex++; //Ide na kolejny punkt
                    if (tempIndex >= n) //Jezeli to byl nty punkt to wracam do pierwszego
                    {
                        tempIndex = 0;
                    }
                    temp = aviableValues[tempIndex]; //Znowu sprawdzam czy ten punkt nie byl juz uzyty

                }
                bodyToReturn[i] = tempIndex; //Jezeli trafilem na nieuzyty punkt to kolejnym punktem w sciezce jest wlasnie ten punkt
                aviableValues[tempIndex] = false; //Ozanczam ze ten punkt juz wykorzystalem
            }

            return bodyToReturn; //Zwracam tablice bedaca ciagiem punktow po ktorych ide

        }

        public int rateSelf ()
        {
            int result = 0;
            for (int i = 0; i < this.body.Length; i++)
            {
                int current = this.body[i];
                int next;
                if (i != this.body.Length - 1)
                {
                    next = this.body[i + 1];
                }
                else
                {
                    next = this.body[0];
                }
                result = result + data.distance2(current, next);
            }
            return result;
        }

        public Specimen(int amount, Random rnd, DataReader data) //Konstruktor tworzacy losowych osobnikow
        {
            this.n = amount;
            body = CreateBody(rnd);
            this.data = data;
            this.rate = rateSelf();
        }
        public Specimen(int amount, DataReader data) //Konstruktor tworzacy pusta tablice osobnikow
        {
            this.n = amount;
            this.data = data;
            body = new int[amount];
        }

        public Specimen(Specimen specimen)
        {
        n = specimen.n; //Długość tablicy; problemu rozwazanego (52 dla Berlin52)
        body = new int[specimen.body.Length]; //Ciało osobnika;
        Array.Copy(specimen.body, body, specimen.body.Length);
        rate = specimen.rate; //Ocena osobnika
        data = specimen.data;
        }

        public Specimen()
        {
            n = 0;
            body = null;
            rate = 0;
            data = null;
        }
    }
}
