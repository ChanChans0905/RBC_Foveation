using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GazePointCoorToShader_2D : MonoBehaviour
{
    [SerializeField] RayCastToGazeTarget2D RayCastToGT;
    [SerializeField] Controller Con;
    Material myMaterial;
    Vector3 UserGazePoint;

    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        UserGazePoint = RayCastToGT.GazeTargetPosForShader;
        myMaterial.SetVector("UserGazePoint", new Vector4(UserGazePoint.x, UserGazePoint.y, UserGazePoint.z, 0));
        myMaterial.SetFloat("BrightnessIntensity", Con.BrightnessIntensity);
        myMaterial.SetFloat("ColorIntensity", Con.ColorIntensity);
        myMaterial.SetFloat("FoveaRegionSize", Con.FoveaRegionSize);
        myMaterial.SetFloat("GroupSize", Con.GroupSize);
    }
}


