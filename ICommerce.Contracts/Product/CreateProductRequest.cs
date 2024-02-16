namespace ICommerce.Contracts;
public record CreateProductRequest(
    string title,
    string description,
    DateTime manufacturedAt
);
