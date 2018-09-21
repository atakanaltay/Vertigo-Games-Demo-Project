using UnityEngine;

public class MoveSystem : MonoBehaviour {

    #region Variables
    public UnityEngine.UI.Text moveText;
    internal int moveCount = 0; 
    #endregion

    #region Public Methods
    public void startGame()
    {
        moveCount = 0;
        setCount();
    }
    public void incrementMoveCount()
    {
        moveCount += 1;
        setCount();
    }
    public void setCount()
    {
        moveText.text = moveCount.ToString();
    }
    
    #endregion
}
