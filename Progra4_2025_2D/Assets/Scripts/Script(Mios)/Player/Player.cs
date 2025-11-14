using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IHealth
{
    [Header("Input System")]
    [SerializeField] InputActionAsset action;
    private InputAction saveAction;
    private InputAction loadAction;

    [Header("Referencias")]
    [SerializeField] public Movement movement;
    [SerializeField] public Turret turret;
    [SerializeField] public AttackTurret attackTurret;

    [Header("Sprite")]
    [SerializeField] TankSpriteModifier spriteModifier;

    [Header("Listas")]
    public List<StatInfo> currentStats = new List<StatInfo>();

    [Header("Current Piece")]
    public TankPieceScriptable[] piecesArr = new TankPieceScriptable[7];
    public ColorPicker colorPicker;

    [Header("Type")]
    StatType[] statTypesArr = new StatType[6];
    TankPieceType[] tankType = new TankPieceType[6];

    [Header("Variables")]
    public Color piece_LightColor;
    public string playerName;
    public int currentDmg;
    public int score;

    [Header("Player Name")]
    //[SerializeField] TMP_InputField inputField;
    public TMPro.TextMeshProUGUI textPlayerName;

    [Header("Vida")]
    public int maxHealth = 100;
    int currentHealth;
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
        saveAction = InputSystem.actions.FindAction("Save Button");
        loadAction = InputSystem.actions.FindAction("Load Button");

        //inputField.onValueChanged.AddListener(OnChangeName);
    }
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateControllerWithTankPiece();
        LoadData();
    }

    private void Update()
    {
        if (saveAction.WasPerformedThisFrame())
        {
            SaveData();
        }
        if (loadAction.WasPerformedThisFrame())
        {
            LoadData();
        }
    }
    public void OnChangeName(string name)
    {
        playerName = name;
        textPlayerName.text = name;
    }
    public void OnTankPieceChange(TankPieceScriptable tankPiece)
    {
        piecesArr[(int)tankPiece.pieceType] = tankPiece;

        Debug.Log("Pieza modificada = " + tankPiece.pieceType);
        Debug.Log("El ID = " + tankPiece.id);
        UpdateControllerWithTankPiece();
    }
    public void OnChangeColorType(Color color)
    {
        piece_LightColor = color;
    }
    public void UpdateControllerWithTankPiece()
    {
        List<StatInfo> statsInfo = new List<StatInfo>();

        foreach (var piece in piecesArr)
        {
            if(piece != null)
            {
                foreach (var item in piece.statInfo)
                {
                    Debug.Log("YEyyyyyy");
                    StatInfo currentStats = statsInfo.Find(x => x.type == item.type);
                    if (currentStats != null)
                    {
                        currentStats.value = item.value;
                    }
                    else
                    {
                        StatInfo newStats = new StatInfo();
                        newStats.value = item.value;
                        newStats.type = item.type;
                        statsInfo.Add(newStats);
                    }
                }
            }

        }
        currentStats = statsInfo;
        UpdateControllers();
    }
    public void UpdateControllers()
    {
        foreach (var stats in currentStats)
        {
            switch (stats.type)
            {
                case StatType.Spd:
                    movement.moveSpd = stats.value;
                    break;
                case StatType.RootSpd:
                    movement.rotateSpd = stats.value;
                    turret.rotateSpd = stats.value;
                    break;
                case StatType.Attack:
                    attackTurret.attackForce = stats.value;
                    break;
                case StatType.Defense:
                    break;
                case StatType.Life:
                    break;
                case StatType.BulletSpd:
                    break;
                default:
                    break;
            }
        }

    }
    public void LoadData()
    {
        //inicializando el load save
        LoadSaveSystem loadSave = new LoadSaveSystem();
        //obtengo la data
        loadSave.LoadPlayerInfo(OnEndLoadData);

        
    }

    void OnEndLoadData(PlayerDataInfo playerData)
    {
        //actualizo player con data obtenida
        playerName = playerData.playerName;
        currentDmg = playerData.currentDmg;
        score = playerData.score;

        //inicializo la carga de resource
        LoadResources loadResource = new LoadResources();

        //color
        piece_LightColor = playerData.colorTank;
        spriteModifier.ChangeLightColor(piece_LightColor);

        //cambiar nombre
        OnChangeName(playerData.playerName);
        //para cada pieza, estoy cargando el scriptable desde resource, segun tipo e ID
        for (int i = 1; i < piecesArr.Length; i++)
        {
            TankPieceType type = (TankPieceType)i;
            string pieceName = playerData.piecesNames[i - 1];
            Debug.Log("type  = " + type + "/id = " + pieceName);
            piecesArr[i] = loadResource.GetTankPieceScriptable(type, pieceName);
            Debug.Log(piecesArr[i]);
            spriteModifier.ChangeSprite(type, piecesArr[i].pieceSprite);
        }
        UpdateControllerWithTankPiece();
    }

    public void SaveData()
    {
        PlayerDataInfo playerData = new PlayerDataInfo();
        TankPieceScriptable pieceS = new TankPieceScriptable();
        playerData.piecesNames = new List<string>();
        playerData.playerName = playerName;
        playerData.currentDmg = currentDmg;
        playerData.colorTank = piece_LightColor;
        playerData.score = score;
        for (int i = 1; i < piecesArr.Length; i++)
        {
            playerData.piecesNames.Add(piecesArr[i].id);
        }
        LoadSaveSystem loadSave = new LoadSaveSystem();
        loadSave.SavePlayerInfo(playerData,OnEndSave);
    }

    private void OnEndSave(string arg1, bool arg2)
    {
       
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Debug.Log("Player Murio");
            Die();
        }
    }
    public void Die()
    {
        LevelManager.Instance.OnPlayerDie();
    }
}
