using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
   

    public Action<RoundResult> actionRoundFail;

    public ScenePreference.Scene scene;
    public Match match;
    

    private PlayerType lastLuser = PlayerType.None;



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
