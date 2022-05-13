using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;
using Easter.Repositories;

namespace Easter.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public void Color(IEgg egg, IBunny bunny)
        {
            while (bunny.Energy > 0 && bunny.Dyes.Any())
            {
                IDye dye = bunny.Dyes.First();
                while (dye.IsFinished() == false && egg.IsDone() == false && bunny.Energy > 0)
                {
                    dye.Use();
                    bunny.Work();
                    egg.GetColored();

                }
                if (dye.IsFinished() == true)
                {
                    bunny.Dyes.Remove(dye);

                }
                if (egg.IsDone() == true)
                {
                    break;
                }
            }
        }
    }
}
