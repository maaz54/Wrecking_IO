using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public static Player instance;
    public Joystick joystick;
    bool canPlay = false;
    public float speed = 5;
    public float rotSpeed = 5;
    public bool dragBegins = false;
    public LineRenderer line;
    public GameObject sphere;
    public joint joint;
     public Rigidbody rigidbody;
    private void Awake()
    {
        
        instance = this;
    }
    void Start()
    {
        GameManager.instance.levelFinish += LevelFinsih;
        GameManager.instance.gameStart += GameStart;
    }
    void Update()
    {
        if (canPlay)
        {
            Movement();

            //transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
        }
        if (line)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, sphere.transform.position);
        }
    }

    void Movement()
    {

        float horizontal = joystick.Horizontal;
        float vertital = joystick.Vertical;
        Vector3 moveDirection = new Vector3(horizontal, 0, vertital);
        moveDirection.Normalize();
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRot = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, rotSpeed * Time.deltaTime);
        }
        Boundary();
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


    public void MouseDown()
    {
    }
    public void MouseUp()
    {
    }
    public void OnDrageBegins()
    {
        dragBegins = true;
    }
    public void OnDrageEnds()
    {
        dragBegins = false;
    }


    void GameStart()
    {
        canPlay = true;
    }
    void LevelFinsih(bool isComplete)
    {
        canPlay = false;
        rigidbody.isKinematic = true;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("power"))
        {
            Destroy(col.gameObject);
            joint.Power();
        }
    }


}
