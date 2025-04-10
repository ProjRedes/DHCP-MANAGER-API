public class VLAN
{
    public int Id { get; set; }
    public required string Nome { get; set; } = string.Empty;  // Evita erro
    public string? OBS { get; set; }  // Permite valor nulo
}
