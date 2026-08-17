using Vault.Data;
using Vault.Models;
using Vault.Repositories;

namespace Vault.Repositories;

public class FranquiaRepository
{
    private readonly JsonRepository<Franquia> _repository;

    public FranquiaRepository()
    {
        AppDataPath.Inicializar();

        _repository = new JsonRepository<Franquia>(AppDataPath.Franquias);
    }

    public List<Franquia> ObterTodos()
    {
        return _repository.ObterTodos();
    }

    public void SalvarTodos(List<Franquia> franquias)
    {
        _repository.SalvarTodos(franquias);
    }
}