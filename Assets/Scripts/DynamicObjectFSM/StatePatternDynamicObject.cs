using UnityEngine;
using System.Collections;

public class StatePatternDynamicObject : MonoBehaviour
{
    [HideInInspector]
    public IDynamicObjectState currentState;
    [HideInInspector]
    public UnmagnetizedState unmagnetizedState;
    [HideInInspector]
    public MagnetizedState magnetizedState;

    public float magnetStartDistance;



    public void Awake()
    {
        unmagnetizedState = new UnmagnetizedState(this, magnetStartDistance);
        magnetizedState = new MagnetizedState(this);
    }

    void Start()
    {
        currentState = unmagnetizedState;
    }

    void Update()
    {
        currentState.UpdateState();
    }
}
