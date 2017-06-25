using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.OS;
using Android.Views;
using Android.Support.V4.App;
using Cleared.Model;
using Android.Widget;
using Cleared.Utils;
using Android.Content;

namespace Cleared.Droid.Views
{
    public class SelectGameFragment : Fragment
    {
        public GameSet GameSet { get; set; }
        ViewGroup root;
        SquareGridLayout grid;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_select_game, container, false);
            root = view.FindViewById<ViewGroup>(Resource.Id.root);

            var label = view.FindViewById<TextView>(Resource.Id.text);
            label.Text = GameSet.Name;

            root.SetBackgroundColor(Android.Graphics.Color.ParseColor(GameSet.Color));

            grid = view.FindViewById<SquareGridLayout>(Resource.Id.grid);

            int gameCount = GameSet.GameCount;
            var cols = (int)Math.Sqrt(gameCount);
            var rows = (int)(gameCount / cols);
            if ((cols * rows) < gameCount)
                rows++;

            grid.ColumnCount = cols;
            grid.RowCount = rows;
            grid.RemoveAllViews();

            for(int i = 0; i < GameSet.GameCount; i++)
            {
                int row = i / grid.ColumnCount;
                int col = i % grid.ColumnCount;

                var gameDefinition = GameSet.Games[i];
                var highScore = GameData.Current.GetGameHighScore(gameDefinition);

                if (string.IsNullOrWhiteSpace(gameDefinition.Name))
                    gameDefinition.Name = (i + 1).ToString();

                var squareWidget = new SquareWidget(Context);
                squareWidget.GameDefinition = gameDefinition;
                squareWidget.Text = gameDefinition.Name;
                //squareWidget.IsSelected = false;
                squareWidget.ShowBackground = highScore == null;

                var layoutParams = new GridLayout.LayoutParams(
                    GridLayout.InvokeSpec(row, 1f),      // Row
                    GridLayout.InvokeSpec(col, 1f));     // Col
                grid.AddView(squareWidget, layoutParams);

                squareWidget.Click += SquareView_Click;
            }

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            ClearSelected();
        }

        private void SquareView_Click(object sender, EventArgs e)
        {
            ClearSelected();

            var squareWidget = sender as SquareWidget;
            var gameDefinition = squareWidget.GameDefinition as GameDefinition;

            //squareWidget.IsSelected = true;
            squareWidget.ShowBackground = false;

            Intent intent = new Intent(Context, typeof(PlayActivity));
            intent.PutExtra(
                PlayActivity.GAMESTARTDATA,
                new GameStartData
                {
                    PackName = gameDefinition.GamePack.Name,
                    SetName = gameDefinition.GameSet.Name,
                    Index = gameDefinition.Index
                }.ToJson());

            StartActivity(intent);
        }

        void ClearSelected()
        {
            for (int i = 0; i < grid.ChildCount; i++)
            {
                var squareWidget = grid.GetChildAt(i) as SquareWidget;
                //if (squareWidget != null)
                //    squareWidget.IsSelected = false;

                var highScore = GameData.Current.GetGameHighScore(squareWidget.GameDefinition as GameDefinition);
                squareWidget.ShowBackground = highScore == null;
            }
        }
    }
}