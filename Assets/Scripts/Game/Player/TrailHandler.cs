using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//Script para denderizar una estela de objetos
public class TrailHandler : MonoBehaviour
{
    private TrailRenderer _trail;

    public void ActivateTrail(TrailRenderer trailRender)
    {
        //Debug.Log(_trail);
        if (_trail == null)
        {
            //Debug.Log("Trail");
            trailRender.emitting = true;
            _trail = trailRender;
        }
    }
    
   public void DeactivateTrail()
    {
        _trail.Clear();
        _trail.emitting = false;
        _trail = null;
    }
    
}
