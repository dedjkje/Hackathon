using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon1Trajectory : MonoBehaviour
{
    public TrajectoryRenderer trajectoryRenderer;
    public Transform origin;
    public UseCannons useCannons;
    public Cannon1Stats cannon1Stats;
    public bool draw = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (draw) // убрать часьб после первого &&
        {
            trajectoryRenderer.ShowTrajetory(origin.position, origin.forward * cannon1Stats.force / 1); // p = mv
        }
        if (cannon1Stats != null) // убрать
        {
            if (cannon1Stats.onCooldown) // убрать
            {
                trajectoryRenderer.UnshowTrajetory(); // убрать
            }
        }
    }
    public void OnCannon()
    {
        useCannons = GameObject.FindWithTag("Player2").GetComponent<UseCannons>();
        cannon1Stats = GameObject.FindWithTag(useCannons.currentTag).GetComponent<Cannon1Stats>();
        origin = GameObject.FindWithTag(useCannons.currentTag).transform.Find("cannon").transform.Find("stvol").transform.Find("ShellPos").transform;
        trajectoryRenderer = GameObject.FindWithTag("Trajectory").GetComponent<TrajectoryRenderer>();
        draw = true;
    }
    public void OutCannon()
    {
        trajectoryRenderer.UnshowTrajetory();
        draw = false;
    }
}
