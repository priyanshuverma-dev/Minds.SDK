using System.Collections.Generic;

public class DatabaseConfig {
    public string Name { get; set; }
    public string Engine { get; set; }
    public string Description { get; set; }
    public Dictionary<string, object> ConnectionData { get; set; } = new Dictionary<string, object>();
    public List<string> Tables { get; set; } = new List<string>();
}
