using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{

    [SerializeField]
    private float margin = 0, lerpSpeed = 10;

    private Camera cam;

    private OrthographicCameraFitter fitter;

    private Vector3 targetPosition;
    private float targetSize;

    [SerializeField]
    private OrthographicCameraFitter.Sticky stickySide;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        fitter = new OrthographicCameraFitter(cam);
        fitter.stickySide = stickySide;
        fitter.margin = margin;
        targetPosition = transform.position;
        targetSize = cam.orthographicSize;
        StartCoroutine(CameraZoomRoutine());
    }

    public void Focus(Bounds bounds)
    {
        fitter.SetObject(bounds);
        targetSize = fitter.CalculateCameraSize();
        targetPosition = fitter.CalculateCameraPosition().ToVector3(transform.position.z);
    }

    public void FocusInstant(Bounds bounds)
    {
        this.Focus(bounds);
        ReachTarget();
    }

    private IEnumerator CameraZoomRoutine()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, lerpSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    private void ReachTarget(){
        transform.position = targetPosition;
        cam.orthographicSize = targetSize;
    }

}