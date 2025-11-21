using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.EventsModels;
using UnityEngine;
using UnityEngine.Rendering;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance;

    //Tamaño maximo antes de enviar telemetria
    [SerializeField] int maxBufferSize = 10;

    //Tiempo entre autosend
    [SerializeField] float autoFlushInterval = 10f;
    
    List<EventContents> buffer = new List<EventContents>();
    float timer = 0f;
    bool isFlushing = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (isFlushing) return;
        timer += Time.deltaTime;
        if (timer >= autoFlushInterval)
        {
            Flush();
        }
    }

    // -----------------------------
    //   API PÚBLICA PARA ENVIAR EVENTOS
    // -----------------------------

    public void LogEvent(string eventName, Dictionary<string,object> data = null, string eventNameSpace = "custom")
    {
        if(data == null) data = new Dictionary<string,object>();

        var evt = new EventContents
        {
            Name = eventName,
            EventNamespace = "com.playfab.events.custom",
            Payload = data,
            OriginalTimestamp = DateTime.UtcNow
        };
        buffer.Add(evt);
        if (buffer.Count > maxBufferSize) Flush();
    }

    // -----------------------------
    //   FLUSH (ENVÍA LOS EVENTOS A PLAYFAB)
    // -----------------------------

    public void Flush()
    {
        if(buffer.Count == 0) return;

        isFlushing = true;
        var request = new WriteEventsRequest
        {
            Events = new List<EventContents>(buffer)
        };
        PlayFabEventsAPI.WriteEvents(request,
            result =>
            {
                isFlushing = false;
                buffer.Clear();
                timer = 0f;
                Debug.Log($"Telemetry enviado({result.AssignedEventIds.Count}eventos)");
            },
            error =>
            {
                isFlushing = false;
                Debug.LogWarning("Error enviado Telemetry, se reintentara automaticamente. " + error.ErrorMessage);
                // No limpiamos el buffer, se reenvía en el próximo Flush
            }
        );

    }

    // -----------------------------
    //   ATAJOS PARA EVENTOS COMUNES
    // -----------------------------

    public void EnemyAllDie(int enemiesCount, float time)
    {
        Dictionary<string,object> data = new Dictionary<string,object>();
        data.Add("enemiesDeath", enemiesCount);
        data.Add("time", time);
        LogEvent("enemyAllDie", data, "gameplay");
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
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("Maxpoint", score);
        LogEvent("score", data, "gameplay");

    }
    public void UsedRoute()
    {

    }
    public void MostUsedPiece(List<TankPieceScriptable> piecesList)
    {
        List<string> namePieces = new List<string>();
        foreach (var item in piecesList)
        {
            namePieces.Add(item.id);
        }
        LogEvent("mostUsedPiece", new Dictionary<string, object> { { "namepiece", namePieces } }, "gameplay");
    }
    public void CrashTheGame(bool isCrash)
    {
        LogEvent("crashGame", new Dictionary<string, object> { { "crashTheGame", isCrash } }, "gameplay");
    }

}
