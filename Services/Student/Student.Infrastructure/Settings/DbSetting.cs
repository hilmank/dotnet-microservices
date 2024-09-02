using System;

namespace Student.Infrastructure.Settings;

public class DbSetting
{
    public const string SectioName = "DbSetting";
    public static string Server { get; set; }
    public static int Port { get; set; }
    public static string UserId { get; set; }
    public static string Password { get; set; }
    public static string Database { get; set; }
    public static int CommandTimeout { get; set; }
}
