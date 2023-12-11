using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chase State Script, which is a dericed class from State Class
// in here it will execute the logic behind the chase state of the AI
public class ChaseState : State
{
    public PatrolState patrolStateScript; // Reference to the patrol state script
    public Player playerScript; // Reference to the player script
    public Transform playerPosition; // Reference for the Player's transform

    // Implementation of the abstract method inside the State script
    public override State RunCurrentState() {
        // If the goalAchieved boolean inside the player's script is true
        // it will make the AI able to see the player
        if(playerScript.goalAchieved) {
            Debug.Log("Let's go get him!!");
            patrolStateScript.canSeePlayer = true;
        }

        // If the AI can see the player, it will set the nav mesh agent
        // destination to the player's position and returns this script to the
        // state machine, looping the chase state
        if (patrolStateScript.canSeePlayer) {
            Debug.Log("I can see you!!");
            patrolStateScript.agent.SetDestination(playerPosition.position);
            return this;
        } else {
            // Else if the AI can no longer see the player
            // it sets the canSeePlayer to false,
            // invokes (calls) the ReturnToInitialPosition method
            // in 7 seconds and will return the patrol script to the state machine
            // making it execute the patrol state from here
            patrolStateScript.canSeePlayer = false;
            Invoke("ReturnToInitialPosition", 7f);
            Debug.Log("Where did you go?");
            return patrolStateScript;
        }
    }

    // Return To Initial Position method, it calls the GoBack() method
    // inside the patrol state script
    void ReturnToInitialPosition() {
        patrolStateScript.GoBack();
    }

    // On Trigger Enter method that is used to detect the collision
    // between the player and AI, and damage the player
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Debug.Log("I got you!! (Game Over)");
            //Deals damage to the player
            playerScript.playerHealth -= 100;
        }
    }
}
