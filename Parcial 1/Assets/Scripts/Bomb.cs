using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float rayDistance = 1;
    public LayerMask rayCastLayer;
    private float t = 0;
    Collider cCollider;
    private float distanceToPlayer = 0.7f;

    void Start()
    {
        cCollider = GetComponent<Collider>();       
    }

    void Update()
    {
        t += Time.deltaTime;
        KillBreakObs();
        IsUnder();        
        Die();
    }

    void KillBreakObs() //Raycast in 4 directions to see what it hits
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, rayCastLayer))
            SwitchLayeredHit(hit);
        if (Physics.Raycast(transform.position, transform.forward * -1, out hit, rayDistance, rayCastLayer))
            SwitchLayeredHit(hit);
        if (Physics.Raycast(transform.position, transform.right, out hit, rayDistance, rayCastLayer))
            SwitchLayeredHit(hit);
        if (Physics.Raycast(transform.position, transform.right * -1, out hit, rayDistance, rayCastLayer))
            SwitchLayeredHit(hit);
    }

    void IsUnder() //Turns on collider when player moves away from it
    {
        Vector3 aux;
        aux = this.transform.position;
        if (aux.x - Player.Get().GetPos().x > distanceToPlayer || aux.x - Player.Get().GetPos().x < -distanceToPlayer || aux.z - Player.Get().GetPos().z > distanceToPlayer || aux.z - Player.Get().GetPos().z < -distanceToPlayer)
            cCollider.enabled = true;
    }

    void Die()
    {        
        Destroy(this.gameObject, 2);        
    }

    void SwitchLayeredHit(RaycastHit hit) //Depending on what it hits after 2 seconds, does something
    {
        string layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);

        switch (layerHitted)
        {
            case "Player":
                if (t >= 2)                
                    Player.Get().Die();  
                break;
            case "BreakObs":                
                Destroy(hit.collider.gameObject, 2);               
                break;
            case "Enemy":
                if (t >= 2)
                    Destroy(hit.collider.gameObject);
                break;
        }
    }













    void Center()
    {
        Vector3 aux;

        if ((int)this.transform.position.z % 2 != 0)
        {
            aux.z = (int)this.transform.position.z;
            if (this.transform.position.x >= 0)
                aux.x = (int)this.transform.position.x + 0.78f;
            else
                aux.x = (int)this.transform.position.x - 0.4f;
            aux.y = this.transform.position.y;
            this.transform.position = aux;
        }

        if ((int)this.transform.position.z % 2 == 0)
        {
            if ((int)this.transform.position.z >= 0)
                aux.z = (int)this.transform.position.z;
            else
            {
                if ((int)this.transform.position.z % 2 != 0)
                    aux.z = (int)this.transform.position.z - 1;

                else
                    aux.z = (int)this.transform.position.z - 1;
            }

            if ((int)this.transform.position.x % 2 != 0)
            {
                if (this.transform.position.x >= 0)
                    aux.x = (int)this.transform.position.x + 0.78f;
                else
                    aux.x = (int)this.transform.position.x - 0.4f;
            }
            else
            {
                if (this.transform.position.z >= 0)
                    aux.x = (int)this.transform.position.x + 1.78f;
                else
                    aux.x = (int)this.transform.position.x - 1.4f;
            }
            aux.y = this.transform.position.y;
            this.transform.position = aux;
        }
    }
}
