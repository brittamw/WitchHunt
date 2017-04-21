using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWitch : MonoBehaviour {
    /*public Transform target;
    public float speed;
    public GameObject witch;
    private Vector3 startPos;*/
    private Vector3 endPos;
    // Use this for initialization
    void Start () {
        //startPos = witch.transform.position;
        endPos = new Vector3(32.5f, 34.0f, 37.55f);
        //speed = 5;
    }
       
    /*void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }*/
    public Transform target;
    public float speed;
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, endPos, step);
    }
}
