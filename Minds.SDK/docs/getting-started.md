# Getting Started

This page will guide you through using **Minds.SDK** in a basic console application. You'll learn how to create a new Mind, list all minds, and delete a mind using the SDK.

## Example: Basic Minds.SDK Usage

Below is a simple console application that demonstrates how to use the Minds.SDK:

```csharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Minds.SDK; // Ensure your SDK's namespace is used

namespace Minds.SDK.ConsoleTest {
    class Program {
        static async Task Main(string[] args) {
            // Create an instance of Client (replace with your actual API key)
            var apiKey = "api-key"; // Replace with your actual API key
            Client client = new(apiKey);

            // Call the SDK methods and print results
            await TestMindsSdk(client);

            Console.ReadLine(); // Pause console for reading output
        }

        private static async Task TestMindsSdk(Client client) {
            try {
                // Test creating a new mind
                Console.WriteLine("Creating a new Mind...");
                var newMind = await client.Minds.Create("TestMind");
                Console.WriteLine($"Created Mind: {newMind.Name}");

                // List all minds
                Console.WriteLine("Listing all minds...");
                var mindsList = await client.Minds.List();
                foreach (var mind in mindsList) {
                    Console.WriteLine($"Mind: {mind.Name}");
                }

                // Delete the created mind
                Console.WriteLine("Deleting TestMind...");
                await client.Minds.Drop("TestMind");
                Console.WriteLine("TestMind deleted.");

            } catch (Exception ex) {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }
    }
}
```

### Key Steps:

1. **Instantiate the SDK Client**: 
   Initialize the SDK client using your API key:
   ```csharp
   Client client = new(apiKey);
   ```

2. **Create a New Mind**:
   The example shows how to create a new mind:
   ```csharp
   var newMind = await client.Minds.Create("TestMind");
   ```

3. **List Existing Minds**:
   List all existing minds in your account:
   ```csharp
   var mindsList = await client.Minds.List();
   ```

4. **Delete a Mind**:
   The example also demonstrates deleting a mind:
   ```csharp
   await client.Minds.Drop("TestMind");
   ```

### Next Steps:
- Learn more about the SDK methods in the [API Reference](../api/Minds.SDK.html).
- Explore advanced use cases in the [Advanced Topics](../api/Minds.SDK.Client.html).
