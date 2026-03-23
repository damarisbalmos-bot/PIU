using LibrarieModele;
namespace InventarBaterii
{
    class Program
    {
        static List<Baterie> baterii = new List<Baterie>();
        static void Main(string[] args)
        {
            int optiune;
            do
            {
                Console.Clear();
                Console.WriteLine("Aplicatie Inventar Baterii");
                Console.WriteLine("1. Adauga Baterie");
                Console.WriteLine("2. Vizualizeaza Inventar");
                Console.WriteLine("3. Sterge Baterie");
                Console.WriteLine("4. Cauta baterie dupa tip");
                Console.WriteLine("5. Iesi");
                Console.Write("Alege o optiune: ");
                optiune = int.Parse(Console.ReadLine());
                switch (optiune)
                {
                    case 1:
                        Adauga();
                        break;
                    case 2:
                        VizualizeazaInventar();
                        break;
                    case 3:
                        Sterge();
                        break;
                    case 4:
                        Cauta();
                        break;
                    case 5:
                        Console.WriteLine("Iesire din aplicatie...");
                        break;
                    default:
                        Console.WriteLine("Optiune invalida!");
                        break;
                }

            } while (optiune != 5);
        }

        static void Adauga()
        {
            Console.Write("Numele bateriei: ");
            string nume = Console.ReadLine();
            Console.Write("Tipul bateriei: ");
            string tip = Console.ReadLine();
            Console.Write("Data de expirare: ");
            DateTime dataExpirare;
            while (!DateTime.TryParse(Console.ReadLine(), out dataExpirare)){
                Console.Write("Data invalida! Faceti din nou:  ");
            }
            Console.Write("Cantitate: ");
            int cantitate;
            while (!int.TryParse(Console.ReadLine(), out cantitate)) {
                Console.Write("Cantitate invalida! Introduceti din nou: ");
            }
            Console.Write("Nume producator: ");
            string numeProd = Console.ReadLine();
            Console.Write("Adresa producator: ");
            string adresaProd = Console.ReadLine();
            Console.Write("Telefon producator: ");
            string telefonProd = Console.ReadLine();

            Producator producator = new Producator(numeProd, adresaProd, telefonProd);
            baterii.Add(new Baterie(nume, tip, dataExpirare, cantitate, producator));
            Console.WriteLine("Baterie adaugata!");
            Console.ReadKey();
        }
        static void VizualizeazaInventar()
        {
            if (baterii.Count == 0)
            {
                Console.WriteLine("Nu exista baterii in inventar.");
            }
            else
            {
                Console.WriteLine("Inventar baterii:");
                foreach (Baterie b in baterii)
                {
                    Console.WriteLine(b.ToString());
                }
            }
            Console.ReadKey();
        }
        static void Cauta()
        {
            Console.Write("Introduceti tipul bateriei cautat: ");
            string tip = Console.ReadLine();

            List<Baterie> rezultate = new List<Baterie>();
            foreach (Baterie b in baterii)
            {
                if (b.Tip == tip)
                    rezultate.Add(b);
            }

            if (rezultate.Count > 0)
            {
                Console.WriteLine($"Au fost gasite {rezultate.Count} baterii:");
                foreach (Baterie b in rezultate)
                    Console.WriteLine(b.ToString());
            }
            else
            {
                Console.WriteLine("Nu a fost gasita nicio baterie cu tipul specificat.");
            }
            Console.ReadKey();
        }
        static void Sterge()
        {
            Console.Write("Introduceti numele bateriei de sters: ");
            string nume = Console.ReadLine();

            Baterie gasita = null;
            foreach (Baterie b in baterii)
            {
                if (b.Nume == nume)
                {
                    gasita = b;
                    break;
                }
            }
            if (gasita != null)
            {
                baterii.Remove(gasita);
                Console.WriteLine("Baterie stearsa cu succes!");
            }
            else
            {
                Console.WriteLine("Bateria nu a fost gasita.");
            }
            Console.ReadKey();
        }

    }
}


