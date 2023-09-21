// See https://aka.ms/new-console-template for more information


using adonet_db_videogame;


Console.WriteLine("Benvenuto nella gestione di Videogichi");

bool check = false;

while(!check)
{
    Console.Write(@"
1 - Inserire un nuovo videogioco
2 - Ricerca un videogioco per ID
3 - Ricercare tutti i videogiochi per nome
4 - Cancellare un videogioco
5 - Esci
Seleziona un numero per svolgere una azione: ");
    int choice = int.Parse(Console.ReadLine());

    switch(choice)
    {
        case 1:
            try
            {
                Console.Write("Inserire il nome del videogioco: ");
                string name = Console.ReadLine();

                Console.Write("Inserire la descrizione: ");
                string overview = Console.ReadLine();

                Console.Write("Inserie la data di rilascio (dd/mm/yyyy): ");
                DateTime releaseDate = DateTime.Parse(Console.ReadLine());

                List<SoftwareHouse> houses = VideogameManager.GetSoftwareHouses();
                foreach(SoftwareHouse house in houses)
                {
                    Console.WriteLine(house.ToString());
                }
                Console.WriteLine("Inserire il numero della software house: ");
                long softwareHouseNumber = long.Parse(Console.ReadLine());

                Videogame newVideogame = new Videogame(name, overview, releaseDate, softwareHouseNumber);
                bool insertResult = VideogameManager.InsertNewVideogame(newVideogame);

                if(insertResult)
                {
                    Console.WriteLine("Videogioco aggiunto con successo");
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            break;
        case 2:
            try
            {
                Console.Write("Inserire il numero del videogioco da cercare: ");
                long searchId = long.Parse(Console.ReadLine());

                Videogame videogame = VideogameManager.SearchVideogameById(searchId);
                if(videogame != null)
                {
                    Console.WriteLine(videogame.ToString());
                }
                else
                {
                    Console.WriteLine($"Nessun videogioco con id:{searchId} travato");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            break;
        case 3:
            try
            {
                Console.Write("Inserire il nome del videogioco che si vuole cercare: ");
                string searchName = Console.ReadLine();


                List<Videogame> searchedVideogame = VideogameManager.SearchVideogameByName(searchName);
                if(searchedVideogame.Count > 0)
                {
                    foreach(Videogame videogame in searchedVideogame)
                    {
                        Console.WriteLine(videogame.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("Nessun videogioco trovato");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            break;
        case 4:
            try
            {
                Console.Write("Inserire il numero del videogioco che si vuole cancellare: ");
                long deleteId = long.Parse(Console.ReadLine());

                bool deletedResult = VideogameManager.DeleteVideogameById(deleteId);
                if(deletedResult)
                    Console.WriteLine("Videogioco cancellato con successo");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            break;
        case 5:
            Console.WriteLine("Arrivederci!!");
            check = true;
            break;
        default:
            Console.WriteLine("Il numero inserito non corrisponde a nessuna azione!");
            break;
    }
}