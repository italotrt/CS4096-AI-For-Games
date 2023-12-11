using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An Abstract State Class that all the states
// of the AI in be derived from
public abstract class State : MonoBehaviour
{
    // Abstract method to be implemented inside
    // the scripts of the states of the AI
    public abstract State RunCurrentState();
}
