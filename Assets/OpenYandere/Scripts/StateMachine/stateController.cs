using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/* public abstract class stateController<Estate> : MonoBehaviour where Estate : Enum
{
    protected Dictionary<Estate, baseState<Estate>> status = new Dictionary<Estate, baseState<Estate>>();

    protected baseState<Estate> currentState;

    protected bool isTransitioningState;
    // Start is called before the first frame update
    void Start()
    {
        currentState.enterState();
    }

    // Update is called once per frame
    void Update()
    {
        Estate statekey = currentState.getNextState();

        if(!isTransitioningState && statekey.Equals(currentState.stateKey))
        {
            currentState.updateState();
        }else if(!isTransitioningState)
        {

            switchState(statekey);
        }
    }

    private void switchState(Estate toState)
    {
        isTransitioningState = true;
        currentState.exitState();
        currentState = status[toState];
        currentState.enterState();
        isTransitioningState = false;
    }
}
*/