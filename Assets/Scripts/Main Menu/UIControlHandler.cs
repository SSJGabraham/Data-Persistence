using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIControlHandler : MonoBehaviour
{
    // High score text field.
    [SerializeField]
    private GameObject highScoreObject;
    private TMP_Text highScoreText;
    private int highScore;
    private string highScoreName;

    // Name input variables.
    [SerializeField]
    private TMP_Text nameInputObject;
    public string nameInput;

    // Start button.
    [SerializeField]
    private TMP_Text startButtonText;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize TMP_Text component.
        highScoreText = highScoreObject.GetComponent<TMP_Text>();

        // Loads the high score and updates the high score text.
        RetrieveHighScore();
        SetHighScoreText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Starts the game in scene 1.
    public void StartGame()
    {
        // Set nameInput to be the string contained in the input box.
        nameInput = nameInputObject.text;

        // Check whether or not inputName is empty or contains invalid characters. Load the Main scene if it is not.
        if (string.IsNullOrEmpty(nameInput))
        {
            startButtonText.text = "Please choose your name before you begin!";
        }
        else if (nameInput.Length < 2 || string.IsNullOrWhiteSpace(nameInput)) 
        {
            startButtonText.text = "Please enter a name of at least one letter.";
        }
        else
        {
            // Valid input. Loads the next scene.
            SetName();
            SceneManager.LoadScene(1);
        }
        
    }

    // Quits the application.
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    // Retrieves the saved high score and stores it in highScore.
    private void RetrieveHighScore()
    {
        highScore = DataManagerHandler.Instance.highScore;
        highScoreName = DataManagerHandler.Instance.highScoreName;
    }

    // Takes the high score text in the menu and sets it to stored values.
    private void SetHighScoreText()
    {
        if (highScore <= 0)
        {
            highScoreText.text = "No high score, yet!";
        }
        else
        {
            highScoreText.text = "HIGH SCORE: " + highScore + " by " + highScoreName;
        }
    }

    // Accepts input from the player and allows their name to be saved.
    public void SetName()
    {
        DataManagerHandler.Instance.playerName = nameInput;
    }

    public void NameDebug()
    {
        Debug.Log("nameInput check: " + string.IsNullOrEmpty(nameInput) + " " + string.IsNullOrWhiteSpace(nameInput) + " " + nameInput.Length);
    }
}
