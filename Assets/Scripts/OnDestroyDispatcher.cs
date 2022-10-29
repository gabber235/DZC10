using System;
using UnityEngine;

public class OnDestroyDispatcher : MonoBehaviour
{
    private void OnDestroy()
    {
        if (OnObjectDestroyed != null) OnObjectDestroyed(gameObject);
    }

    public event Action<GameObject> OnObjectDestroyed;
}