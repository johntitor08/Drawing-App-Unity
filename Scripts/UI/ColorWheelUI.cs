using UnityEngine;
using UnityEngine.UI;

public class ColorWheelUI : MonoBehaviour
{
    [Header("Preview")]
    public Image colorPreview;

    [Header("HSV Sliders")]
    public Slider hueSlider;
    public Slider satSlider;
    public Slider valSlider;

    [Header("Hex Input")]
    public TMPro.TMP_InputField hexInput;

    void Start()
    {
        hueSlider?.onValueChanged.AddListener(_ => OnHSVChanged());
        satSlider?.onValueChanged.AddListener(_ => OnHSVChanged());
        valSlider?.onValueChanged.AddListener(_ => OnHSVChanged());
        hexInput?.onEndEdit.AddListener(OnHexInput);
        SyncFromBrush();
    }

    void OnHSVChanged()
    {
        float h = hueSlider ? hueSlider.value : 0f;
        float s = satSlider ? satSlider.value : 1f;
        float v = valSlider ? valSlider.value : 1f;
        var color = Color.HSVToRGB(h, s, v);
        BrushSettings.Instance.color = color;
        colorPreview?.color = color;
        hexInput?.text = ColorUtility.ToHtmlStringRGB(color);
    }

    void OnHexInput(string hex)
    {
        if (ColorUtility.TryParseHtmlString("#" + hex, out var color))
        {
            BrushSettings.Instance.color = color;
            colorPreview?.color = color;
            Color.RGBToHSV(color, out float h, out float s, out float v);
            hueSlider?.value = h;
            satSlider?.value = s;
            valSlider?.value = v;
        }
    }

    void SyncFromBrush()
    {
        var c = BrushSettings.Instance.color;
        Color.RGBToHSV(c, out float h, out float s, out float v);
        hueSlider?.value = h;
        satSlider?.value = s;
        valSlider?.value = v;
        colorPreview?.color = c;
    }
}
