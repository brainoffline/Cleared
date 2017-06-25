﻿// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using Cleared.Model;
using Cleared.ViewModels;
using Foundation;
using UIKit;
using XAnimations;
using System.Threading.Tasks;
using System.Collections.Generic;
using Cleared.Utils;
using CoreGraphics;

namespace Cleared.iOS
{
    public partial class PlayViewController : UIViewController, IRunnerView
    {
        public GameDefinition GameDefinition { get; set; }
        GameRunner runner;
        UITapGestureRecognizer nextRecognizer;

        public List<UIColor> ColorPalette { get; set; }

        GameCell selectedGameCell;

        public PlayViewController (IntPtr handle) : base (handle)
        {
            ColorPalette = new List<UIColor>
            {
                "#FFF176".ToUIColor(),
                "#F57C00".ToUIColor(),
                "#FF8A65".ToUIColor(),
                "#388E3C".ToUIColor(),
                "#8BC34A".ToUIColor(),
                "#1976D2".ToUIColor(),
                "#03A9F4".ToUIColor(),
                "#00796B".ToUIColor(),
                "#9C27B0".ToUIColor(),
                "#4A148C".ToUIColor(),
            };
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = GameDefinition.GameSet.Color.ToUIColor();

            if (!string.IsNullOrWhiteSpace(GameDefinition.Instructions))
                TopLabel.Text = GameDefinition.Instructions;
            else
                TopLabel.Text = GameDefinition.DisplayLevel;

            if (GameDefinition.GameSet.Palette != null)
            {
                var colors = new List<UIColor>();
                foreach (var colorStr in GameDefinition.GameSet.Palette)
                {
                    var color = colorStr.ToUIColor();
                    colors.Add(color);
                }
                ColorPalette = colors;
            }


            BackButton.TouchUpInside += (sender, e) => { CloseScreen(); };
            RestartButton.TouchUpInside += (sender, e) => { ResetGame(); };

            nextRecognizer = new UITapGestureRecognizer(() => {
                NextGame();
            });
            FinishedPanel.AddGestureRecognizer(nextRecognizer);


            Grid.ShowsHorizontalScrollIndicator = false;
            Grid.ShowsVerticalScrollIndicator = false;

            FinishedPanel.Hidden = true;

            runner = new GameRunner(this);
            runner.SetGameDefinition(GameDefinition);

            LayoutGrid();

            Grid.BackgroundColor = GameDefinition.GameSet.Color.ToUIColor();

            runner.StartGame();
            Grid.Source = new PlayDataSource(runner, this);
            Grid.UserInteractionEnabled = true;

            Grid.TouchBegan += Grid_TouchBegan;
            Grid.TouchMoved += Grid_TouchMoved;
            Grid.TouchEnded += Grid_TouchEnded;
            Grid.TouchCanceled += Grid_TouchEnded;

        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            LayoutGrid();
        }

        void LayoutGrid()
        {
            var gridWidth = Grid.Frame.Width;
            var gridHeight = Grid.Frame.Height;
            // For now, we know this is 5x5 cells

            var cellWidth = Math.Floor(gridWidth / GameDefinition.Width );
            var cellHeight = Math.Floor(gridHeight / GameDefinition.Height);
            var cellSize = Math.Min(cellWidth, cellHeight);

            var remainderWidth = (nfloat)(gridWidth - (cellSize * GameDefinition.Width));
            var remainderHeight = (nfloat)(gridHeight - (cellSize * GameDefinition.Height));
            var edge = new UIEdgeInsets(remainderHeight / 2, remainderWidth / 2, remainderHeight / 2, remainderWidth / 2 );

            var layout = Grid.CollectionViewLayout as UICollectionViewFlowLayout;
            layout.ItemSize = new CoreGraphics.CGSize(cellSize, cellSize);
            layout.SectionInset = edge;
        }

        public async void OnGameOver()
        {
            RestartButton.Hidden = true;

            await Task.Delay(400);

            Grid.Hidden = true;
            FinishedPanel.Hidden = false;

            await ArrowForward.CreateAnimator<BounceInLeftAnimator>()
                              .SetStartDelay(0.3f)
                              .SetDuration(0.3f)
                              .Animate();
        }

        public async void NextGame()
        {
            await ArrowForward.CreateAnimator<BounceOutRightAnimator>().SetDuration(0.4f).Animate();
            FinishedPanel.Hidden = true;
            Grid.Hidden = false;

            RestartButton.Hidden = false;

            if (!runner.CanNext)
            {
                CloseScreen();
                return;
            }
            runner.Next();

            if (string.IsNullOrWhiteSpace(runner.Definition.Instructions))
                TopLabel.Text = runner.Definition.DisplayLevel;
            else
                TopLabel.Text = runner.Definition.Instructions;
            
            LayoutGrid();
            ShowAllTheCells();
        }

