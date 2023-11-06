﻿using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Models.Employee;
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

        context.Projects.AddRange(AddTestUser());
        context.SaveChanges();
        context.Database.EnsureCreated();

        return context;
    }

    public static void Destroy(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }

    private static List<Project> AddTestUser()
    {
        var projects = new List<Project>();

        var result1 = User.Create(
            TestDataConstants.TestUserFirstName,
            TestDataConstants.TestUserLastName, 
            TestDataConstants.TestUserEmail,
            TestDataConstants.TestUserMiddleName);

        var result2 = User.Create(
            $"{TestDataConstants.TestUserFirstName} 2",
            $"{TestDataConstants.TestUserLastName} 2",
            $"2{TestDataConstants.TestUserEmail}",
            $"{TestDataConstants.TestUserMiddleName} 2");

        var project = Project.Create(
            TestDataConstants.TestProjectName,
            TestDataConstants.TestCustomerCompany,
            TestDataConstants.TestExecutorCompany,
            result1.Value,
            TestDataConstants.StartDate,
            TestDataConstants.EndDate,
            Priority.Medium);

        var project2 = Project.Create(
           $"2{TestDataConstants.TestProjectName}",
           $"2{TestDataConstants.TestCustomerCompany}",
           $"2{TestDataConstants.TestExecutorCompany}",
           result2.Value,
           TestDataConstants.StartDate.AddMonths(1).AddDays(10),
           TestDataConstants.EndDate.AddMonths(2).AddDays(2),
           Priority.Low);

        project.Value.AddUser(result2.Value);

        projects.Add(project.Value);
        projects.Add(project2.Value);

        return projects;
    }
}
