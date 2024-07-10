using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfScreen : MonoBehaviour
{
    void Update()
    {
        if (!Screen.safeArea.Contains(Camera.main.WorldToScreenPoint(transform.position)))
        {
            Destroy(gameObject);
        }
    }
}
