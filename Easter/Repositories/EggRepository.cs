using System.Collections.Generic;
using System.Linq;
using Easter.Models.Eggs.Contracts;
using Easter.Repositories.Contracts;

namespace Easter.Repositories
{
    public class EggRepository : IRepository<IEgg>
    {
        private List<IEgg> eggsToColor;

        public EggRepository()
        {
            this.eggsToColor = new List<IEgg>();
        }

        public IReadOnlyCollection<IEgg> Models => this.eggsToColor.AsReadOnly();

        public void Add(IEgg model) => this.eggsToColor.Add(model);

        public IEgg FindByName(string name) => this.eggsToColor.FirstOrDefault(m => m.Name == name);

        public bool Remove(IEgg model) => this.eggsToColor.Remove(model);
    }
}
