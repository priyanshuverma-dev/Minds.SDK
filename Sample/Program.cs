using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Minds.SDK; // Ensure your SDK's namespace is used

namespace Minds.SDK.ConsoleTest {
    class Program {
        static async Task Main(string[] args) {
            // Create an instance of RestAPI (assuming it takes an API key as a parameter)
            var apiKey = "e02d9bde2a76f16bd6285931ab403d6a919cce899ec3273e81ff089eeb3914b3"; // Replace with your actual API key
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
