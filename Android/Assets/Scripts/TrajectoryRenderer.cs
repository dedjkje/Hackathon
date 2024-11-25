using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Vector3[] points;
    public int pointNumber;

    // Start is called before the first frame update
    void Start()
    {

        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowTrajetory(Vector3 origin, Vector3 speed)
    {
        lineRenderer.enabled = true;
        points = new Vector3[pointNumber]; // длинна линии
        lineRenderer.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;
            points[i] = origin + speed * time + Physics.gravity * time * time / 2f;
        }
        lineRenderer.SetPositions(points);
    }
    public void UnshowTrajetory()
    {
        GetComponent<LineRenderer>().enabled = false;
    }
}
