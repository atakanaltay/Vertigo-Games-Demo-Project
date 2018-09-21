using UnityEngine;

public class ColorSystem : MonoBehaviour {

	#region Variables
    public Colors colorsSc;
    public static int colorCount;
	#endregion
    #region Unity Methods
    void Awake()
    {
        colorCount = GameManager.instance.colorSys.colorsSc.Colorz.Count;
    } 
    #endregion

    }
