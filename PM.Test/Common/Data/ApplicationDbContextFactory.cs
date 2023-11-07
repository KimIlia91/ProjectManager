using Microsoft.EntityFrameworkCore;
using PM.Domain.Common.Enums;
using PM.Domain.Entities;
using PM.Infrastructure.Persistence;
using PM.Test.Common.Constants;

namespace PM.Test.Common.Data;

public class ApplicationDbContextFactory
{
    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);

        context.Projects.AddRange(AddProjectsWithUsers());
        context.Users.AddRange(AddUsersWithOutProject());
        context.SaveChanges();
        context.Database.EnsureCreated();

        return context;
    }

    private static List<User> AddUsersWithOutProject()
    {
        var users = new List<User>()
        {
            User.Create(
                $"{TestDataConstants.TestUserFirstName} 3",
                $"{TestDataConstants.TestUserLastName} 3",
                $"3{TestDataConstants.TestUserEmail}",
                $"{TestDataConstants.TestUserMiddleName} 3").Value
        };

        return users;
    }

    private static List<Project> AddProjectsWithUsers()
    {
        var projects = new List<Project>();

        var user1 = User.Create(
            TestDataConstants.TestUserFirstName,
            TestDataConstants.TestUserLastName, 
            TestDataConstants.TestUserEmail,
            TestDataConstants.TestUserMiddleName);

        var user2 = User.Create(
            $"{TestDataConstants.TestUserFirstName} 2",
            $"{TestDataConstants.TestUserLastName} 2",
            $"2{TestDataConstants.TestUserEmail}",
            $"{TestDataConstants.TestUserMiddleName} 2");

        var project1 = Project.Create(
            TestDataConstants.TestProjectName,
            TestDataConstants.TestCustomerCompany,
            TestDataConstants.TestExecutorCompany,
            user1.Value,
            TestDataConstants.StartDate,
            TestDataConstants.EndDate,
            Priority.Medium);

        var project2 = Project.Create(
            $"2{TestDataConstants.TestProjectName}",
            $"2{TestDataConstants.TestCustomerCompany}",
            $"2{TestDataConstants.TestExecutorCompany}",
            user2.Value,
            TestDataConstants.StartDate.AddMonths(1).AddDays(10),
            TestDataConstants.EndDate.AddMonths(2).AddDays(2),
            Priority.Low);

        var task1 = PM.Domain.Entities.Task.Create(
            "Title1", 
            user1.Value, 
            user2.Value, 
            project1.Value, 
            "Comment", 
            Status.ToDo, 
            Priority.High);

        var task2 = PM.Domain.Entities.Task.Create(
            "Title2",
            user2.Value,
            user2.Value,
            project1.Value,
            "Comment",
            Status.ToDo,
            Priority.High);

        var task3 = PM.Domain.Entities.Task.Create(
            "Title3",
            user2.Value,
            user2.Value,
            project2.Value,
            "Comment",
            Status.ToDo,
            Priority.High);

        project1.Value.AddTask(task1.Value);
        project1.Value.AddTask(task2.Value);
        project2.Value.AddTask(task3.Value);
        project2.Value.AddUser(user2.Value);
        project1.Value.AddUser(user2.Value);

        projects.Add(project1.Value);
        projects.Add(project2.Value);

        return projects;
    }

    public static void Destroy(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}
