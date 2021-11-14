using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackground : MonoBehaviour
{
    private float speed = 4f;

    //Moving background 
    void Update()
    {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            if (transform.position.x <= -23.6f) transform.position = new Vector3(23.5f, transform.position.y, transform.position.z);

    }
}
