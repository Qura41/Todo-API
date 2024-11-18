// Models/DbSettings.cs

namespace TodoAPI.Models
{
    public class DbSettings
    {
        public TodoDbSettings? TodoAPI { get; set; }
        public UserDbSettings? UserAPI { get; set; }
    }

    public class TodoDbSettings
    {
        public string? ConnectionString { get; set; }
    }

    public class UserDbSettings
    {
        public string? ConnectionString { get; set; }
    }
}