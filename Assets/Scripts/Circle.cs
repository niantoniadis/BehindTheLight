using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    Circle[] subColliders;
    Vector3 position;
    float radius;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        radius = GetComponent<SpriteRenderer>().bounds.size.x/2;
        subColliders = GetComponentsInChildren<Circle>();
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
    }

    public bool IsCollidingWith(Circle collider)
    {
        if (collider.radius + radius > Mathf.Abs(Vector3.Distance(position, collider.position)))
        {
            if (collider.subColliders.Length > 0 || subColliders.Length > 0)
            {
                if (checkSubs(collider))
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    private bool checkSubs(Circle main)
    {
        if (subColliders.Length > 0 && main.subColliders.Length > 0)
        {
            for (int i = 0; i < subColliders.Length; i++)
            {
                for (int j = 0; j < main.subColliders.Length; j++)
                {
                    if (subColliders[i].radius + main.subColliders[j].radius > Mathf.Abs(Vector3.Distance(subColliders[i].position, main.subColliders[j].position)))
                    {
                        return true;
                    }
                }
            }
        }
        else if (subColliders.Length > 0)
        {
            for(int i = 0; i < subColliders.Length; i++)
            {
                if(subColliders[i].radius + main.radius > Mathf.Abs(Vector3.Distance(subColliders[i].position, main.position)))
                {
                    return true;
                }
            }
        }
        else if (main.subColliders.Length > 0)
        {
            for (int i = 0; i < main.subColliders.Length; i++)
            {
                if (main.subColliders[i].radius + radius > Mathf.Abs(Vector3.Distance(position, main.subColliders[i].position)))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
