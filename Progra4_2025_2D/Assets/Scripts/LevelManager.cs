using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] ScoreManager scoreManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);

    }

    public void OnPlayerDie()
    {
        scoreManager.SaveDataToLeaderBoard(OnEndSave);
    }

    private void OnEndSave(string arg1, bool arg2)
    {

        if(true)
        {
            SceneManager.LoadScene("LeaderBoard");
        }
    }

    public void AddPoints(int amount)
    {
        scoreManager.AddPoints(amount);
    }
}
