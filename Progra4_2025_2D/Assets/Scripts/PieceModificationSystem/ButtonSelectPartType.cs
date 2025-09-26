using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonSelectPartType : MonoBehaviour
{
    [SerializeField] Image buttonImg;
    public TankPieceType pieceType;
    public UnityEvent<TankPieceType> SelectedPieceType;

    public void SetTankPieceSelection(Sprite sprite)
    {
        buttonImg.sprite = sprite;
    }

    public void OnPartTypeSelected()
    {
        SelectedPieceType?.Invoke(pieceType);
    }
}
