using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField] LeaderBoardContent[] leaderBoardContents;


    void SetContent(List<LeaderBoardData> leaderBoardDataList)
    {
        for (int i = 0; i < leaderBoardContents.Length; i++)
        {
            if(i < leaderBoardDataList.Count)
            {
                leaderBoardContents[i].gameObject.SetActive(true);
                leaderBoardContents[i].SetLeaderBoardContent(leaderBoardDataList[i]);
            }
            else leaderBoardContents[i].gameObject.SetActive(false);
        }
    }
}
