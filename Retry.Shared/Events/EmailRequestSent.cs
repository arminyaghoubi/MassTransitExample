namespace Retry.Shared.Events;

public record EmailRequestSent(Guid RequestId,string Email, string Message,DateTime CreationDate);
