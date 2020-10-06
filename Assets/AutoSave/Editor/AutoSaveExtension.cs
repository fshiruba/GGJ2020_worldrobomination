using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace EckTechGames
{
    [InitializeOnLoad]
    public class AutoSaveExtension
    {
        // Static constructor that gets called when unity fires up.
        static AutoSaveExtension()
        {
            EditorApplication.playModeStateChanged += AutoSaveWhenPlaymodeStarts;
        }

        private static void AutoSaveWhenPlaymodeStarts(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode)
            {
                Debug.Log("Autosave!");
                EditorSceneManager.SaveOpenScenes();
                AssetDatabase.SaveAssets();
            }
        }

        /*
        private static void AutoSaveWhenPlaymodeStarts()
        {
            // If we're about to run the scene...
            if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
            {
                // Save the scene and the assets.
                EditorApplication.SaveScene();
                
                AssetDatabase.SaveAssets();
            }
        }
        */
    }
}