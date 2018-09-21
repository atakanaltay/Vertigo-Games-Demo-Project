using UnityEngine;

[RequireComponent(typeof(GridSystem))]
[RequireComponent(typeof(NodeManager))]
[RequireComponent(typeof(CoverObjectManager))]
[RequireComponent(typeof(ExplosionManager))]
[RequireComponent(typeof(ColorSystem))]
[RequireComponent(typeof(InputSystem))]
[RequireComponent(typeof(Score))]
[RequireComponent(typeof(MoveSystem))]
[RequireComponent(typeof(BombSystem))]
[RequireComponent(typeof(UIManager))]

public class GameManager : MonoBehaviour {

	#region Variables
    public static GameManager instance;

    internal GridSystem gridSys;
    internal NodeManager nodeManager;
    internal ExplosionManager explosionManager;
    internal CoverObjectManager coverObjManager;
    internal ColorSystem colorSys;
    internal InputSystem inputSys;
    internal Score scoreSys;
    internal MoveSystem moveSys;
    internal BombSystem bombSys;
    internal UIManager uiManager;
	#endregion

    #region Unity Methods
    void Awake()
    {
        nodeManager = GetComponent<NodeManager>();
        gridSys = GetComponent<GridSystem>();
        coverObjManager = GetComponent<CoverObjectManager>();
        colorSys = GetComponent<ColorSystem>();
        inputSys = GetComponent<InputSystem>();
        scoreSys = GetComponent<Score>();
        moveSys = GetComponent<MoveSystem>();
        bombSys = GetComponent<BombSystem>();
        explosionManager = GetComponent<ExplosionManager>();
        uiManager = GetComponent<UIManager>();

        instance = this;
    }
    void Start()
    {
        startGame();
    } 
    #endregion
    #region Public Methods
    /// <summary>
    /// Start the game from beginning.
    /// </summary>
    public void startGame()
    {
        gridSys.startGame();
        scoreSys.startGame();
        moveSys.startGame();
        bombSys.startGame();
        explosionManager.startGame();
        inputSys.startGame();
    }
    /// <summary>
    /// Checks if the game is over.
    /// </summary>
    public void gameOverCheck()
    {
        if (GridSystem.uniqueTriples.checkGroupTriplesHaveSameCountOfColorByGivenCountOfColor(1))
        {
            explosionManager.currentExplosion.startExplodingTheGivenNodes(GridSystem.uniqueTriples.giveEveryGroupThatWillExplode(1));
        }
        else
        {
            if (!bombSys.checkBombsDestroyCounts())
            {
                if (GridSystem.uniqueTriples.IsThereAnyPossibleMoveInGame())
                {
                    inputSys.enabled = true;
                    SwipeLogger.checkForInput();
                    coverObjManager.currentCoverObject.gameObject.SetActive(true);
                    moveSys.incrementMoveCount();
                }
                else
                {
                    uiManager.openGameOverPanel();
                }

            }
        }
    }
    public void gameOver()
    {
        Extensions.gameOverExplode();
        uiManager.openGameOverPanel();
    } 
    #endregion
}
