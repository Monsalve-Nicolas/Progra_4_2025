using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField] LeaderBoardContent[] leaderBoardContents;

    private void Start()
    {
        //score = UnityEngine.Random.Range(0, 10000);
        StartCoroutine(LoadLeaderBoardCorrutine());
        
    }
    IEnumerator LoadLeaderBoardCorrutine()
    {
        yield return new WaitForSeconds(2);
        LoadleaderBoard();
    }
    //desde aqui esto va cuando el jugador termina el nivel o gameover
   

    //private void OnEndSaveScore(string msg, bool result)//esto va en mi sisteme de gameover
    //{
    //    Debug.Log("End save");
    //    LoadleaderBoard();
    //    //cuando termina de guardar cambiamos la escena a LeaderBoard

    //}
    //hasta aca esto va cuando el juegador termina el nivel o gameover
    void LoadleaderBoard()//esto se queda aca
    {
        Debug.Log("Load");
        PlayFabManager playFabManager = new PlayFabManager();
        playFabManager.GetDataFromMaxScore(SetContent);
    }

    void SetContent(List<LeaderBoardData> leaderBoardDataList)
    {
        Debug.Log("Set");
        for (int i = 0; i < leaderBoardContents.Length; i++)
        {
            if(i < leaderBoardDataList.Count)
            {
                Debug.Log("Miawwwww");
                leaderBoardContents[i].gameObject.SetActive(true);
                leaderBoardContents[i].SetLeaderBoardContent(leaderBoardDataList[i]);
            }
            else leaderBoardContents[i].gameObject.SetActive(false);
        }
    }
}
