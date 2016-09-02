using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DynamicObjectFiniteStateMachine
{
    public interface State
    {
        void OnEnable();
        void OnDisable();
        void UpdateState();
    }
}
