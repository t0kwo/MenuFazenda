using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuOptions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void Play()
    {
        SceneManager.LoadScene("Level1");
    }
   public void Quit()
    {
        Application.Quit();
                #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
