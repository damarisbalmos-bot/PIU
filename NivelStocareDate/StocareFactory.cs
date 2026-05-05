using System.Configuration;

namespace NivelStocareDate
{
    public static class StocareFactory
    {
        private const string FORMAT_SALVARE = "FormatSalvare";
        private const string NUME_FISIER = "NumeFisier";

        public static IStocareData GetAdministratorStocare()
        {
            string formatSalvare = ConfigurationManager.AppSettings[FORMAT_SALVARE] ?? "";
            string numeFisier = ConfigurationManager.AppSettings[NUME_FISIER] ?? "";
            string locatieSolutie = Directory.GetParent(
                Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName ?? "";
            string caleFisier = locatieSolutie + "\\" + numeFisier;

            switch (formatSalvare)
            {
                case "txt":
                    return new AdministrareBateriiFisierText(caleFisier + "." + formatSalvare);
                case "memorie":
                default:
                    return new AdministrareBateriiMemorie();
            }
        }
    }
}
