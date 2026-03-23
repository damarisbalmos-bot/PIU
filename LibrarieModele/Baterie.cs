

namespace LibrarieModele
{
    public class Baterie
    {
        public string Nume { get; set; }
        public string Tip { get; set; }
        public DateTime DataExpirare { get; set; }
        public int Cantitate { get; set; }
        public Producator Producator { get; set; }
        public Baterie(string nume, string tip, DateTime dataExpirare, int cantitate, Producator producator)
        {
            Nume = nume;
            Tip = tip;
            DataExpirare = dataExpirare;
            Cantitate = cantitate;
            Producator = producator;
        }
        public override string ToString()
        {
            return $"{Nume}-{Tip}-Expirare: {DataExpirare.ToShortDateString()}-Cantitate: {Cantitate}, Producator: {Producator.Info()}";
        }
    }
}
