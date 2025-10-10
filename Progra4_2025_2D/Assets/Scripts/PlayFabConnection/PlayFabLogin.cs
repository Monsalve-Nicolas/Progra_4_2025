using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayFabLogin : MonoBehaviour
{
    private Action<string,bool> OnFinishActionEvent;
    private Action<string, bool> OnFinishLoadEvent;
    private Action<List<LeaderBoardData>> leaderBoardEvent;



    public void SaveDataInfo(string data,string datakey,Action<string,bool> OnFinishLoad)
    {
         OnFinishLoadEvent = OnFinishLoad;
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {datakey, data},
            },
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSave, OnError);
    }
    public void LoadDataInfo(string datakey, Action<string,bool> OnFinishLoad)
    {
        OnFinishLoadEvent = OnFinishLoad;
        var request = new GetUserDataRequest();
        PlayFabClientAPI.GetUserData(request, result =>
        {
            if (result.Data != null && result.Data.ContainsKey(datakey))
            {
                string data = result.Data[datakey].Value;
                OnFinishLoadEvent?.Invoke(data, true);
            }
            else
            {
                Debug.Log("Not Key Found");
                OnFinishLoadEvent?.Invoke(default, false);
            }
        }, OnError);
    }
    public void OnDataSave(UpdateUserDataResult result)
    {
        Debug.Log("Success");
        OnFinishLoadEvent?.Invoke("Success", true);
        OnFinishLoadEvent = null;
    }

    void OnLoginSuccess(LoginResult result)
    {
        OnFinishActionEvent?.Invoke("Success", true);
        Debug.Log("Exelente!, haz hecho tu primera llamada API exisotasa");
    }
    void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Algo salio mal con tu primera llamada de API. :(");
        Debug.LogError("Aqui hay informacion del Debug");
        Debug.LogError(error.GenerateErrorReport());
        OnFinishActionEvent?.Invoke(error.GenerateErrorReport(),false);
    }
    public void LoginUser(string mail, string password, Action<string,bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        var request = new LoginWithEmailAddressRequest
        {
            Email = mail,
            Password = password,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginResult, OnError);
    }
    public void LoginUserName(string userName, string password, Action<string, bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        var request = new LoginWithPlayFabRequest
        {
            Username = userName,
            Password = password
        };

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnError);
    }
    void OnLoginResult(LoginResult result)
    {
        OnFinishActionEvent?.Invoke("Success",true);
        Debug.Log("Login Success");
    }

    public void RegisterUser(string userName,string mail, string password, Action<string,bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        var request = new RegisterPlayFabUserRequest
        {
            Username = userName,
            Email = mail,
            Password = password,           
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterUserResult, OnError);
    }
    public void RecoveryAccount(string email,Action<string,bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = email,
            TitleId = PlayFabSettings.staticSettings.TitleId,
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRequestSuccess,OnError);
    }
    public void AddDataToMaxScore(int score, Action<string,bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "MaxScore", // nombre de tu leaderboard
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnStatisticsResult, OnError);
    }
    public void GetDataFromMaxScore(Action<List<LeaderBoardData>> leaderBoard)
    {
        leaderBoardEvent = leaderBoard;
        var request = new GetLeaderboardRequest
        {
            StatisticName = "MaxScore", // nombre de tu leaderboard
            StartPosition = 0,             // posición inicial (0 = primer lugar)
            MaxResultsCount = 10           // cantidad máxima de resultados
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardLoad, OnError);
    }
    private void OnLeaderBoardLoad(GetLeaderboardResult result)
    {
        List<LeaderBoardData> dataList = new List<LeaderBoardData>();
        foreach (var item in result.Leaderboard)
        {
            LeaderBoardData newData = new LeaderBoardData()
            {
                displayName = item.DisplayName,
                score = item.StatValue,
                boardPos = item.Position
            };
            dataList.Add(newData);
        }
        leaderBoardEvent?.Invoke(dataList);
    }

    public void SetDisplayName(string displayName, Action<string, bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = displayName,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnEndRequestDisplayName, OnError);
    }

    private void OnEndRequestDisplayName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Success");
        OnFinishLoadEvent?.Invoke("Success", true);
        OnFinishLoadEvent = null;
    }

    private void OnStatisticsResult(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Success");
        OnFinishLoadEvent?.Invoke("Success", true);
        OnFinishLoadEvent = null;
    }
    private void OnRequestSuccess(SendAccountRecoveryEmailResult result)
    {
        Debug.Log("Correo de recuperacion envaido");
        OnFinishActionEvent?.Invoke("Correo de recuperacion envaido", true);
    }

    void OnError(PlayFabError error)
    {
        OnFinishActionEvent?.Invoke(error.GenerateErrorReport(), false);
        Debug.LogError(error.GenerateErrorReport());
    }
    void OnRegisterUserResult(RegisterPlayFabUserResult result)
    {
        OnFinishActionEvent?.Invoke("Success",true);
        Debug.Log("Tu usuario ha sido creado");
    }
    public void LoginAnonimo(Action<string,bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "68515";
        }
        var request = new LoginWithCustomIDRequest
        {
            CustomId = "NicolasMonsalve",
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }
}
