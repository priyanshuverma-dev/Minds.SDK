using NUnit.Framework;
using Minds.SDK; // Adjust based on your namespace
using Moq; // For mocking dependencies

[TestFixture]
public class MindsTests {
    private Mock<RestAPI> _mockApi;
    private Minds _minds;

    [SetUp]
    public void Setup() {
        // Initialize mock and the class under test
        _mockApi = new Mock<RestAPI>();
        _minds = new Minds(_mockApi.Object);
    }

    [Test]
    public async Task Test_CreateMind() {
        // Arrange
        string mindName = "TestMind";
        // Set up your mock behavior
        _mockApi.Setup(api => api.Post(It.IsAny<string>(), It.IsAny<object>()))
                 .Returns(Task.CompletedTask);
        _mockApi.Setup(api => api.Get<Mind>(It.IsAny<string>()))
                 .ReturnsAsync(new Mind(...)); // Return a new Mind object

        // Act
        var result = await _minds.Create(mindName);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(mindName, result.Name);
    }

    // Additional tests go here...
}
