using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostModes : MonoBehaviour
{
    [SerializeField] protected Ghost ghost;
    [SerializeField] protected float duration;

    private void Awake()
    {
        ghost = GetComponent<Ghost>();  
    }

    public void Enable()
    {

        Enable(duration);
       
    }

    public virtual void Enable(float duration)
    {
        enabled = true;

        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        enabled = false;

        CancelInvoke();
    }
}