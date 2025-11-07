using UnityEngine;

public class LoadResources
{
    public TankPieceScriptable GetTankPieceScriptable(TankPieceType type,string id)
    {
        //el path es desde la carpeta Resource/nombreCarpetaEnResources/NombreArchivo
        string path = type.ToString() + "/" + id;
        Debug.Log(path);
        //aca estoy cargando el archivo desde la carpeta resources
        TankPieceScriptable piece = Resources.Load<TankPieceScriptable>(path);
        return piece;
    }
}
