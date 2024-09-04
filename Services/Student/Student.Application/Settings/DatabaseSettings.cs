using System;

namespace Student.Application.Settings;

public class DatabaseSettings
{
    public const string SectioName = "DatabaseSettings";
    public static string ConnectionString { get; set; }
}
