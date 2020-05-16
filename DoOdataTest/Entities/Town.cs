using System;
using Xtensive.Orm;

namespace DoOdataTest.Entities
{
    [HierarchyRoot]
    public class Town : Entity
    {
        protected Town()
        {
        }

        public Town(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        [Field, Key]
        public virtual Guid Id { get; set; }

        [Field(Length = 100)]
        public virtual string Name { get; set; }
    }
}
