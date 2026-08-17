namespace Vault.Data;

public static class AppDataPath
{
    public static string PastaBase =>
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "MediaVault");

    public static string PastaDados =>
        Path.Combine(PastaBase, "Data");

    public static string Filmes =>
        Path.Combine(PastaDados, "filmes.json");

    public static string Series =>
        Path.Combine(PastaDados, "series.json");

    public static string Franquias =>
        Path.Combine(PastaDados, "franquias.json");

    public static void Inicializar()
    {
        Directory.CreateDirectory(PastaDados);
    }
}