using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))] // Create a custom editor for the FieldOfView script

// Define the FieldOfViewEditor class, inheriting from the Editor class
public class FieldOfViewEditor : Editor
{
    // Called when the Scene view is being rendered
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target; // Get the FieldOfView component attached to the inspected GameObject
        Handles.color = Color.white; // Set the Handles color to white
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius); // Draw a wire arc representing the field of view in the Scene view

        // Calculate the two vector endpoints of the field of view angle
        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow; // Set Handles color to yellow

        // Draw lines representing the edges of the field of view angle
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.radius);

        // Check if the player is within the field of view
        if (fov.patrolStateScript.canSeePlayer)
        {
            Handles.color = Color.green; // Set Handles color to green
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position); // Draw a line from the FieldOfView's position to the player's position
        }
    }

    // Calculate a direction vector from an angle in degrees
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY; // Adjust the angle by adding the object's rotation in the Y-axis

        // Calculate the direction vector using trigonometric functions
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
