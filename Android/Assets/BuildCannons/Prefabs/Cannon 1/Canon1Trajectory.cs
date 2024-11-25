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

    Alteruna.Avatar avatar;
    // Start is called before the first frame update
    void Start()
    {
        avatar = GetComponent<Alteruna.Avatar>();
        if (!avatar.IsMe) return;
    }

    // Update is called once per frame
    void Update()
    {
        if (!avatar.IsMe) return;
        if (draw)
        {
            trajectoryRenderer.ShowTrajetory(origin.position, origin.forward * cannon1Stats.force / 1); // p = mv
        }
    }
    public void OnCannon()
    {
        useCannons = GameObject.FindWithTag("Player1").GetComponent<UseCannons>();
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
