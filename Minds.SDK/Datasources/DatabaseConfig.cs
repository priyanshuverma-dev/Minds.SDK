using System.Collections.Generic;

/// <summary>
/// Represents the configuration settings for a database, including its name, engine type, and connection details.
/// </summary>
public class DatabaseConfig
{
    /// <summary>
    /// Gets or sets the name of the database.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the type of database engine (e.g., MySQL, PostgreSQL).
    /// </summary>
    public string Engine { get; set; }

    /// <summary>
    /// Gets or sets the description of the database.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the connection data for the database.
    /// This dictionary holds key-value pairs that provide information needed to connect to the database.
    /// </summary>
    public Dictionary<string, object> ConnectionData { get; set; } = new Dictionary<string, object>();

    /// <summary>
    /// Gets or sets a list of table names in the database.
    /// This list contains the names of tables that exist in the configured database.
    /// </summary>
    public List<string> Tables { get; set; } = new List<string>();
}
