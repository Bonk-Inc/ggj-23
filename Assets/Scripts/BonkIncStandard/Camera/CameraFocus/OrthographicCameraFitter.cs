
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthographicCameraFitter
{
    
    private Bounds bounds;

    private Camera camera;

    private Vector2 offset;

    public Sticky stickySide = Sticky.NONE;

    public float margin = 0;

    public OrthographicCameraFitter(Camera camera){
        this.camera = camera;
    }

    public void SetObject(Bounds fitObject){
        bounds = fitObject;
    }

    public float CalculateCameraSize(){
        offset = Vector2.zero;
        float size;
        float boundsAspect = bounds.size.x / bounds.size.y;
        if (boundsAspect < camera.aspect)
        {
            size = bounds.size.y;
        }
        else
        {
            size = bounds.size.y / camera.aspect * boundsAspect;
            if(stickySide == Sticky.TOP){
                offset = Vector2.up * (((bounds.size.y - size) / 2) - margin);     
            } else if (stickySide == Sticky.BOTTOM){
                offset = Vector2.down * (( (bounds.size.y - size) / 2) - margin);            
            }
            
        }
        return (size / 2) + margin;
    }

    public Vector2 CalculateCameraPosition(){
        return bounds.center + offset.ToVector3();
    }

    public enum Sticky {
        NONE,
        TOP,
        BOTTOM
    }

}