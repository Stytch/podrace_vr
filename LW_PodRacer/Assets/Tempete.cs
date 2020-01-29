using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tempete : MonoBehaviour
{
    public float moveSpeed = 1000f;
    public bool moveEnabled = false;
    void Update()
    {
        if (moveEnabled)
        {
            var pos = transform.position;
            pos.z += Time.deltaTime * moveSpeed;
            transform.position = pos;
        }
    }
}
