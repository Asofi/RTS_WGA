using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

    private Material Mat;
    private float cutout = 1;
    private float targetCutout = 0;
    private Vector3 spawnPos;

	// Use this for initialization
	void Start () {
        Mat = GetComponent<Renderer>().sharedMaterial;
        StartCoroutine(Splashing());
    }

    IEnumerator Splashing()
    {
        float t = 0;
        while (t < 1)
        {
            float newSpeed = Mathf.Lerp(cutout, targetCutout, t);
            cutout = newSpeed;
            t += 0.01f * Time.deltaTime;
            Mat.SetFloat("_BackgroundCutoff", cutout);
            yield return new WaitForEndOfFrame();
        }
    }
}
