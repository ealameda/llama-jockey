using UnityEngine;
using System.Collections;
using Stateless;

public class DynamicObjectStatePatternManager : MonoBehaviour
{
    enum Trigger
    {
        InProximity,
        OutOfProximity
    }

    enum State
    {
        Magnetized,
        Unmagnetized,
    }

    public void Awake()
    {
        var dynamicObjectStateMachine = new StateMachine<State, Trigger>(State.Unmagnetized);

        dynamicObjectStateMachine.Configure(State.Unmagnetized)
            .Permit(Trigger.InProximity, State.Magnetized);

        dynamicObjectStateMachine.Configure(State.Magnetized)
            .Permit(Trigger.OutOfProximity, State.Unmagnetized);
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
    }

        

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
        }

        static void StartCallTimer()
        {
            Console.WriteLine("[Timer:] Call started at {0}", DateTime.Now);
        }

        static void StopCallTimer()
        {
            Console.WriteLine("[Timer:] Call ended at {0}", DateTime.Now);
        }

        static void Fire(StateMachine<State, Trigger> phoneCall, Trigger trigger)
        {
            Console.WriteLine("[Firing:] {0}", trigger);
            phoneCall.Fire(trigger);
        }

        static void Print(StateMachine<State, Trigger> phoneCall)
        {
            Console.WriteLine("[Status:] {0}", phoneCall);
        }
    }
}
