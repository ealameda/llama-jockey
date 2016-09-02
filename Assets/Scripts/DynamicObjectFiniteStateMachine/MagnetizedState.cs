using UnityEngine;
using System.Collections;
using Assets.Scripts.DynamicObjectFiniteStateMachine;

public class MagnetizedState : State
{
    private readonly DynamicObjectStateManager stateMachine;
    private float magnetStartDistance;

    public MagnetizedState() { }
    public void OnEnable() { }
    public void OnDisable() { }
    public void UpdateState() { }
}
