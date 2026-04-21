using HamsterHub.Domain.Entities;
using HamsterHub.Domain.Interfaces;

namespace HamsterHub.Application.Services;

public class ReviewService(IReviewRepository repo) : IReviewService
{
    public async Task<IEnumerable<Review>> GetByHamsterIdAsync(int hamsterId) =>
        await repo.GetByHamsterIdAsync(hamsterId);
    
    public async Task<Review> AddAsync(Review review) =>
        await repo.AddAsync(review);
    
    public async Task DeleteAsync(int id) =>
        await repo.DeleteAsync(id);
}