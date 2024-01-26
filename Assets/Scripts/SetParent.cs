using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParent : MonoBehaviour
{
    private void Start()
    {
        if (transform.parent != null)
        transform.SetParent(transform.parent.parent);
    }
}
