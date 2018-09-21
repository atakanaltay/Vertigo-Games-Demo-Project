using UnityEngine;

public class Score : MonoBehaviour
{
    #region Variables
    public UnityEngine.UI.Text scoreUiText, highScoreUiText;
    GameObject scoreText;
    internal int currentScore = 0;
    #endregion
    #region Unity Methods
    void Awake()
    {
        scoreText = Resources.Load("Prefabs/ScoreText") as GameObject;
    } 
    #endregion
    #region Public Methods
    public void startGame()
    {
        currentScore = 0;
        setScore();
        setHighScore(PlayerPrefs.GetInt("highScore", 0));
    }
    /// <summary>
    /// Increments and shows the score according to give scorepoint and position
    /// </summary>
    /// <param name="scorePoint"></param>
    /// <param name="position"></param>
    public void incrementScore(int scorePoint, Vector2 position)
    {
        currentScore += scorePoint;
        setScore();
        StartCoroutine(Instantiate(scoreText, (Vector3)(position) + Vector3.back, Quaternion.identity).GetComponent<ScoreText>().moveToPos(scorePoint));
    } 
    #endregion
    #region Private Methods
    /// <summary>
    /// Shows the current score
    /// </summary>
    private void setScore()
    {
        scoreUiText.text = currentScore.ToString();
        checkHighScore(currentScore);
    }
    /// <summary>
    /// Sets the high score
    /// </summary>
    /// <param name="highScore"></param>
    private void setHighScore(int highScore)
    {
        highScoreUiText.text = highScore.ToString();
    }
    /// <summary>
    /// Checks if highscore is lower than given current score
    /// </summary>
    /// <param name="currentScore"></param>
    private void checkHighScore(int currentScore)
    {
        if (PlayerPrefs.GetInt("highScore") <= currentScore)
        {
            PlayerPrefs.SetInt("highScore", currentScore);
            setHighScore(currentScore);
        }
    } 
    #endregion

}
