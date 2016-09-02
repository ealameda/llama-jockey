using UnityEngine;
using System.Collections;

public interface IDynamicObjectState
{
    void OnEnable();
    void OnDisable();
    void UpdateState();
}
