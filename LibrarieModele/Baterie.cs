

namespace LibrarieModele
{
    public enum TipBaterie
    {
        Alcalina,
        Litiu,
        NichelMetal,
        Plumb
    }
    [Flags]
    public enum OptiuniBaterie
    {
        Niciuna = 0,
        Reincarcabila = 1,
        Impermeabila = 2,
        TemperaturaScazuta = 4,
        TemperaturaRidicata = 8
    }
    public class Baterie
    {
        public string Nume { get; set; }
        public TipBaterie Tip { get; set; }
        public OptiuniBaterie Optiuni { get; set; }
        public DateTime DataExpirare { get; set; }
        public int Cantitate { get; set; }
        public Producator Producator { get; set; }
        public Baterie(string nume, TipBaterie tip, DateTime dataExpirare, int cantitate, Producator producator, OptiuniBaterie optiuni)
        {
            Nume = nume;
            Tip = tip;
            DataExpirare = dataExpirare;
            Cantitate = cantitate;
            Producator = producator;
            Optiuni = optiuni;
        }
        public override string ToString()
        {
            return $"{Nume}-{Tip}-Expirare: {DataExpirare.ToShortDateString()}-Cantitate: {Cantitate}, Optiuni:{Optiuni}, Producator: {Producator.Info()}";
        }
    }
}
