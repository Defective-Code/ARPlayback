using Google.XR.ARCoreExtensions;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaybackManager))]
public class ArPlaybackManager : MonoBehaviour
{
    [SerializeField] private ARSession arSession;

    // ARPlaybackManager is a MonoBehaviour provided by ARCore Extensions,
    // usually living on the same GameObject as ARCoreExtensions/ARSession.
    // Still used for stopping playback and reading status.
    [SerializeField] private ARPlaybackManager playbackManager;

    private bool playingBack; // are we playing back

    private ARCoreSessionSubsystem subsystem;

    public event Action SessionReset; // fires on both playback start AND stop, whenever we clear state

    private string PlaybackFolder => Path.Combine(Application.persistentDataPath, "Recordings");

    private void Awake()
    {
        if (!Directory.Exists(PlaybackFolder))
            Directory.CreateDirectory(PlaybackFolder);

        if (playbackManager == null)
            playbackManager = arSession.GetComponent<ARPlaybackManager>();

        if (playbackManager == null)
            Debug.LogError("ArPlaybackManager: No ARPlaybackManager component found. " +
                            "Assign it in the inspector or ensure it's on the ARSession GameObject.");
    }

    // Call this with a recording folder name selected from your UI
    public bool StartPlayback(string folderName)
    {
        string fullPath = Path.Combine(PlaybackFolder, folderName); // recording folder path
        string recordingPath = Path.Combine(fullPath, "recording.mp4"); // ARCore mp4 recording file
        string uri = new System.Uri(recordingPath).AbsoluteUri; // have to add this on Android

        Debug.Log($"Checking path: {recordingPath}");
        Debug.Log($"Exists: {File.Exists(recordingPath)}");
        Debug.Log($"Exists: {File.Exists(uri)}");

        if (!File.Exists(recordingPath))
        {
            Debug.LogError($"Playback file not found: {recordingPath}");
            return false;
        }

        if (subsystem == null)
        {
            subsystem = arSession.subsystem as ARCoreSessionSubsystem;
            if (subsystem == null)
            {
                Debug.LogError("ArPlaybackManager: ARCoreSessionSubsystem not available. " +
                                "Is the ARSession active and ARCore the active XR loader?");
                return false;
            }

        }

        // Clear all trackables and anchor bookkeeping BEFORE starting playback,
        // so the recorded session starts from a completely blank slate.
        ClearSessionState();

        playingBack = true;

        Debug.Log($"Printing filepath : {uri}");

        // StartPlaybackUri handles pause -> set dataset -> resume internally,
        // which avoids the timing issues that cause SessionNotReady when doing it manually.
        ArStatus status = subsystem.StartPlaybackUri(uri);
        if (status != ArStatus.Success)
        {
            Debug.LogError($"Failed to start playback: {status}");
            return false;
        }

        return true;
    }

    public string[] GetAvailableRecordings()
    {
        return Directory.GetDirectories(PlaybackFolder); // recording folders inside the Recordings folder
    }

    public PlaybackStatus GetCurrentStatus()
    {
        return playbackManager.PlaybackStatus;
    }

    public void StopPlayback()
    {
        if (!playingBack) //  we never started a playback, therefore we do not need to actually stop
        {
            Debug.Log("Ignoring stop request as no playback is occuring");
            return;
        }

        subsystem?.StopPlaybackUri();
        playingBack = false;

        // Clear again on the way back to the live session, so it also starts blank.
        ClearSessionState();
    }

    private void ClearSessionState()
    {
        arSession.Reset(); // destroys all trackables (anchors, tracked images, planes, etc.)
        //anchorData.ResetAnchors(); // clear our own bookkeeping, since Reset() doesn't know about it
        SessionReset?.Invoke(); // notify listeners (e.g. ImageTargetSession) to clear their own local state
    }
}