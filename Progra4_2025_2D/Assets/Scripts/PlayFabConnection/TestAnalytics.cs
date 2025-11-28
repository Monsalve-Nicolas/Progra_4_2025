using UnityEngine;

public class TestAnalytics : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            AnalyticsManager.Instance.SaveMyFirstCustomEvent(Random.Range(0f,1f));
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            AnalyticsManager.Instance.SaveMySecondEvent(Random.Range(0,2) == 0,"Wiiiii",Random.Range(1,10));
        }
    }
}
