﻿using UnityEngine;
using System.Collections;

public class InvertColorEffect : MonoBehaviour {
    
    public Material mat;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, mat);
    }
}
