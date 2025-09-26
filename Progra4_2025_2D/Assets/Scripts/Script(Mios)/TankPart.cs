using UnityEngine;

[CreateAssetMenu(fileName = "TankPart", menuName = "Scriptable Objects/TankPart")]
public class TankPart : ScriptableObject
{
    public enum Partes { Motor, Ruedas, Cañon }
    public Partes type;
    //modificadores 
    public float modificadorSpd;
    public float modificadorAccel;
    public float modificadorRotate;
    public float modificadorDefense;
    
}


