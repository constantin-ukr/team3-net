namespace CommentsService.Settings
{
    public class SqlDbSettings
    {
        public string Host { get; init; }

        public int Port { get; init; }

       // public string ConnectionString => $"Server=(localdb)\\MSSQLLocalDB:{Port}; Initial Catalog = {ServiceName}; Integrated Security = True;";
    }
}
