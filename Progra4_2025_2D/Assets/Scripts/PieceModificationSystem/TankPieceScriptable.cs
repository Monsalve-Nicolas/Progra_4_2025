using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TPiece_", menuName = "Scriptable Objects/TankPieceScriptable")]
public class TankPieceScriptable : ScriptableObject
{
    public string id;
    public TankPieceType pieceType;
    public Sprite pieceSprite;
    public List<StatInfo> statInfo;
}
