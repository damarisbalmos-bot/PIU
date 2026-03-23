namespace LibrarieModele
{
    public class Baterie
    {
        public string Nume { get; set; }
        public string Tip { get; set; }
        public DateTime DataExpirare { get; set; }
        public int Cantitate { get; set; }
        public Baterie(string nume, string tip, DateTime dataExpirare, int cantitate)
        {
            Nume = nume;
            Tip = tip;
            DataExpirare = dataExpirare;
            Cantitate = cantitate;
        }
        public override string ToString()
        {
            return $"{Nume}-{Tip}-Expirare: {DataExpirare.ToShortDateString()}-Cantitate: {Cantitate}";
        }
    }
}
