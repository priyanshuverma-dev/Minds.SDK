<h1 align="center">🧠 Minds SDK for C# 🧠</h1>
<br />
<p align="center">
    <img alt="hero" width="450" src="https://raw.githubusercontent.com/priyanshuverma-dev/Minds.SDK/master/.github/hero.png" style="max-width: 100%;"/>
</p>
<br /><br />
<p align="center">
<a href="https://www.nuget.org/packages/Minds.SDK">
<img alt="nuget version" src="https://img.shields.io/nuget/v/Minds.SDK.svg">
</a>
    <a href="https://www.nuget.org/packages/Minds.SDK">
<img alt="repo size" src="https://img.shields.io/github/repo-size/priyanshuverma-dev/Minds.SDK?color=green">
        </a>    
    <a href="https://github.com/priyanshuverma-dev/Minds.SDK/releases">
<img alt="repo release" src="https://img.shields.io/github/v/release/priyanshuverma-dev/Minds.SDK">   
    </a> 
    <a href="https://github.com/priyanshuverma-dev/Minds.SDK/blob/master/LICENSE">
<img alt="license" src="https://img.shields.io/badge/License-MIT-blue.svg"> 
    </a> 
</p>

> [!NOTE]
>
> A comprehensive **C# SDK** for interacting with the [**Minds AI API**](https://mdb.ai). Effortlessly integrate AI functionalities into your .NET applications.

## 📚 Table of Contents

- [🚀 Features](#-features)
- [🎪 Installation](#-installation)
- [🏁 Getting Started](#-getting-started)
- [📘 API Reference](#-api-reference)
  - [Client](#client)
  - [Datasources](#datasources)
  - [Minds](#minds)
  - [Exceptions](#exceptions)
- [🌟 Usage Examples](#-usage-examples)
- [📄 License](#-license)

---

## 🚀 Features

- **🔐 Secure API Authentication**  
  Ensure safe and encrypted connections with the Minds API, mastertaining data security and privacy.
  
- **📊 Easy Data Source Management**  
  Simplified management of your data sources. Easily configure and manage data connections.

- **🧠 AI Model Customization**  
  Create and tailor AI models to suit your application's needs with ease.

- **📡 Real-Time Interactions**  
  Support for real-time streaming and interaction with AI models, making your app dynamic and responsive.

- **📦 Modular and Extensible Architecture**  
  Flexible architecture that lets you extend and customize as your project evolves.

## 🎪 Installation

> Install **[`Minds.SDK`](https://www.nuget.org/packages/Minds.SDK)** through NuGet.

###### terminal

```bash
dotnet add package Minds.SDK
```

## 🏁 Getting Started

Import the SDK and initialize a client:

```csharp
using Minds.SDK;

var client = new("YOUR_API_KEY");
// Now you're ready to use the SDK!
```

> Check out the [`example`](https://github.com/priyanshuverma-dev/Minds.SDK/blob/master/Sample/Program.cs) for a demo!

---

## 📘 API Reference

### [Client](https://github.com/priyanshuverma-dev/Minds.SDK/blob/master/Minds.SDK/Client.cs)

| Property                     | Description                   |
| ---------------------------- | ----------------------------- |
| `RestAPI Api`                 | Instance for making API calls |
| `Datasources Datasources`     | Manages data sources          |
| `Minds Minds`                 | Manages Minds (AI models)     |

### [Datasources](https://github.com/priyanshuverma-dev/Minds.SDK/blob/master/Minds.SDK/Datasources.cs)

| Method                        | Description                   |
| ----------------------------- | ----------------------------- |
| `List()`                      | List all datasources          |
| `Get(string name)`            | Get a specific datasource     |
| `Create(DatabaseConfig config)`| Create a new datasource       |
| `Drop(string name)`           | Delete a datasource           |

### [Minds](https://github.com/priyanshuverma-dev/Minds.SDK/blob/master/Minds.SDK/Minds.cs)

| Method                        | Description                   |
| ----------------------------- | ----------------------------- |
| `List()`                      | List all Minds (AI models)    |
| `Get(string name)`            | Get a specific Mind           |
| `Create(MindConfig config)`   | Create a new Mind             |
| `Drop(string name)`           | Delete a Mind                 |


### Exceptions

| Exception                     | Description                   |
| ----------------------------- | ----------------------------- |
| `ObjectNotFoundException`      | Object not found              |
| `UnauthorizedAccessException`  | Unauthorized access           |
| `UnknownErrorException`        | Unspecified error             |

---

## 🌟 Usage Examples

### 💿 Managing Datasources

> #### List Datasources

```csharp
var datasources = await client.Datasources.List();
foreach (var ds in datasources)
{
    Console.WriteLine($"Datasource: {ds.Name}");
}
```

> #### Create a Datasource

```csharp
var newDatasource = await client.Datasources.Create(new DatabaseConfig
{
    Name = "my_postgres_db",
    Engine = "postgres",
    ConnectionData = new Dictionary<string, object>
    {
        { "host", "localhost" },
        { "port", 5432 },
        { "user", "username" },
        { "password", "password" },
        { "database", "mydb" }
    }
});
Console.WriteLine($"Created datasource: {newDatasource.Name}");
```

> #### Delete a Datasource

```csharp
await client.Datasources.Drop("my_postgres_db");
Console.WriteLine("Datasource deleted successfully");
```

### 🧠 Managing Minds

> #### Create a Mind

```csharp
var newMind = await client.Minds.Create(new MindConfig
{
    Name = "my_predictor",
    ModelName = "gpt-4o",
    Provider = "openai",
    Datasources = new List<string> { "my_postgres_db" }
});
Console.WriteLine($"Created Mind: {newMind.Name}");
```


---

## 📄 License

This project is licensed under the <a href="https://github.com/priyanshuverma-dev/Minds.SDK/blob/master/LICENSE"><strong>License</strong></a>.

This project is heavily influenced by *Minds Python SDK*
You can find it [here](https://github.com/mindsdb/minds_python_sdk)
