using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tiempo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float currentTimer;
    int minutos, segundos;

    private void Start()
    {
        StartCoroutine(StartTimer());
    }
    private void Update()
    {
        if(currentTimer <= 0)
        {
            LevelManager.Instance.OnPlayerDie();
        }
    }

    IEnumerator StartTimer()
    {
        while (true)
        {
            currentTimer -= Time.deltaTime;
            minutos = (int)(currentTimer / 60f);
            segundos = (int)(currentTimer - minutos * 60f);
            timerText.text = string.Format("{0:00}:{1:00}",minutos,segundos);
            yield return null;
        }
    }
}
