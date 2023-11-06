﻿using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Models.Employee;
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
        context.Users.AddRange(AddTestUser());
        context.SaveChanges();
        context.Database.EnsureCreated();

        return context;
    }

    public static void Destroy(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }

    private static List<User> AddTestUser()
    {
        var users = new List<User>();

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

        users.Add(result1.Value);
        users.Add(result2.Value);

        return users;
    }
}