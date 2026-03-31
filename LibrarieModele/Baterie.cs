
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
        private const char SEPARATOR_PRINCIPAL = ';';
        private const char SEPARATOR_PRODUCATOR = '|';
        private const int IDX_NUME = 0;
        private const int IDX_TIP = 1;
        private const int IDX_DATA = 2;
        private const int IDX_CANTITATE = 3;
        private const int IDX_OPTIUNI = 4;
        private const int IDX_PROD_NUME = 5;
        private const int IDX_PROD_ADRESA = 6;
        private const int IDX_PROD_TELEFON = 7;
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
        public Baterie(string linieFisier)
        {
            string[] date = linieFisier.Split(SEPARATOR_PRINCIPAL);

            Nume = date[IDX_NUME];
            Tip = (TipBaterie)Enum.Parse(typeof(TipBaterie), date[IDX_TIP]);
            DataExpirare = DateTime.Parse(date[IDX_DATA]);
            Cantitate = int.Parse(date[IDX_CANTITATE]);
            Optiuni = (OptiuniBaterie)int.Parse(date[IDX_OPTIUNI]);
            Producator = new Producator(
                date[IDX_PROD_NUME],
                date[IDX_PROD_ADRESA],
                date[IDX_PROD_TELEFON]
            );
        }
        public string ConversieLaSirPentruFisier()
        {
            return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}",
                SEPARATOR_PRINCIPAL,
                Nume,
                Tip.ToString(),
                DataExpirare.ToString("yyyy-MM-dd"),
                Cantitate,
                (int)Optiuni,
                Producator.Nume,
                Producator.Adresa,
                Producator.Telefon);
        }
        public override string ToString()
        {
            return $"{Nume} -{Tip} -Expirare: {DataExpirare.ToShortDateString()} -Cantitate: {Cantitate} -Optiuni:{Optiuni} -Producator: {Producator.Info()}";
        }
    }
}
