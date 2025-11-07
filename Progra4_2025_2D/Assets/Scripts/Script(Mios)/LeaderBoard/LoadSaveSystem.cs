using System;
using Unity.VisualScripting;
using UnityEngine;

public class LoadSaveSystem
{
    string playerInfoDataKey = "PlayerInfo";

    public void LoadPlayerInfo(Action<PlayerDataInfo> onEndLoadData)
    {
        //Obtener el json desde playerprefs
        string json = PlayerPrefs.GetString(playerInfoDataKey);
        //convierte el json a Datasave
        PlayerDataInfo loadData = JsonUtility.FromJson<PlayerDataInfo>(json);
        PlayFabManager playFabManager = new PlayFabManager();
        playFabManager.LoadDataInfo(playerInfoDataKey, (data, Result) =>
        {
            if(Result == true)
            {
                json = data;
                PlayerDataInfo loadData = JsonUtility.FromJson<PlayerDataInfo>(json);
                onEndLoadData(loadData);
                Debug.Log("Load Success");
            }
        });

    }
    public void SavePlayerInfo(PlayerDataInfo dataToSave, Action<string,bool> OnFinishSave)
    {
        //Lo primero q hacemos es convertir la clase a txto (JSON)
        string json = JsonUtility.ToJson(dataToSave);
        //Ya q tenemos un texto guardamos el archivo
        //con parametros Key,Json
        PlayerPrefs.SetString(playerInfoDataKey, json);
        PlayFabManager playFabManager = new PlayFabManager();
        playFabManager.SaveDataInfo(json, playerInfoDataKey, OnFinishSave);
        Debug.Log("Save Success");
    }
}
