using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public abstract class CoverObject : MonoBehaviour
{
    #region Public Methods
    public virtual void cover(List<int> closestIndexes)
    {
        throw new System.NotImplementedException();
    }
    public virtual void rotate(CoverObjectDirection cod, List<int> closestIndexes)
    {
        throw new System.NotImplementedException();
    } 
    #endregion
    #region Internal Methods
    internal virtual IEnumerator rotateRoutine(CoverObjectDirection cod, List<int> closestIndexes)
    {
        throw new System.NotImplementedException();
    }
    internal virtual IEnumerator turnAnim(float dir, float duration)
    {
        throw new System.NotImplementedException();
    } 
    #endregion
}
