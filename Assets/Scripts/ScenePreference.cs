using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "ScenePreference", menuName = "ScenePreference")]
public class ScenePreference : ScriptableObject
{
    private static ScenePreference singleton;
    public static ScenePreference Singleton
    {
        get
        {
            if (singleton == null)
            {
                singleton = Resources.Load("ScenePreference") as ScenePreference;
            }

            return singleton;
        }
    }

    public Scene[] scenes;

    [System.Serializable]
    public struct Scene
    {
        public string name;
        public Sprite sprite;
        public int rounds;
        public GameDifficult difficult;
        public int buildIndex;
        public string description;
    }

    public enum GameDifficult
    {
        Easy,
        Normal,
        Hard
    }
}
