using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    NavMeshPath path;
    public float timeForNewpath;
    bool inCoRoutine;
    Vector3 target;
    bool validPath;
    public float speed = 7;
    public float distanceToStop = 2;
    public float distanceToRestart = 2;
    public float idleTime = 2;
    public Transform targetEnemy;
    public Transform targetEnemy1;
    public Transform targetEnemy2;

    private float t;

    [SerializeField] private EnemyState state;

    public enum EnemyState
    {
        Idle, //0
        GoingToTarget, //1
        GoAway, //2
        Last, //3
    }

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }

    private void Update()
    {

        transform.rotation = Quaternion.identity; //Keeps enemies earthed
        t += Time.deltaTime;
        switch (state) //Enemies AI StateMachine
        {
            case EnemyState.Idle:
                if (t > idleTime)
                {
                    NextState();
                }
                break;
            case EnemyState.GoingToTarget:
                if (!inCoRoutine)
                {
                    StartCoroutine(DoSomething());
                }
                if (targetEnemy != null)
                {
                    if (Vector3.Distance(transform.position, targetEnemy.position) < distanceToStop)
                        NextState();
                }
                if (targetEnemy1 != null)
                {
                    if (Vector3.Distance(transform.position, targetEnemy1.position) < distanceToStop)
                        NextState();
                }
                if (targetEnemy2 != null)
                {
                    if (Vector3.Distance(transform.position, targetEnemy2.position) < distanceToStop)
                        NextState();
                }
                break;
            case EnemyState.GoAway:
                Vector3 dir02;
                if (targetEnemy != null)
                    dir02 = transform.position - targetEnemy.position;
                else if (targetEnemy1 != null)
                    dir02 = transform.position - targetEnemy1.position;

                else
                    dir02 = transform.position - targetEnemy2.position;


                transform.Translate(dir02.normalized * speed * Time.deltaTime);
                if (targetEnemy != null)
                    if (Vector3.Distance(transform.position, targetEnemy.position) > distanceToRestart)
                        NextState();
                if (targetEnemy1 != null)
                    if (Vector3.Distance(transform.position, targetEnemy1.position) > distanceToRestart)
                        NextState();
                if (targetEnemy2 != null)
                    if (Vector3.Distance(transform.position, targetEnemy2.position) > distanceToRestart)
                        NextState();
                break;
        }


    }

    Vector3 GetNewRandomPosition() //get new position if obstacle
    {
        float x = Random.Range(-10, 10);
        float z = Random.Range(-10, 10);
        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }

    IEnumerator DoSomething()
    {
        inCoRoutine = true;
        yield return new WaitForSeconds(timeForNewpath);
        GetNewPath();
        validPath = navMeshAgent.CalculatePath(target, path);
        while (!validPath)
        {
            yield return new WaitForSeconds(0.0f);
            GetNewPath();
            validPath = navMeshAgent.CalculatePath(target, path);
        }
        inCoRoutine = false;
    }

    void GetNewPath() //gets new path
    {
        target = GetNewRandomPosition();
        navMeshAgent.SetDestination(target);
    }

    private void NextState() //goes to next state
    {
        t = 0;
        int intState = (int)state;
        intState++;
        intState = intState % ((int)EnemyState.Last);
        SetState((EnemyState)intState);
    }

    private void SetState(EnemyState es) //Sets state
    {
        state = es;
        switch (state)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.GoingToTarget:
                break;
            case EnemyState.GoAway:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
            Player.Get().Die();
    }

    private void OnDestroy()
    {
        Player.Get().AddEnemiesKilled();
    }
}


