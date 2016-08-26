using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class RoomManager : Photon.MonoBehaviour
{
    private UnityAction playerJoinedEvent;

    #region Singleton
    private static RoomManager instance;
    public static RoomManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(RoomManager)) as RoomManager;

                if (instance == null)
                {
                    Debug.LogError("There needs to be one active RoomManager script on a GameObject in your scene.");
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

    public void ThumbsUpACK()
    {
    }

    void OnEnable()
    {
        EventManager.StartListening(EventName.ThumbsUp, ThumbsUpACK);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventName.ThumbsUp, ThumbsUpACK);
    }
}
