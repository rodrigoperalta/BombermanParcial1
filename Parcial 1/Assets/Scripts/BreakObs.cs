using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObs : MonoBehaviour {
    private void OnDestroy() //Adds score when destroyed
    {
        Player.Get().AddScore();
    }
}
