using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [Header("Clases referentes")]
    [SerializeField] PlayFabLogin playfabLogin;

    [Header("Variables")]
    public string user;
    public string mail;
    public string password;
    public string repetirPassword;

    [Header("Objetos")]
    [SerializeField] GameObject blockPanel;
    [SerializeField] GameObject[] panels;

    [Header("Textos")] 
    [SerializeField] TMPro.TextMeshProUGUI textFeedback;

    [Header("Input Field")]
    [SerializeField] TMP_InputField inputFieldMail;
    [SerializeField] TMP_InputField inputFieldPassword;


    private void Awake()
    {
        inputFieldMail.onValueChanged.AddListener(OnChangeUser);
        inputFieldPassword.onValueChanged.AddListener(OnChangePass);
    }
    private void Start()
    {
        SetPanel(LoginPanelType.Login);
        playfabLogin.LoginUser(mail, password,OnFinishAction);
    }
    void SetBlockPanel(string message, bool enable)
    {
        
        textFeedback.text = message;
        blockPanel.SetActive(enable);
    }
    public void OnChangeUser(string val)
    {
        user = val;
    }
    public void OnChangeEmail(string val)
    {
        mail = val;
    }
    public void OnChangePass(string val)
    {
        password = val;
    }
    public void OnChangeRepeatPassword(string val)
    {
        repetirPassword = val;
    }
    public void OnLoginButton()
    {
        SetBlockPanel("Loading...", true);
        playfabLogin.LoginUser(user, password, OnFinishAction);
    }
    public void CreateAccountButton()
    {
        SetPanel(LoginPanelType.Register);
    }
    public void CreateAccountCreateButton()
    {
        if(password == repetirPassword)
        {
            SetBlockPanel("Creating...", true);
            playfabLogin.LoginAnonimo(null);
            playfabLogin.RegisterUser(user, mail, password, OnFinishAction);
        }
        else
        {
            Debug.Log("Las claves deben coincidir");
        }
        
    }
    void OnFinishAction(string message, bool result)
    {
        if(result == true)
        {
            SetBlockPanel(message, false);
        }
        else
        {
            SetBlockPanel(message, true);
        }
    }
    void SetPanel(LoginPanelType panelType)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if(i == (int)panelType)
            {
                panels[i].SetActive(true);
            }
            else panels[i].SetActive(false);
        }
    }
}
public enum LoginPanelType
{
    Login,
    Register,
    Recovery
}
