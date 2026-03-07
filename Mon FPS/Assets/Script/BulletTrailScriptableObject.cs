using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BulletTrail", menuName = "ScriptableObjects/Bullet Trail Config ")]

public class BulletTrailScriptableObject : ScriptableObject
{
    public AnimationCurve WidthCurve;
    public float Time = 0.5f;
    public float MinvertexDistance = 0.1f;
    public Gradient ColorGradient;
    public Material Material;
    public int CornerVertices;
    public int EndCapVertices;

    public void Setuptrail(TrailRenderer TrailRenderer)
    {
        TrailRenderer.widthCurve = WidthCurve;
        TrailRenderer.time = Time;
        TrailRenderer.minVertexDistance = MinvertexDistance;
        TrailRenderer.colorGradient = ColorGradient;
        TrailRenderer.sharedMaterial = Material;
        TrailRenderer.numCornerVertices = CornerVertices;
        TrailRenderer.numCapVertices = EndCapVertices;
    }
}
