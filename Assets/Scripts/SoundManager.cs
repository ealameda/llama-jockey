using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Photon.MonoBehaviour 
{
    public AudioSource editingWaypointFX;
    public AudioSource dynamicObjectMovingFX;

    private AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();
    }

    void PlayDynamicObjectMovingFX()
    {
        if (dynamicObjectMovingFX != null && !dynamicObjectMovingFX.isPlaying)
        {
            dynamicObjectMovingFX.Play();
        }
    }

    void PlayEditingWaypoingFX()
    {
        editingWaypoint = !editingWaypoint;
        if (editingWaypointFX != null && !editingWaypointFX.isPlaying && !editingWaypoint)
        {
            editingWaypointFX.Play();
        }
    }

    void OnEnable()
    {
        EventManager.StartListening(EventName.DynamicObjectIntersectingPath, PlayDynamicObjectMovingFX);
        EventManager.StartListening(EventName.EditingWaypointOnOff, PlayEditingWaypoingFX);
        EventManager.StartListening(EventName.WaypointAdded, PlayEditingWaypoingFX);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventName.DynamicObjectIntersectingPath, PlayDynamicObjectMovingFX);
        EventManager.StopListening(EventName.EditingWaypointOnOff, PlayEditingWaypoingFX);
        EventManager.StopListening(EventName.WaypointAdded, PlayEditingWaypoingFX);
    }
}
