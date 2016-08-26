using UnityEngine;
using System.Collections;

public class WaypointAdder : MonoBehaviour {

    public Vector3 posistionOffset;
    public float actionDelay;
    private float countdown;
    private bool waypointAdded;
    private Color baseColor;
    private Color color = new Color();
    private MeshRenderer waypointAdderRenderer;

    void Awake()
    {
        waypointAdderRenderer = gameObject.GetComponent<MeshRenderer>();
        baseColor = waypointAdderRenderer.material.color;
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        waypointAdded = false;
        Debug.Log("i touched a waypoint");

        if (collisionInfo.gameObject.transform.parent.name == "index")
        {
            countdown = actionDelay;
            color = baseColor;
        }
    }

    void OnTriggerStay(Collider collisionInfo)
    {
        if (!waypointAdded && collisionInfo.gameObject.transform.parent.name == "index")
        {
            color.a += 0.8f;
            color.b += 0.5f;
            waypointAdderRenderer.material.color = color;

            if (countdown <= 0)
            {
                Debug.Log("adding waypoint now");
                EventManager.TriggerEvent(EventName.WaypointAdded);
                waypointAdded = true;
            }
            countdown -= Time.deltaTime;
        }
    }

    void OnTriggerExit(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.transform.parent.name == "index")
        {
            waypointAdderRenderer.material.color = baseColor;
        }
    }
}
