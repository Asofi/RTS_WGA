using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashController : MonoBehaviour {

    public Material SplashMat;
    public float Radius;
    public Vector3 Center;
    public float TargetRadius;
    public Texture MainTex;
    public RenderTexture render;

	void Start () {
        MainTex = SplashMat.GetTexture("_MainTex");
	}
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            Radius = 0;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                Center = hit.point;

            SplashMat.SetVector("_Center", Center);
            StartCoroutine(Splashing());
        }
        SplashMat.SetFloat("_Radius", Radius);
    }

    IEnumerator Splashing()
    {
        float t = 0;
        while (t < 1)
        {
            float newSpeed = Mathf.Lerp(Radius, TargetRadius, t);
            Radius = newSpeed;
            t += 0.1f * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        SplashMat.SetTexture("_MainTex", MainTex);
        Shit();
    }

    void Shit()
    {
        var mat = new Material(Shader.Find("Custom/Splash"));
        Graphics.Blit(MainTex, render, mat);
    }
}
