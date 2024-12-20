﻿using ClosedXML.Excel;

using Grid = Microsoft.Maui.Controls.Grid;

using CommunityToolkit.Maui.Storage;

namespace SheetsApp
{
    public partial class MainPage : ContentPage
    {
        public Grid MainGrid => grid;
        const int CountColumn = 10; 
        const int CountRow = 10;

        private bool isDirty = false;


        private Dictionary<(int, int), Cell> cells = new Dictionary<(int, int), Cell>();

        private Entry lastEntryFocused;

        private HashSet<string> visiting = new HashSet<string>();

        public MainPage()
        {
            InitializeComponent();
            CreateGrid();
        }

        private void CreateGrid()
        {
            AddColumnsAndColumnLabels();
            AddRowsAndCellEntries();
            isDirty = false;
        }
        private Label CreateLabel(string text, int row, int column)
        {
            var label = new Label
            {
                Text = text,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Padding = 5
            };
            Grid.SetRow(label, row);
            Grid.SetColumn(label, column);
            return label;
        }
        private Entry CreateCellEntry(int row, int column)
        {
            var entry = new Entry
            {
                Text = "",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 100,

            };
            entry.Focused += OnEntryFocused;
            entry.Unfocused += OnEntryUnfocused;
            entry.TextChanged += OnEntryTextChanged;
            cells[(row, column)] = new Cell(GetCellName(row, column), "", "");
            Grid.SetRow(entry, row);
            Grid.SetColumn(entry, column);
            return entry;
        }
        internal Entry GetEntryAt(int row, int column)
        {
            foreach (var child in grid.Children)
            {
                if (grid.GetRow(child) == row && grid.GetColumn(child) == column && child is Entry entry)
                {
                    return entry;
                }
            }
            return null;
        }
        private string GetColumnName(int colIndex)
        {
            int dividend = colIndex;
            string columnName = string.Empty;
            while (dividend > 0)
            {
                int modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;

            }
            return columnName;
        }
        private string GetCellName(int row, int column)
        {
            return GetColumnName(column) + row.ToString();
        }
        private int GetColumnIndex(string columnName)
        {
            int columnIndex = 0;
            for (int i = 0; i < columnName.Length; i++)
            {
                columnIndex *= 26;
                columnIndex += (columnName[i] - 'A' + 1);
            }
            return columnIndex;
        }
        private void RemoveGridElements(Func<Microsoft.Maui.IView, bool> matchCondition)
        {
            var childrenToRemove = new List<Microsoft.Maui.IView>();
            foreach (var child in grid.Children)
            {
                if (matchCondition(child))
                {
                    childrenToRemove.Add(child);
                }
            }

            foreach (var child in childrenToRemove)
            {
                grid.Children.Remove(child);
            }
        }
        private void AddRow(int newRow)
        {
            foreach (var child in grid.Children)
            {
                int currentRow = grid.GetRow(child);
                if (currentRow >= newRow)
                {
                    if (child is Label label)
                    {
                        label.Text = (currentRow + 1).ToString();
                    }
                    grid.SetRow(child, currentRow + 1);
                }
            }

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var rowLabel = CreateLabel(newRow.ToString(), newRow, 0);
            grid.Children.Add(rowLabel);

            for (int col = 0; col < grid.ColumnDefinitions.Count; col++)
            {
                var entry = CreateCellEntry(newRow, col + 1);
                grid.Children.Add(entry);
            }
            isDirty = true;

        }
        private void UpdateDependants(Cell cell)
        {
            foreach (var dependant in cell.Dependents)
            {
                if (visiting.Contains(dependant.Name))
                    continue;

                visiting.Add(dependant.Name);
                try
                {
                    if (cell.Value == "ERR" && dependant.Expression.Contains(cell.Name))
                    {
                        dependant.Value = "ERR";
                    }
                    else
                    {
                        dependant.Value = new SheetsAppVisitor(cells).Eval(dependant).ToString();
                    }

                    var dependantCoords = cells.FirstOrDefault(kv => kv.Value == dependant).Key;
                    var entry = GetEntryAt(dependantCoords.Item1, dependantCoords.Item2);
                    if (entry != null)
                    {
                        entry.TextChanged -= OnEntryTextChanged;
                        entry.Text = dependant.Value;
                        entry.TextChanged += OnEntryTextChanged;
                    }
                }
                catch (Exception ex)
                {
                    if (dependant.Expression.Contains(cell.Name))
                    {
                        dependant.Value = "ERR";

                        var dependantCoords = cells.FirstOrDefault(kv => kv.Value == dependant).Key;
                        var entry = GetEntryAt(dependantCoords.Item1, dependantCoords.Item2);
                        if (entry != null)
                        {
                            entry.TextChanged -= OnEntryTextChanged;
                            entry.Text = dependant.Value;
                            entry.TextChanged += OnEntryTextChanged;
                        }
                    }
                }

                UpdateDependants(dependant);

                visiting.Remove(dependant.Name);
            }
        }


