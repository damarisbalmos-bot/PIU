using LibrarieModele;

namespace NivelStocareDate
{
    public interface IStocareData
    {
        void AddBaterie(Baterie baterie);
        List<Baterie> GetBaterii();
        List<Baterie> GetBaterii(TipBaterie tip);
        Baterie GetBaterie(string nume);
        bool StergeBaterie(string nume);
    }
}
