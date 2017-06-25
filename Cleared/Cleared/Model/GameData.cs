﻿using Newtonsoft.Json;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cleared.Model
{
    public class GameData
    {
#if DEBUG
        public const int SHOW_FEEDBACK_AFTER = 3;      
#else
        public const int SHOW_FEEDBACK_AFTER = 25;      // Only show feedback after 25 games played
#endif

        private static GameData _current;
        public static GameData Current
        {
            get { return _current ?? (_current = new GameData()); }
        }

        public List<GameHighScore> LevelsPlayed { get; set; }
        public int GamesPlayed => LevelsPlayed?.Count ?? 0;

        public bool SeenHowToPlay { get; set; }
        public bool FinishedTraining { get; set; }
        public bool FeedbackSent { get; set; }

        public bool MuteMusic { get; set; }
        public bool MuteSounds { get; set; }


        public GameData()
        {
            LevelsPlayed = new List<GameHighScore>();
        }

        public void ToggleMusic()
        {
            MuteMusic = !MuteMusic;
            SaveData();
        }

        public async void LevelPlayed(GameDefinition definition, TimeSpan timeTaken)
        {
            var gamePlayed = GetLevelPlayed(definition);
            if (gamePlayed == null)
            {
                gamePlayed = new GameHighScore
                {
                    PackName = definition.GamePack.Name,
                    SetName = definition.GameSet.Name,
                    GameIndex = definition.Index,
                    TimeTaken = timeTaken
                };
                LevelsPlayed.Add(gamePlayed);
            }
            else
            {
                if (timeTaken > gamePlayed.TimeTaken)
                    return;

                gamePlayed.TimeTaken = timeTaken;
            }

            await SaveData();
        }

        public GameHighScore GetLevelPlayed(GameDefinition definition)
        {
            var gamePlayed = LevelsPlayed.FirstOrDefault(g =>
                g.PackName == definition.GamePack.Name &&
                g.SetName == definition.GameSet.Name &&
                g.GameIndex == definition.Index);

            return gamePlayed;
        }

        public async void ResetScores()
        {
            LevelsPlayed.Clear();
            await SaveData();
        }

        public async Task LoadData()
        {
            try
            {
                var f = await FileSystem.Current.LocalStorage.CreateFileAsync("GameData.json", CreationCollisionOption.OpenIfExists);
                var json = await f.ReadAllTextAsync();
                _current = JsonConvert.DeserializeObject<GameData>(json);

                if (_current == null)
                    _current = new GameData();

                // Cleanup any bad data
                foreach (var levelPlayed in _current.LevelsPlayed.ToList())
                {
                    if (string.IsNullOrWhiteSpace(levelPlayed.PackName) ||
                        string.IsNullOrWhiteSpace(levelPlayed.SetName))
                        _current.LevelsPlayed.Remove(levelPlayed);
                }
            }
            catch (Exception)
            {
                _current = new GameData();
            }
        }

        public async Task SaveData()
        {
            try
            {
                var t = FileSystem.Current.LocalStorage.CreateFileAsync("GameData.json", CreationCollisionOption.ReplaceExisting);
                var f = await t.ConfigureAwait(false);
                var json = _current.ToJson();
                await f.WriteAllTextAsync(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public int GamesPlayedForGamePack(string packName)
        {
            return LevelsPlayed.Count(lp => lp.PackName == packName);
        }

        public int GamesPlayedForGameSet(GameSet gameSet)
        {
            return LevelsPlayed.Count(lp => ((lp.PackName == gameSet.GamePack.Name) && (lp.SetName == gameSet.Name)));
        }

        public GameHighScore GetGameHighScore(GameDefinition definition)
        {
            return GetGameHighScore(definition.GamePack.Name, definition.GameSet.Name, definition.Index);
        }

        public GameHighScore GetGameHighScore(string packName, string setName, int index)
        {
            return LevelsPlayed.FirstOrDefault(lp =>
                lp.PackName == packName &&
                lp.SetName == setName &&
                lp.GameIndex == index);
        }

        public TimeSpan GetBestTime(string packName, string setName, int index)
        {
            var levelPlayed = GetGameHighScore(packName, setName, index);
            if (levelPlayed != null)
                return levelPlayed.TimeTaken;
            return TimeSpan.Zero;
        }
    }

}
