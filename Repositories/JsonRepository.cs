using System.Text.Json;

namespace Vault.Repositories;

public class JsonRepository<T>
{
    private readonly string _caminhoArquivo;

    private readonly JsonSerializerOptions _opcoes = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public JsonRepository(string caminhoArquivo)
    {
        _caminhoArquivo = caminhoArquivo;

        CriarArquivoSeNaoExistir();
    }

    private void CriarArquivoSeNaoExistir()
    {
        string? diretorio = Path.GetDirectoryName(_caminhoArquivo);

        if (!string.IsNullOrWhiteSpace(diretorio))
        {
            Directory.CreateDirectory(diretorio);
        }

        if (!File.Exists(_caminhoArquivo))
        {
            File.WriteAllText(_caminhoArquivo, "[]");
        }
    }

    public List<T> ObterTodos()
    {
        string json = File.ReadAllText(_caminhoArquivo);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<T>();
        }

        return JsonSerializer.Deserialize<List<T>>(json, _opcoes)
               ?? new List<T>();
    }

    public void SalvarTodos(List<T> itens)
    {
        string json = JsonSerializer.Serialize(itens, _opcoes);

        File.WriteAllText(_caminhoArquivo, json);
    }
}