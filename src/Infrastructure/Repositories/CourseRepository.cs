using CourseBookingAppBackend.src.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.Application.DTOs;
using CourseBookingAppBackend.src.Application.Mappers;
using CourseBookingAppBackend.src.Application.QueryParams;
using CourseBookingAppBackend.src.Domain.Entities;
using CourseBookingAppBackend.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseBookingAppBackend.src.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
  private readonly AppDbContext _db;

  public CourseRepository(AppDbContext db)
  {
    _db = db;
  }

  public async Task<IEnumerable<Course>> GetAllAsync()
      => await _db.Courses.AsNoTracking().ToListAsync();

  public async Task<Course?> GetByIdAsync(int id)
      => await _db.Courses.FirstOrDefaultAsync(c => c.Id == id);

  public async Task AddAsync(Course course)
    {
        await _db.Courses.AddAsync(course);
    }

public Task RemoveAsync(Course course)
    {
        course.SoftDelete();
        return Task.CompletedTask;
    }

  public async Task SaveChangesAsync()
      => await _db.SaveChangesAsync();
    public async Task<PagedResult<CourseDto>> GetAllAsync(CourseQueryParams query)
    {
        var queryable = _db.Courses
            .Where(c => !c.IsDeleted)
            .AsQueryable();

        // Filter by search
        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var searchLower = query.Search.ToLower();
            queryable = queryable.Where(c => c.Title.ToLower().Contains(searchLower)
                                          || c.Description.ToLower().Contains(searchLower));
        }

        // Filter by price
        if (query.MinPrice.HasValue)
            queryable = queryable.Where(c => c.Price >= query.MinPrice.Value);
        if (query.MaxPrice.HasValue)
            queryable = queryable.Where(c => c.Price <= query.MaxPrice.Value);

        // Get total count before pagination
        var totalCount = await queryable.CountAsync();

        // Apply pagination
        var items = await queryable
            .OrderByDescending(c => c.Created)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(c => CourseMapper.ToDto(c))
            .ToListAsync();

        return new PagedResult<CourseDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }

}
