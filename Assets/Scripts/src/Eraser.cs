using UnityEngine;
using System.Collections;

public class Eraser : Photon.MonoBehaviour
{
    public Vector3 posistionOffset;
    private GameObject[] lines;
    public float eraseDelay;
    private float countdown;
    private bool erased;
    private Color baseColor;
    private Color color = new Color();
    private MeshRenderer eraserRenderer;

    void Awake()
    {
        eraserRenderer = gameObject.GetComponent<MeshRenderer>();
        baseColor = eraserRenderer.material.color;
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        erased = false;

        if (collisionInfo.gameObject.transform.parent.name == "index")
        {
            countdown = eraseDelay;
            color = baseColor;
        }
    }

    void OnTriggerStay(Collider collisionInfo)
    {
        if (!erased && collisionInfo.gameObject.transform.parent.name == "index")
        {
            color.a += 0.02f;
            color.r += 0.01f;
            eraserRenderer.material.color = color;

            if (countdown <= 0)
            {
                lines = GameObject.FindGameObjectsWithTag("Line");
                foreach (GameObject line in lines)
                {
                    Destroy(line);
                }
                photonView.RPC("EraseRemoteLines", PhotonTargets.OthersBuffered);
                erased = true;
            }
            countdown -= Time.deltaTime;
        }
    }

    void OnTriggerExit(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.transform.parent.name == "index")
        {
            eraserRenderer.material.color = baseColor;
        }
    }

    [PunRPC]
    void EraseRemoteLines()
    {
        lines = GameObject.FindGameObjectsWithTag("RemoteLine");
        foreach (GameObject line in lines)
        {
            Destroy(line);
        }
    }
}
