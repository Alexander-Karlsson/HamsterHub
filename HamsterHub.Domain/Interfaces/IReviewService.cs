using HamsterHub.Domain.Entities;

namespace HamsterHub.Domain.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<Review>> GetByHamsterIdAsync(int hamsterId);
    Task<Review> AddAsync(Review review);
    Task DeleteAsync(int id);
}