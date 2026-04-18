using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Tool Buttons")]
    public Button brushBtn;
    public Button eraserBtn;
    public Button fillBtn;
    public Button eyedropperBtn;

    [Header("Shape Buttons")]
    public Button lineBtn;
    public Button rectBtn;
    public Button circleBtn;

    [Header("Brush Controls")]
    public Slider sizeSlider;
    public Slider hardnessSlider;
    public Slider opacitySlider;
    public TextMeshProUGUI sizeLabel;

    [Header("Color")]
    public Image colorPreview;
    public Button[] paletteButtons;

    [Header("Layer")]
    public Button addLayerBtn;
    public Button removeLayerBtn;
    public Button mergeDownBtn;

    [Header("File")]
    public Button savePNGBtn;
    public Button saveNativeBtn;
    public Button undoBtn;
    public Button redoBtn;
    public Button clearBtn;

    void Start()
    {
        var bs = BrushSettings.Instance;
        brushBtn?.onClick.AddListener(() => SetTool(ToolType.Brush));
        eraserBtn?.onClick.AddListener(() => SetTool(ToolType.Eraser));
        fillBtn?.onClick.AddListener(() => SetTool(ToolType.Fill));
        eyedropperBtn?.onClick.AddListener(() => SetTool(ToolType.Eyedropper));
        lineBtn?.onClick.AddListener(() => { SetTool(ToolType.Shape); bs.activeShape = ShapeType.Line; });
        rectBtn?.onClick.AddListener(() => { SetTool(ToolType.Shape); bs.activeShape = ShapeType.Rectangle; });
        circleBtn?.onClick.AddListener(() => { SetTool(ToolType.Shape); bs.activeShape = ShapeType.Circle; });

        sizeSlider?.onValueChanged.AddListener(v =>
        {
            bs.size = (int)v;
            sizeLabel?.text = $"{(int)v}px";
        });

        hardnessSlider?.onValueChanged.AddListener(v => bs.hardness = v);
        opacitySlider?.onValueChanged.AddListener(v => bs.opacity = v);

        foreach (var btn in paletteButtons)
        {
            if (btn == null)
                continue;

            var col = btn.GetComponent<Image>().color;

            btn.onClick.AddListener(() =>
            {
                bs.color = col;
                colorPreview?.color = col;
                SetTool(ToolType.Brush);
            });
        }

        addLayerBtn?.onClick.AddListener(() => LayerManager.Instance.AddLayer());
        removeLayerBtn?.onClick.AddListener(() => LayerManager.Instance.RemoveLayer(LayerManager.Instance.ActiveIndex));
        mergeDownBtn?.onClick.AddListener(() => LayerManager.Instance.MergeDown(LayerManager.Instance.ActiveIndex));
        savePNGBtn?.onClick.AddListener(() => { SaveManager.Instance.SavePNG(); AudioManager.Instance?.PlaySave(); });
        saveNativeBtn?.onClick.AddListener(() => { SaveManager.Instance.SaveNative(); AudioManager.Instance?.PlaySave(); });
        undoBtn?.onClick.AddListener(() => DrawingCanvas.Instance.Undo());
        redoBtn?.onClick.AddListener(() => DrawingCanvas.Instance.Redo());
        clearBtn?.onClick.AddListener(() => LayerManager.Instance.ActiveLayer?.Clear());
    }

    static void SetTool(ToolType t) => BrushSettings.Instance.activeTool = t;
}
