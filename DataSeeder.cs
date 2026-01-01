using Bogus;
using CourseBookingAppBackend.src.Domain.Entities;
using CourseBookingAppBackend.src.Domain.Enums;
using CourseBookingAppBackend.src.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext appDbContext, PasswordHasher<User> passwordHasher)
    {
        Console.WriteLine("🌱 Starting database seeding...");

        await appDbContext.Database.MigrateAsync();

        // 1️⃣ Seed Admin
        if (!await appDbContext.Users.AnyAsync(u => u.Type == UserType.Admin))
        {
            var adminPasswordHash = passwordHasher.HashPassword(null!, "Admin@123");
            var admin = User.Create("admin@example.com", adminPasswordHash);
            admin.ChangeRole(UserType.Admin);
            admin.UpdateProfile(
                firstName: "System",
                lastName: "Admin",
                phoneNumber: "08090000000",
                physicalAddress: "HQ"
            );

            await appDbContext.Users.AddAsync(admin);
            await appDbContext.SaveChangesAsync();

            Console.WriteLine("✔ Admin user created.");
        }

        // 2️⃣ Seed Students
        if (!await appDbContext.Users.AnyAsync(u => u.Type == UserType.Student))
        {
            var faker = new Faker("en");
            var students = new List<User>();

            for (int i = 0; i < 10; i++)
            {
                var email = faker.Internet.Email().ToLower();
                var passwordHash = passwordHasher.HashPassword(null!, "password");
                var student = User.Create(email, passwordHash);

                student.UpdateProfile(
                    firstName: faker.Name.FirstName(),
                    lastName: faker.Name.LastName(),
                    phoneNumber: faker.Random.ReplaceNumbers("080########"),
                    physicalAddress: faker.Address.FullAddress()
                );

                students.Add(student);
            }

            await appDbContext.Users.AddRangeAsync(students);
            await appDbContext.SaveChangesAsync();

            Console.WriteLine($"✔ Seeded {students.Count} students.");
        }

        // 3️⃣ Seed Courses
        if (!await appDbContext.Courses.AnyAsync())
        {
            string[] sampleImages = new[]
            {
                "https://via.placeholder.com/300x200/0000FF?text=Course",
                "https://via.placeholder.com/300x200/FF0000?text=Course",
                "https://via.placeholder.com/300x200/00FF00?text=Course",
                "https://via.placeholder.com/300x200/FFFF00?text=Course",
                "https://via.placeholder.com/300x200/FF00FF?text=Course"
            };

            var faker = new Faker("en");

            var courses = Enumerable.Range(1, 20)
                .Select(_ => new Course(
                    title: faker.Company.CatchPhrase(),
                    description: faker.Lorem.Paragraph(),
                    price: decimal.Round(faker.Random.Decimal(5000m, 35000m), 2),
                    type: faker.PickRandom<CourseType>()
                ))
                .ToList();

            foreach (var course in courses)
            {
                course.UpdateImage(faker.PickRandom(sampleImages), null);
                if (faker.Random.Bool(0.2f)) course.MarkAsSoldOut(); // 20% chance
                if (faker.Random.Bool(0.3f)) course.PutOnSale();      // 30% chance
            }

            await appDbContext.Courses.AddRangeAsync(courses);
            await appDbContext.SaveChangesAsync();

            Console.WriteLine($"✔ Seeded {courses.Count} courses.");
        }

        // 4️⃣ Seed Enrollments
        if (!await appDbContext.Enrollments.AnyAsync())
        {
            var students = await appDbContext.Users.Where(u => u.Type == UserType.Student).ToListAsync();
            var courses = await appDbContext.Courses.ToListAsync();
            var faker = new Faker();
            var enrollments = new List<Enrollment>();

            foreach (var student in students)
            {
                // Random number of courses (at least 1)
                int courseCount = faker.Random.Int(1, courses.Count);
                var selectedCourses = courses.OrderBy(_ => Guid.NewGuid()).Take(courseCount);

                foreach (var course in selectedCourses)
                {
                    var enrollment = Enrollment.Create(student.Id, course.Id);
                    enrollments.Add(enrollment);
                }
            }

            await appDbContext.Enrollments.AddRangeAsync(enrollments);
            await appDbContext.SaveChangesAsync();

            Console.WriteLine($"✔ Seeded {enrollments.Count} enrollments.");
        }

        Console.WriteLine("🎉 Database seeding complete!");
    }
}
