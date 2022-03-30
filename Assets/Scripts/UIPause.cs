using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPause : UIPanel
{
    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _ressumBtn;

    private void OnEnable()
    {
        Game.Instance.SetStatus(Game.Status.pause);
    }

    private void Start()
    {
        _restartBtn.onClick.AddListener(OnButtonRestart);
        _ressumBtn.onClick.AddListener(OnButtonRessum);
    }

    private void OnButtonRessum()
    {
        UIHud.Instance.ChangePanel("GamePanel");
    }

    private void OnButtonRestart()
    {
        SceneManager.LoadScene(0);
    }

}
