using UnityEngine;
using System.Collections.Generic;
public class BombSystem : MonoBehaviour {

    #region Variables
    [Range(3, 10)]
    public int minBombDestroyCounter = 3;
    [Range(3, 10)]
    public int maxBombDestroyCounter = 10;
    internal int givenBombCount;
    internal List<BombNode> currentBombs; 
    #endregion
    #region Public Methods
    public void startGame()
    {
        currentBombs = new List<BombNode>();
        givenBombCount = 0;
    }
    /// <summary>
    /// Initializes new bomb.
    /// </summary>
    /// <param name="bombNode"></param>
    public void initializeBomb(BombNode bombNode)
    {
        givenBombCount++;
        bombNode.destroyCounter = Random.Range(minBombDestroyCounter, maxBombDestroyCounter);
        bombNode.initializedMoveValue = GameManager.instance.moveSys.moveCount;
        bombNode.writeCount();
        currentBombs.Add(bombNode);
    }
    /// <summary>
    /// Checks if bomb will explode. destroycounter==0
    /// </summary>
    /// <returns></returns>
    public bool checkBombsDestroyCounts()
    {
        for (int i = 0; i < currentBombs.Count; i++)
        {
            if (currentBombs[i].initializedMoveValue != GameManager.instance.moveSys.moveCount)
            {
                if (currentBombs[i].isCounterFinished())
                {
                    GameManager.instance.gameOver();
                    return true;
                }
            }
        }
        return false;
    } 
    #endregion
 
}
