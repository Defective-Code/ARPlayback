using UnityEngine;
using UnityEngine.UIElements;

public class PlaybackPanelController : MonoBehaviour
{
    [SerializeField] private PanelRenderer panelRenderer;

    private VisualElement playbackPanel;
    private bool isVisible = true;

    void OnEnable()
    {
        panelRenderer.RegisterUIReloadCallback(OnUIReload);
    }

    void OnDisable()
    {
        panelRenderer.UnregisterUIReloadCallback(OnUIReload);
    }

    void OnUIReload(PanelRenderer renderer, VisualElement rootElement, int version)
    {
        playbackPanel = rootElement.Q<VisualElement>("playback-panel");

        var toggleButton = rootElement.Q<Button>("toggle-panel-button");
        toggleButton.clicked += TogglePanel;
    }

    void TogglePanel()
    {
        isVisible = !isVisible;
        playbackPanel.style.display = isVisible ? DisplayStyle.Flex : DisplayStyle.None;
    }
}