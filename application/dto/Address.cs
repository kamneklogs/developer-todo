namespace e06.application.dto;

public record Address(
    string street,
    string suite,
    string city,
    string zipcode,
    Geo geo
);