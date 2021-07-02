using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joint : MonoBehaviour
{
    public Transform player;
    bool canPlay = false;
    public Quaternion targetRot;
    public float rotateSpeed;
    public float Speed;
    public float powerStrenght;
    public float powertime;
    float power = 0;
    public bool rotatePower = false;
    public sphere sphere;
    Vector3 spherePos;
    public float maxSphereSpringLimit;
    public float minSphereSpringLimit;

    void Start()
    {
        GameManager.instance.levelFinish += LevelFinsih;
        GameManager.instance.gameStart += GameStart;
        spherePos = sphere.transform.localPosition;
    }
    void Update()
    {
        // if (canPlay)
        {
            transform.position = player.transform.position;
            float dist = Vector3.Distance(transform.localEulerAngles, player.localEulerAngles);

            // transform.position =Vector3.Lerp(transform.position,player.transform.position,Speed*Time.deltaTime);
            if (rotatePower)
            {
                transform.Rotate(Vector3.up * powerStrenght * Time.deltaTime);
                power += Time.deltaTime;
                if (power > powertime)
                {
                    rotatePower = false;
                    power=0;
                }
            }
            else
            {
                targetRot = player.transform.rotation;
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);

            }
            sphere.transform.localPosition = Vector3.Lerp(sphere.transform.localPosition, spherePos, Speed * Time.deltaTime);
            //    SphereSprintLimit();
            // sphere.transform.localPosition =spherePos;
        }
    }

    void SphereSprintLimit()
    {
        Vector3 p = sphere.transform.localPosition;
        if (p.z < maxSphereSpringLimit)
        {
            p.z = maxSphereSpringLimit;
            Debug.LogError("maxSphereSpringLimit");
        }
        else if (p.z > minSphereSpringLimit)
        {
            p.z = minSphereSpringLimit;
            Debug.LogError("minSphereSpringLimit");
        }
        sphere.transform.localPosition = p;
    }

    [ContextMenu("applyPower")]
    public void Power()
    {
        rotatePower = true;
        power = 0;
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
