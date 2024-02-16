namespace ICommerce.Contracts;
public record UpsertProductRequest(
    Guid id,
    string title,
    string description,
    DateTime manufacturedAt
);
