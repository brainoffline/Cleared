using Cleared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleared.ViewModels
{
    public class GamePackVM
    {
        public GamePack Data { get; set; }

        public List<GameSetVM> GameSets { get; set; }

        public GamePackVM(GamePack gamePack)
        {
            Data = gamePack;
            GameSets = new List<GameSetVM>();

            foreach (var gameSet in gamePack.GameSets)
                GameSets.Add(new GameSetVM(gameSet));
        }
    }

    public class GameSetVM
    {
        public GameSet Data { get; set; }
        public List<GameVM> Games { get; set; }

        public string Color
        {
            get { return Data.Color; }
        }

        public GameSetVM(GameSet gameSet)
        {
            Data = gameSet;
            Games = new List<GameVM>(gameSet.Games.Count);
            foreach (var game in gameSet.Games)
                Games.Add(new GameVM(game));
        }
    }

    public class GameVM
    {
        public GameDefinition Definition { get; private set; }

        public string HighScore { get; set; }
        public string Color { get; set; }

        public GameVM(GameDefinition gameDefinition)
        {
            Definition = gameDefinition;

            UpdateHighScore();

            Color = Definition.GameSet.Color;
        }

        public void UpdateHighScore()
        {
            var highScore = GameManager.GetHighScore(Definition);
            if (highScore != null)
            {
                HighScore = string.Format(@"{0:m\:ss}", highScore.TimeTaken);
                HasHighScore = true;
            }
        }

        public int Level { get { return Definition.Index + 1; } }
        public bool HasHighScore { get; set; }
        public double BackgroundOpacity { get { return HasHighScore ? 1.0 : 0.2; } }

    }

}
