using Vault.Models;
using Vault.Repositories;

namespace Vault.Services;

public class SerieService
{
    private readonly SerieRepository _repository;

    public SerieService()
    {
        _repository = new SerieRepository();
    }

    public List<Serie> ObterTodos()
    {
        return _repository.ObterTodos();
    }

    public Serie? ObterPorId(int id)
    {
        return ObterTodos()
            .FirstOrDefault(s => s.Id == id);
    }

    public void Adicionar(Serie serie)
    {
        ValidarSerie(serie);

        var series = ObterTodos();

        serie.Id = GerarNovoId(series);

        series.Add(serie);

        _repository.SalvarTodos(series);
    }

    public void Atualizar(Serie serie)
    {
        ValidarSerie(serie);

        var series = ObterTodos();

        var indice = series.FindIndex(s => s.Id == serie.Id);

        if (indice == -1)
        {
            throw new InvalidOperationException(
                "A série informada não foi encontrada.");
        }

        series[indice] = serie;

        _repository.SalvarTodos(series);
    }

    public void Excluir(int id)
    {
        var series = ObterTodos();

        var serie = series.FirstOrDefault(s => s.Id == id);

        if (serie == null)
        {
            throw new InvalidOperationException(
                "A série informada não foi encontrada.");
        }

        series.Remove(serie);

        _repository.SalvarTodos(series);
    }

    public List<Serie> ObterFavoritas(decimal notaMinima = 9)
    {
        return ObterTodos()
            .Where(s => s.Avaliacao >= notaMinima)
            .OrderByDescending(s => s.Avaliacao)
            .ThenBy(s => s.Nome)
            .ToList();
    }

    public List<Serie> ObterEmAndamento()
    {
        return ObterTodos()
            .Where(s => s.Assistida && !s.Finalizada)
            .OrderBy(s => s.Nome)
            .ToList();
    }

    public List<Serie> ObterFinalizadas()
    {
        return ObterTodos()
            .Where(s => s.Finalizada)
            .OrderBy(s => s.Nome)
            .ToList();
    }

    private static int GerarNovoId(List<Serie> series)
    {
        return series.Count == 0
            ? 1
            : series.Max(s => s.Id) + 1;
    }

    private static void ValidarSerie(Serie serie)
    {
        if (string.IsNullOrWhiteSpace(serie.Nome))
        {
            throw new ArgumentException(
                "O nome da série é obrigatório.");
        }

        if (serie.QuantidadeTemporadas <= 0)
        {
            throw new ArgumentException(
                "A quantidade de temporadas deve ser maior que zero.");
        }

        if (serie.Avaliacao < 0 || serie.Avaliacao > 10)
        {
            throw new ArgumentException(
                "A avaliação deve estar entre 0 e 10.");
        }

        if (serie.Finalizada && !serie.Assistida)
        {
            throw new ArgumentException(
                "Uma série não pode estar finalizada sem ter sido assistida.");
        }
    }
}