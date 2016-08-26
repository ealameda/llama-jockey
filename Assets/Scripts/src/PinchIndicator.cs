using UnityEngine;
using System.Collections;

public class PinchIndicator : MonoBehaviour
{
    private bool pinching = false;
    private bool beganPinching = false;
    private MeshRenderer renderer;
    private float pinchStartTime = 0.0f;
    private Color startColor;
    private Color endColor;
    private Color pinchActionColor;

    public Material pinchIndicatorMaterial;
    public Material pinchActionMaterial;
    [Range(0, 1)]
    public float colorLerpSpeed;

    private bool pinchActionTriggered = false;

    public float pinchActionDelay;

    private Transform grabbedDynamicObject;
    private Transform grabbedWaypoint;

    public void OnPinchDetected()
    {
        pinching = true;
        pinchStartTime = Time.time;
        if (grabbedDynamicObject == null && grabbedWaypoint == null)
        {
            renderer.enabled = true;
            pinchActionColor = renderer.material.color;
            endColor = pinchIndicatorMaterial.color;
            startColor = new Color(endColor.r, endColor.g, endColor.b, 0.1f);
            renderer.material.color = startColor;
            EventManager.TriggerEvent(EventName.WaypointPinch);
        }
        else if (grabbedWaypoint != null)
        {
            Debug.Log("pinch indicator grabbed a waypoint");
            EventManager.TriggerEvent(EventName.EditingWaypointOnOff);
        }
        else if (grabbedDynamicObject != null)
        {
            EventManager.TriggerEvent(EventName.DynamicObjectGrabbed);
        }
    }

    public void OnNoPinchDetected()
    {
        pinching = false;
        pinchActionTriggered = false;

        if (grabbedDynamicObject == null && grabbedWaypoint == null)
        {
            renderer.material = pinchIndicatorMaterial;
            renderer.material.color = startColor;
            renderer.enabled = false;
            EventManager.TriggerEvent(EventName.WaypointUnPinch);
        }
        else if (grabbedDynamicObject != null)
        {
            grabbedDynamicObject.GetComponent<DynamicObject>().RemoveWaypointPositions();
            grabbedDynamicObject = null;
            EventManager.TriggerEvent(EventName.DynamicObjectReleased);
        }
        else if (grabbedWaypoint != null)
        {
            Debug.Log("unpinched a waypoint");
            grabbedWaypoint = null;
            EventManager.TriggerEvent(EventName.EditingWaypointOnOff);
        }
    }

    void Start()
    {
        renderer = transform.Find("PinchTool").GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Color color = new Color();
        if (renderer != null)
        {
            color = renderer.material.color;
        }

        if (pinching && grabbedDynamicObject == null && grabbedWaypoint == null)
        {
            if ((Time.time - pinchStartTime) >= pinchActionDelay)
            {
                if (!pinchActionTriggered)
                {
                    renderer.material = pinchActionMaterial;
                    pinchActionTriggered = true;
                    if (gameObject.name == "PinchDetector_R")
                    {
                        EventManager.TriggerEvent(EventName.ReadyToSetWaypoint);
                    }
                }
            }
            else
            {
                // lerpPercent = percent of time elapsed from when they started pinching 
                // and how much time remains till pinch action is met
                // time = 20, start time = 19, action delay = 3, lerp% = 1/3
                float lerpPercent = ((Time.time - pinchStartTime) / pinchActionDelay) * colorLerpSpeed;
                renderer.material.color = Color.Lerp(color, endColor, lerpPercent);
            }
        }
        else if(pinching && grabbedDynamicObject != null)
        {
            grabbedDynamicObject.position = transform.position;
        }
        else if (pinching && grabbedWaypoint != null)
        {
            grabbedWaypoint.position = transform.position;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "DynamicObject")
        {
            //EventManager.TriggerEvent(EventName.DynamicObjectGrabbed);
            if (!pinching)
            {
                grabbedDynamicObject = col.transform;
            }
        }
        if (col.gameObject.tag == "Waypoint")
        {
            //EventManager.TriggerEvent(EventName.DynamicObjectEnterExit);
            if (!pinching)
            {
                grabbedWaypoint = col.transform;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "DynamicObject")
        {
            //EventManager.TriggerEvent(EventName.DynamicObjectGrabbed);
            grabbedDynamicObject = null;
        }
        if (col.gameObject.tag == "Waypoint")
        {
            grabbedWaypoint = null;
        }
    }
}

