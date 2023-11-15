using UnityEngine;
using System.Collections;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;


[CreateAssetMenu(fileName = "ScenePreference", menuName = "ScenePreference")]
public partial class ScenePreference : ScriptableObject
{
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
    private static ScenePreference singleton;

    public Scene[] scenes;

    private int nextScene;
    public Scene GameScene
    {
        set
        {
            for (int i = 0; i < scenes.Length; i++)
            {
                if (scenes[i].arrayIndex == value.arrayIndex)
                {
                    scenes[i] = value;
                    nextScene = i;
                    return;
                }
            }
        }

        get
        {
            return scenes[nextScene];
        }
    }

    [System.Serializable]
    public class Scene
    {
        public string name;
        public int arrayIndex;
        public Sprite sprite;
        public MatchPreference matchPreference;
        [HideInInspector] public GameDifficult difficultEnum;
        public int buildIndex;
        public int ballIndex;
        public Unit playerPrefab;
        public Unit aiPrefab;
        public PlayerType[] playersType = new PlayerType[2] { PlayerType.Human, PlayerType.Bot};
        public LocalizedString descriptionString;
        public LocalizedString nameString;

        public int difficult { set { difficultEnum = (GameDifficult)value; } }

    }
}
