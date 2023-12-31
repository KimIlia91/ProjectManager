﻿using PM.Domain.Common.Enums;

namespace PM.Test.Common.Constants;

internal static class TestDataConstants
{
    public const int TestProjectId = 1;
    public const int TestUserId = 1;
    public const string TestUserEmail = "UserTest@User.com";
    public const string TestUserLastName = "Test User Last Name";
    public const string TestUserFirstName = "Test User First Name";
    public const string TestUserMiddleName = "Test User Middle Name";

    public const string TestProjectName = "Test Project Name";
    public const string TestCustomerCompany = "Test Customer Company";
    public const string TestExecutorCompany = "Test Executor Company";
    public static DateTime StartDate = DateTime.UtcNow;
    public static DateTime EndDate = DateTime.UtcNow.AddDays(1);

    public const string TestTaskName1 = "1Title";
    public const string TestTaskName2 = "2Title";
    public const string TestTaskName3 = "3Title";
    public const Status TestTaskStatus = Status.Done;
    public const Priority TestTaskPriority = Priority.Medium;
}
