using Vault.Data;
using Vault.Models;
using Vault.Repositories;

namespace Vault.Repositories;

public class SerieRepository
{
    private readonly JsonRepository<Serie> _repository;

    public SerieRepository()
    {
        AppDataPath.Inicializar();

        _repository = new JsonRepository<Serie>(AppDataPath.Series);
    }

    public List<Serie> ObterTodos()
    {
        return _repository.ObterTodos();
    }

    public void SalvarTodos(List<Serie> series)
    {
        _repository.SalvarTodos(series);
    }
}