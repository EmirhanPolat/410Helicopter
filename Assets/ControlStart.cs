using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartTheGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadInstructions()
    {
        SceneManager.LoadScene("InstructionsScene");
    }

    public void CloseInstructions()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void QuitGame()
    {
#if UNITY_STANDALONE
        // Quit the application
        Application.Quit();
#endif

        // If we are running in the editor
#if UNITY_EDITOR
        // Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
