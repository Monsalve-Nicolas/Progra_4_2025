using UnityEngine;

public class LoadSaveSystem
{
    string playerInfoDataKey = "PlayerInfo";

    public PlayerDataInfo LoadPlayerInfo()
    {
        //Obtener el json desde playerprefs
        string json = PlayerPrefs.GetString(playerInfoDataKey);
        //convierte el json a Datasave
        PlayerDataInfo loadData = JsonUtility.FromJson<PlayerDataInfo>(json);
        Debug.Log("Load Success");
        return loadData;
    }
    public void SavePlayerInfo(PlayerDataInfo dataToSave)
    {
        //Lo primero q hacemos es convertir la clase a txto (JSON)
        string json = JsonUtility.ToJson(dataToSave);
        //Ya q tenemos un texto guardamos el archivo
        //con parametros Key,Json
        PlayerPrefs.SetString(playerInfoDataKey, json);
        Debug.Log("Save Success");
    }
}
