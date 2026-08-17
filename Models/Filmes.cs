namespace Vault.Models;

public class Filme
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public decimal Avaliacao { get; set; }

    public bool Assistido { get; set; }

    public int? FranquiaId { get; set; }
}