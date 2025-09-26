using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PanelPieceSelection : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] List<ButtonSelectPart> button_SelectPart;
    public UnityEvent<TankPieceType, string> ChangePieceEvent;

    private void Awake()
    {
        foreach (var item in button_SelectPart)
        {
            item.AddListenerOnSelect(OnPartSelected);
        }
    }

    private void OnDestroy()
    {
        foreach (var item in button_SelectPart)
        {
            item.RemoveListenerOnSelect(OnPartSelected);
        }
    }

    public void EnablePanel(bool isEnable)
    {
        panel.SetActive(isEnable);
    }

    public void SetPanelSelection(List<TankPieceScriptable> pieces)
    {
        for (int i = 0; i < button_SelectPart.Count; i++)
        {
            ButtonSelectPart button = button_SelectPart[i];
            if(i < pieces.Count)
            {
                button.gameObject.SetActive(true);
                TankPieceScriptable tankPieceScriptable = pieces[i];
                button.SetButton(tankPieceScriptable.pieceType, tankPieceScriptable.pieceSprite, tankPieceScriptable.id);
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }
    }


    public void OnPartSelected(TankPieceType type, string pieceID)
    {
        ChangePieceEvent?.Invoke(type, pieceID);
    }

    
}
