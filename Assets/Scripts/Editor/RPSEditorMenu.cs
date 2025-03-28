using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ESG.RockPaperScissors
{
    public class RPSEditorMenu
    {
        private const string CONFIG_PATH = "Assets/ScriptableObjects/GameConfig.asset";

        [MenuItem("RPS/Create Game Config")]
        static void CreateGameConfig()
        {
            GameConfig gameConfig;
            
            // First try to find the existing config file
            gameConfig = AssetDatabase.LoadAssetAtPath<GameConfig>(CONFIG_PATH);
            // Otherwise create the ScriptableObject file
            if(gameConfig == null)
            {
                gameConfig = ScriptableObject.CreateInstance<GameConfig>();
                AssetDatabase.CreateAsset(gameConfig, CONFIG_PATH);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            // Focus the file in the Project window
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = gameConfig;
        }
    }
}