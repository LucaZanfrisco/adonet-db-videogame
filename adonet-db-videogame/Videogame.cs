using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db_videogame
{
    public class Videogame
    {
        public int id {  get; private set; }
        public string Name { get; private set; }
        public string Overview { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public long SoftwareHouse { get; private set; }

        public Videogame(int id, string name, string overview, DateTime releaseDate, long softwareHouse)
        {
            this.id = id;
            this.Name = name;
            this.Overview = overview;
            this.ReleaseDate = releaseDate;
            this.SoftwareHouse = softwareHouse;
        }

        public Videogame(string name, string overview, DateTime releaseDate, long softwareHouse)
        {
            this.Name = name;
            this.Overview = overview;
            this.ReleaseDate = releaseDate;
            this.SoftwareHouse = softwareHouse;
        }
    }
}
