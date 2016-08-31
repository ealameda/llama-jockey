using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class WaypointAddedAnimation : MonoBehaviour
{
    public float lifeTime;
    public float scaleSize;

    private Vector3 maxScale;
    private float startTime;
    private Vector3 startScale;
    private Color startColor;
    private Color endColor;
    private Material material;
	// Use this for initialization
	void Start ()
    {
        Destroy(this, lifeTime);
        maxScale = new Vector3(scaleSize, scaleSize, scaleSize);
        startTime = Time.time;
        startScale = transform.localScale;
        startColor = GetComponent<MeshRenderer>().material.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);
        material = GetComponent<MeshRenderer>().material;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    float lerpScale = (Time.time - startTime)/lifeTime;
	    transform.localScale = Vector3.Lerp(startScale, maxScale, lerpScale);
	    GetComponent<MeshRenderer>().material.color =  Color.Lerp(startColor, endColor, lerpScale);
	}
}
