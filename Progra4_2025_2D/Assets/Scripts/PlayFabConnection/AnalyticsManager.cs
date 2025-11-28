using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.EventsModels;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Rendering;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance;
    [HideInInspector] public bool isInitialized = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        /*
         if(Instance == null) Instance = this
         else Destoy(gameObject)   
         */
    }
    //siempre que queramos usar task necesitamos crear o usar funciones asincronas
    async void Start()
    {
        //para poder llamar una task y detenr el proceso usaremos la palabra clave await
        await UnityServices.InitializeAsync(); // en este caso InitializeAsync es una task de unity para conectar en modo anonimo
        AnalyticsService.Instance.StartDataCollection();
        isInitialized = true;
        //y aca ya estamos conectados wiiiiiiiiiii
    }
    // Update is called once per frame

    public void SaveMyFirstCustomEvent(float MFCE_LindoFloat)
    {
        if (isInitialized)
        {
            
            MyFirstCustomEvent myFirstCustomEvent = new MyFirstCustomEvent()
            {
                MFCE_LindoFloat = MFCE_LindoFloat,
                
            };

            AnalyticsService.Instance.RecordEvent(myFirstCustomEvent);
            Debug.Log("Hola,Mi primera vez");
        }
    }
    public void SaveMySecondEvent(bool MSE_LindoBool, string MSE_LindoString, int MSE_LindoInt)
    {
        if (isInitialized)
        {
            
            MySecondEvent mySecondEvent = new MySecondEvent()
            {
                MSE_LindoBool = MSE_LindoBool,
                MSE_LindoInt = MSE_LindoInt,
                MSE_LindoString = MSE_LindoString,
            };
            AnalyticsService.Instance.RecordEvent(mySecondEvent);
            AnalyticsService.Instance.Flush();//esto manda el evento altiro
            Debug.Log("Hola,Mi segunda vez");
        }
    }
    // -----------------------------
    //   ATAJOS PARA EVENTOS COMUNES
    // -----------------------------

    public void EnemyAllDie(int enemiesCount, float time)
    {
        if (isInitialized)
        {
            EnemyAllDieEvent enemyAllDieEvent = new EnemyAllDieEvent()
            {
                EAD_enemiesCount = enemiesCount,
                EAD_time = time
            };
            AnalyticsService.Instance.RecordEvent(enemyAllDieEvent);
        }
       
    }
    //public void GameDefeat(string reason,float time)
    //{
    //    Dictionary<string, object> data = new Dictionary<string, object>();
    //    data.Add("time", time);
    //    data.Add("reason", reason);
    //    LogEvent("timeInLevel", data, "gameplay");
    //}
    public void ScoreAfterDie(int score)
    {
        if (isInitialized)
        {
            ScoreAfterDieEvent scoreAfterDieEvent = new ScoreAfterDieEvent()
            {
                SAD_score = score
            };
            AnalyticsService.Instance.RecordEvent(scoreAfterDieEvent);
            AnalyticsService.Instance.Flush();
        }
    }
    public void UsedRoute()
    {

    }
    public void MostUsedPiece(List<TankPieceScriptable> piecesList)
    {
        foreach (var item in piecesList)
        {
            string id = item.id;

            MostUsedPieceEvent mostUsedPieceEvent = new MostUsedPieceEvent()
            {
                MUP_stringID = id,
                MUP_stringTipoPieza = item.pieceType.ToString(),
            };
            AnalyticsService.Instance.RecordEvent(mostUsedPieceEvent);
        }
    }
    public void CrashTheGame(bool isCrash)
    {
        if (isInitialized)
        {
            CrashTheGameEvent crashTheGameEvent = new CrashTheGameEvent()
            {
                CTG_crasheo = isCrash
            };
            AnalyticsService.Instance.RecordEvent(crashTheGameEvent);
            AnalyticsService.Instance.Flush();
        }
    }

}
