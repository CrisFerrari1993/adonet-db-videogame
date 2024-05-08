namespace adonet_db_videogame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userChoice = "";

            while (userChoice != "esci")
            {
                Console.WriteLine("Scegli il tipo di operazione fra:");
                Console.WriteLine("1 - inserire un nuovo videogioco");
                Console.WriteLine("2 - ricercare un videogioco per id");
                Console.WriteLine("3 - ricercare tutti i videogiochi aventi il nome contenente una determinata stringa inserita in input");
                Console.WriteLine("4 - cancellare un videogioco");
                Console.WriteLine("5 - chiudere il programma");
                userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        {
                            Console.Write("Inserisci nome videogioco: ");
                            string nome = Console.ReadLine();
                            Console.Write("Inserisci descrizione videogioco: ");
                            string overview = Console.ReadLine();
                            Console.Write("Inserisci data rilascio videogioco (gg-mm-aaaa): ");
                            string dateToParse = Console.ReadLine();
                            // Prova a parsare la stringa di input nel formato "gg/MM/aaaa"

                            DateTime relase;
                            if (DateTime.TryParseExact(dateToParse, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                            {
                                relase = parsedDate;
                            }
                            else
                            {
                                // Gestisci il caso in cui la data di input non sia nel formato corretto
                                // Ad esempio, puoi lanciare un'eccezione o impostare _data a un valore predefinito
                                throw new ArgumentException("Formato data non valido");
                            }
                            DateTime create_at = DateTime.Now;
                            DateTime update_at = DateTime.Now;
                            Console.Write("Inserisci id software house videogioco (numero intero): ");
                            int idSh = int.Parse(Console.ReadLine());
                            Videogame game = new(nome, overview, relase, create_at, update_at, idSh);
                            VideogameManager.InserisciVideogame(game);
                            break;
                        }
                    case "2":
                        {
                            Console.Write("Inserisci id videogioco(numero intero): ");
                            int id = int.Parse(Console.ReadLine());
                            VideogameManager.CercaPerId(id);
                            break;
                        }
                    case "3":
                        {
                            Console.Write("Inserisci qualsiasi frase o parola( in latino ;) ): ");
                            string parola =Console.ReadLine();
                            VideogameManager.RicercaPerStringa(parola);
                            break;
                            
                        }
                    case "4":
                        {
                            Console.Write("Inserisci id videogioco(numero intero): ");
                            int id = int.Parse(Console.ReadLine());
                            VideogameManager.Delete(id);
                            break;
                        }
                    case "5":
                        {
                            userChoice = "esci";
                            break;
                        }
                }
            }
        }
    }
}
