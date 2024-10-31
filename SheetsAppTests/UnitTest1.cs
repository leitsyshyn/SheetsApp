//using Xunit;
//using Microsoft.Maui.Controls;
//using SheetsApp;
//using Grid = Microsoft.Maui.Controls.Grid;
//namespace SheetsApp.Tests
//{
//    public class MainPageTests
//    {
//        [Fact]
//        public void AddRow_ShouldIncreaseRowCountAndAddNewRowToGrid()
//        {
//            // Arrange
//            var mainPage = new MainPage();
//            int initialRowCount = mainPage.MainGrid.RowDefinitions.Count;

//            // Act
//            mainPage.AddRowButton_Clicked(null, null);

//            // Assert
//            int newRowCount = mainPage.MainGrid.RowDefinitions.Count;
//            Assert.Equal(initialRowCount + 1, newRowCount);

//            // Check if the last row in the grid has the correct labels and entries
//            var lastRowLabel = mainPage.MainGrid.Children
//                .OfType<Label>()
//                .FirstOrDefault(label => Grid.GetRow(label) == newRowCount - 1 && Grid.GetColumn(label) == 0);
//            Assert.NotNull(lastRowLabel);
//            Assert.Equal((newRowCount - 1).ToString(), lastRowLabel.Text);

//            for (int col = 1; col < mainPage.MainGrid.ColumnDefinitions.Count; col++)
//            {
//                var entry = mainPage.GetEntryAt(newRowCount - 1, col);
//                Assert.NotNull(entry);
//                Assert.Equal(string.Empty, entry.Text);
//            }
//        }
//    }
//}
