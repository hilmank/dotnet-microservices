using System;

namespace UserApp.Application.Settings;

public class DirectorySettings
{
    public const string SectioName = "DirectorySettings";
    public static string BasePath { get; set; }
    public static string BaseUrl { get; set; }
    public static string BaseUrlProxy { get; set; }
    public static string PathUrl { get; set; }

    public static string FileUser { get; set; }
    public static string UrlFileUser { get; set; }
    public static string PathFileUser { get; set; }
    public static string FileApp { get; set; }
    public static string UrlFileApp { get; set; }
    public static string PathFileApp { get; set; }
}
