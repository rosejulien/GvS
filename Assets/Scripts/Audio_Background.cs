using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Background : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
