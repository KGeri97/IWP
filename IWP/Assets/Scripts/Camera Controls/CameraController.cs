using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    Vector3 _mouseScrollScrollStartPos;
    Camera _camera;
    // [SerializeField] int _borderSize = 100;
    // [SerializeField] float ScrollSpeed = 0.1f; 
    [SerializeField] private float _zoomSpeed = 50f;
    public float minCameraSize = 2f, maxCameraSize = 10f;

    public Transform target; // The object the camera should follow
    public Vector2 maxPosition; // The maximum position the camera can reach
    private Vector2 _initialMaxPosition;
    
    void Start()
    { 
        _camera = GetComponent<Camera>();
        _initialMaxPosition = maxPosition;
    }
    
    void LateUpdate()
    {
        UpdateMaxPosition();
        
        if (target != null)
        {
            // Get the current position of the camera
            Vector3 newPosition = transform.position;

            // Clamp the camera's position within the maximum thresholds
            newPosition.x = Mathf.Clamp(target.position.x, -maxPosition.x, maxPosition.x);
            newPosition.z = Mathf.Clamp(target.position.z, -maxPosition.y, maxPosition.y);

            // Update the camera's position
            transform.position = newPosition;
        }
    }
    
    private void UpdateMaxPosition()
    {
        if (minCameraSize <= 0)
        {
            Debug.LogError("minCameraSize must be greater than zero.");
            return;
        }

        float sizeRatio = Mathf.Max(_camera.orthographicSize / minCameraSize, 1f);
        maxPosition = new Vector2(_initialMaxPosition.x * sizeRatio, _initialMaxPosition.y * sizeRatio);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMiddleMouseScrolling();

        HandleWheelZoom();

        ClampCameraPosition();
        
        // RestrictToMapRect();
    }

    private void HandleWheelZoom() 
    {
        if (Input.mouseScrollDelta.y != 0) 
        { 
            _camera.orthographicSize -= Input.mouseScrollDelta.y * Time.deltaTime * _zoomSpeed;
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, minCameraSize, maxCameraSize);
        }
    }

    private void HandleMiddleMouseScrolling() {
        if (Input.GetMouseButtonDown(2))
        {
            _mouseScrollScrollStartPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 movement= Vector3.zero;
            movement = _camera.ScreenToWorldPoint(Input.mousePosition) - _mouseScrollScrollStartPos;
            _camera.transform.position -= movement;
        }
    }
    
    private void ClampCameraPosition()
    {
        Vector3 position = _camera.transform.position;
        position.x = Mathf.Clamp(position.x, -maxPosition.x, maxPosition.x);
        position.z = Mathf.Clamp(position.z, -maxPosition.y, maxPosition.y);
        _camera.transform.position = position;
    }
}
