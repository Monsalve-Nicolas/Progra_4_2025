using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [Header("Clases referentes")]
    [SerializeField] PlayFabLogin playfabLogin;

    [Header("Variables")]
    public string mail;
    public string password;

    [Header("Objetos")]
    [SerializeField] GameObject blockPanel;

    [Header("Textos")]
    [SerializeField] TMPro.TextMeshProUGUI textFeedback;

    [Header("Input Field")]
    [SerializeField] InputField inputField;

    private void Awake()
    {
        inputField = GetComponent<InputField>();    
    }
    private void Start()
    {
        playfabLogin.LoginUser(mail, password,OnFinishAction);
    }
    void SetBlockPanel(string message, bool enable)
    {
        textFeedback.text = message;
        blockPanel.SetActive(enable);
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
}
