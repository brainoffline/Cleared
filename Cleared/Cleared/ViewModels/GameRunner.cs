using Cleared.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Cleared.ViewModels
{
    public interface IRunnerView
    {
        void OnUpdateAllCells();
        void OnGameOver();
    }

    public class GameRunner
    {
        public GameDefinition Definition { get; set; }
        public IRunnerView View { get; set; }

        public GameHighScore Best { get; set; }

        public string Level { get; set; }

        protected GameDefinition.GameLine SelectedLine { get; set; }
        public List<int> SelectedCells { get; set; } = new List<int>();

        public SquareDataViewModel[,] Grid { get; set; }

        public Stack<GridState> GridStates;


        public DateTime StartTime { get; set; }
        public TimeSpan CompletedTime { get; set; }
        public bool IsGameOver { get; set; }

        public string HighScore { get; set; }
        public bool IsHighScore { get; set; }
        public string PlayTime { get; set; }
        public string PlayTimeMinutes { get; set; }
        public string PlayTimeSeconds { get; set; }
        public string Color { get; set; }

        public GameRunner(IRunnerView view)
        {
            View = view;
        }

        public void SetGameDefinition(GameDefinition gameDefinition)
        {
            IsGameOver = false;
            IsHighScore = false;
            Definition = gameDefinition;
            SelectedCells.Clear();

            StartTime = DateTime.Now;
            Grid = new SquareDataViewModel[Definition.Width, Definition.Height];
            GridStates = new Stack<GridState>();
            Color = Definition.GameSet.Color;

            for (int x = 0; x < Definition.Width; x++)
            {
                for (int y = 0; y < Definition.Height; y++)
                {
                    Grid[x, y] = new SquareDataViewModel
                    {
                        MarkerVisible = true,
                        //HighColor = Color,
                        //MedColor = "#ADD8E6",
                    };
                }
            }

            int index = 0;
            foreach (var gameLine in Definition.Lines)
            {
                int startX, startY;
                int endX, endY;

                index++;
                //if (string.IsNullOrWhiteSpace(gameLine.Key))
                //    gameLine.Key = index.ToString();

                Definition.GetStartXY(gameLine, out startX, out startY);
                Definition.GetEndXY(gameLine, out endX, out endY);

                var square = Grid[startX, startY];
                square.Fixed = true;
                square.PaletteIndex = index;
                square.Text = gameLine.Text;
                square.GameLine = gameLine;

                square = Grid[endX, endY];
                square.Fixed = true;
                square.Text = gameLine.Text;
                square.PaletteIndex = index;
                square.GameLine = gameLine;
            }

            Level = string.Format("Level {0}", Definition.Index + 1);

            GetHighScore();
        }

        public void GetHighScore()
        {
            Best = GameManager.GetHighScore(Definition);
            if (Best != null)
                HighScore = string.Format(@"{0:m\:ss}", Best.TimeTaken);
        }

        public void StartGame()
        {
            StartTime = DateTime.Now;
            IsGameOver = false;
        }

        public void UpdateAllCells()
        {
            View.OnUpdateAllCells();
            /*
            for (int x = 0; x < Definition.Width; x++)
            {
                for (int y = 0; y < Definition.Height; y++)
                {
                    var square = Grid[x, y];
                    square.View.Update();
                }
            }
            */
        }

        public void UpdateCells(GameDefinition.GameLine selectedLine)
        {
            for (int x = 0; x < Definition.Width; x++)
            {
                for (int y = 0; y < Definition.Height; y++)
                {
                    var square = Grid[x, y];
                    if (square.GameLine != selectedLine) continue;

                    square.View.Update();
                }
            }
        }

        public bool FindXY(SquareDataViewModel cell, out int x, out int y)
        {
            x = y = 0;
            for (int x1 = 0; x1 < Definition.Width; x1++)
            {
                for (int y1 = 0; y1 < Definition.Height; y1++)
                {
                    if (Grid[x1, y1] == cell)
                    {
                        x = x1;
                        y = y1;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool TouchStart(SquareDataViewModel cell)
        {
            int x, y;
            FindXY(cell, out x, out y);

            Debug.WriteLine($"TouchStart: {x}, {y}: {cell}");

            if (IsGameOver) return false;
            if (cell?.GameLine == null)
                return false;

            if (!cell.Fixed)
                return false;

            if (cell.TouchState == TouchState.Touched)
            {

                SelectedLine = cell.GameLine;
                ClearSelectedShape();

                return false;
            }

            SelectedLine = cell.GameLine;
            SelectedCells.Add(Definition.GetCellIndex(x, y));
            cell.TouchState = TouchState.Touching;

            return true;
        }

        public bool TouchCell(SquareDataViewModel cell)
        {
            if (cell == null) return false;

            int x, y;
            FindXY(cell, out x, out y);
            int cellIndex = Definition.GetCellIndex(x, y);
            int lastCellIndex = SelectedCells.Count == 0 ? -1 : SelectedCells.Last();

            Debug.WriteLine($"TouchCell: {x}, {y}: {cell}");

            if (IsGameOver)
                return false;
            if (SelectedLine == null)
                return false;
            if (lastCellIndex == cellIndex)
                return false;       // Ain't moved

            if (cell.TouchState == TouchState.Touched)
                return false;


            // Touching another line start/end
            if (cell.Fixed && cell.GameLine != SelectedLine)
            {
                return false;
            }

            // If selected cell is already in the list.  This is when the user goes backwards
            if (SelectedCells.Contains(cellIndex))
            {
                // The user has back tracked
                var index = SelectedCells.LastIndexOf(cellIndex);

                while (SelectedCells.Count > index)
                {

                    var last = SelectedCells.Last();
                    SelectedCells.RemoveAt(SelectedCells.Count() - 1);

                    int lastX, lastY;
                    Definition.GetCellXY(last, out lastX, out lastY);
                    var lastCell = Grid[lastX, lastY];

                    lastCell.TouchState = TouchState.UnTouched;
                }

                if (index < SelectedCells.Count)
                    SelectedCells.RemoveRange(index + 1, SelectedCells.Count - index);
                return true;
            }

            if (lastCellIndex >= 0)
            {
                if (!AreCellsNextToEachOther(lastCellIndex, cellIndex))
                    return false;
            }

            cell.TouchState = TouchState.Touching;
            cell.GameLine = SelectedLine;
            SelectedCells.Add(cellIndex);

            return true;
        }

        bool AreCellsNextToEachOther(int one, int two)
        {
            if ((two == one + 1) ||                 // right
                (two == one - 1) ||                 // left
                (two == one - Definition.Width) ||  // up
                (two == one + Definition.Width))    // down
                return true;
            return false;
        }

        public bool TouchFinish(SquareDataViewModel cell, out bool cleared)
        {
            int x, y;

            cleared = false;
			if (SelectedCells.Count == 0) return false;

			var state = CreateGridState();

            if (cell == null)
            {
                var cellIndex = SelectedCells.Last();
                x = cellIndex % Definition.Width;
                y = cellIndex / Definition.Width;
                cell = Grid[x, y];
            }
            else
                FindXY(cell, out x, out y);

            Debug.WriteLine($"TouchFinish: {x}, {y}: {cell}");

            if (IsGameOver) return false;
            if (SelectedLine == null) return false;

            if (SelectedCells.Count >= 2)
            {
                int cellIndex = Definition.GetCellIndex(x, y);
                int lastCellIndex = SelectedCells.Last();
                if (lastCellIndex == cellIndex)
                    lastCellIndex = SelectedCells[SelectedCells.Count - 2];

                if (!AreCellsNextToEachOther(lastCellIndex, cellIndex))
                {
                    ClearSelectedShape();
                    return false;
                }
            }


            var allCells = Grid.Flatten().ToList();
            var touchingCelling = allCells.Where(c => c.TouchState == TouchState.Touching);

            if (cell.Fixed &&                           // Start or end
                cell.GameLine == SelectedLine &&        // Same as beggining
                (touchingCelling.Count() > 1))          // Selected more than a single cell
            {
                foreach (var touchedCell in touchingCelling)
                {
                    touchedCell.TouchState = TouchState.Touched;
                    touchedCell.MarkerVisible = false;
                }
                cell.TouchState = TouchState.Touched;
                cell.MarkerVisible = false;

                SelectedLine = null;
                SelectedCells.Clear();

                UpdateAllCells();
                if (CheckComplete())
                    return false;

                GridStates.Push(state);
                return true;
            }

            ClearSelectedShape();
            return false;
        }

        private void ClearSelectedShape()
        {
            if (SelectedLine == null)
                return;

            var allCells = Grid.Flatten().ToList();
            foreach (var square in allCells.Where(s => s.GameLine == SelectedLine))
            {
                square.TouchState = TouchState.UnTouched;
                square.MarkerVisible = true;

                if (!square.Fixed)
                    square.GameLine = null;

                square.View.Update();
            }
            SelectedLine = null;
            SelectedCells.Clear();
        }

        public bool CheckComplete()
        {
            var allCells = Grid.Flatten().ToList();
            var count = allCells.Count(s => s.TouchState == TouchState.Touched);
            if (count == Definition.Width * Definition.Height)
            {
                CompletedTime = DateTime.Now - StartTime;
                GameData.Current.LevelPlayed(Definition, CompletedTime);

                GetHighScore();

                IsGameOver = true;
                IsHighScore = CompletedTime == Best.TimeTaken;
                View.OnGameOver();

                return true;
            }
            return false;
        }

        public bool CanNext
        {
            get { return Definition.Index < Definition.GameSet.Games.Count - 1; }
        }

        public void Next()
        {
            if (!CanNext)
                return;

            var definition = GameManager.GetGameDefinition(new GameStartData
            {
                PackName = Definition.GamePack.Name,
                SetName = Definition.GameSet.Name,
                Index = Definition.Index + 1
            });

            SetGameDefinition(definition);
        }

        public void Reset()
        {
            IsGameOver = false;
            StartTime = DateTime.Now;
            GridStates.Clear();

            foreach (var square in Grid)
            {
                square.MarkerVisible = true;
                square.TouchState = TouchState.UnTouched;
                if (!square.Fixed)
                    square.GameLine = null;
            }

        }

        public bool CanUndo
        {
            get { return GridStates.Count > 0; }
        }
        public void Undo()
        {
            if (IsGameOver) return;
            if (!CanUndo) return;

            var lastState = GridStates.Pop();
            RestoreFromGridState(lastState);

            UpdateAllCells();
        }

        public void Tick()
        {
            var playTime = (IsGameOver ? CompletedTime : (DateTime.Now - StartTime));
            PlayTime = playTime.ToString(@"m\:ss");
            PlayTimeMinutes = playTime.TotalMinutes.ToString("0");
            PlayTimeSeconds = playTime.Seconds.ToString("00");
        }

        GridState CreateGridState()
        {
            var allCells = Grid.Flatten().ToList();
            var state = new GridState
            {
                TouchStates = allCells.Select(c => c.TouchState == TouchState.Touched).ToList()
            };
            return state;
        }

        void RestoreFromGridState(GridState gridState)
        {
            if (gridState == null) return;

            var allCells = Grid.Flatten().ToList();

            for (int i = 0; i < allCells.Count; i++)
            {
                var touched = gridState.TouchStates[i];
                var cell = allCells[i];
                cell.TouchState = touched ? TouchState.Touched : TouchState.UnTouched;
                cell.MarkerVisible = !touched;
                if (!cell.Fixed)
                    cell.GameLine = null;
            }
        }

        public String Dump
        {
            get {
                var allCells = Grid.Flatten().ToList();
                var cells = allCells.Select(c => c.ToString());
                return JsonConvert.SerializeObject(cells);
            }
        }
        public String Dump1Text
        {
            get
            {
                var allCells = Grid.Flatten().ToList();
                var cells = allCells.Select(c => c.Text);
                return JsonConvert.SerializeObject(cells);
            }
        }
        public String Dump3Touch
        {
            get
            {
                var allCells = Grid.Flatten().ToList();
                var cells = allCells.Select(c => c.TouchState);
                return JsonConvert.SerializeObject(cells);
            }
        }
        public String Dump4Shape
        {
            get
            {
                var allCells = Grid.Flatten().ToList();
                var cells = allCells.Select(c => c.GameLine);
                return JsonConvert.SerializeObject(cells);
            }
        }

    }

}
