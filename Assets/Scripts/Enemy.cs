using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    BABY, LURKER, BIGGESTBRAINIST
}

public class Enemy : Vehicle
{
    protected EnemyType behavior;
    // Start is called before the first frame update
    void Start()
    {
        mass = 10;
        MAX_SPEED = 4f;
        int type = Random.Range(1, 3);
        if(type == 1)
            behavior = EnemyType.BABY;
        else if(type == 2) 
            behavior = EnemyType.LURKER;
        else
            behavior = EnemyType.BIGGESTBRAINIST;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement();
    }

    public void Move(Player player)
    {
        if(behavior == EnemyType.BABY)
        {
            //if not light
            Seek(player);
        }
    }


}
