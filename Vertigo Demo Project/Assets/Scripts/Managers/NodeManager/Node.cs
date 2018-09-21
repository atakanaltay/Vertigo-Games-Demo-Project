using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Node : MonoBehaviour
{
    #region Public Methods
    public virtual void select(Vector3 _TouchPos)
    {
        throw new System.NotImplementedException();
    }
    public virtual IEnumerator Move(Vector3 position)
    {
        throw new System.NotImplementedException();

    }
    public virtual void correctRotation()
    {
        throw new System.NotImplementedException();
    } 
    #endregion
}
