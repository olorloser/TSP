using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komiwojazer
{

    class CompetetiveSelection
    {
        Population Population;
        Random random;

        public CompetetiveSelection(Population population, Random random)
        {
            this.Population = population;
            population.getPopulationRate();
            this.random = random;
        }

        public Specimen Tournament (int sizeOfTournament)
        {
            Specimen[] specimen = new Specimen[sizeOfTournament];
            int[] specimenRated = new int[sizeOfTournament];
            for (int i = 0; i < sizeOfTournament; i++)
            {
                int temp = random.Next(Population.amountOfSpecimen);
                specimen[i] = Population.specimen[temp];
                specimenRated[i] = Population.ratedSpecimen[temp];
            }

            int currentLowest = specimenRated[0];
            int currentLowestIndex = 0;

            for (int i = 0; i < sizeOfTournament; i++)
            {
                if (specimenRated[i] < currentLowest)
                {
                    currentLowest = specimenRated[i];
                    currentLowestIndex = i;
                }
            }
            return specimen[currentLowestIndex];
        }

        public Specimen[] Repopulate(int SizeOfNewPopulation, int SizeOfTournament)
        {
            Specimen[] result = new Specimen[SizeOfNewPopulation];
            for (int i = 0; i < SizeOfNewPopulation; i++)
            {
                result[i] = Tournament(SizeOfTournament);
            }
            return result;
        }
    }
}
