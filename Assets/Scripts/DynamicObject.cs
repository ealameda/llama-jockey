using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;

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
    public float glowPowerUpTime;

    private Vector3 objectOrigin;
    private int wayPointIndex;
    private bool reverse = false;
    private bool intersectingObject = false;
    private bool pinching = false;
    private Transform indexFinger;
    private bool enableMovement = true;
    private Animation anim;
    private float glowStartTime = 0.0f;
    private float unglowStartTime = 0.0f;
    private Color baseMaterialColor;
    private Material glowMat;
    private bool growCRStarted = false;
    private AudioSource audioSource;


    void Start()
    {
        wayPointIndex = 0;
        anim = transform.GetComponent<Animation>();
        objectOrigin = transform.position;
        glowMat = GetComponent<MeshRenderer>().material;
        baseMaterialColor = glowMat.color;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // TODO: this will not work for 2 players nicely, we think......
        GameObject rightPinchDetector = GameObject.Find("PinchDetector_R");
        // Object moving through waypoints
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
        // Check to see if we magnetize
        else if (waypointPositions == null && rightPinchDetector != null)
        {
            float handToObjectDistance = Vector3.Distance(transform.position, rightPinchDetector.transform.position);
            // see if the hand is in the magnetize sphere and the object hasn't left the distance of the sphere
            if (handToObjectDistance < magnetStartDistance
                && Vector3.Distance(transform.position, objectOrigin) < magnetStartDistance)
            {

                if (handToObjectDistance > magnetStopDistance)
                {
                    transform.position = Vector3.Lerp(transform.position, rightPinchDetector.transform.position,
                        Time.deltaTime * movementSmoothing);
                }

                if (!growCRStarted)
                {
                    StopCoroutine(ChangeEmission(0.0f, false));
                    StartCoroutine(ChangeEmission(Time.time, true));
                }
            }
            //hand leaves magnet field
            else if (handToObjectDistance > magnetStartDistance && transform.position != objectOrigin)
            {
                transform.position = Vector3.Lerp(transform.position, objectOrigin, Time.deltaTime * movementSmoothing);
                if (growCRStarted)
                {
                    StopCoroutine(ChangeEmission(0.0f, true));
                    StartCoroutine(ChangeEmission(Time.time, false));
                }
            }
        }
        //todo: object was moved? think this is wrong
        else if (transform.position != objectOrigin)
        {
            transform.position = Vector3.Lerp(transform.position, objectOrigin, Time.deltaTime * movementSmoothing);
        }
        else
        {
            objectOrigin = transform.position;
        }
    }

    IEnumerator ChangeEmission(float startTime, bool glowingUp)
    {
        growCRStarted = glowingUp;
        Debug.Log("Coroutine time: " + startTime);
        int count = 3;
        if (glowMat != null)
        {
            while (Time.time - startTime < glowPowerUpTime && count < 4)
            {
                if (glowingUp)
                {
                    glowMat.SetColor("_EmissionColor",
                        Color.white * Mathf.Lerp(1.0f, 4.0f, (Time.time - startTime) / glowPowerUpTime));
                }
                else
                {
                    glowMat.SetColor("_EmissionColor",
                        Color.white * Mathf.Lerp(4.0f, 1.0f, (Time.time - startTime) / glowPowerUpTime));
                }
                yield return null;
            }
        }
        yield return null;
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
            motionEffect.SetActive(true);
        }

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void StopAnimation()
    {
        if (motionEffect != null && motionEffect.GetActive())
        {
            motionEffect.SetActive(false);
        }

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
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
