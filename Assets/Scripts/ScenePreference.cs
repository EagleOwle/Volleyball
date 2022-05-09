﻿using UnityEngine;
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

    public int nextScene;

    public Scene[] scenes;

    public void SetScene(Scene scene)
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            if(scenes[i].arrayIndex == scene.arrayIndex)
            {
                scenes[i] = scene;
                nextScene = i;
                return;
            }
        }
    }

    [System.Serializable]
    public struct Scene
    {
        public int arrayIndex;
        public string name;
        public Sprite sprite;
        public int rounds;
        public GameDifficult difficultEnum;
        public int difficult
        {
            set { difficultEnum = (GameDifficult)value; }
        }
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
