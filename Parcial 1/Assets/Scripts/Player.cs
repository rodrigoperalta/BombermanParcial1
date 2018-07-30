using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float horizontal;
    float vertical;
    public float speed;
    public GameObject bomb;
    Vector3 grounded;
    public int lives;
    Vector3 originalPos;
    public static Player player;
    public GameObject winScreen;
    public GameObject loseScreen;
    GameObject[] enemies;
    private int score;
    private int enemiesKilled;
    private float time;
    private bool fps;
    private BoxCollider compCollider;

    //FPS Variables
    private CharacterController charController;
    public float gravity = -9.8f;
    private float fPSSpeed = 1.0f;



    public static Player Get()
    {
        return player;
    }


    void Start() //gets component and positions, sets the game to be started as Third Person
    {
        charController = GetComponent<CharacterController>();
        compCollider = GetComponent<BoxCollider>();
        grounded.y = this.transform.position.y;
        originalPos = this.transform.position;
        time = 0;
        fps = false;
    }

    private void Awake() //Deactivates win and lose screens
    {
        player = this;
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }


    void Update()
    {
        Physics.IgnoreCollision(charController, compCollider); //Ignores collision between character controller and collider
        SwitchThirdToFPS(); //Switche between Third and First person controllers
        if (fps == false) 
        {
            StayEarthed();
            ThirdPersonKeyInput(); //Third Person Controller
        }
        if (fps == true) 
            FPSKeyInput(); //FPS controller
        DropBomb(); //Drops Bombs
        enemies = GameObject.FindGameObjectsWithTag("Enemy"); //Knows how many enemies there are
        time = time + 1 * Time.deltaTime;
    }

    void SwitchThirdToFPS()
    {
        if (Input.GetKeyDown("1"))
            fps = false;
        if (Input.GetKeyDown("2"))
            fps = true;
    }

    void ThirdPersonKeyInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector3 movement = Vector3.zero;
        movement.x = vertical;
        movement.z = -horizontal;
        transform.position += movement * speed * Time.deltaTime;
    }

    void FPSKeyInput()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * 10, 0);
        horizontal = Input.GetAxis("Horizontal") * fPSSpeed;
        vertical = Input.GetAxis("Vertical") * fPSSpeed;
        Vector3 fPSMovement = new Vector3(horizontal, 0, vertical);
        fPSMovement = Vector3.ClampMagnitude(fPSMovement, fPSSpeed); //Limits Speed
        fPSMovement.y = gravity;
        fPSMovement *= Time.deltaTime;
        fPSMovement = transform.TransformDirection(fPSMovement);
        charController.Move(fPSMovement);
    }

    void DropBomb() //Drops bombs when mouse left click, limit 1 bomb
    {
        if (Time.timeScale == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!GameObject.Find("Bomb(Clone)"))
                    Instantiate(bomb, this.transform.position, Quaternion.identity);
            }
        }
    }

    void StayEarthed() //Keeps the player earthed
    {
        transform.rotation = Quaternion.identity;

    }

    public void Die() //Resetes position when hit, ends game when out of lives
    {
        if (lives > 0)
        {
            this.transform.position = originalPos;
            lives--;
        }
        else
            Lose();
    }

    public Vector3 GetPos()
    {
        return this.transform.position;
    }

    private void OnCollisionEnter(Collision collision) //If there are no enemies and collides with door, Win
    {
        if (collision.gameObject.tag == "Door" && enemies.Length == 0)
            Win();
    }

    private void Win()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }

    private void Lose()
    {
        Time.timeScale = 0;
        loseScreen.SetActive(true);
    }

    public void AddScore()
    {
        score += 10;
    }

    public void AddEnemiesKilled()
    {
        enemiesKilled += 1;
    }

    public int ReturnScore()
    {
        return score;
    }

    public int ReturnLives()
    {
        return lives;
    }

    public int ReturnEnemiesKilled()
    {
        return enemiesKilled;
    }

    public int ReturnTime()
    {
        return (int)time;
    }

    public bool IsFPSActive()
    {
        return fps;
    }
}
