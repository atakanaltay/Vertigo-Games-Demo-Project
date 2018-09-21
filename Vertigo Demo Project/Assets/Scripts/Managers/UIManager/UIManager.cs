using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    #region Variables
    public GameObject gameOverPanel;
    public UnityEngine.UI.Button restartButton;
    
    #endregion
    #region Public Methods
    public void openGameOverPanel()
    {
        StartCoroutine(uiDelay());
    } 
    #endregion
    #region Private Methods
    private IEnumerator uiDelay()
    {
        yield return new WaitForSeconds(1.5f);
        gameOverPanel.SetActive(true);
        if (!restartButton.IsInteractable())
        {
            restartButton.interactable = true;
        }
        
    } 
    #endregion
}
