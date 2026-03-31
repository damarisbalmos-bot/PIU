using LibrarieModele;
using NivelStocareDate;

namespace InventarBaterii
{
    class Program
    {
        static IStocareData admin = StocareFactory.GetAdministratorStocare();
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
                Console.WriteLine("5. Modificare");
                Console.WriteLine("6. Iesi");
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
                        Modifica();
                        break;

                    case 6:
                        Console.WriteLine("Iesire din aplicatie...");
                        break;
                    default:
                        Console.WriteLine("Optiune invalida!");
                        break;
                }

            } while (optiune != 6);
        }

        static void Adauga()
        {
            Console.Write("Numele bateriei: ");
            string nume = Console.ReadLine();
            Console.WriteLine("Tipul bateriei : ");
            foreach (TipBaterie t in Enum.GetValues(typeof(TipBaterie)))
                Console.WriteLine($"  {(int)t} - {t}");
            TipBaterie tip = TipBaterie.Alcalina;
            bool tipValid = false;
            while (!tipValid)
            {
                Console.Write("Alegeti tipul (valoare numerica): ");
                try
                {
                    int valTip = int.Parse(Console.ReadLine());
                    if (!Enum.IsDefined(typeof(TipBaterie), valTip))
                        throw new ArgumentOutOfRangeException($"Valoarea {valTip} nu este valida.");
                    tip = (TipBaterie)valTip;
                    tipValid = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Eroare: introduceti o valoare numerica.");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Eroare: {ex.Message}");
                }
            }
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
            Console.WriteLine("Optiuni: ");
            foreach (OptiuniBaterie o in Enum.GetValues(typeof(OptiuniBaterie)))
                Console.WriteLine($"  {(int)o} - {o}");
            Console.Write("Alegeti optiunea : ");
            int.TryParse(Console.ReadLine(), out int valOptiuni);
            OptiuniBaterie optiuni = (OptiuniBaterie)valOptiuni;

            Console.Write("Nume producator: ");
            string numeProd = Console.ReadLine();
            Console.Write("Adresa producator: ");
            string adresaProd = Console.ReadLine();
            Console.Write("Telefon producator: ");
            string telefonProd = Console.ReadLine();

            Producator producator = new Producator(numeProd, adresaProd, telefonProd);
            admin.AddBaterie(new Baterie(nume, tip, dataExpirare, cantitate, producator, optiuni));
            Console.WriteLine("Baterie adaugata!");
            Console.ReadKey();
        }
        static void VizualizeazaInventar()
        {
            List<Baterie> baterii = admin.GetBaterii();
            if (baterii.Count == 0)
                Console.WriteLine("Nu exista baterii in inventar.");
            else
            {
                Console.WriteLine("Inventar baterii:");
                foreach (Baterie b in baterii)
                    Console.WriteLine(b.ToString());
            }
            Console.ReadKey();
        }
        static void Cauta()
        {
            Console.Write("Introduceti tipul bateriei cautat: ");
            foreach (TipBaterie t in Enum.GetValues(typeof(TipBaterie)))
                Console.WriteLine($"  {(int)t} - {t}");
            Console.Write("Introduceti tipul cautat: ");

            try
            {
                int val = int.Parse(Console.ReadLine());
                if (!Enum.IsDefined(typeof(TipBaterie), val))
                    throw new ArgumentOutOfRangeException("Tip invalid.");
                TipBaterie tip = (TipBaterie)val;


                List<Baterie> rezultate = admin.GetBaterii(tip);
                if (rezultate.Count > 0)
                {
                    Console.WriteLine($"Au fost gasite {rezultate.Count} baterii:");
                    foreach (Baterie b in rezultate)
                        Console.WriteLine(b.ToString());
                }
                else
                    Console.WriteLine("Nu a fost gasita nicio baterie.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare: {ex.Message}");
            }
            Console.ReadKey();
        }
        static void Sterge()
        {
            Console.Write("Introduceti numele bateriei de sters: ");
            string nume = Console.ReadLine();
            bool ok = admin.StergeBaterie(nume);
            Console.WriteLine(ok ? "Baterie stearsa cu succes!" : "Bateria nu a fost gasita.");
            Console.ReadKey();
        }
        static void Modifica()
        {
            Console.Write("Introduceti numele bateriei de modificat: ");
            string nume = Console.ReadLine();

            Baterie existenta = admin.GetBaterie(nume);
            if (existenta == null)
            {
                Console.WriteLine("Bateria nu a fost gasita.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine($"Baterie gasita: {existenta}");
            Console.WriteLine("Introduceti datele noi (Enter pentru a pastra valoarea existenta):");

            Console.Write($"Cantitate noua (actual: {existenta.Cantitate}): ");
            string inputCant = Console.ReadLine();
            if (int.TryParse(inputCant, out int cantitateNoua))
                existenta.Cantitate = cantitateNoua;

            Console.Write($"Data expirare noua (actual: {existenta.DataExpirare:yyyy-MM-dd}): ");
            string inputData = Console.ReadLine();
            if (DateTime.TryParse(inputData, out DateTime dataNoua))
                existenta.DataExpirare = dataNoua;
            bool ok = admin.UpdateBaterie(existenta);
            Console.WriteLine(ok ? "Baterie modificata cu succes!" : "Eroare la modificare.");
            Console.ReadKey();
        }

    }
}


