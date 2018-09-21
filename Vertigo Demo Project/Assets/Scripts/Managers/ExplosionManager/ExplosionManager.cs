using UnityEngine;
using System.Collections.Generic;

public class ExplosionManager : MonoBehaviour {

    #region Variables
    public GameObject[] explosions;
    internal Explosion currentExplosion; 
    #endregion
    #region Unity Methods
    public void startGame()
    {
        for (int i = 0; i < explosions.Length; i++)
        {
            explosions[i] = Instantiate(explosions[i], transform);
        }
    } 
    #endregion
    #region Public Methods
    /// <summary>
    /// Initializes explosion type and starts explode
    /// </summary>
    /// <param name="coverObjectType"></param>
    /// <param name="explodingNodes"></param>
    public void startExplode(System.Type coverObjectType, List<List<int>> explodingNodes)
    {
        setCurrentExploder(coverObjectType);
        currentExplosion.startExplodingTheGivenNodes(explodingNodes);
    }
    /// <summary>
    /// Sets the current explosion 
    /// </summary>
    /// <param name="coverObjectType"></param>
    public void setCurrentExploder(System.Type coverObjectType)
    {
        if (coverObjectType == typeof(TripleCoverObject))
        {
            currentExplosion = explosions[0].GetComponent<Explosion>();
        }
    } 
    #endregion
   
	
}
