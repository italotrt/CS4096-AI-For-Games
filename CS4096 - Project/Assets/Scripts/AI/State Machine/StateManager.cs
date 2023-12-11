using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State currentState; // Public variable to hold the current state

    void Update()
    {
        // Call the RunStateMachine method to handle state transitions and execution
        RunStateMachine();
    }

    // Method to run the state machine
    private void RunStateMachine() {
        // Attempt to get the next state by executing the current state's RunCurrentState method
        State nextState = currentState?.RunCurrentState();

        // Check if a next state is returned
        if (nextState != null) {
            // Call the SwitchToTheNextState method to transition to the next state
            SwitchToTheNextState(nextState);
        }
    }

    // Method to switch to the next state
    private void SwitchToTheNextState(State nextState) {
        // Update the current state to the provided next state
        currentState = nextState;
    }
}
