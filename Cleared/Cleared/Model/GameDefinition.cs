using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Cleared.Model
{
    public class GameDefinition
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }

        public List<GameLine> Lines { get; set; }

        [JsonIgnore]
        public GamePack GamePack { get; set; }

        [JsonIgnore]
        public GameSet GameSet { get; set; }

        [JsonIgnore]
        public int Index { get; set; }

        [JsonIgnore]
        public int DisplayIndex
        {
            get { return Index + 1; }
        }

        [JsonIgnore]
        public string DisplayLevel
        {
            get { return string.Format("{0} / {1}", Index + 1, GameSet?.GameCount ?? 0); }
        }

        [JsonIgnore]
        public string BackgroundColor
        {
            get
            {
                var score = GameManager.GetHighScore(this);
                if (score == null)
                    return "#33000000";
                return GameSet.Color;
            }
        }

        [JsonIgnore]
        public string BestTime
        {
            get
            {
                var score = GameManager.GetHighScore(this);
                if ((score != null) && (score.TimeTaken > TimeSpan.Zero))
                {
                    var str = string.Format("{0}:{1:00}",
                        Math.Round(score.TimeTaken.TotalMinutes, 0),
                        score.TimeTaken.Seconds);
                    return str;
                }
                return "";
            }
        }

        public class GameLine
        {
            public int Start { get; set; }
            public int End { get; set; }
            public string Text { get; set; }
        }


        public string GetKey()
        {
            return string.Format("{0},{1},{2}", GamePack.Name, GameSet.Name, Index);
        }

        public int GetCellIndex(int x, int y)
        {
            return (y * Width) + x;
        }

        public void GetCellXY(int index, out int x, out int y)
        {
            x = index % Width;
            y = index / Width;
        }

        public void GetStartXY(GameLine line, out int x, out int y)
        {
            GetCellXY(line.Start, out x, out y);
        }

        public void GetEndXY(GameLine line, out int x, out int y)
        {
            GetCellXY(line.End, out x, out y);
        }

    }
}
