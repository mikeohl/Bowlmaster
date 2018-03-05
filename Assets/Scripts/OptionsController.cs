/* OptionsController manages and persistently saves ball
 * color in the options menu. 
 */

using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    public Slider ballColorSlider;
    public LevelManager levelManager;
    
    private string ballColor;
    
    // Use this for initialization
    void Start () {
        ballColor = PlayerPrefsManager.GetBallColor();
        
    }
    
    // Save options from the option menu to persistent storage
    public void SaveAndExit () {
        PlayerPrefsManager.SetBallColor (ballColor);
        levelManager.LoadLevel ("01a Start Menu");
    }
}
