using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komiwojazer
{
    class Population
    {
        DataReader data;
        public Specimen[] specimen;
        public int[] ratedSpecimen;
        public int amountOfSpecimen;
        public int amountOfPoints;

        public void Populate(Random rnd)
        {
            specimen = new Specimen[amountOfSpecimen]; //Stwórz tablice o wielkosci m (arbitralnej)
            for (int i = 0; i < amountOfSpecimen; i++)
            {
                specimen[i] = new Specimen(amountOfPoints, rnd, data);
            }
        }

        //public int RateSpecimen(Specimen spec, DataReader data) //Private?
        //{
        //    int result = 0;
        //    for (int i = 0; i < spec.body.Length; i++)
        //    {
        //        int current = spec.body[i];
        //        int next;
        //        if (i != spec.body.Length - 1)
        //        {
        //            next = spec.body[i + 1];
        //        }
        //        else
        //        {
        //            next = spec.body[0];
        //        }
        //        result = result + data.distance2(current, next);
        //    }
        //    return result;
        //}

        //public void Rate()
        //{
        //    ratedSpecimen = new int[amountOfSpecimen];
        //    for (int i = 0; i < amountOfSpecimen; i++)
        //    {
        //        ratedSpecimen[i] = RateSpecimen(specimen[i], data);
        //    }
        //}

        public void getPopulationRate()
        {
            ratedSpecimen = new int[amountOfSpecimen];
            for (int i = 0; i < amountOfSpecimen; i++)
            {
                ratedSpecimen[i] = this.specimen[i].rate;
            }
        }
        public Population(int amountOfSpecimen, DataReader data)
        {
            this.amountOfSpecimen = amountOfSpecimen;
            this.data = data;
            specimen = new Specimen[amountOfSpecimen]; //Stwórz tablice o wielkosci m (arbitralnej)
            this.amountOfPoints = data.amountOfLines;
        }

        public Population(Specimen[] specimenToAdd, DataReader data)
        {
            this.amountOfSpecimen = specimenToAdd.GetLength(0);
            this.data = data;
            this.amountOfPoints = data.amountOfLines;
            this.specimen = new Specimen[amountOfSpecimen]; //Stwórz tablice o wielkosci m (arbitralnej)

            for (int i = 0; i < amountOfSpecimen; i++)
            {
                this.specimen[i] = specimenToAdd[i];
            }
            getPopulationRate();
        }

        public Population CreateOffspring(double MutationProbability, double CrossOverProbability, Random random)
        {
            Population result = new Population(this.amountOfSpecimen, this.data);
            double randomChance;
            for (int i = 0; i < this.amountOfSpecimen; i++)
            {
                randomChance = random.NextDouble();
                if (randomChance < MutationProbability)
                {
                    result.specimen[i] = Genetics.InversionMutation(this.specimen[i], random);
                }
                else if (randomChance > MutationProbability && randomChance < MutationProbability + CrossOverProbability && i + 1 < this.amountOfSpecimen)
                {
                    result.specimen[i] = Genetics.OXCrossOver(this.specimen[i], this.specimen[i + 1], random);
                    result.specimen[i + 1] = Genetics.OXCrossOver(this.specimen[i + 1], this.specimen[i], random);
                    i++;
                }
                else //Nic nie rób
                {
                    result.specimen[i] = this.specimen[i];
                }
            }
            return result;
        }

        public void Mutate(double MutationProbability, Random random)
        {
            double randomChance;
            //int oldRate = 0;
            //int newRate = 0;
            for (int i = 0; i < this.amountOfSpecimen; i++)
            {
                randomChance = random.NextDouble();
                if (randomChance < MutationProbability)
                {
                    //oldRate = this.specimen[i].rate;
                    this.specimen[i] = Genetics.InversionMutation(this.specimen[i], random);
                    this.specimen[i].rateSelf();
                    //Do debugowania
                    //newRate = this.specimen[i].rateSelf();
                    //System.Console.WriteLine("zmutowano osobnika o ocenie:{0}, na ocenę: {1}", oldRate, newRate);
                    //System.Console.WriteLine("zmienne: amountofspecimen: {0}, randomchance: {1}, i: {2}", this.amountOfSpecimen, randomChance, i);
                }
            }

            getPopulationRate();
            
        }

       
        public void CrossOver(double CrossoverProbability, Random random)
        {
            double randomChance;
            for (int i = 0; i < this.amountOfSpecimen; i++)
            {
                randomChance = random.NextDouble();
                if (randomChance < CrossoverProbability && i+1 != this.amountOfSpecimen)
                {
                    this.specimen[i] = Genetics.OXCrossOver(this.specimen[i], this.specimen[i+1], random);
                    this.specimen[i + 1] = Genetics.OXCrossOver(this.specimen[i + 1], this.specimen[i], random);
                    this.specimen[i].rateSelf();
                    this.specimen[i+1].rateSelf();
                }
            }
            getPopulationRate();
        }
    }
}
