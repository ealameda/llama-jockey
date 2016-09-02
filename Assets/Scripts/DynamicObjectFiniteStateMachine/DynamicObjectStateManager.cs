using UnityEngine;
using System.Collections;
using Stateless;
using Assets.Scripts.DynamicObjectFiniteStateMachine;

public class DynamicObjectStateManager : MonoBehaviour
{
    StateMachine<State, Trigger> stateMachine;
    private UnmagnetizedState unmagnetizedState;
    private MagnetizedState magnetizedState;
    public float magnetStartDistance;

    public void Awake()
    {
        unmagnetizedState = new UnmagnetizedState(magnetStartDistance);
        magnetizedState = new MagnetizedState();

        stateMachine = new StateMachine<State, Trigger>(unmagnetizedState);

        stateMachine.Configure(unmagnetizedState)
            .Permit(Trigger.InProximity, magnetizedState);

        stateMachine.Configure(magnetizedState)
            .Permit(Trigger.OutOfProximity, unmagnetizedState);
    }

    public void Fire(Trigger trigger)
    {
        stateMachine.Fire(trigger);
    }
}


        //[HideInInspector]
        //public IDynamicObjectState currentState;
        //[HideInInspector]
        //public UnmagnetizedState unmagnetizedState;
        //[HideInInspector]
        //public MagnetizedState magnetizedState;

        //public float magnetStartDistance;

        //public void Awake()
        //{
        //    unmagnetizedState = new UnmagnetizedState(this, magnetStartDistance);
        //    magnetizedState = new MagnetizedState(this);
        //}

        //void Start()
        //{
        //    //default state
        //    currentState = unmagnetizedState;
        //}

        //void Update()
        //{
        //    currentState.UpdateState();
        //}
//    }

        

            //dynamicObjectStateMachine.Configure(State.Connected)
            //    .OnEntry(t => StartCallTimer())
            //    .OnExit(t => StopCallTimer())
            //    .Permit(Trigger.LeftMessage, State.OffHook)
            //    .Permit(Trigger.HungUp, State.OffHook)
            //    .Permit(Trigger.PlacedOnHold, State.OnHold);

            //dynamicObjectStateMachine.Configure(State.OnHold)
            //    .SubstateOf(State.Connected)
            //    .Permit(Trigger.TakenOffHold, State.Connected)
            //    .Permit(Trigger.HungUp, State.OffHook)
            //    .Permit(Trigger.PhoneHurledAgainstWall, State.PhoneDestroyed);

            //Print(dynamicObjectStateMachine);
            //Fire(dynamicObjectStateMachine, Trigger.CallDialed);
            //Print(dynamicObjectStateMachine);
            //Fire(dynamicObjectStateMachine, Trigger.CallConnected);
            //Print(dynamicObjectStateMachine);
            //Fire(dynamicObjectStateMachine, Trigger.PlacedOnHold);
            //Print(dynamicObjectStateMachine);
            //Fire(dynamicObjectStateMachine, Trigger.TakenOffHold);
            //Print(dynamicObjectStateMachine);
            //Fire(dynamicObjectStateMachine, Trigger.HungUp);
            //Print(dynamicObjectStateMachine);

            //Console.WriteLine("Press any key...");
            //Console.ReadKey(true);
//        }

//        static void StartCallTimer()
//        {
//            Console.WriteLine("[Timer:] Call started at {0}", DateTime.Now);
//        }

//        static void StopCallTimer()
//        {
//            Console.WriteLine("[Timer:] Call ended at {0}", DateTime.Now);
//        }

//        static void Fire(StateMachine<State, Trigger> phoneCall, Trigger trigger)
//        {
//            Console.WriteLine("[Firing:] {0}", trigger);
//            phoneCall.Fire(trigger);
//        }

//        static void Print(StateMachine<State, Trigger> phoneCall)
//        {
//            Console.WriteLine("[Status:] {0}", phoneCall);
//        }
//    }
//}
