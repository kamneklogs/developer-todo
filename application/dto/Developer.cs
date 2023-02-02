namespace e06.application.dto;

public record Developer(
    int id,
    string name,
    string username,
    string email,
    Address address,
    string phone,
    string website,
    Todo[] todos
);