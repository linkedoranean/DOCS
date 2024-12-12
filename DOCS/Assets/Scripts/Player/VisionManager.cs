using System;
using UnityEngine;

public class VisionManager : MonoBehaviour
{
    private LayerMask layerMask;
    [SerializeField] private float hitDistance;

    void Awake()
    {
        layerMask = LayerMask.GetMask("FloorTile");
        MovementInput.OnMoved += CastRaycast;
    }

    void OnDestroy()
    {
        MovementInput.OnMoved -= CastRaycast;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * hitDistance, Color.green);
    }

    void CastRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, hitDistance, layerMask))
        {
            hit.transform.GetComponent<TileManager>().DisplayTile();
            //Debug.LogError(hit.transform.name);
        }
    }
}