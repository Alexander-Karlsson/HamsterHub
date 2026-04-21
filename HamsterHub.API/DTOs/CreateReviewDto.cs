namespace HamsterHub.API.DTOs;

public record CreateReviewDto (
    int HamsterId,
    string CustomerName,
    string Comment,
    int Score
);