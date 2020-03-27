using System;
using Xtensive.Orm;

namespace DoOdataTest.Entities
{
    [HierarchyRoot]
    public class WeatherForecast : Entity
    {
        [Field, Key]
        public virtual Guid Id { get; set; }

        [Field]
        public virtual DateTime Date { get; set; }

        [Field]
        public virtual int TemperatureC { get; set; }

        [Field(Length = 100)]
        public virtual string Summary { get; set; }
    }
}
