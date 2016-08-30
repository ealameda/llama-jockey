using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicObject : MonoBehaviour
{
    private List<Vector3> waypointPositions;
    public float movementSpeed;
    public Vector3 minScale;
    public Vector3 maxScale;
    public float magnetStartDistance;
    public float magnetStopDistance;
    public float movementSmoothing;
	public GameObject motionEffect;

    private Vector3 objectOrigin;
    private int wayPointIndex;
    private bool reverse = false;
    private bool intersectingObject = false;
    private bool pinching = false;
    private Transform indexFinger;
    private bool enableMovement = true;
    private Animation anim;


    void Start()
    {
        wayPointIndex = 0;
        anim = transform.GetComponent<Animation>();
        objectOrigin = transform.position;
    }

    void Update ()
    {
        // TODO: this will not work for 2 players nicely, we think......
        GameObject rightPinchDetector = GameObject.Find("PinchDetector_R");
        if (waypointPositions != null && waypointPositions.Count > 1 && enableMovement)
        {
            if (Vector3.Distance(transform.position, waypointPositions[wayPointIndex]) < 0.01f)
            {
                // go to the next waypoint, unless we're at the end and then go in the reverse direction
                // diff scenarios - @ 0, moving between 0 and N, @ n, moving between n and 0
                if (wayPointIndex == 0)
                {
                    wayPointIndex++;
                    reverse = false;
                }
                else if (!reverse && (wayPointIndex < (waypointPositions.Count - 1)))
                {
                    wayPointIndex++;
                }
                else if (wayPointIndex == (waypointPositions.Count - 1))
                {
                    wayPointIndex--;
                    reverse = true;
                }
                else if (reverse && wayPointIndex != 0)
                {
                    wayPointIndex--;
                }
            }
            transform.position = Vector3.Lerp(transform.position, waypointPositions[wayPointIndex], Time.deltaTime * movementSpeed);
            transform.LookAt(waypointPositions[wayPointIndex]);
        }
        else if (waypointPositions == null && rightPinchDetector != null)
        {
            float handToObjectDistance = Vector3.Distance(transform.position, rightPinchDetector.transform.position);
            if (handToObjectDistance < magnetStartDistance && handToObjectDistance > magnetStopDistance)
            {
                transform.position = Vector3.Lerp(transform.position, rightPinchDetector.transform.position, Time.deltaTime * movementSmoothing);
            }
            else if (handToObjectDistance > magnetStartDistance && transform.position != objectOrigin)
            {
                transform.position = Vector3.Lerp(transform.position, objectOrigin, Time.deltaTime * movementSmoothing);
            }
            else
            {
                objectOrigin = transform.position;
            }
        }
        else if (transform.position != objectOrigin)
        {
            transform.position = Vector3.Lerp(transform.position, objectOrigin, Time.deltaTime * movementSmoothing);
        }
        else
        {
            objectOrigin = transform.position;
        }
    }

    public void SetWaypointPositions(List<GameObject> waypoints)
    {
        waypointPositions = new List<Vector3>();
        foreach (GameObject waypoint in waypoints)
        {
            waypointPositions.Add(waypoint.transform.position);
        }
    }

    public void RemoveWaypointPositions()
    {
        waypointPositions = null;
    }

    void ToggleIntersectingObject()
    {
        intersectingObject = !intersectingObject;
    }

    public void AddWaypoint(GameObject waypoint)
    {
        waypointPositions.Add(waypoint.transform.position);
    }

    void PauseMovement()
    {
        //enableMovement = !enableMovement;
    }

    void PlayAnimation()
    {
        if (motionEffect != null && !motionEffect.GetActive())
        {
			motionEffect.SetActive (true);
        }
    }

    void StopAnimation()
    {
        if (motionEffect != null && motionEffect.GetActive())
        {
			motionEffect.SetActive (false);
        }
    }

    void OnEnable()
    {
        EventManager.StartListening(EventName.DynamicObjectGrabbed, ToggleIntersectingObject);
        EventManager.StartListening(EventName.EditingWaypointOnOff, PauseMovement);
        EventManager.StartListening(EventName.DynamicObjectIntersectingPath, PlayAnimation);
        EventManager.StartListening(EventName.DynamicObjectOffPath, StopAnimation);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventName.DynamicObjectGrabbed, ToggleIntersectingObject);
        EventManager.StopListening(EventName.EditingWaypointOnOff, PauseMovement);
        EventManager.StopListening(EventName.DynamicObjectIntersectingPath, PlayAnimation);             
        EventManager.StopListening(EventName.DynamicObjectOffPath, StopAnimation);
    }
}
