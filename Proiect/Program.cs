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
                Console.WriteLine("4. Iesi");
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
                        Console.WriteLine("Iesire din aplicatie...");
                        break;
                    default:
                        Console.WriteLine("Optiune invalida!");
                        break;
                }

            } while (optiune != 4);
        }

        static void Adauga() { }
        static void VizualizeazaInventar() { }
        static void Sterge() { }

    }
}


