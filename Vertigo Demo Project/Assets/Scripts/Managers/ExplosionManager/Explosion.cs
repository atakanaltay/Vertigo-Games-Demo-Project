using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Explosion : MonoBehaviour {
    #region Variables
    internal List<Coroutine> MoveRoutines;
    internal GameObject[] Effects;
    internal Vector3 zOffsetOfEffect = Vector3.forward * 2f;//for increasing explosion perception it must be in front of the texts 
    #endregion
    #region Unity Methods
    public virtual void Awake()
    {
        throw new System.NotImplementedException();
    } 
    #endregion
    #region Public Methods
    public virtual void startExplodingTheGivenNodes(List<List<int>> explodingNodes)
    {
        throw new System.NotImplementedException();
    }
    public virtual IEnumerator explode(Dictionary<int, List<int>> columnsAndIndexesOfExplodingNodes)
    {
        throw new System.NotImplementedException();
    }
    public virtual IEnumerator startMovingOfNodes(List<int> allMovingIndexesInAColumn)
    {
        throw new System.NotImplementedException();
    }
    public virtual void createAndPlayEffect(Vector3 pos, int colorIndex)
    {
        throw new System.NotImplementedException();
    } 
    #endregion
   
}
