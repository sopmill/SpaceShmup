using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Utils : MonoBehaviour
{
    static public Vector3 Bezier(float u, params Vector3[] points){
        Vector3[,] vArr = new Vector3[points.Length, points.Length];
        int r = points.Length - 1;
        for (int c = 0; c < points.Length; c++){
            vArr[r, c] = vList[c];
        }
        for(r--; r >= 0 ; r--){
            for(int c = 0; c <= r; c++){
                vArr[r, c] = Vector3.LerpUnclamped(vArr[r+1,c], vArr[r+1, c+1], u);
            }
        }
        return vArr[0,0];
    }
}
