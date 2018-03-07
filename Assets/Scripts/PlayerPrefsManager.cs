/* PlayerPrefsManager saves High Score, Last Score,
 * and ball color in Unity PlayerPrefs */

using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {

    const string HIGH_SCORE_KEY = "high_score";
    const string LAST_SCORE_KEY = "last_score";
    const string BALL_COLOR_KEY = "ball_color";
    
    public static void SetHighScore (int score) {
        if (score >= 0 && score <= 300) {
            PlayerPrefs.SetInt (HIGH_SCORE_KEY, score);
        } else {
            Debug.LogError ("Score out of range");
        }
    }
    
    public static int GetHighScore () {
        return PlayerPrefs.GetInt (HIGH_SCORE_KEY);
    }

    public static void SetLastScore(int score) {
        if (score >= 0 && score <= 300) {
            PlayerPrefs.SetInt(LAST_SCORE_KEY, score);
        }
        else {
            Debug.LogError("Score out of range");
        }
    }

    public static int GetLastScore () {
        return PlayerPrefs.GetInt (LAST_SCORE_KEY);
    }

    public static void SetBallColor(string color){
        PlayerPrefs.SetString(BALL_COLOR_KEY, color);
    }

    public static string GetBallColor() {
        return PlayerPrefs.GetString(BALL_COLOR_KEY);
    }
}
