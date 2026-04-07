using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuOptions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject painel2;
    public GameObject paineloptions;
    public GameObject MenuOptions1;
   public void Play()
    {
        SceneManager.LoadScene("Level1");
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
   public void AbrirQuit()
    {
            painel2.SetActive(true);
            MenuOptions1.SetActive(false);
    }
    public void FecharQuit()
    {
            painel2.SetActive(false);
            MenuOptions1.SetActive(true);
    }
       public void AbrirOptions()
    {
            paineloptions.SetActive(true);
            MenuOptions1.SetActive(false);
    }
    public void FecharOptions()
    {
            paineloptions.SetActive(false);
            MenuOptions1.SetActive(true);
    }
       public void QuitVerdadeiro()
    {
        Application.Quit();

                #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
