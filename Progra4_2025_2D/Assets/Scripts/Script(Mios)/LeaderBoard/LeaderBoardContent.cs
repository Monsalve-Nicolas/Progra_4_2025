using UnityEngine;

public class LeaderBoardContent : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI text_Number;
    [SerializeField] TMPro.TextMeshProUGUI text_DisplayName;
    [SerializeField] TMPro.TextMeshProUGUI text_Points;

    public void SetLeaderBoardContent(LeaderBoardData leaderBoardData)
    {
        text_DisplayName.text = leaderBoardData.displayName;
        text_Number.text = (leaderBoardData.boardPos +1).ToString();
        text_Points.text = leaderBoardData.score.ToString();
    }
}
