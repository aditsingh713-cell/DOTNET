namespace InventoryManagement.Api.DTOs;

public record CreateInventoryItemDto(string Name, string? Description, int Quantity, decimal Price);
public record UpdateInventoryItemDto(string Name, string? Description, int Quantity, decimal Price);