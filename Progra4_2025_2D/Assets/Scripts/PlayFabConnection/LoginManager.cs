using System;
using System.Collections.Generic;
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
    public string repeatPassword;
    public string displayName;
    public int score;
    public int lifePoints;

    [Header("Objetos")]
    [SerializeField] GameObject blockPanel;
    [SerializeField] GameObject[] panels;

    [Header("Textos")] 
    [SerializeField] TMPro.TextMeshProUGUI textFeedback;

    //[Header("Input Field")]
    //[SerializeField] TMP_InputField inputFieldMail;
    //[SerializeField] TMP_InputField inputFieldPassword;

    [Header("Listas")]
    public List<LeaderBoardData> leaderBoard;

    //private void Awake()
    //{
    //    inputFieldMail.onValueChanged.AddListener(OnChangeUser);
    //    inputFieldPassword.onValueChanged.AddListener(OnChangePass);
    //}
    private void Start()
    {
        SetPanel(LoginPanelType.Login);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            SavePJData();
            playfabLogin.AddDataToMaxScore(score, OnFinishAction);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            LoadPJData();
            LoadLeaderBoard();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
           
            playfabLogin.SetDisplayName(displayName,OnFinishAction);
        }
    }
    void LoadLeaderBoard()
    {
        playfabLogin.GetDataFromMaxScore(OnEndLoadLeaderBoard);
    }
    void OnEndLoadLeaderBoard(List<LeaderBoardData> data)
    {
        leaderBoard = data;
    }
    void SavePJData()
    {
        PJData pJData = new PJData()
        {
            score = score,
            lifePoints = lifePoints,
        };
        string json = JsonUtility.ToJson(pJData);
        SetBlockPanel("Saving data, not close the app", true);
        playfabLogin.SaveDataInfo(json, "PjInfo", OnFinishAction);
    }
    void LoadPJData()
    {
        SetBlockPanel("Loading data, not close the app", true);
        playfabLogin.LoadDataInfo("PjInfo", OnLoadData); 
    }
    void OnLoadData(string json,bool success)
    {
        if(success)
        {
            PJData pjData = JsonUtility.FromJson<PJData>(json);
            score = pjData.score;
            lifePoints = pjData.lifePoints;
            SetBlockPanel("Load Success", false);
        }
        else
        {
            SetBlockPanel("Sucedio un error en la carga de datos", true);
        }
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
        repeatPassword = val;
    }
    public void OnLoginButton()
    {
        SetBlockPanel("Loading...", true);
        playfabLogin.LoginUserName(user, password, OnFinishAction);
    }
    public void CreateAccountButton()
    {
        SetPanel(LoginPanelType.Register);
    }
    public void BackButton()
    {
        SetPanel(LoginPanelType.Login);
    }
    public void RecoveryButtonAccess()
    {
        SetPanel(LoginPanelType.Recovery);
    }
    public void CreateAccountCreateButton()
    {
        if(password == repeatPassword)
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
    public void RecoveryButton()
    {
        SetBlockPanel("Enviando correo de recuperacion..",true);
        playfabLogin.RecoveryAccount(mail,OnFinishAction);
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

[System.Serializable]

public class PJData
{
    public int score;
    public int lifePoints;
}

[System.Serializable]

public class LeaderBoardData
{
    public string displayName;
    public int score;
    public int boardPos;
}