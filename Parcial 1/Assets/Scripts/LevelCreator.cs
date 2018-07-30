using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{

    public GameObject templates;
    public int cantBreakObs = 10;
    public GameObject obs;
    private Vector3 obsPos;
    public static LevelCreator lC2;
    private int doorLocation;
    public GameObject door;
    private bool isDoorPlaced;
    private Vector3 auxPos;

    public static LevelCreator Get()
    {
        return lC2;
    }


    void Start()  //Creates level = breakable objects, unbreackable objects and door
    {
        isDoorPlaced = false;
        obsPos = obs.transform.position;
        int n;
        lC2 = this;
        doorLocation = Random.Range(-1, 15);
        for (int i = -1; i < 16; i++)
        {
            for (int h = -1; h < 16; h++)
            {
                n = Random.Range(0, 10);
                if (n == 5)
                {
                    if (h % 2 != 0)
                    {
                        Vector3 pos = new Vector3(obsPos.x + i, obsPos.y, obsPos.z - h);
                        Instantiate(templates, pos, Quaternion.identity);
                        if (h == doorLocation && !isDoorPlaced)
                        {
                            Instantiate(door, pos, Quaternion.identity);
                            isDoorPlaced = true;
                        }
                    }
                    else
                    {
                        if (i % 2 != 0)
                        {
                            Vector3 pos = new Vector3(obsPos.x + i, obsPos.y, obsPos.z - h);
                            Instantiate(templates, pos, Quaternion.identity);
                            auxPos = pos;
                            if (i == doorLocation && !isDoorPlaced)
                            {
                                Instantiate(door, pos, Quaternion.identity);
                                isDoorPlaced = true;
                            }
                        }
                    }
                }                
            }
        }
        if (isDoorPlaced == false)
            Instantiate(door, auxPos, Quaternion.identity);
    }




}
