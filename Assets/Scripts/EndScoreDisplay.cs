/* EndScoreDisplay links Unity UI Text in End Screen to display the
 * score of the previous game along with the high score. Both are retrieved
 * from player prefs through the custom PlayerPrefsManager. 
 */

using UnityEngine;
using UnityEngine.UI;

public class EndScoreDisplay : MonoBehaviour {

    public Text highScore;
    public Text lastScore;

	// Use this for initialization
	void Start () {
        highScore.text = "High Score: " + PlayerPrefsManager.GetHighScore().ToString();
        lastScore.text = "Your Score: " + PlayerPrefsManager.GetLastScore().ToString();
	}

}
