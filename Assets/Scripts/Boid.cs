using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : Vehicle
{


    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        direction.Normalize();
        position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
