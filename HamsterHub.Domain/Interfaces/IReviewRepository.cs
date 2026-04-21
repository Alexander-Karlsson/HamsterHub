using HamsterHub.Domain.Entities;

namespace HamsterHub.Domain.Interfaces;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllAsync();
    Task<IEnumerable<Review>> GetByHamsterIdAsync(int hamsterId);
    Task<Review> AddAsync(Review review);
    Task DeleteAsync(int id);
}