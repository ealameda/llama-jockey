using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Photon.MonoBehaviour 
{
    public AudioClip waypointFX;
    public AudioClip dynamicObjectFX;
    public AudioSource fxAudioSource;
    
    private bool editingWaypoint = false;

    #region Singleton
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;

                if (instance == null)
                {
                    Debug.LogError("There needs to be one active SoundManager script on a GameObject in your scene.");
                }
            }
            return instance;
        }
    }

    void Init()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    void Awake()
    {
        Init();
    }

    void PlayDynamicObjectMovingFX()
    {
        if (fxAudioSource != null && dynamicObjectFX != null && !fxAudioSource.isPlaying)
        {
            fxAudioSource.clip = dynamicObjectFX;
            fxAudioSource.Play();
        }
    }

    void PlayEditingWaypointFX()
    {
        editingWaypoint = !editingWaypoint;
        if (fxAudioSource != null && waypointFX != null && !editingWaypoint)
        {
            fxAudioSource.PlayOneShot(waypointFX);
        }
    }

    void PlayAddWaypointFX()
    {
		Debug.Log ("trying to play add waypoint");
        if (fxAudioSource != null && waypointFX != null)
        {
            fxAudioSource.PlayOneShot(waypointFX);
        }
    }

    void StopDynamicObjectMovingFX()
    {
        if (fxAudioSource != null && dynamicObjectFX != null)
        {
            fxAudioSource.Stop();
        }
    }

    void OnEnable()
    {
        EventManager.StartListening(EventName.DynamicObjectIntersectingPath, PlayDynamicObjectMovingFX);
        EventManager.StartListening(EventName.DynamicObjectOffPath, StopDynamicObjectMovingFX);
        EventManager.StartListening(EventName.EditingWaypointOnOff, PlayEditingWaypointFX);
        EventManager.StartListening(EventName.WaypointAdded, PlayAddWaypointFX);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventName.DynamicObjectIntersectingPath, PlayDynamicObjectMovingFX);
        EventManager.StopListening(EventName.DynamicObjectOffPath, StopDynamicObjectMovingFX);
        EventManager.StopListening(EventName.EditingWaypointOnOff, PlayEditingWaypointFX);
        EventManager.StopListening(EventName.WaypointAdded, PlayAddWaypointFX);
    }
}
