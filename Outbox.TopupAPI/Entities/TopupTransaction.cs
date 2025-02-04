namespace Outbox.TopupAPI.Entities;

public class TopupTransaction
{
    public int Id { get; set; }
    public string TerminalId { get; set; }
    public string SystemTrace { get; set; }
    public string Mobile { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreationDateTime { get; set; }
}
