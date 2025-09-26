using UnityEngine;
using UnityEngine.Events;

public class ColorPicker : MonoBehaviour
{
    public GameObject panel;
    public FlexibleColorPicker flexibleColorPicker;
    public UnityEvent<Color> OnColorChangeEvent;

    public void OnColorChange(Color color)
    {
        OnColorChangeEvent.Invoke(color);
    }

    public void SetStartColor(Color color)
    {
        flexibleColorPicker.SetColor(color);
    }

    public void EnablePanel(bool isEnable)
    {
        panel.SetActive(isEnable);
    }

}
