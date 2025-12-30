using CourseBookingAppBackend.src.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.Application.DTOs;
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
        var coursesQuery = _db.Courses
            .AsNoTracking()
            .Where(c => !c.IsDeleted);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            coursesQuery = coursesQuery.Where(c =>
                c.Title.Contains(query.Search));
        }

        if (query.MinPrice.HasValue)
            coursesQuery = coursesQuery.Where(c => c.Price >= query.MinPrice.Value);

        if (query.MaxPrice.HasValue)
            coursesQuery = coursesQuery.Where(c => c.Price <= query.MaxPrice.Value);

        var totalCount = await coursesQuery.CountAsync();

        var items = await coursesQuery
            .OrderByDescending(c => c.Created)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Price = c.Price,
                Type = c.Type,
                Created = c.Created
            })
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
