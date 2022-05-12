using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentObstacle : MonoBehaviour {
    public float damp;
    public float displacement;
    float pingpong;
    float height;


    void Start() {
       height = transform.position.y;
    }

    void Update() {
        pingpong = Mathf.SmoothStep(-1, 1, Mathf.PingPong(Time.time / damp, 1));
        transform.localPosition = Vector3.up * height + transform.forward * pingpong * displacement;
    }
}
