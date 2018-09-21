using UnityEngine;
using System.Collections.Generic;

public class CoverObjectManager : MonoBehaviour {

	#region Variables
    public GameObject[] coverObjects;
    internal CoverObject currentCoverObject;
    internal List<int> currentIndexes;
	#endregion
    #region UnityMethods
    void Awake()
    {
        for (int i = 0; i < coverObjects.Length; i++)
        {
            coverObjects[i] = Instantiate(coverObjects[i], transform);

        }
    } 
    #endregion
    #region Public Methods
    /// <summary>
    /// Corrects the scale of the coverobjects by the scale calculated in grid system(oneSideScale)
    /// </summary>
    /// <param name="oneSideScale"></param>
    public void correctScale(float oneSideScale)
    {
        for (int i = 0; i < coverObjects.Length; i++)
        {
            coverObjects[i].transform.localScale = new Vector3(oneSideScale, oneSideScale, 1f);
        }
    }
    /// <summary>
    /// Starts covering process according to given node type and nodes indexes
    /// </summary>
    /// <param name="nodeType"></param>
    /// <param name="closestIndexs"></param>
    public void startCover(System.Type nodeType, List<int> closestIndexs)
    {
        setCurrentCoverObject(nodeType);
        currentIndexes = closestIndexs;
        currentCoverObject.cover(closestIndexs);
        currentCoverObject.gameObject.SetActive(true);
    }
    /// <summary>
    /// Sets the current coverobject according to given node type
    /// </summary>
    /// <param name="nodeType"></param>
    public void setCurrentCoverObject(System.Type nodeType)
    {
        if (nodeType == typeof(HexaNode))
        {
            currentCoverObject = coverObjects[0].GetComponent<CoverObject>();
        }
    }
    /// <summary>
    /// Starts rotating current coverobject according to coverobjectdrection
    /// </summary>
    /// <param name="cod"></param>
    public void startRotate(CoverObjectDirection cod)
    {
        currentCoverObject.rotate(cod, currentIndexes);
    } 
    #endregion
}
public enum CoverObjectDirection
{
    Clockwise,
    CounterClockwise
}

