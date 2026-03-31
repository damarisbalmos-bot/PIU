using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareBateriiFisierText : IStocareData
    {
        private string numeFisier;

        public AdministrareBateriiFisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            Stream s = File.Open(numeFisier, FileMode.OpenOrCreate);
            s.Close();
        }
        public void AddBaterie(Baterie baterie)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                sw.WriteLine(baterie.ConversieLaSirPentruFisier());
            }
        }
        public List<Baterie> GetBaterii()
        {
            List<Baterie> baterii = new List<Baterie>();

            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string? linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    baterii.Add(new Baterie(linie));
                }
            }

            return baterii;
        }
        public List<Baterie> GetBaterii(TipBaterie tip)
        {
            return GetBaterii()
                .Where(b => b.Tip == tip)
                .ToList();
        }
        public Baterie? GetBaterie(string nume)
        {
            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string? linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    Baterie b = new Baterie(linie);
                    if (b.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase))
                        return b;
                }
            }
            return null;
        }
        public bool StergeBaterie(string nume)
        {
            List<Baterie> baterii = GetBaterii();
            int nrInitial = baterii.Count;

            baterii = baterii
                .Where(b => !b.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (baterii.Count == nrInitial) return false;

            using (StreamWriter sw = new StreamWriter(numeFisier, false))
            {
                foreach (Baterie b in baterii)
                    sw.WriteLine(b.ConversieLaSirPentruFisier());
            }

            return true;
        }

        public bool UpdateBaterie(Baterie baterieActualizata)
        {
            List<Baterie> baterii = GetBaterii();
            bool gasita = false;

            using (StreamWriter sw = new StreamWriter(numeFisier, false))
            {
                foreach (Baterie b in baterii)
                {
                    Baterie deScris = b;
                    if (b.Nume.Equals(baterieActualizata.Nume, StringComparison.OrdinalIgnoreCase))
                    {
                        deScris = baterieActualizata;
                        gasita = true;
                    }
                    sw.WriteLine(deScris.ConversieLaSirPentruFisier());
                }
            }

            return gasita;
        }
    }
}
