namespace Vault.Models;

public class Serie
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public int QuantidadeTemporadas { get; set; }

    public decimal Avaliacao { get; set; }

    public bool Finalizada { get; set; }

    public bool Assistida { get; set; }

    public int? FranquiaId { get; set; }
}