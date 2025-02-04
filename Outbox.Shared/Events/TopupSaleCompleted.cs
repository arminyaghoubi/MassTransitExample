namespace Outbox.Shared.Events;

public record TopupSaleCompleted(string TerminalId, string SystemTrace, string Mobile, decimal Amount);
