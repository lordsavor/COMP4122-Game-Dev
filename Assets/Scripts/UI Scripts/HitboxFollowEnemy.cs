using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxFollowEnemy : MonoBehaviour
{    
    public GameObject target;
    public Transform enemyLookAt;
    public Vector3 coords;
    Vector3 belowCamera;
    private Camera cam;

    void Start() { 
        cam = Camera.main;
        belowCamera = new Vector3(0, -1000, 0);
    }
    
    void Update()
    {
        try {
            var targetRender = target.GetComponent<Renderer>();
            if (IsVisible(target)) {
                Vector3 pos = cam.WorldToScreenPoint(enemyLookAt.position + coords);
                    if (transform.position != pos) transform.position = pos;
            }
            else {
                transform.position = cam.transform.position + belowCamera;
            }
        } catch{
            Destroy(this.gameObject);
        }
    }

    private bool IsVisible(GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(cam);
        var point = target.transform.position;

        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point)< 0)
            {
                return false;
            }
        }
        return true;
    }
}
