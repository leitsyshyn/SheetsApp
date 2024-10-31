using NUnit.Framework; // Or `using Xunit;` if using xUnit
using SheetsApp; // Replace with your actual namespace
using System.Threading;
using System.Threading.Tasks;


[TestFixture] // Use `[Fact]` attribute for xUnit instead of `[Test]` for NUnit
public class MainPageTests
{
    [Test] // Use `[Fact]` for xUnit
    public void AddRow_ShouldIncreaseRowCount()
    {
        // Arrange
        var mainPage = new MainPage();
        int initialRowCount = mainPage.grid.RowDefinitions.Count;

        // Act
        mainPage.AddRow(initialRowCount); // Adds a new row

        // Assert
        int newRowCount = mainPage.grid.RowDefinitions.Count;
        Assert.AreEqual(initialRowCount + 1, newRowCount); // For xUnit use Assert.Equal
    }

    [Test] // Use `[Fact]` for xUnit
    public async Task SaveFile_ShouldSaveNonEmptyEntries()
    {
        // Arrange
        var mainPage = new MainPage();
        var cancellationToken = new CancellationToken();

        // Simulate user input
        var testEntry = mainPage.GetEntryAt(1, 1);
        testEntry.Text = "Test Value";

        // Act
        bool saveResult = await mainPage.SaveFile(cancellationToken);

        // Assert
        Assert.IsTrue(saveResult, "The file should be saved successfully."); // For xUnit use Assert.True
    }

    [Test] // Use `[Fact]` for xUnit
    public void GetCellName_ShouldReturnCorrectName()
    {
        // Arrange
        var mainPage = new MainPage();

        // Act & Assert
        Assert.AreEqual("A1", mainPage.GetCellName(1, 1)); // For xUnit use Assert.Equal
        Assert.AreEqual("B2", mainPage.GetCellName(2, 2));
        Assert.AreEqual("Z10", mainPage.GetCellName(10, 26));
        Assert.AreEqual("AA1", mainPage.GetCellName(1, 27));
    }
}
