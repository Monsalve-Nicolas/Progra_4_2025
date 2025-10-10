using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayFabLogin : MonoBehaviour
{
    private Action<string,bool> OnFinishActionEvent;

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
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRequestSuccess,OnError);
    }

    private void OnRequestSuccess(SendAccountRecoveryEmailResult result)
    {
        
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
            PlayFabSettings.staticSettings.TitleId = "146940";
        }
        var request = new LoginWithCustomIDRequest
        {
            CustomId = "NicolasMonsalve",
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }
}
