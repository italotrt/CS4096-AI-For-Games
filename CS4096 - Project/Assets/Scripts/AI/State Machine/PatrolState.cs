using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Patrol State Script, which is a dericed class from State Class
// in here it will execute the logic behind the patrol state of the AI
public class PatrolState : State
{
    [SerializeField] private Transform initialPosition, finalPosition; // Reference for the Initial and Final Position's Transforms
    public NavMeshAgent agent; // Reference to the NavMeshAgent
    public ChaseState chaseStateScript; // Reference to the Chase State Script
    public Player playerScript; // Reference to the Player's script
    public bool canSeePlayer; // Boolean variable used to determine rather if the AI can or cannot see the player
    public float timer = 2.5f; // Float variable used for the Invoke method

    // Go To Target method, it sets the destination of
    // the Nav Mesh Agent to the final position of the patrol
    // state of the AI
    public void GoToTarget() {
        agent.destination = finalPosition.position;
    }

    // Go Back method, it sets the destination of
    // the Nav Mesh Agent to the initial position of the patrol
    // state of the AI
    public void GoBack() {
        agent.destination = initialPosition.position;
    }

    // Implementation of the abstract method inside the State script
    public override State RunCurrentState() {
        // This if statement checks if the x and z position of the AI is the same as
        // the initial position, if it is, it will invoke the GoToTarget() method
        // after the timer is finished
        if(this.gameObject.transform.position.x == initialPosition.position.x &&
             this.gameObject.transform.position.z == initialPosition.position.z && !canSeePlayer) {
            Invoke("GoToTarget", timer);
            Debug.Log("Going back to initial position");
        }

        // This if statement checks if the x and z position of the AI is the same as
        // the final position, if it is, it will invoke the GoToTarget() method
        // after the timer is finished
        if(this.gameObject.transform.position.x == finalPosition.position.x &&
            this.gameObject.transform.position.z == finalPosition.position.z && !canSeePlayer) {
            Invoke("GoBack", timer);
            Debug.Log("Going to final position");
        }

        // If the player collects the goal object
        // it makes the AI chase it, by changing the canSeePlayer to true
        if(playerScript.goalAchieved) {
            canSeePlayer = true;
        }

        // If the AI can see the player it returns the chase script to the state machine
        // else it returns this current state, looping it
        if (canSeePlayer) {
            return chaseStateScript;
        } else {
            return this;
        }
    }
}
