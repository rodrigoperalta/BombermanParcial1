using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUDManager : MonoBehaviour {
    
    public Text score;
    public Text lives;
    public Text enemiesKilled;
    public Text time;
   
	void Update () {  //Keeps HUD Updated
        score.text = "Score: " + Player.Get().ReturnScore().ToString();
        lives.text = "Lives: " + Player.Get().ReturnLives().ToString();
        enemiesKilled.text = "Enemies Killed: " + Player.Get().ReturnEnemiesKilled().ToString();
        time.text = "Time: " + Player.Get().ReturnTime().ToString();
    }

    
}