        private void OnEntryFocused(object sender, FocusEventArgs e)
        {
            lastEntryFocused = (Entry)sender;
            var cell = cells[(grid.GetRow(lastEntryFocused), grid.GetColumn(lastEntryFocused))];
            textInput.TextChanged -= OnTextInputTextChanged;
            textInput.Text = cell.Expression;
            textInput.TextChanged += OnTextInputTextChanged;
        }
        private void OnEntryUnfocused(object sender, FocusEventArgs e)
        {
            var cell = cells[(grid.GetRow(lastEntryFocused), grid.GetColumn(lastEntryFocused))];

            var visitor = new SheetsAppVisitor(cells);
            if (cell.Expression != "")
            {
                try
                {
                    cell.Value = visitor.Eval(cell).ToString();
                }
                catch (Exception ex)
                {
                    cell.Value = "ERR";
                }
            }
    
            

            UpdateDependants(cell);

            lastEntryFocused.TextChanged -= OnEntryTextChanged;
            lastEntryFocused.Text = cell.Value; 
            lastEntryFocused.TextChanged += OnEntryTextChanged;

        }
        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            textInput.Text = e.NewTextValue;
            var cell = cells[(grid.GetRow(lastEntryFocused), grid.GetColumn(lastEntryFocused))];
            cell.Expression = e.NewTextValue;
            isDirty = true;
        }
        private void OnTextInputTextChanged(object sender, TextChangedEventArgs e)
        {
            lastEntryFocused.Text = e.NewTextValue;
            var cell = cells[(grid.GetRow(lastEntryFocused), grid.GetColumn(lastEntryFocused))];
            cell.Expression = e.NewTextValue;
            cell.Value = e.NewTextValue;

        }
        void AddColumn(int newColumn)
        {
            foreach (var child in grid.Children)
            {
                int currentColumn = grid.GetColumn(child);
                if (currentColumn >= newColumn)
                {
                    if (child is Label label)
                    {
                        label.Text = GetColumnName(currentColumn + 1);
                    }
                    grid.SetColumn(child, currentColumn + 1);
                }
            }
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            var colLabel = CreateLabel(GetColumnName(newColumn), 0, newColumn);
            grid.Children.Add(colLabel);

            for (int row = 0; row < grid.RowDefinitions.Count; row++)
            {
                var entry = CreateCellEntry(row + 1, newColumn);
                grid.Children.Add(entry);
            }
            isDirty = true;
        }
        private void DeleteRow(int lastRowIndex)
        {
            if (lastRowIndex < 2)
            {
                return;
            }

            foreach (var key in cells.Keys.Where(k => k.Item1 == lastRowIndex).ToList())
            {
                cells.Remove(key);
            }

            var updatedCells = new Dictionary<(int, int), Cell>();
            foreach (var kvp in cells)
            {
                var (row, col) = kvp.Key;
                if (row > lastRowIndex)
                {
                    updatedCells[(row - 1, col)] = kvp.Value;
                    kvp.Value.Name = GetCellName(row - 1, col); 
                }
                else
                {
                    updatedCells[(row, col)] = kvp.Value;
                }
            }
            cells = updatedCells;

            RemoveGridElements(child => grid.GetRow(child) == lastRowIndex);
            grid.RowDefinitions.RemoveAt(lastRowIndex);
            isDirty = true;
        }


        private void DeleteColumn(int lastColumnIndex)
        {
            if (lastColumnIndex < 2)
            {
                return;
            }

            foreach (var key in cells.Keys.Where(k => k.Item2 == lastColumnIndex).ToList())
            {
                cells.Remove(key);
            }

            var updatedCells = new Dictionary<(int, int), Cell>();
            foreach (var kvp in cells)
            {
                var (row, col) = kvp.Key;
                if (col > lastColumnIndex)
                {
                    updatedCells[(row, col - 1)] = kvp.Value;
                    kvp.Value.Name = GetCellName(row, col - 1); 
                }
                else
                {
                    updatedCells[(row, col)] = kvp.Value;
                }
            }
            cells = updatedCells;

            RemoveGridElements(child => grid.GetColumn(child) == lastColumnIndex);
            grid.ColumnDefinitions.RemoveAt(lastColumnIndex);
            isDirty = true;
        }


