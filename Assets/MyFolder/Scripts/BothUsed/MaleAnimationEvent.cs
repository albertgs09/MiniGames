using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MaleAnimationEvent : MonoBehaviour
{
    public UnityEvent OnFinishAttacking;


    private void ResetSpeed()
    {
        OnFinishAttacking?.Invoke();
    }
}
