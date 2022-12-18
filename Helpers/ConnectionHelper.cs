namespace dotnetdevs.Helpers
{
    public static class ConnectionHelper
    {
        public static string GetConnectionString()
        {
            var connectionString = Environment.GetEnvironmentVariable("LOCAL_DB");
            return !string.IsNullOrEmpty(connectionString) ? connectionString : BuildConnectionString();
        }

        private static string BuildConnectionString()
        {
            var server = Environment.GetEnvironmentVariable("MYSQLHOST");
            var port = Environment.GetEnvironmentVariable("MYSQLPORT");
            var user = Environment.GetEnvironmentVariable("MYSQLUSER");
            var db = Environment.GetEnvironmentVariable("MYSQLDATABASE");
            var password = Environment.GetEnvironmentVariable("MYSQLPASSWORD");
            return $"server={server}; port={port}; database={db}; user={user}; password={password}; Persist Security Info=False; Connect Timeout=300";
        }
    }
}
