using UnityEngine;


public class BombNode : HexaNode {

    #region Variables
    internal int destroyCounter;
    internal int initializedMoveValue;
    [SerializeField]
    TextMesh countText; 
    #endregion
    #region Unity Methods
    void onEnable()
    {
        writeCount();
    }
    void OnDisable()
    {
        GameManager.instance.bombSys.currentBombs.Remove(this);
    } 
    #endregion
    #region Public Methods
    public bool isCounterFinished()
    {
        destroyCounter--;
        writeCount();
        if (destroyCounter == 0)
        {
            return true;//gameover
        }
        else
        {
            return false;
        }
    }
    public void writeCount()
    {
        countText.text = destroyCounter.ToString();
    }
    
    #endregion
}
