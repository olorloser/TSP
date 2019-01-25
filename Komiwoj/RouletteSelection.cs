using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komiwojazer
{
    class RouletteSelection
    {

        Population Population;
        Random random;

        public RouletteSelection(Population population, Random random)
        {
            this.Population = population;
            this.random = random;
        }

        public Specimen[] Roulette(int sizeOfRoulette)
        {
            Specimen[] specimen = new Specimen[sizeOfRoulette]; //Tablica na osobników którzy 'przychodzą' z poprzedniej populacji
            Specimen[] specimenResult = new Specimen[sizeOfRoulette]; //Tablica osobników będących nową populacją
            int[] specimenRated = new int[sizeOfRoulette]; //Tablica ocen osobników
            double[] specimenProbability = new double[sizeOfRoulette]; //Tablica prawdopodobienstw kazdego z osobników
            double[] probabilityTable = new double[sizeOfRoulette]; //Tablica ktora zlacza prawdopodobienstwo kazdgeo z osobnikow
            double sumOfProbablity = 0; //Suma prawdopodobienstaa (dodaje sie do 1)
            double highestProbability = 0;
            int sumOfRoulette = 0; //Suma ruletki (sumuje wszystkie wartosci ocen)
            for (int i = 0; i < sizeOfRoulette; i++) //Dla wielkości ruletki (wielkość nowej populacji)
            {
                int temp = random.Next(Population.amountOfSpecimen); //Wybieram losowego osobnika
                specimen[i] = Population.specimen[temp]; //To co wyżej
                specimenRated[i] = Population.specimen[temp].rate; //Zapamiętuje też jego ocenę
                sumOfRoulette = sumOfRoulette + specimenRated[i]; //Suma ruletki powiększa się o ocenę
            }

            for (int i = 0; i < sizeOfRoulette; i++) //Dla wielkosci ruletki (wielkosci nowej populacji)
            {
                specimenProbability[i] = (double)specimenRated[i] / (double)sumOfRoulette; //Prawdopodobienstwo i-tego osobnika ze zostanie wylosowany
                if (specimenProbability[i] > highestProbability)
                {
                    highestProbability = specimenProbability[i];
                } 
                //sumOfProbablity = sumOfProbablity + specimenProbability[i]; //Ile z "1" prawdopodobienstwa juz mam zajęte
                //probabilityTable[i] = sumOfProbablity; //Jakiej wylosowanej liczbie < 1 będzie odpowiadał i-ty osobnik
            }

            for (int i = 0; i < sizeOfRoulette; i++) //Dla wielkosci ruletki (wielkosci nowej populacji)
            {
                sumOfProbablity = sumOfProbablity + specimenProbability[i]; //Ile z "1" prawdopodobienstwa juz mam zajęte
                probabilityTable[i] = highestProbability - sumOfProbablity + 1; //Jakiej wylosowanej liczbie < 1 będzie odpowiadał i-ty osobnik
            }

            for (int i = 0; i < sizeOfRoulette; i++) //Dla wielkości ruletki (wielkosci nowej populacji)
            {
                double randomNumber = random.NextDouble(); //Losuje losowa liczbe (0,1>
                for(int j = sizeOfRoulette-1; j > 0; j--) //Dla całego zakresu (0,1>
                {
                    if (j - 1 > 0 && randomNumber >= probabilityTable[j] && randomNumber < probabilityTable[j - 1]) //Jeżeli wylosowana liczba jest większa od pierwszej zmapowanej liczby i mniejsza od drugiej; to drugi osobnik jest tym wylosowanym
                    {
                        specimenResult[i] = specimen[j - 1];
                        break;
                    }
                    else if (randomNumber <= probabilityTable[j]) //Jezeli wylosowana liczba jest mniejsza od rozwazanej liczby to jest szukana liczba
                    {
                        specimenResult[i] = specimen[j];
                        break;
                    }
                    else if (randomNumber < probabilityTable[0] && randomNumber >= probabilityTable[1])
                    {
                        specimenResult[i] = specimen[0];
                        break;
                    }
                        //W przeciwnym razie szukam dalej na prawo

                }

            }

            return specimenResult;
        }
    }
}
