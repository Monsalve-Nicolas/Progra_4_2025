using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] Tiempo tiempo;
    [SerializeField] Player player;
    [SerializeField] TextMeshProUGUI textEnemyCount;
    public int enemiesCount;

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
        AnalyticsManager.Instance.ScoreAfterDie(scoreManager.score);
        AnalyticsManager.Instance.PlayerDiePos(tiempo.currentTimer,player.transform.position);
        AnalyticsManager.Instance.EnemyAllDie(enemiesCount,tiempo.currentTimer);
        Debug.Log(tiempo.currentTimer);
    }
    public void UpdateEnemyCount()
    {
        textEnemyCount.text = enemiesCount.ToString("000");
    }
    public void OnEnemyDie()
    {
        enemiesCount--;
        UpdateEnemyCount();
        if(enemiesCount <= 0)
        {
            OnPlayerDie();
        }
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
