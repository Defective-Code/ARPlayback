using UnityEngine;
using UnityEngine.UIElements;

// Add this Class to ARPlayback. 
// This class enables the toggle-playback-ui button if playback is enabled in this project
public class PlaybackPanelController : MonoBehaviour
{
    //[SerializeField] private PanelRenderer playbackPanelRenderer;
    //[SerializeField] private PanelRenderer targetPanelRenderer;

    //private VisualElement playbackPanel;
    //private bool isVisible = true;

    //void OnEnable()
    //{
    //    if (playbackPanelRenderer != null) playbackPanelRenderer.RegisterUIReloadCallback(PlaybackOnUIReload);
    //    if (targetPanelRenderer != null) targetPanelRenderer.RegisterUIReloadCallback(TargetOnUIReload);
    //}

    //void OnDisable()
    //{
    //    if (playbackPanelRenderer != null) playbackPanelRenderer.UnregisterUIReloadCallback(PlaybackOnUIReload);
    //    if (targetPanelRenderer != null) targetPanelRenderer.UnregisterUIReloadCallback(TargetOnUIReload);
    //}

    //void PlaybackOnUIReload(PanelRenderer renderer, VisualElement rootElement, int version)
    //{
    //    playbackPanel = rootElement.Q<VisualElement>("playback-panel");

    //    var toggleButton = rootElement.Q<Button>("toggle-panel-button");
    //    toggleButton.clicked += TogglePanel;
    //}

    //void TargetOnUIReload(PanelRenderer renderer, VisualElement rootElement, int version)
    //{
    //    var toggleButton = rootElement.Q<Button>("toggle-panel-button");
    //    toggleButton.clicked += TogglePanel;
    //}

    //void TogglePanel()
    //{
    //    isVisible = !isVisible;
    //    playbackPanel.style.display = isVisible ? DisplayStyle.Flex : DisplayStyle.None;
    //    //playbackPanelRenderer.enabled = isVisible;
    //}
}