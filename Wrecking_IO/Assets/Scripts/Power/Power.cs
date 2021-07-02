using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Power : MonoBehaviour
{
    public Vector3 instantiatPos;
    public GameObject parachut;
    public Rigidbody rigidbody;
    public void INIT()
    {
         //y 0.6;
        transform.position = new Vector3(instantiatPos.x,instantiatPos.y+20,instantiatPos.z);
         instantiatPos.y =0.6f;
        transform.DOMove(instantiatPos,5).OnComplete(()=>{
            parachut.gameObject.SetActive(false);
            rigidbody.isKinematic = false;
        });
    }
}
