using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;

using Easter.Core.Contracts;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops;
using Easter.Models.Workshops.Contracts;
using Easter.Repositories;
using Easter.Utilities.Messages;

namespace Easter.Core
{
    public class Controller : IController
    {

        private EggRepository eggs;
        private BunnyRepository bunnies;
        private int coloredEggs = 0;

        public Controller()
        {
            this.bunnies = new BunnyRepository();
            this.eggs = new EggRepository();
        }

        public string AddBunny(string bunnyType, string bunnyName)
        {
            if (bunnyType == "HappyBunny")
            {
                bunnies.Add(new HappyBunny(bunnyName));
            }
            else if (bunnyType == "SleepyBunny")
            {
                bunnies.Add(new SleepyBunny(bunnyName));
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);
            }

            return string.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            IBunny bunny = bunnies.FindByName(bunnyName);
            IDye dye = new Dye(power);
            if (bunny.Name == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);
            }

            bunny.AddDye(dye);

            return string.Format(OutputMessages.DyeAdded, power, bunnyName);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            this.eggs.Add(new Egg(eggName, energyRequired));
            return string.Format(OutputMessages.EggAdded, eggName);
        }

        public string ColorEgg(string eggName)
        {
            IEgg egg = eggs.FindByName(eggName);
            IWorkshop workshop = new Workshop();
            List<IBunny> workedBunnies = bunnies.Models
                                                .Where(x => x.Energy >= 50)
                                                .OrderByDescending(e=>e.Energy)
                                                .ToList();

            if (workedBunnies.Any() == false)
            {
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);
            }

            while (workedBunnies.Any())
            {
                IBunny currentBunny = workedBunnies.First();

                while (egg.IsDone() == false)
                {
                    if (currentBunny.Energy == 0 || currentBunny.Dyes.All(x => x.IsFinished()))
                    {
                        workedBunnies.Remove(currentBunny);
                        break;
                    }

                    workshop.Color(egg, currentBunny);
                }

                if (egg.IsDone())
                {
                    break;
                }

            }

            if (egg.IsDone() == true)
            {
                coloredEggs++;
                eggs.Remove(egg);
                return string.Format(OutputMessages.EggIsDone, eggName);
            }

            return string.Format(OutputMessages.EggIsNotDone, eggName);
        }


        public string Report()
        {

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{coloredEggs} eggs are done!");
            sb.AppendLine("Bunnies info:");
            foreach (var bunny in bunnies.Models.Where(b=>b.Energy > 0))
            {
                sb.Append($"Name: {bunny.Name}" + Environment.NewLine +
                          $"Energy: {bunny.Energy}" + Environment.NewLine +
                          $"Dyes {bunny.Dyes.Count} not finished"
                          + Environment.NewLine);
            }

            return sb.ToString().TrimEnd();

        }
    }
}

