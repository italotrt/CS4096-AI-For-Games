using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define the FieldOfView class, inheriting from MonoBehaviour
public class FieldOfView : MonoBehaviour
{
    public float radius; // Public variable for the field of view radius

    // Public variable for the field of view angle, with a range from 0 to 360
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef; // Reference to the player GameObject
    public LayerMask targetMask; // Layer mask for filtering the target objects
    public LayerMask obstructionMask; // Layer mask for filtering obstructions in the field of view
    public PatrolState patrolStateScript; // Reference to the PatrolState script
    
    private void Start() {
        playerRef = GameObject.FindGameObjectWithTag("Player"); // Find and assign the player GameObject using its tag
        StartCoroutine(FOVRoutine()); // Start the FOVRoutine coroutine
    }

    // Coroutine to repeatedly check the field of view
    private IEnumerator FOVRoutine() {
        // Create a WaitForSeconds object with a duration of 0.2 seconds
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        // Infinite loop to repeatedly check the field of view
        while (true) {
            yield return wait; // Wait for the specified duration
            FieldOfViewCheck(); // Call the FieldOfViewCheck method
        }
    }

    // Method to check the field of view
    private void FieldOfViewCheck() {
        // Use Physics.OverlapSphere to find colliders within the specified radius and target mask
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        // Check if there are any objects within the field of view
        if(rangeChecks.Length != 0) {
            // Get the transform of the first object within the field of view
            Transform target = rangeChecks[0].transform;
            // Calculate the normalized direction from the current position to the target position
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // Check if the angle between the forward direction and the direction to the target is within half of the specified angle
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2) {
                // Calculate the distance to the target
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                // Check for obstructions along the line of sight to the target
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) {
                    // Set the canSeePlayer boolean to true in the PatrolState script
                    patrolStateScript.canSeePlayer = true;

                } else patrolStateScript.canSeePlayer = false; // If there is an obstruction, set the canSeePlayer boolean to false

            } else patrolStateScript.canSeePlayer = false; // If the angle check fails, set the canSeePlayer boolean to false

        } else if (patrolStateScript.canSeePlayer) patrolStateScript.canSeePlayer = false; // If no objects are within the field of view, set the canSeePlayer boolean to false
    }
}
