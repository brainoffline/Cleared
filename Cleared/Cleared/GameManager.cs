﻿using Cleared.Model;
using Cleared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleared
{
    public class GameManager
    {
        public static GamePackVM GetGamePack(string packName)
        {
            var gamePack = Current.GamePacks.FirstOrDefault(gp => gp.Name == packName);
            if (gamePack != null)
                return new GamePackVM(gamePack);
            return null;
        }

        public static GameDefinition GetGameDefinition(GameStartData startData)
        {
            var gamePack = Current.GamePacks.FirstOrDefault(gp => gp.Name == startData.PackName);
            var gameSet = gamePack.GameSets.FirstOrDefault(gs => gs.Name == startData.SetName);
            var gameDefinition = gameSet.Games[startData.Index];

            return gameDefinition;
        }


        public static GameHighScore GetHighScore(GameDefinition definition)
        {
            return GameData.Current.GetGameHighScore(definition);
        }


        private static GameManager _current;

        public static GameManager Current
        {
            get { return _current ?? (_current = new GameManager()); }
        }

        public List<GamePack> GamePacks { get; set; }
        public List<GameSet> AllGameSets { get; set; }

        public GamePack SelectedGamePack { get; set; }
        public GameSet SelectedGameSet { get; set; }
        public int SelectedGameIndex { get; set; }



        private GameManager()
        {
            GamePacks = new List<GamePack>();
        }

        public void LoadKnownGamePacks()
        {
            var packNames = new List<string> { "PackA.json" };

            foreach (var packName in packNames)
            {
                var gamePack = ResourceUtil.Impl.Read<GamePack>("Packs", packName);

                GamePacks.Add(gamePack);

                foreach (var filename in gamePack.GameSetFilenames)
                {
                    var gameSet = ResourceUtil.Impl.Read<GameSet>("Packs", filename);
                    gameSet.GamePack = gamePack;

                    for (int i = 0; i < gameSet.Games.Count; i++)
                    {
                        var definition = gameSet.Games[i];
                        definition.GameSet = gameSet;
                        definition.GamePack = gamePack;
                        definition.Index = i;
                    }

                    gamePack.GameSets.Add(gameSet);
                }
            }

            AllGameSets = new List<GameSet>();
            foreach (var gamePack in GamePacks)
                AllGameSets.AddRange(gamePack.GameSets);
        }

        public async Task<bool> CheckTrainingFinished()
        {
            if (GameData.Current.FinishedTraining)
                return true;

            var trainingSet = AllGameSets.FirstOrDefault();
            if (GameData.Current.GamesPlayedForGameSet(trainingSet) == trainingSet.GameCount)
            {
                GameData.Current.FinishedTraining = true;
                await GameData.Current.SaveData();
            }
            return GameData.Current.FinishedTraining;
        }

        public int GamesPlayedForPack(string packName)
        {
            return GameData.Current.GamesPlayedForGamePack(packName);
        }

    }
}
