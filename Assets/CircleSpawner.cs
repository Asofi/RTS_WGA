using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour {

    public Transform Prefab;
    public float Rate;
    private float timer;
    private float nextTimer;

    private void Start()
    {
        timer = Time.timeSinceLevelLoad;
        nextTimer = timer + Rate;
    }

    // Update is called once per frame
    void Update () {
        timer = Time.timeSinceLevelLoad;
        if (timer < nextTimer)
        {
            if (Input.GetMouseButton(0))
            {

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    Transform obj = Instantiate(Prefab);
                    Vector3 newPos = new Vector3(hit.point.x, hit.point.y + 1 , hit.point.z);
                    obj.position = newPos;
                    print(hit.transform.name);
                }
            }
        }
        else
            nextTimer = timer + Rate;
            

    }
}
