using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 vect = new Vector3(x, y, 0);
        vect = vect.normalized * (speed * Time.deltaTime);
        transform.position += vect;
    }
}
