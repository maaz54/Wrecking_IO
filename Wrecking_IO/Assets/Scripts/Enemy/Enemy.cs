using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    bool canPlay = false;
    bool canFollow = true;
    public float speed;
    public float rotSpeed;
    public LineRenderer line;
    public GameObject sphere;
    public Rigidbody rb;

    void Start()
    {
        GameManager.instance.levelFinish += LevelFinsih;
        GameManager.instance.gameStart += GameStart;
        target = Player.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPlay)
        {
            if (target)
            {
                if (canFollow)
                {
                    Vector3 dir = (target.position - transform.position);
                    dir.y = 0;
                    dir.Normalize();
                    transform.Translate(dir * speed * Time.deltaTime, Space.World);
                    if (dir != Vector3.zero)
                    {
                        Quaternion toRot = Quaternion.LookRotation(dir, Vector3.up);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, rotSpeed * Time.deltaTime);
                    }
                    Boundary();
                }
            }
            if (line)
            {
                line.SetPosition(0, transform.position);
                line.SetPosition(1, sphere.transform.position);
            }
        }
    }

    void Boundary()
    {
        Vector3 pos = transform.position;
        if (pos.x > 18)
        {
            pos.x = 18;
        }
        else if (pos.x < -18)
        {
            pos.x = -18;
        }

        if (pos.z > 18)
        {
            pos.z = 18;
        }
        else if (pos.z < -18)
        {
            pos.z = -18;
        }
        if (pos.y > .5f)
        {
            pos.y = .5f;
        }

        transform.position = pos;
    }






    public float sphereForce;
    public float MaxDamage;

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="col">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("sphere"))
        {

            if (canPlay)
            {
                Vector3 sphereVel = col.transform.GetComponent<sphere>().sphereForce;
                Vector3 dir = col.contacts[0].point - transform.position;

                dir.y = .5f;
                dir = -dir.normalized;
                // rb.velocity = (dir *sphereVel.x* sphereForce);
                if (sphereVel.x > MaxDamage || sphereVel.x < -MaxDamage)
                {
                    canFollow = false;
                    Destroy(transform.parent.gameObject, 9);
                    GameManager.instance.EnemyDead();
                    canPlay = false;
                    line.positionCount = 0;

                }
                if(sphereVel.x <0)
                {
                    sphereVel.x = sphereVel.x*-1;
                }
                rb.velocity = (dir * sphereVel.x);

            }
        }
    }

    void GameStart()
    {
        canPlay = true;
    }
    void LevelFinsih(bool isComplete)
    {
        canPlay = false;
    }
}

