using HamsterHub.Domain.Entities;
using HamsterHub.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HamsterHub.Infrastructure.Repositories;

public class ReviewRepository(HamsterDbContext db) : IReviewRepository
{
    public async Task<IEnumerable<Review>> GetByHamsterIdAsync(int hamsterId) =>
        await db.Reviews
            .AsNoTracking()
            .Where(r => r.HamsterId == hamsterId)
            .OrderByDescending(r => r.ReviewCreatedDate)
            .ToListAsync();

    public async Task<Review> AddAsync(Review review)
    {
        await db.Reviews.AddAsync(review);
        await db.SaveChangesAsync();
        return review;
    }

    public async Task DeleteAsync(int id)
    {
        var review = await db.Reviews.FindAsync(id);
        if (review is null)
            throw new KeyNotFoundException($"Recension med id: {id} hittades inte...");
        
        db.Reviews.Remove(review);
        await db.SaveChangesAsync();
    }



}