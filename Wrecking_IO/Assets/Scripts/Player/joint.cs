using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joint : MonoBehaviour
{
    public Transform player;
    bool canPlay = false;
    public Quaternion targetRot;
    public float rotateSpeed;
    public float powerStrenght;
    float power=0;
    public bool rotatePower = false;
    public sphere sphere;
     Vector3 spherePos;
     
    void Start()
    {
        GameManager.instance.levelFinish += LevelFinsih;
        GameManager.instance.gameStart += GameStart;
        spherePos = sphere.transform.localPosition;
    }
    void Update()
    {
        if (canPlay)
        {
            transform.position = player.transform.position;
            if (rotatePower)
            {
                transform.Rotate(Vector3.up * power * Time.deltaTime);
                power -= 50 * Time.deltaTime; 
                if(power < 5)
                rotatePower = false;
            }
            else
            {
                targetRot = player.transform.rotation;
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
                
  
            }
                sphere.transform.localPosition =spherePos;
        }
    }


[ContextMenu("applyPower")]
    public void Power()
    {
rotatePower=true;
power = powerStrenght;
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