        public void ResetGame()
        {
            runner.Reset();
            LayoutGrid();
            ShowAllTheCells();
        }

        void ShowAllTheCells()
        {
            Grid.ReloadData();

            //foreach (var child in Grid.Subviews)
            //{
            //    if (child is IUpdateable updateable)
            //        updateable.Update(true);
            //}
        }

        public void CloseScreen() 
        {
            DismissViewController(true, null);
        }

        private void Grid_TouchBegan(object sender, UITouch touch)
        {
            var cell = FindCell(touch);
            Debug.WriteLine($"TouchBegan({cell?.ViewModel})");
            if (runner.TouchStart(cell?.ViewModel))
            {
                selectedGameCell = cell;
                selectedGameCell.HighlightColor = selectedGameCell.MarkerColor;
            }
        }

        private void Grid_TouchMoved(object sender, UITouch touch)
        {
            var cell = FindCell(touch);
            Debug.WriteLine($"TouchMoved({cell?.ViewModel})");

            if (cell == null) return;

            if (runner.TouchCell(cell?.ViewModel) && selectedGameCell != null)
                cell.HighlightColor = selectedGameCell.MarkerColor;
        }

        private void Grid_TouchEnded(object sender, UITouch touch)
        {
            var cell = FindCell(touch);
            Debug.WriteLine($"TouchEnded({cell?.ViewModel})");

            bool cleared;
            if (runner.TouchFinish(cell?.ViewModel, out cleared))
            {
                if (!cleared)
                {
                    //TODO: TAP sound
                }
            }
            if (cleared)
            {
                // TODO: POP sound
            }
            if (!runner.IsGameOver)
            {
                //TODO: Show Undo Button
            }
            selectedGameCell = null;
        }

        GameCell FindCell(UITouch touch)
        {
            var touchLocation = touch.LocationInView(Grid);

            foreach (var subView in Grid.Subviews)
            {
                if (subView.Frame.Contains(touchLocation))
                {
                    var cell = subView as GameCell;
                    if (cell != null)
                        return cell;
                }
            }
            return null;
        }

        class AnimationData
        {
            public SquareDataViewModel Cell;
            public GameCell View;
            public int X, Y;
            //public int Offset;

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

            int delay = (max > 0) ? Math.Max(300 / max, 50) : 0;
            foreach (var data in changedCells)
            {
                data.View.Update(false, (data.X + data.Y) * delay);
            }
        }


    }


    internal class PlayDataSource : UICollectionViewSource
    {
        GameRunner runner;
        PlayViewController controller;
        Dictionary<int, GameCell> cells = new Dictionary<int, GameCell>();
        int entranceType = 0;
        float cellDelay = 0.04f;
        float duration = 0.3f;
        float startDelay = 0.0f;

        public PlayDataSource(GameRunner runner, PlayViewController controller)
        {
            this.runner = runner;
            this.controller = controller;

			entranceType = RandomManager.Next(3);
			if (runner.Definition.Width <= 5)
			{
                duration = 0.6f;
                cellDelay = 0.06f;
			}
		}

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return runner.Definition.Width * runner.Definition.Height;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            int index = (int)indexPath.Item;

            var cell = collectionView.DequeueReusableCell(GameCell.CellID, indexPath) as GameCell;
            int x = index % runner.Definition.Width;
            int y = index / runner.Definition.Width;

            cell.ColorPalette = controller.ColorPalette;
            cell.ViewModel = runner.Grid[x, y];
            cell.ViewModel.View = cell;

            cell.Update(true);

            int distanceX = x;
            int distanceY = y;
            switch(entranceType)
            {
                case 1:
					distanceX = Math.Min(x, runner.Definition.Width - 1 - x);
					distanceY = Math.Min(y, runner.Definition.Height - 1 - y);
					break;
                case 2:
					distanceX = Math.Max(x, runner.Definition.Width - 1 - x);
					distanceY = Math.Max(y, runner.Definition.Height - 1 - y);
                    break;
                default:
                    break;
            }
			var delay = startDelay + ((distanceX + distanceY) * cellDelay);

			var finalCellFrame = cell.Frame;
			cell.Frame = new CGRect(finalCellFrame.X, finalCellFrame.Y - finalCellFrame.Height, finalCellFrame.Width, finalCellFrame.Height);
            cell.Alpha = 0;
			UIView.Animate(
				duration, delay,
				UIViewAnimationOptions.CurveEaseOut,
				() =>
				{
                    cell.Alpha = 1;
					cell.Frame = finalCellFrame;
				}, null);

			return cell;
        }
    }
}
