using Vault.Models;
using Vault.Repositories;

namespace Vault.Services;

public class FranquiaService
{
    private readonly FranquiaRepository _repository;

    public FranquiaService()
    {
        _repository = new FranquiaRepository();
    }

    public List<Franquia> ObterTodos()
    {
        return _repository.ObterTodos();
    }

    public Franquia? ObterPorId(int id)
    {
        return ObterTodos()
            .FirstOrDefault(f => f.Id == id);
    }

    public void Adicionar(Franquia franquia)
    {
        ValidarFranquia(franquia);

        var franquias = ObterTodos();

        if (franquias.Any(f =>
            string.Equals(
                f.Nome.Trim(),
                franquia.Nome.Trim(),
                StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException(
                "Já existe uma franquia com esse nome.");
        }

        franquia.Id = GerarNovoId(franquias);

        franquia.Nome = franquia.Nome.Trim();

        franquias.Add(franquia);

        _repository.SalvarTodos(franquias);
    }

    public void Atualizar(Franquia franquia)
    {
        ValidarFranquia(franquia);

        var franquias = ObterTodos();

        var franquiaExistente = franquias
            .FirstOrDefault(f => f.Id == franquia.Id);

        if (franquiaExistente == null)
        {
            throw new InvalidOperationException(
                "A franquia informada não foi encontrada.");
        }

        bool nomeDuplicado = franquias.Any(f =>
            f.Id != franquia.Id &&
            string.Equals(
                f.Nome.Trim(),
                franquia.Nome.Trim(),
                StringComparison.OrdinalIgnoreCase));

        if (nomeDuplicado)
        {
            throw new InvalidOperationException(
                "Já existe outra franquia com esse nome.");
        }

        franquia.Nome = franquia.Nome.Trim();

        var indice = franquias.FindIndex(f => f.Id == franquia.Id);

        franquias[indice] = franquia;

        _repository.SalvarTodos(franquias);
    }

    public void Excluir(int id)
    {
        var franquias = ObterTodos();

        var franquia = franquias.FirstOrDefault(f => f.Id == id);

        if (franquia == null)
        {
            throw new InvalidOperationException(
                "A franquia informada não foi encontrada.");
        }

        franquias.Remove(franquia);

        _repository.SalvarTodos(franquias);
    }

    private static int GerarNovoId(List<Franquia> franquias)
    {
        return franquias.Count == 0
            ? 1
            : franquias.Max(f => f.Id) + 1;
    }

    private static void ValidarFranquia(Franquia franquia)
    {
        if (string.IsNullOrWhiteSpace(franquia.Nome))
        {
            throw new ArgumentException(
                "O nome da franquia é obrigatório.");
        }
    }
}