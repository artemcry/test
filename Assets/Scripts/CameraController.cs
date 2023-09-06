using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smooth = 5.0f;
    public Vector3 offset;
    GameObject player;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, Time.deltaTime * smooth);
    }
    void Start()
    {
        player = GameObject.Find("Player");        
    }
}
