using Vault.Models;
using Vault.Data;

namespace Vault.Repositories;

public class FilmeRepository
{
    private readonly JsonRepository<Filme> _repository;

    public FilmeRepository()
    {
        AppDataPath.Inicializar();

        _repository = new JsonRepository<Filme>(AppDataPath.Filmes);
    }

    public List<Filme> ObterTodos()
    {
        return _repository.ObterTodos();
    }

    public void SalvarTodos(List<Filme> filmes)
    {
        _repository.SalvarTodos(filmes);
    }
}