namespace fastaffo_api.src.Domain.Entities;

public class StaffJob
{
    public Guid Id { get; set; }

    public required Guid StaffId { get; set; }
    public Staff Staff { get; set; } = null!;

    public Guid? JobId { get; set; }
    public Job? Job { get; set; }

    public Guid? TeamId { get; set; }
    public Team? Team { get; set; }

    public string? Role { get; set; }

    public required DateTime StartTime { get; set; }
    public required DateTime FinishTime { get; set; }

    public int BaseRate { get; set; }

    // Extras adicionados pelo staff
    public int TravelTimeMinutes { get; set; }
    public decimal Kilometers { get; set; }

    // Cópia de políticas (RatePolicy) - você pode expandir isso depois
    public string? Notes { get; set; }

    // Valor total calculado
    public int TotalAmount { get; set; }

    // Campos exclusivos para jobs pessoais
    public string? Title { get; set; }
    public string? Location { get; set; }
    public bool IsPersonalJob { get; set; }
}
