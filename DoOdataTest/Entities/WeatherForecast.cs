using System;
using Xtensive.Orm;

namespace DoOdataTest.Entities
{
    [HierarchyRoot]
    public class WeatherForecast : Entity
    {
        protected WeatherForecast()
        {
        }

        public WeatherForecast(Town town)
        {
            if (town == null)
                throw new ArgumentNullException(nameof(town));

            Town = town;
        }

        [Field, Key]
        public virtual Guid Id { get; set; }

        [Field]
        public virtual DateTime Date { get; set; }

        [Field]
        public virtual int TemperatureC { get; set; }

        [Field(Length = 100)]
        public virtual string Summary { get; set; }

        [Field]
        public virtual Town Town { get; set; }
    }
}