        private void AddColumnsAndColumnLabels()
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            for (int col = 1; col < CountColumn + 1; col++)
            {
                AddColumn(col);
            }
        }
        private void AddRowsAndCellEntries()
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            for (int row = 1; row < CountRow + 1; row++)
            {
                AddRow(row);
            }
        }
        private void ClearGrid()
        {
            var entries = grid.Children.OfType<Entry>().ToList();
            foreach (var entry in entries)
            {
                entry.Text = string.Empty;
            }
        }
        private async Task<bool> SaveFile(CancellationToken cancellationToken)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Sheet1");

                    foreach (var cellEntry in cells)
                    {
                        int row = cellEntry.Key.Item1;
                        int col = cellEntry.Key.Item2;
                        worksheet.Cell(row, col).Value = cellEntry.Value.Expression;
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        stream.Position = 0;

                        var fileSaverResult = await FileSaver.Default.SaveAsync("sheet.xlsx", stream, cancellationToken);
                        isDirty = false;
                        return fileSaverResult != null;
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Помилка", "При збереженні файлу виникла помилка.", "OK");
                return false;
            }
        }


        private async Task AskToSave()
        {
            if (!isDirty)
                return;

            bool answer = await DisplayAlert("Підтвердження", "Зберегти поточну таблицю?", "Так", "Ні");
            if (answer)
            {
                bool saved = await SaveFile(CancellationToken.None);
                if (!saved)
                {
                    await DisplayAlert("Помилка", "При збереженні файлу виникла помилка.", "OK");
                    return; 
                }
            }
        }
        internal void AddRowButton_Clicked(object sender, EventArgs e)
        {
            int newRow = grid.RowDefinitions.Count;
            AddRow(newRow);
        }
        private void AddColumnButton_Clicked(object sender, EventArgs e)
        {
            int newColumn = grid.ColumnDefinitions.Count;
            AddColumn(newColumn);
        }
        private void DeleteRowButton_Clicked(object sender, EventArgs e)
        {
            if (grid.RowDefinitions.Count > 1)
            {
                int lastRowIndex = grid.RowDefinitions.Count - 1;

                DeleteRow(lastRowIndex);
            }
        }
        private void DeleteColumnButton_Clicked(object sender, EventArgs e)
        {
            if (grid.ColumnDefinitions.Count > 1)
            {
                int lastColumnIndex = grid.ColumnDefinitions.Count - 1;
                DeleteColumn(lastColumnIndex);
            }
        }
        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            SaveFile(CancellationToken.None);
        }
        private async void CreateButton_Clicked(object sender, EventArgs e)
        {
            await AskToSave();
            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();
            CreateGrid();
        }


        private async void ReadButton_Clicked(object sender, EventArgs e)
        {
            await AskToSave();

            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Виберіть файл типу .xlsx",
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.WinUI, new[] { ".xlsx", ".xls" } },
            { DevicePlatform.Android, new[] { ".xlsx", ".xls" } },
            { DevicePlatform.iOS, new[] { ".xlsx", ".xls" } },
            { DevicePlatform.MacCatalyst, new[] { ".xlsx", ".xls" } }
        })
            });

            if (result != null)
            {
                try
                {
                    using (var stream = await result.OpenReadAsync())
                    {
                        using (var workbook = new XLWorkbook(stream))
                        {
                            var worksheet = workbook.Worksheet(1);

                            grid.Children.Clear();
                            grid.RowDefinitions.Clear();
                            grid.ColumnDefinitions.Clear();
                            cells.Clear();

                            CreateGrid();

                            for (int row = 1; row <= worksheet.LastRowUsed().RowNumber(); row++)
                            {
                                if (row >= grid.RowDefinitions.Count)
                                {
                                    AddRow(grid.RowDefinitions.Count);
                                }

                                for (int col = 1; col <= worksheet.LastColumnUsed().ColumnNumber(); col++)
                                {
                                    if (col >= grid.ColumnDefinitions.Count)
                                    {
                                        AddColumn(grid.ColumnDefinitions.Count);
                                    }

                                    var cellExpression = worksheet.Cell(row, col).GetString();
                                    var cellKey = (row, col);

                                    if (!cells.ContainsKey(cellKey))
                                    {
                                        cells[cellKey] = new Cell(GetCellName(row, col), cellExpression, "");
                                    }
                                    else
                                    {
                                        cells[cellKey].Expression = cellExpression;
                                        cells[cellKey].Value = ""; 
                                    }

                                    var entry = GetEntryAt(row, col);
                                    if (entry != null)
                                    {
                                        entry.TextChanged -= OnEntryTextChanged;
                                        entry.Text = cellExpression; 
                                        entry.TextChanged += OnEntryTextChanged;
                                    }
                                }
                            }

                            RecalculateAllValues();
                            isDirty = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Помилка", "При відкритті файла виникла помилка", "OK");
                }
            }
        }


        private void RecalculateAllValues()
        {
            foreach (var cell in cells.Values)
            {
                try
                {
                    if (!string.IsNullOrEmpty(cell.Expression))
                    {
                        cell.Value = new SheetsAppVisitor(cells).Eval(cell).ToString();
                    }
                }
                catch
                {
                    cell.Value = "ERR";
                }

                var cellCoords = cells.FirstOrDefault(kv => kv.Value == cell).Key;
                var entry = GetEntryAt(cellCoords.Item1, cellCoords.Item2);
                if (entry != null)
                {
                    entry.TextChanged -= OnEntryTextChanged;
                    entry.Text = cell.Value;
                    entry.TextChanged += OnEntryTextChanged;
                }
            }
        }


        private async void HelpButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Довідка", "Лабораторна робота з ООП №1\nСтудента групи К24\nТимофія ЛЕЙЦИШИНА\nВаріант 8) 1,3,4,5\n+, -, *, / (бінарні операції)\n+, - (унарні операції)\n^ (піднесення у степінь)\ninc(), dec()",
            "OK");
        }
        private async void ExitButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Підтвердження", "Ви дійсно хочете вийти?",
            "Так", "Ні");
            if (answer)
            {
                await AskToSave();
                System.Environment.Exit(0);
            }
        }
    }
}