using UnityEngine;
using System.Collections;

public interface IDynamicObjectState
{
    void ToMagnetizedState();
    void ToUnmagnetizedState();
    void ToGrabbedState();
    void ToUngrabbedState();
    void ToIntertOnPathState();
    void ToMovingOnPathState();
    void ToPausedOnPathState();
    void OnEnable();
    void OnDisable();
    void UpdateState();
}
