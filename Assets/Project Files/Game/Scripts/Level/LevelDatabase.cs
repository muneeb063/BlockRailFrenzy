using System.Collections.Generic;
using UnityEngine;

namespace Watermelon.BusStop
{
    [CreateAssetMenu(fileName = "Level Database", menuName = "Data/Level Database")]
    public class LevelDatabase : ScriptableObject
    {
        [SerializeField, LevelEditorSetting] LevelData[] levels;
        public LevelData[] Levels => levels;
        public List<LevelData> NewLevelsList;
        public int LevelCount = 10;
        [SerializeField] LevelElement[] levelElements;
        public LevelElement[] LevelElements => levelElements;

        [SerializeField] ElementSpecialEffect[] specialEffects;
        public ElementSpecialEffect[] SpecialEffects => specialEffects;

        [SerializeField] Texture2D editorSpawnerTexure;
      
        public void Initialise()
        {
            for(int i = 0; i < levelElements.Length; i++)
            {
                levelElements[i].Init();
            }
           // StoreLevelsIntoNewArray();
        }
        public void StoreLevelsIntoList()
        {
            for(int i = 0; i < LevelCount; i++)
            {
                NewLevelsList.Add(Levels[i]);
            }
        }
        public void Unload()
        {
            for (int i = 0; i < levelElements.Length; i++)
            {
                levelElements[i].Unload();
            }
        }

        public int GetRandomLevelIndex(int displayLevelNumber, int lastPlayedLevelNumber, bool replayingLevel)
        {
            if (levels.IsInRange(displayLevelNumber))
            {
                return displayLevelNumber;
            }

            if(replayingLevel)
            {
                return lastPlayedLevelNumber;
            }

            int randomLevelIndex;

            do
            {
                randomLevelIndex = Random.Range(0, levels.Length);
            }
            while (!levels[randomLevelIndex].UseInRandomizer && randomLevelIndex != lastPlayedLevelNumber);

            return randomLevelIndex;
        }

        public LevelData GetLevel(int levelIndex)
        {
            levelIndex = Mathf.Clamp(levelIndex, 0, levels.Length - 1);

            return levels[levelIndex];
        }
    }
}
