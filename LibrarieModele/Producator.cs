namespace LibrarieModele
{
    public class Producator
    {
        public string Nume { get; set; }
        public string Adresa { get; set; }
        public string Telefon {  get; set; }
        public Producator(string  nume, string adresa, string telefon)
        {
            Nume = nume;
            Adresa = adresa;
            Telefon = telefon;
        }
        public string Info()
        {
            return $"Nume: {Nume}, Adresa: {Adresa}, Numar de telefon: {Telefon}";
        }
    }
}
