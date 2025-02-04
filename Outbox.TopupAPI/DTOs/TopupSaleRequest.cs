namespace Outbox.TopupAPI.DTOs;

internal record TopupSaleRequest(string TerminalId, string SystemTrace, string Mobile, decimal Amount);
