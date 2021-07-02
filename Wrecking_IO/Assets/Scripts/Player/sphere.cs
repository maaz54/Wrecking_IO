using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere : MonoBehaviour
{
    public Vector3 sphereForce;
    Vector3 currPos;
    Vector3 previousPos;
    public float Force;
    public float speed;
    Vector3 initPOs;
    public float maxZ;
    public float minZ;
    private void Start()
    {
        initPOs = transform.localPosition;
    }
    private void Update()
    {
        currPos = transform.position;
        sphereForce.x = (currPos - previousPos).magnitude / Time.deltaTime;
        previousPos = currPos;
    }
    void OnCollisionEnter(Collision col)
    {
        Debug.LogError(" OnCollisionEnter Spehere Force : " + sphereForce);
        if (col.gameObject.CompareTag("enemy"))
        {
            col.gameObject.GetComponent<Enemy>().GetDamage();
        }
        Vector3 dir = col.contacts[0].point - transform.position;
        dir.y = .1f;
        dir = dir.normalized;

        // if(sphereForce.x <0)
        // {
        //     sphereForce.x = sphereForce.x *-1;
        // }
        // if(sphereForce.x >.4f)
        // {
        //     sphereForce.x =1;
        // }
        if (col.gameObject.CompareTag("enemy"))
        {
        col.gameObject.GetComponent<Rigidbody>().AddForce(dir * Force * sphereForce.x);

        }
        else
        {
        col.gameObject.GetComponent<Rigidbody>().AddForce(dir * Force * sphereForce.x/2);

        }


    }
}
