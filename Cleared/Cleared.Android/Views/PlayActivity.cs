using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Cleared.Model;
using Android.Views;
using Android.Widget;
using Cleared.ViewModels;
using XAnimations;
using Cleared.Utils;
using Android.Graphics;
using Cleared.Droid.Engine;
using System.Threading.Tasks;

namespace Cleared.Droid.Views
{
    [Activity(
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class PlayActivity : AppCompatActivity, IRunnerView
    {
        public static string GAMEDEFINITION = "GameDefinition";
        public static string GAMESTARTDATA = "GameStartData";

        GameRunner runner;
        bool building;

        ViewGroup root;
        TextView topLabel;
        TextView winText;
        FrameLayout frame;
        FrameLayout gameOverFrame;
        Button nextButton;
        ImageButton refreshButton;
        ImageButton undoButton;
        SquareGridLayout grid;
        float textSize;
        BaseViewAnimator winTextAnimator;
        GameCell selectedGameCell;

        List<Color> colorPalette;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            colorPalette = new List<Color>
            {
                new Color( ApplicationContext.GetColor(Resource.Color.yellow_300)),
                new Color( ApplicationContext.GetColor(Resource.Color.orange_700)),
                new Color( ApplicationContext.GetColor(Resource.Color.deeporange_300)),
                new Color( ApplicationContext.GetColor(Resource.Color.green_700)),
                new Color( ApplicationContext.GetColor(Resource.Color.lightgreen)),
                new Color( ApplicationContext.GetColor(Resource.Color.blue_700)),
                new Color( ApplicationContext.GetColor(Resource.Color.lightblue)),
                new Color( ApplicationContext.GetColor(Resource.Color.teal_700)),
                new Color( ApplicationContext.GetColor(Resource.Color.purple)),
                new Color( ApplicationContext.GetColor(Resource.Color.purple_900)),
            };
            //trainingColorPallet = new List<Color>
            //{
            //    new Color( ApplicationContext.GetColor(Resource.Color.yellow_300)),
            //    new Color( ApplicationContext.GetColor(Resource.Color.deeporange_300)),
            //};


            SetContentView(Resource.Layout.activity_play);

            GameDefinition gameDefinition = null;
            var json = Intent.GetStringExtra(GAMESTARTDATA);
            var startData = json.FromJson<GameStartData>();
            if (startData != null)
                gameDefinition = GameManager.GetGameDefinition(startData);
            
            if (gameDefinition == null)
                Finish();

            if (gameDefinition.Width <= 4)
                textSize = Resources.GetDimension(Resource.Dimension.cell4_font_size);
            else if (gameDefinition.Width == 5)
                textSize = Resources.GetDimension(Resource.Dimension.cell5_font_size);
            else if (gameDefinition.Width == 6)
                textSize = Resources.GetDimension(Resource.Dimension.cell6_font_size);
            else if (gameDefinition.Width == 7)
                textSize = Resources.GetDimension(Resource.Dimension.cell7_font_size);
            else if (gameDefinition.Width == 8)
                textSize = Resources.GetDimension(Resource.Dimension.cell8_font_size);
            else if (gameDefinition.Width == 9)
                textSize = Resources.GetDimension(Resource.Dimension.cell9_font_size);
            else 
                textSize = Resources.GetDimension(Resource.Dimension.cell10_font_size);

            root = FindViewById<ViewGroup>(Resource.Id.root);
            root.SetBackgroundColor(Android.Graphics.Color.ParseColor(gameDefinition.GameSet.Color));

            topLabel = FindViewById<TextView>(Resource.Id.text);
            if (!string.IsNullOrWhiteSpace(gameDefinition.Instructions))
                topLabel.Text = gameDefinition.Instructions;
            else
                topLabel.Text = gameDefinition.DisplayLevel;

            winText = FindViewById<TextView>(Resource.Id.win_text);
            winText.SetTypeface(FontManager.IconFont, TypefaceStyle.Normal);
            winText.Text = "\uE8dc";

            if (gameDefinition.GameSet.Palette != null)
            {
                var colors = new List<Color>();
                foreach (var colorStr in gameDefinition.GameSet.Palette)
                {
                    var color = Android.Graphics.Color.ParseColor(colorStr);
                    colors.Add(color);
                }
                colorPalette = colors;
            }

            frame = FindViewById<FrameLayout>(Resource.Id.frame);
            grid = FindViewById<SquareGridLayout>(Resource.Id.grid);

            grid.RowCount = gameDefinition.Height;
            grid.ColumnCount = gameDefinition.Width;

            gameOverFrame = FindViewById<FrameLayout>(Resource.Id.gameover_frame);
            nextButton = FindViewById<Button>(Resource.Id.next);
            nextButton.SetBackgroundColor(Android.Graphics.Color.ParseColor(gameDefinition.GameSet.Color));
            nextButton.Click += (s, e) => { NextGame(); };
            nextButton.SetTypeface(FontManager.IconFont, TypefaceStyle.Normal);
            nextButton.Text = "\uE037";

            refreshButton = FindViewById<ImageButton>(Resource.Id.refresh);
            refreshButton.Click += (s, e) => { ResetGame(); };

            undoButton = FindViewById<ImageButton>(Resource.Id.undo);
            undoButton.Click += (s, e) => { Undo(); };

            runner = new GameRunner(this);
            runner.SetGameDefinition(gameDefinition);
            BuildGrid(300);
            runner.StartGame();

            grid.Touch += Grid_Touch;
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (!GameData.Current.MuteSounds)
                MusicManager.Instance.PlayMusic();
        }

        class AnimationData
        {
            public SquareDataViewModel Cell;
            public GameCell View;
            public int X, Y;
            public int Offset;

        }

        public void OnUpdateAllCells()
        {
            var definition = runner.Definition;
            var changedCells = new List<AnimationData>();

            for (int y = 0; y < definition.Height; y++)
            {
                for (int x = 0; x < definition.Width; x++)
                {
                    var cell = runner.Grid[x, y];
                    var cellView = cell.View as GameCell;

                    var changed = cellView.ShowMarker != cell.MarkerVisible;
                    if (changed)
                    {
                        changedCells.Add(new AnimationData
                        {
                            Cell = cell,
                            View = cellView,
                            X = x, 
                            Y = y
                        });
                    }
                }
            }

            if (changedCells.Count == 0)
                return;

            //Shuffle(changedCells);
            var minX = changedCells.Min(c => c.X);
            var minY = changedCells.Min(c => c.Y);
            foreach (var data in changedCells)
            {
                data.X -= minX;
                data.Y -= minY;
            }
            var max = changedCells.Max(c => c.X + c.Y);

            int delay = Math.Max(300 / max, 50);
            foreach (var data in changedCells)
            {
				// data.View.Update(false, (data.X + data.Y) * delay);
				data.View.Update(false, 0);
			}
        }

        public async void OnGameOver()
        {
            refreshButton.Visibility = ViewStates.Invisible;
            undoButton.Visibility = ViewStates.Invisible;

            await Task.Delay(400);

            frame.Visibility = ViewStates.Invisible;

            gameOverFrame.Visibility = ViewStates.Visible;
            await gameOverFrame
                .CreateAnimator<LandingAnimator>()
                .SetDuration(500)
                .Animate();

            winTextAnimator = winText
                .CreateAnimator<PulseAnimator>()
                .SetMax(1.4f)
                .Start();
        }

        public async void NextGame()
        {
            winTextAnimator.Stop();

            await gameOverFrame
                .CreateAnimator<TakingOffAnimator>()
                .SetDuration(300)
                .Animate();
            gameOverFrame.Visibility = ViewStates.Gone;

            frame.Visibility = ViewStates.Visible;
            if (!runner.CanNext)
            {
                Finish();
                return;
            }
            refreshButton.Visibility = ViewStates.Visible;
            runner.Next();
            if (string.IsNullOrWhiteSpace(runner.Definition.Instructions))
                topLabel.Text = runner.Definition.DisplayLevel;
            else
                topLabel.Text = runner.Definition.Instructions;
            BuildGrid(300);
        }

        public void ResetGame()
        {
            runner.Reset();
            BuildGrid(0);
        }

        public void Undo()
        {
            runner.Undo();
            undoButton.Visibility = runner.CanUndo ? ViewStates.Visible : ViewStates.Gone;
        }

        void BuildGrid(int startDelay)
        {
            if (building)
                return;

            building = true;

            undoButton.Visibility = ViewStates.Gone;

            grid.RemoveAllViews();
            grid.ColumnCount = runner.Definition.Width;
            grid.RowCount = runner.Definition.Height;

            var cellDelay = 40;
            var entranceType = RandomManager.Next(3);
            var duration = 1000;

            if (runner.Definition.Width <= 5)
            {
                duration = 1500;
                cellDelay = 60;
            }

            if (!runner.Definition.GameSet.IsTraining)
                Shuffle(colorPalette);

            for (int x = 0; x < runner.Definition.Width; x++)
            {
                for (int y = 0; y < runner.Definition.Height; y++)
                {
                    var cell = runner.Grid[x, y];
                    var cellView = new GameCell(this);

                    cellView.ColorPalette = colorPalette;
                    if (runner.Definition.GameSet.IsTraining)
                        cellView.Font = FontManager.IconFont;

                    cellView.ViewModel = cell;

                    cell.View = cellView;
                    cellView.TextSize = textSize;

                    var layoutParams = new GridLayout.LayoutParams(
                            GridLayout.InvokeSpec(y, 1f),      // Row
                            GridLayout.InvokeSpec(x, 1f));     // Col
                    grid.AddView(cellView, layoutParams);

                    switch(entranceType)
                    {
                        case 1: // Outside-in
                            {
                                int distanceX = Math.Min(x, runner.Definition.Width - 1 - x);
                                int distanceY = Math.Min(y, runner.Definition.Height - 1 - y);
                                cellView.CreateAnimator<BounceInUpAnimator>()
                                    .SetStartDelay(startDelay + ((distanceX + distanceY) * cellDelay))
                                    .SetDuration(duration)
                                    .Animate();
                            }
                            break;

                        case 2: // Inside-out
                            {
                                int distanceX = Math.Max(x, runner.Definition.Width - 1 - x);
                                int distanceY = Math.Max(y, runner.Definition.Height - 1 - y);
                                cellView.CreateAnimator<BounceInUpAnimator>()
                                    .SetStartDelay(startDelay + ((distanceX + distanceY) * cellDelay))
                                    .SetDuration(duration)
                                    .Animate();
                            }
                            break;

                        default:        // Start Top Left
                            cellView.CreateAnimator<BounceInUpAnimator>()
                                .SetStartDelay(startDelay + ((x + y) * cellDelay))
                                .SetDuration(duration)
                                .Animate();
                            break;
                    }
                }
            }
            building = false;
        }

        private void Grid_Touch(object sender, View.TouchEventArgs e)
        {
            e.Handled = true;

            var ev = e.Event;
            var id = ev.GetPointerId(0);
            try
            {
                var x = ev.GetX(id);
                var y = ev.GetY(id);

                GameCell gameCell = null;

                for (int i = 0; i < grid.ChildCount; i++)
                {
                    var child = grid.GetChildAt(i) as GameCell;
                    if (child == null) continue;

                    if (IsPointInsideView(x, y, child))
                    {
                        gameCell = child;
                        break;
                    }
                }
                var cell = gameCell?.ViewModel;

                switch (e.Event.Action)
                {
                    case MotionEventActions.Down:
                        if (cell == null) return;

                        if (runner.TouchStart(cell))
                        {
                            selectedGameCell = gameCell;
                            selectedGameCell.HighlightColor = selectedGameCell.MarkerColor;
                        }
                        break;

                    case MotionEventActions.Move:
                        if (cell == null) return;

                        if (runner.TouchCell(cell) && selectedGameCell != null)
                        {
                            gameCell.HighlightColor = selectedGameCell.MarkerColor;
                        }
                        break;

                    case MotionEventActions.Up:
                    case MotionEventActions.Cancel:
                        bool cleared;
                        if (!runner.TouchFinish(cell, out cleared))
                        {
                            if (!cleared)
                            {
                                // TODO: TAP sound
                            }
                        }
                        if (cleared)
                        {
                            // TODO: Pop sound
                        }
                        if (!runner.IsGameOver)
                        {
                            undoButton.Visibility = ViewStates.Visible;
                        }
                        selectedGameCell = null;
                        break;

                    default:
                        System.Diagnostics.Debug.WriteLine($"Touch: {cell}: {e.Event.Action}, {e.Event.ActionMasked}");
                        break;
                }
            }
            catch
            {
                // Can get odd exceptions with multiple touch
            }

            e.Handled = true;
        }

        public bool IsPointInsideView(float x, float y, View view)
        {
            var viewX = view.GetX();
            var viewY = view.GetY();

            if ((x > viewX && x < (viewX + view.Width)) &&
                (y > viewY && y < (viewY + view.Height)))
            {
                return true;
            }
            return false;
        }

        private static Random rng = new Random();

        public void Shuffle<T>(IList<T> list)
        {
            if (list == null) return;

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}