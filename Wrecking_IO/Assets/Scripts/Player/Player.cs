using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public static Player instance;
    public Joystick joystick;
    bool canPlay = false;
    bool canMove = false;
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
    // void Update()
    void FixedUpdate()
    {
        if (canPlay)
        {
            if (Input.GetMouseButton(0))
            {
                if(canMove)
                Movement();
            }
             Boundary();
            //transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
        }
        if (line)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, sphere.transform.position);
        }
    }
    float horizontal;
    float vertital;
    void Movement()
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            horizontal = joystick.Horizontal;
            vertital = joystick.Vertical;
        }


        Vector3 moveDirection = new Vector3(horizontal, 0, vertital);
        moveDirection.Normalize();
        // transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        rigidbody.AddForce(moveDirection * speed * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRot = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, rotSpeed * Time.deltaTime);
        }
       
    }
    void Boundary()
    {
        Vector3 pos = transform.position;
        // if (pos.x > 18)
        // {
        //     pos.x = 18;
        // }
        // else if (pos.x < -18)
        // {
        //     pos.x = -18;
        // }

        // if (pos.z > 18)
        // {
        //     pos.z = 18;
        // }
        // else if (pos.z < -18)
        // {
        //     pos.z = -18;
        // }
        if (pos.y > .5f)
        {
            pos.y = .5f;
        }
        if (pos.y < -3)
        {
            GameManager.instance.LevelFinsih(false);
        }
        transform.position = pos;
    }
    public void GetDamage()
    {
        if (getDamageCoroutine != null)
        {
            StopCoroutine(getDamageCoroutine);
        }
        getDamageCoroutine = StartCoroutine(iGetDamage());
    }
    Coroutine getDamageCoroutine;
    IEnumerator iGetDamage()
    {
        canMove=false;
        yield return new WaitForSeconds(2);
        rigidbody.velocity = Vector3.zero;
      canMove=true;

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
        canMove=true;
    }
    void LevelFinsih(bool isComplete)
    {
        canPlay = false;
        canMove=true;
        // if(isComplete)
        // rigidbody.isKinematic = true;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("power"))
        {
            Destroy(col.transform.parent.gameObject);
            joint.Power();
        }
    }


}
