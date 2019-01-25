using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Komiwojazer
{
    class Program
    {
        public static Random random = new Random();
        static void Main(string[] args)
        {
            string path = @"D:\komiwoj\Komiwoj\berlin52.txt";
            int amountOfSpecimenToProduce = 100; //20 jest tu arbitralna wielkoscia tablicy specimenow
            double mutationChance = 0.05;
            double crossoverChance = 0.95;
            int sizeOfTournament = 20; //Do ustalania jako paramter
            int sizeOfNewPopulation = 100; //
            DataReader data = new DataReader(path);
            Population population = new Population(amountOfSpecimenToProduce, data);
            population.Populate(random);
            //population.Rate();

            //Okej paramtery

            //int amountOfSpecimenToProduce = 600;
            //double mutationChance = 0.05;
            //double crossoverChance = 0.55;
            //int sizeOfTournament = 60;

            //////////////////////////////
            //Debug losowosc populacja//////
            //////////////////////////////

            //for (int i = 0; i < amountOfSpecimenToProduce; i++)
            //{
            //    System.Console.WriteLine(population.ratedSpecimen[i]);
            //}
            //System.Console.ReadKey();

            //////////////////////////////
            //Debug seleckja turniejowa///
            //////////////////////////////

            //CompetetiveSelection cs = new CompetetiveSelection(population, random);
            //Population newPopulationCompetetive = new Population(cs.Repopulate(sizeOfNewPopulation, sizeOfTournament), data);
            //newPopulationCompetetive.Rate();

            //System.Console.WriteLine("Po selekcji");
            //for (int i = 0; i < newPopulationCompetetive.amountOfSpecimen; i++)
            //{
            //    System.Console.WriteLine(newPopulationCompetetive.ratedSpecimen[i]);
            //}
            //System.Console.ReadKey(); 

            //////////////////////////////
            //Debug seleckja ruletki//////
            //////////////////////////////

            //RouletteSelection rs = new RouletteSelection(population, random);
            //Population newPopulationRoulette = new Population(rs.Roulette(sizeOfNewPopulation), data);
            //newPopulationRoulette.Rate();

            //for (int i = 0; i < newPopulationRoulette.amountOfSpecimen; i++)
            //{
            //    System.Console.WriteLine(newPopulationRoulette.ratedSpecimen[i]);
            //}
            //System.Console.ReadKey();

            //////////////////////////////
            //Część właściwa programu/////
            //////////////////////////////

            int LowestRateFound = 35000; //Arbitralnie duza wartosc bo sie kompilator czepia ze mozliwe ze sie nie zincjalizuje
            Specimen BestSpeciman = new Specimen(); //Przechowam najlepszego osobnika by sprawdzic jaka trase objal

            /////////////////////////////////////////////
            //Selekcja turniejowa; i - liczba iteracji///
            /////////////////////////////////////////////

            Population iteration = new Population(sizeOfNewPopulation, data); //Zainicjalizowane żeby kompilator sie nie czepial o niezaicjalizowana zmienna (bo powinien być do while zamiast if)
            CompetetiveSelection selectedPopulation;
            for (int i = 0; i < 1000; i++)
            {
                if (i == 0)
                {
                    selectedPopulation = new CompetetiveSelection(population, random);
                    iteration = new Population(selectedPopulation.Repopulate(sizeOfNewPopulation, sizeOfTournament), data);
                    iteration.Mutate(mutationChance, random);
                    iteration.CrossOver(crossoverChance, random);

                    for (int j = 0; j < iteration.amountOfSpecimen; j++)
                    {
                        if (iteration.ratedSpecimen[j] < LowestRateFound)
                        {
                            LowestRateFound = iteration.ratedSpecimen[j];
                            BestSpeciman = iteration.specimen[j];
                        }
                    }

                }
                else
                {
                    selectedPopulation = new CompetetiveSelection(iteration, random);
                    iteration = new Population(selectedPopulation.Repopulate(sizeOfNewPopulation, sizeOfTournament), data);
                    iteration.Mutate(mutationChance, random);
                    iteration.CrossOver(crossoverChance, random);

                    for (int j = 0; j < iteration.amountOfSpecimen; j++)
                    {
                        if (iteration.specimen[j].rate < LowestRateFound)
                        {
                            LowestRateFound = iteration.specimen[j].rate;
                            BestSpeciman = new Specimen(iteration.specimen[j]);
                        }
                    }
                }

                ////////////////////////////
                //Wyswietlanie iteracji/////
                ////////////////////////////
                //if (i % 100 == 0)
                //{
                //    System.Console.WriteLine("Po Iteracjach {0}, Mutacja: {1}, Crossover: {2}", i, mutationChance, crossoverChance);
                //    for (int j = 0; j < iteration.amountOfSpecimen; j++)
                //    {
                //        //System.Console.WriteLine(iteration.ratedSpecimen[j]);
                //    }
                //    System.Console.WriteLine("Najlepszy:{0}", LowestRateFound);
                //    System.Console.ReadKey();
                //}
            }

            ////////////////////////////
            //Selekcja ruletkowa////////
            ////////////////////////////

            //RouletteSelection selectedPopulation1;

            //for (int i = 0; i < 10000; i++)
            //{
            //    if (i == 0)
            //    {
            //        selectedPopulation1 = new RouletteSelection(population, random);
            //        iteration = new Population(selectedPopulation1.Roulette(sizeOfNewPopulation), data);
            //        iteration.Mutate(mutationChance, random);
            //        iteration.CrossOver(crossoverChance, random);

            //        for (int j = 0; j < iteration.amountOfSpecimen; j++)
            //        {
            //            if (iteration.ratedSpecimen[j] < LowestRateFound)
            //            {
            //                LowestRateFound = iteration.ratedSpecimen[j];
            //                BestSpeciman = iteration.specimen[j];
            //            }
            //        }

            //    }
            //    else
            //    {
            //        selectedPopulation1 = new RouletteSelection(iteration, random);
            //        iteration = new Population(selectedPopulation1.Roulette(sizeOfNewPopulation), data);
            //        iteration.Mutate(mutationChance, random);
            //        iteration.CrossOver(crossoverChance, random);

            //        for (int j = 0; j < iteration.amountOfSpecimen; j++)
            //        {
            //            if (iteration.ratedSpecimen[j] < LowestRateFound)
            //            {
            //                LowestRateFound = iteration.ratedSpecimen[j];
            //                BestSpeciman = iteration.specimen[j];
            //            }
            //        }
            //    }
            //}

            ////////////////////////////////
            //Wyniki - ostatnia populacja///
            ////////////////////////////////

            System.Console.WriteLine("Po Iteracjach");
            for (int i = 0; i < iteration.amountOfSpecimen; i++)
            {
                //System.Console.WriteLine(iteration.ratedSpecimen[i]);
            }
            System.Console.WriteLine("Najlepszy:{0}", LowestRateFound);
            String trasa = String.Empty;

            foreach (int i in BestSpeciman.body)
            {
                if(String.IsNullOrEmpty(trasa))
                {
                    trasa = trasa + Convert.ToString(i);
                }
                else
                {
                    trasa = trasa + "-" + Convert.ToString(i);
                }
            }
            System.Console.WriteLine(trasa);
            System.Console.ReadKey();
        }


    }
    }
