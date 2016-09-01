using UnityEngine;
using System.Collections;

public class WaypointGrower : MonoBehaviour
{
    private float normalScale;

    public float maxScale;
    public float growStartDistance;

    // Use this for initialization
    void Start ()
    {
        normalScale = transform.localScale.x;
	}

    // Update is called once per frame
    void Update()
    {
        GameObject rightPinchDetector = GameObject.Find("PinchDetector_R");
        if (rightPinchDetector != null)
        {
            float handToObjectDistance = Vector3.Distance(transform.position, rightPinchDetector.transform.position);
            if (handToObjectDistance < growStartDistance)
            {
                float x = normalScale + (1 - (handToObjectDistance / growStartDistance)) * (maxScale - normalScale);
                float y = normalScale + (1 - (handToObjectDistance / growStartDistance)) * (maxScale - normalScale);
                float z = normalScale + (1 - (handToObjectDistance / growStartDistance)) * (maxScale - normalScale);
                transform.localScale = new Vector3(x, y, z);
            }
        }
    }
      
}
