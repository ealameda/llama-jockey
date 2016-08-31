using UnityEngine;
using System.Collections.Generic;

public class WaypointManager : Photon.MonoBehaviour
{
    #region Public Variables
    public GameObject dynamicObjectPrefab;
    public GameObject dynamicObjectGameObject;
    public GameObject waypointIndicatorPrefab;
    public GameObject pathPrefab;
	public Material pathInertMaterial;
	public Material pathActiveMaterial;
    #endregion

    #region Private Variables
    private DynamicObject dynamicObject;
    private Transform rightPinchDetector;
    private bool editingWaypoint = false;
    private LineRenderer pathLineRenderer;
    private bool readyToSetWaypointToggle = false;
    private List<GameObject> waypoints;
    private bool dynamicObjectGrabbed = false;
    #endregion

    #region Singleton
    private static WaypointManager instance;
    public static WaypointManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(WaypointManager)) as WaypointManager;

                if (instance == null)
                {
                    Debug.LogError("There needs to be one active WayPointManager script on a GameObject in your scene.");
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

    void OnEnable()
    {
        EventManager.StartListening(EventName.WaypointPinch, TrackIndexFinger);
        EventManager.StartListening(EventName.ReadyToSetWaypoint, ReadyToSetWaypoint);
        EventManager.StartListening(EventName.WaypointUnPinch, CheckIfReadyToAddWaypoint);
        EventManager.StartListening(EventName.DynamicObjectReleased, ReleasedDynamicObject);
        EventManager.StartListening(EventName.EditingWaypointOnOff, ToggleEditingWaypoint);
        EventManager.StartListening(EventName.DynamicObjectGrabbed, DynamicObjectIsGrabbed);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventName.WaypointPinch, TrackIndexFinger);
        EventManager.StopListening(EventName.ReadyToSetWaypoint, ReadyToSetWaypoint);
        EventManager.StopListening(EventName.WaypointUnPinch, CheckIfReadyToAddWaypoint);
        EventManager.StopListening(EventName.DynamicObjectReleased, ReleasedDynamicObject);
        EventManager.StopListening(EventName.EditingWaypointOnOff, ToggleEditingWaypoint);
        EventManager.StopListening(EventName.DynamicObjectGrabbed, DynamicObjectIsGrabbed);
    }

    void Update()
    {
        if (pathLineRenderer != null && waypoints != null)
        {
            if (!editingWaypoint && rightPinchDetector != null)
            {
                pathLineRenderer.SetVertexCount(waypoints.Count + 1);
                pathLineRenderer.SetPosition(waypoints.Count, rightPinchDetector.position);
            }
            else
            {
                for (int i=0;i<waypoints.Count;i++)
                {
                    pathLineRenderer.SetPosition(i, waypoints[i].transform.position);
                }
                if (dynamicObject != null)
                {
                    dynamicObject.SetWaypointPositions(waypoints);
                }
            }

            if (dynamicObjectGrabbed)
            {
                CheckIfObjectIntersectingPath();
            }
        }
    }

    void TrackIndexFinger()
    {
        rightPinchDetector = GameObject.Find("PinchDetector_R").transform;
        Vector3 waypoint = rightPinchDetector.position;

        if (pathLineRenderer == null)
        {
            GameObject path = Instantiate(pathPrefab, waypoint, Quaternion.identity) as GameObject;
            pathLineRenderer = path.GetComponent<LineRenderer>();
        }
    }

    void ReadyToSetWaypoint()
    {
        readyToSetWaypointToggle = true;
    }

    void CheckIfReadyToAddWaypoint()
    {
        if (readyToSetWaypointToggle)
        {
            AddWaypoint();
        }
        else if (pathLineRenderer != null && waypoints != null)
        {
            pathLineRenderer.SetVertexCount(waypoints.Count);
        }

        rightPinchDetector = null;
    }

    void AddWaypoint()
    {
        Vector3 waypointPosition = rightPinchDetector.position;
        if (waypoints == null)
        {
            waypoints = new List<GameObject>();
        }
        GameObject waypoint = (GameObject)Instantiate(waypointIndicatorPrefab, waypointPosition, Quaternion.identity);
        waypoints.Add(waypoint);
	EventManager.TriggerEvent(EventName.WaypointAdded);
        pathLineRenderer.SetVertexCount(waypoints.Count);
        pathLineRenderer.SetPosition(waypoints.Count - 1, waypointPosition);
        readyToSetWaypointToggle = false;
        if (dynamicObject != null)
        {
            dynamicObject.AddWaypoint(waypoint);
        }
        photonView.RPC("AddRemoteWaypoint", PhotonTargets.OthersBuffered, waypointPosition);
    }

    void ReleasedDynamicObject()
    {
        dynamicObject = CheckIfObjectIntersectingPath();
        if (dynamicObject != null)
        {
            dynamicObject.SetWaypointPositions(waypoints); 
        }
        else
        {
            EventManager.TriggerEvent(EventName.DynamicObjectOffPath);
        }
        dynamicObjectGrabbed = false;
		{
			pathLineRenderer.material = pathInertMaterial;
		}
    }

    DynamicObject CheckIfObjectIntersectingPath()
    {
        if (waypoints != null && waypoints.Count > 1)
        {
            for (int i = 0; i < waypoints.Count - 1; i++)
            {
                Debug.Log("calling check if object intersectiong path");
                RaycastHit hit;
                Vector3 start = waypoints[i].transform.position;
                Vector3 end = waypoints[i + 1].transform.position;

                //waypoints[i].transform.GetComponent<MeshRenderer>().material.color = Color.red;
                //waypoints[i + 1].transform.GetComponent<MeshRenderer>().material.color = Color.red;

                float distance = Vector3.Distance(start, end);

                if (Physics.Linecast(start, end, out hit) && hit.transform.tag == "DynamicObject")
                {
                    DynamicObject intersectedDynamicObject = hit.transform.GetComponent<DynamicObject>();
                    EventManager.TriggerEvent(EventName.DynamicObjectIntersectingPath);
                    return intersectedDynamicObject;
                }
            }
        }
        EventManager.TriggerEvent(EventName.DynamicObjectOffPath);
        return null;
    }

    void ToggleEditingWaypoint()
    {
        editingWaypoint = !editingWaypoint;
    }

    void DynamicObjectIsGrabbed()
    {
        dynamicObjectGrabbed = true;
		if (pathLineRenderer != null) 
		{
			pathLineRenderer.material = pathActiveMaterial;
		}
    }

    [PunRPC]
    void AddRemoteWaypoint(Vector3 waypoint)
    {
        //if (dynamicObject == null)
        //{
        //    GameObject instantaitedObject = Instantiate(dynamicObjectPrefab, waypoint, Quaternion.identity) as GameObject;
        //    dynamicObject = instantaitedObject.GetComponent<DynamicObject>();
        //}
        //dynamicObject.waypointPositions.Add(waypoint);
        //Instantiate(waypointIndicatorPrefab, waypoint, Quaternion.identity);
    }
}
