using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PanelPartTypeSelection : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI textPieceTypeSelected;
    [SerializeField] List<ButtonSelectPartType> buttonsSelectPartTypes;
    public UnityEvent<TankPieceType> OnSelectPartTypeEvent;

    public void SetButtonSelectPartType(TankPieceType pieceType, Sprite newSprite)
    {
        buttonsSelectPartTypes.Find(x => x.pieceType == pieceType).SetTankPieceSelection(newSprite);
        foreach (var partType in buttonsSelectPartTypes)
        {
            if(partType.pieceType == pieceType)
            {
                partType.transform.localScale = Vector3.one * 1.1f;
                partType.SetTankPieceSelection(newSprite);
            }
            else partType.transform.localScale = Vector3.one;
        }
    }

    public void OnButtonSelectPartType(TankPieceType pieceType)
    {
        textPieceTypeSelected.text = pieceType.ToString();
        foreach (var partType in buttonsSelectPartTypes)
        {
            if (partType.pieceType == pieceType)
            {
                partType.transform.localScale = Vector3.one * 1.5f;
            }
            else partType.transform.localScale = Vector3.one;
        }
        OnSelectPartTypeEvent?.Invoke(pieceType);
    }
}
