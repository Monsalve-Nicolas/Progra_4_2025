using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectPart : MonoBehaviour
{
    [SerializeField] Image buttonImg;
    [SerializeField] TankPieceType pieceType;
    [SerializeField] string pieceID;
    public delegate void ButtonSelectPartDelegate(TankPieceType pieceType, string pieceID);
    ButtonSelectPartDelegate OnPartSelectEvent;

    public void SetButton(TankPieceType _pieceType, Sprite sprite, string pieceID)
    {
        pieceType = _pieceType;
        buttonImg.sprite = sprite;
        this.pieceID = pieceID;
    }

    public void AddListenerOnSelect(ButtonSelectPartDelegate onPartSelect)
    {
        OnPartSelectEvent += onPartSelect;
    }

    public void RemoveListenerOnSelect(ButtonSelectPartDelegate onPartSelect)
    {
        OnPartSelectEvent -= onPartSelect;
    }

    public void OnButtonPressed()
    {
        OnPartSelectEvent?.Invoke(pieceType, pieceID);
    }
}
