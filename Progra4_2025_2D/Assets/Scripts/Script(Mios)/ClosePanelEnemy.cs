using UnityEngine;
using UnityEngine.InputSystem;

public class ClosePanelEnemy : MonoBehaviour
{
    [Header("Input System")]
    [SerializeField] InputActionAsset action;
    private InputAction puaseAction;

    [Header("Componentes Objetos")]
    public GameObject panelSystem;
    public GameObject panelButton;
    public Enemy enemy;

    private void OnEnable()
    {
        action.FindActionMap("UI").Enable();

    }
    private void OnDisable()
    {
        action.FindActionMap("UI").Disable();
    }
    private void Awake()
    {
        puaseAction = InputSystem.actions.FindAction("Puase");
    }
    //Update is called once per frame
    void Update()
    {
        if (puaseAction.WasPerformedThisFrame())
        {
            Close_Open_Panel();
        }

    }
    public void Close_Open_Panel()
    {
        bool panelState = !panelSystem.activeSelf;
        panelSystem.SetActive(panelState);

        enemy.enabled = !panelState;
        //player.turret.enabled = !panelState;
        //player.attackTurret.enabled = !panelState;
        if (panelState == true)
        {
            panelButton.SetActive(false);
        }
        else
        {
            panelButton.SetActive(true);
        }
    }

}
