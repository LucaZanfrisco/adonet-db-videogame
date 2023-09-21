using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db_videogame
{
    public class Videogame
    {
        public long Id {  get; private set; }
        public string Name { get; private set; }
        public string Overview { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public long SoftwareHouse { get; private set; }

        public Videogame(long id, string name, string overview, DateTime releaseDate, long softwareHouse)
        {
            this.Id = id;
            SetName(name);
            SetName(overview);
            this.ReleaseDate = releaseDate;
            this.SoftwareHouse = softwareHouse;
        }

        public Videogame(string name, string overview, DateTime releaseDate, long softwareHouse)
        {
            this.Id = 0;
            SetName(name);
            SetOverview(overview);
            this.ReleaseDate = releaseDate;
            this.SoftwareHouse = softwareHouse;
        }

        private void SetName(string name)
        {
            if(string.IsNullOrEmpty(name))
                throw new Exception("Il nome del videogioco non puo essere vuoto o nullo");

            this.Name = name;
        }

        private void SetOverview(string overview)
        {
            if(string.IsNullOrEmpty(overview))
                throw new Exception("Il contenuto della descrizione non puo essere vuoto o nullo");

            this.Overview = overview;
        }

        private void SetSoftwareHouse(long softwareHouse)
        {
            List<SoftwareHouse> softwareHouses = VideogameManager.GetSoftwareHouses();

            bool check = false;
            for(int i = 0; i < softwareHouses.Count; i++)
            {
                if(softwareHouses[i].Id == softwareHouse)
                {
                    check = true; 
                    break;
                }
            }
            if(check == false)
            {
                throw new Exception("Il numero della Software House inserito non esiste");
            }
            this.SoftwareHouse = softwareHouse;
        }


        public override string ToString()
        {
            return $@"
{this.Name.ToUpper()} 

Descrizione:
------------
{this.Overview}
------------

Data di rilascio: {this.ReleaseDate}
";
        }

    }
}
