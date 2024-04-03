using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Controller : MonoBehaviour
{
    float CameraFOV;
    public float FoveaRegionSize;
    public float BrightnessIntensity;
    public float ColorIntensity;
    public float GroupSize;
    int Term;

    void Start()
    {
        CameraFOV = 10f;
        BrightnessIntensity = 0.0f;
        ColorIntensity = 0.0f;
        GroupSize = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Term = 1;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            Term = 2;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Term = 3;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Term = 4;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            CameraFOV = 10f;
            BrightnessIntensity = 0.0f;
            ColorIntensity = 0.0f;
            GroupSize = 1f;
        }

        if (Term == 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CameraFOV -= 5;
                AdjustFoveation();
                Debug.Log("Fovea Region Radius is : " + CameraFOV);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                CameraFOV += 5;
                AdjustFoveation();
                Debug.Log("Fovea Region Radius is : " + CameraFOV);
            }
        }
        else if (Term == 2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GroupSize -= 0.05f;
                Debug.Log("GroupSize is : " + GroupSize);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                GroupSize += 0.05f;
                Debug.Log("GroupSize is : " + GroupSize);
            }
        }
        else if (Term == 3)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ColorIntensity += 0.01f;
                Debug.Log("ColorIntensity is : " + ColorIntensity);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && ColorIntensity > 0)
            {
                ColorIntensity -= 0.01f;
                Debug.Log("ColorIntensity is : " + ColorIntensity);
            }
        }
        else if (Term == 4)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                BrightnessIntensity += 0.01f;
                Debug.Log("BrightnessIntensity is : " + BrightnessIntensity);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                BrightnessIntensity -= 0.01f;
                Debug.Log("BrightnessIntensity is : " + BrightnessIntensity);
            }
        }
    }

    public void AdjustFoveation()
    {
        FoveaRegionSize = 2 * (Mathf.Tan(Mathf.Deg2Rad * CameraFOV / 2f) * 5.0f);
    }
}
