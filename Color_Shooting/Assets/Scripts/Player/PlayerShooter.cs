using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;

    Vector3 aimPoint;

    private Camera cam;
    public bool fire { get; private set; }
    public bool reload { get; private set; }

    private void Awake()
    {
        cam = Camera.main;
    }
    public void UpdateAimTarget()
    {
        RaycastHit hit;
        var ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if(Physics.Raycast(ray, out hit, gun.fireDistance))
        {
            aimPoint = hit.point;
        }
        else
        {
            aimPoint = cam.transform.position + cam.transform.forward * gun.fireDistance;
        }
    }
    private void Update()
    {
        fire = Input.GetButtonDown("Fire1");
        reload = Input.GetButtonDown("Reload");

        UpdateAimTarget();
        if (fire)
        {
            gun.Fire(aimPoint);
        }
        else if (reload)
        {
            if (gun.Reload()) Debug.Log("reload");
        }
    }
}
