using UnityEngine;
using UnityEngine.UIElements;

public class PlaybackPanelController : MonoBehaviour
{
    [SerializeField] private PanelRenderer playbackPanelRenderer;
    [SerializeField] private PanelRenderer targetPanelRenderer;

    private VisualElement playbackPanel;
    private bool isVisible = true;

    void OnEnable()
    {
        playbackPanelRenderer.RegisterUIReloadCallback(PlaybackOnUIReload);
        targetPanelRenderer.RegisterUIReloadCallback(TargetOnUIReload);
    }

    void OnDisable()
    {
        playbackPanelRenderer.UnregisterUIReloadCallback(PlaybackOnUIReload);
        targetPanelRenderer.UnregisterUIReloadCallback(TargetOnUIReload);
    }

    void PlaybackOnUIReload(PanelRenderer renderer, VisualElement rootElement, int version)
    {
        playbackPanel = rootElement.Q<VisualElement>("playback-panel");

        var toggleButton = rootElement.Q<Button>("toggle-panel-button");
        toggleButton.clicked += TogglePanel;
    }

    void TargetOnUIReload(PanelRenderer renderer, VisualElement rootElement, int version)
    {
        //var toggleButton = rootElement.Q<Button>("toggle-panel-button");
        //toggleButton.clicked += TogglePanel;
    }

    void TogglePanel()
    {
        isVisible = !isVisible;
        playbackPanel.style.display = isVisible ? DisplayStyle.Flex : DisplayStyle.None;
        //playbackPanelRenderer.enabled = isVisible;
    }
}