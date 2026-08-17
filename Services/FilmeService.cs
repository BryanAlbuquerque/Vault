using Vault.Models;
using Vault.Repositories;

namespace Vault.Services;

public class FilmeService
{
    private readonly FilmeRepository _repository;

    public FilmeService()
    {
        _repository = new FilmeRepository();
    }

    public List<Filme> ObterTodos()
    {
        return _repository.ObterTodos();
    }

    public Filme? ObterPorId(int id)
    {
        return ObterTodos()
            .FirstOrDefault(f => f.Id == id);
    }

    public void Adicionar(Filme filme)
    {
        ValidarFilme(filme);

        var filmes = ObterTodos();

        filme.Id = GerarNovoId(filmes);

        filmes.Add(filme);

        _repository.SalvarTodos(filmes);
    }

    public void Atualizar(Filme filme)
    {
        ValidarFilme(filme);

        var filmes = ObterTodos();

        var indice = filmes.FindIndex(f => f.Id == filme.Id);

        if (indice == -1)
        {
            throw new InvalidOperationException(
                "O filme informado não foi encontrado.");
        }

        filmes[indice] = filme;

        _repository.SalvarTodos(filmes);
    }

    public void Excluir(int id)
    {
        var filmes = ObterTodos();

        var filme = filmes.FirstOrDefault(f => f.Id == id);

        if (filme == null)
        {
            throw new InvalidOperationException(
                "O filme informado não foi encontrado.");
        }

        filmes.Remove(filme);

        _repository.SalvarTodos(filmes);
    }

    public List<Filme> ObterFavoritos(decimal notaMinima = 9)
    {
        return ObterTodos()
            .Where(f => f.Avaliacao >= notaMinima)
            .OrderByDescending(f => f.Avaliacao)
            .ThenBy(f => f.Nome)
            .ToList();
    }

    private static int GerarNovoId(List<Filme> filmes)
    {
        return filmes.Count == 0
            ? 1
            : filmes.Max(f => f.Id) + 1;
    }

    private static void ValidarFilme(Filme filme)
    {
        if (string.IsNullOrWhiteSpace(filme.Nome))
        {
            throw new ArgumentException(
                "O nome do filme é obrigatório.");
        }

        if (filme.Avaliacao < 0 || filme.Avaliacao > 10)
        {
            throw new ArgumentException(
                "A avaliação deve estar entre 0 e 10.");
        }
    }
}