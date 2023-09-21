using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db_videogame
{
    public class SoftwareHouse
    {

        public long Id {  get; private set; }
        public string Name { get; private set; }

        public SoftwareHouse(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $@"
ID: {this.Id} - Name: {this.Name}
------------";
        }
    }
}
