using System;

namespace UserApp.Application.Settings;

public class DatabaseSettings
{
    public const string SectioName = "DatabaseSettings";
    public static string ConnectionString { get; set; }
}
