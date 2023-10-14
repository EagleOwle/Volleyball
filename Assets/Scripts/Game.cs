using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputManagerEntry;

public class Game : MonoBehaviour
{
    #region Singleton
    private static Game _instance;
    public static Game Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Game>();
            }

            return _instance;
        }
    }
    #endregion

    [SerializeField] private bool starMatch = true;
    [SerializeField] private bool useTargetFramerate = false;
    [SerializeField] private int targetFrameRate = 30;
    [SerializeField] private AudioClip roundFallClip;
    private string debugCurrentGameState;
    [SerializeField] private GameObject RunningLights;
    [SerializeField] private GameObject Shuts;

    public Action<RoundResult> actionRoundFail;

    public ScenePreference.Scene scene;
    public Match match;
    

    private PlayerType lastLuser = PlayerType.None;

    private void Awake()
    {
        if (useTargetFramerate) Application.targetFrameRate = targetFrameRate;
        int qualityLevel = QualitySettings.GetQualityLevel();
        if (qualityLevel == 0) 
        {
            RunningLights.SetActive(false);
            Shuts.SetActive(false);
        }
        if (qualityLevel == 1) 
        {
            RunningLights.SetActive(true); 
            Shuts.SetActive(false);
        }
        if (qualityLevel == 2) 
        {
            RunningLights.SetActive(true);
            Shuts.SetActive(true);
        }
    }

    private void Start()
    {
        if (SceneLoader.Instance == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            return;
        }

        scene = ScenePreference.Singleton.GameScene;

        StateMachine.InitBeheviors();
        StateMachine.actionChangeState += ChangeState;

        match = new Match();
        match.Initialise(scene.rounds);

        if (starMatch)
        {
            SceneInitialize.Initialise(scene);
            Invoke(nameof(StartRound), Time.deltaTime);
        }
    }

    private void ChangeState(State obj)
    {
        debugCurrentGameState = obj.nameState;
    }

    public void OnRoundFall(PlayerType luser)
    {
        AudioController.Instance.PlayClip(roundFallClip);

        RoundResult roundResult = match.SetScore(luser);
        actionRoundFail?.Invoke(roundResult);

        lastLuser = luser;

        StateMachine.SetState<FallState>();
    }

    public void RestartRaund()
    {
        StartRound();
    }

    private void StartRound()
    {
        match.round++;
        SceneInitialize.StartRound(lastLuser);
    }

    public void EndGame()
    {
        SceneLoader.Instance.LoadMenu();
    }

}
