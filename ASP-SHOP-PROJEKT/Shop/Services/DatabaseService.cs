using MySql.Data.MySqlClient;
// Made by Ismet Konan

public class DatabaseService
{
    // Connect the Database

    private readonly string _connectionString;

    public DatabaseService(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "CS: __DefaultConnection not found!");
    }

    // User Creation
    public void CreateUser(User user)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
    
            var cmd = new MySqlCommand(@"
            INSERT INTO Users 
            (Username, PasswordHash, FirstName, LastName, Address, Email, Phone, IsAdmin)
            VALUES (@u, @p, @f, @l, @a, @e, @ph, 0)", conn);
    
            cmd.Parameters.AddWithValue("@u", user.Username);
            cmd.Parameters.AddWithValue("@p", user.PasswordHash);
            cmd.Parameters.AddWithValue("@f", user.FirstName);
            cmd.Parameters.AddWithValue("@l", user.LastName);
            cmd.Parameters.AddWithValue("@a", user.Address);
            cmd.Parameters.AddWithValue("@e", user.Email);
            cmd.Parameters.AddWithValue("@ph", user.Phone);
    
            cmd.ExecuteNonQuery();
    }
        
    // Get user from Login
    public User? GetUserByLogin(string username, string passwordHash)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();
    
        var cmd = new MySqlCommand(@"
        SELECT * FROM Users 
        WHERE Username = @u AND PasswordHash = @p", conn);
    
        cmd.Parameters.AddWithValue("@u", username);
        cmd.Parameters.AddWithValue("@p", passwordHash);
    
        using var r = cmd.ExecuteReader();
        if (!r.Read()) return null;
    
        return new User
        {
            Id = r.GetInt32("Id"),
            Username = r.GetString("Username"),
            IsAdmin = r.GetBoolean("IsAdmin")
        };
    }
    
    // Create Article
    public void CreateArticle(Article a)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();
    
        var cmd = new MySqlCommand(
            "INSERT INTO Articles (Description, Quantity, Price) VALUES (@d, @q, @p)",
            conn
        );
    
        cmd.Parameters.AddWithValue("@d", a.Description);
        cmd.Parameters.AddWithValue("@q", a.Quantity);
        cmd.Parameters.AddWithValue("@p", a.Price);
    
        cmd.ExecuteNonQuery();
    }
    
    // Get all Users Admin Manage Users
    public List<User> GetAllUsers()
    {
        var users = new List<User>();
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();
    
        var cmd = new MySqlCommand("SELECT * FROM Users", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            users.Add(new User
            {
                Id = reader.GetInt32("Id"),
                Username = reader.GetString("Username"),
                FirstName = reader.GetString("FirstName"),
                LastName = reader.GetString("LastName"),
                Email = reader.GetString("Email"),
                Phone = reader.GetString("Phone"),
                IsAdmin = reader.GetBoolean("IsAdmin")
            });
        }
        return users;
    }

    // Make Admin Admin Manage Users
    public void ToggleAdmin(int userId)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();
    
        var cmd = new MySqlCommand(
            "UPDATE Users SET IsAdmin = NOT IsAdmin WHERE Id = @id", conn);
        cmd.Parameters.AddWithValue("@id", userId);
        cmd.ExecuteNonQuery();
    }

    // Delete Users Admin Manage Users
    public void DeleteUser(int userId)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();
    
        var cmd = new MySqlCommand("DELETE FROM Users WHERE Id = @id", conn);
        cmd.Parameters.AddWithValue("@id", userId);
        cmd.ExecuteNonQuery();
    }
    
    // Delte Article
    public void DeleteArticle(int articleId)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();
    
        var cmd = new MySqlCommand("DELETE FROM Articles WHERE Id = @id", conn);
        cmd.Parameters.AddWithValue("@id", articleId);
        cmd.ExecuteNonQuery();
    }
    
    // List Article
    public List<Article> GetArticles()
    {
        var articles = new List<Article>();
    
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
    
        var cmd = new MySqlCommand(
            "SELECT Id, Description, Quantity, Price FROM Articles",
            connection
        );
    
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            articles.Add(new Article
            {
                Id = reader.GetInt32("Id"),
                Description = reader.GetString("Description"),
                Quantity = reader.GetInt32("Quantity"),
                Price = reader.GetDecimal("Price")
            });
        }
    
        return articles;
    }
}