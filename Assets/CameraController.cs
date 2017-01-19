using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Vector2 mousePos;
    Vector3 newPos;
    public Transform cameraHolder;
    public float speed;
	// Use this for initialization
	void Start () {
        newPos = cameraHolder.position;
	}
	
	// Update is called once per frame
	void Update () {
        mousePos = Input.mousePosition;

        if (mousePos.y < 100)
            newPos = new Vector3(newPos.x, newPos.y, newPos.z - speed * Time.deltaTime);
        if (mousePos.y > Screen.height - 100)
            newPos = new Vector3(newPos.x, newPos.y, newPos.z + speed * Time.deltaTime);
        if (mousePos.x < 100)
            newPos = new Vector3(newPos.x - speed * Time.deltaTime, newPos.y, newPos.z);
        if (mousePos.x > Screen.width - 100)
            newPos = new Vector3(newPos.x + speed * Time.deltaTime, newPos.y, newPos.z);
        cameraHolder.position = newPos;
	}
}
