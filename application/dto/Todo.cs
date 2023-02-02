namespace e06.application.dto;

public record Todo(
    int userId,
    int id,
    string title,
    bool completed
);