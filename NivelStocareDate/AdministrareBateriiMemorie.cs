using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareBateriiMemorie : IStocareData
    {
        private List<Baterie> baterii = new List<Baterie>();

        public void AddBaterie(Baterie baterie)
        {
            baterii.Add(baterie);
        }
        public List<Baterie> GetBaterii()
        {
            return baterii.ToList();
        }
        public List<Baterie> GetBaterii(TipBaterie tip)
        {
            return baterii.Where(b => b.Tip == tip).ToList();
        }
        public Baterie GetBaterie(string nume)
        {
            return baterii.FirstOrDefault(b => b.Nume == nume);
        }
        public bool StergeBaterie(string nume)
        {
            Baterie gasita = baterii.FirstOrDefault(b => b.Nume == nume);
            if (gasita == null) return false;
            baterii.Remove(gasita);
            return true;
        }
    }
}
