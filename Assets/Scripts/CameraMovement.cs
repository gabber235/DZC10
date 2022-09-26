using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    public List<Transform> players;
    public Vector3 offset;
    
    public float smoothTime = 0.5f;
    private Vector3 _velocity = Vector3.zero;
    
    public float minZoom = 40f;
    public float maxZoom = 10f;
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        var centerPoint = GetCenterPoint();
        var newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref _velocity, 0);
    }

    void LateUpdate()
    {
        var centerPoint = GetCenterPoint();
        var newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref _velocity, smoothTime);
        
        var newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / (70*70));
        _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, newZoom, Time.deltaTime);
    }

    Vector3 GetCenterPoint()
    {
        if (players.Count == 1)
        {
            return players[0].position;
        }
        
        var bounds = new Bounds(players[0].position, Vector3.zero);
        foreach (var player in players)
        {
            bounds.Encapsulate(player.position);
        }
        return bounds.center;
    }
    
    float GetGreatestDistance()
    {
        var bounds = new Bounds(players[0].position, Vector3.zero);
        foreach (var player in players)
        {
            bounds.Encapsulate(player.position);
        }
        // Diagonal of the xz plane
        return bounds.size.x*bounds.size.x + bounds.size.z*bounds.size.z;
    }
}
