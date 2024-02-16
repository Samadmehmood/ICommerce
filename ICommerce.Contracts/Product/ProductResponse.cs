namespace ICommerce.Contracts;
public record ProductResponse(
    Guid id,
    string title,
    string description,
    DateTime manufacturedAt,
    DateTime createdAt,
    DateTime lastUpdatedAt
);
