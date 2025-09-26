using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats Base")]
    [SerializeField] float baseSpd;
    [SerializeField] float baseAccel;
    [SerializeField] float baseRotate;
    [SerializeField] float baseDefense;

    [Header("Stats Finales")]
    public float currentSpd;
    public float currentAccel;
    public float currentRotate;
    public float currentDefense;

    [Header("Piezas Equipadas")]
    public TankPart equiparMotor;
    public TankPart equiparRuedas;
    public TankPart equiparCañon;

    [Header("Evento")]
    public UnityEvent<Movement> OnStatsUpdate;
    private void Start()
    {
        CalcularStats();
    }
    public void CalcularStats()
    {
        //calculos
    }
    public void EquipTankPart(TankPart part)
    {
        switch (part.type)
        {
            case TankPart.Partes.Motor:
                equiparMotor = part;
                break;
            case TankPart.Partes.Ruedas:
                equiparRuedas = part;
                break;
            case TankPart.Partes.Cañon:
                equiparCañon = part;
                break;
        }
        CalcularStats();
    }
}
