using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class ScenesManager : MonoBehaviour
{
    public float delay;
    public string newNameScene;


    void Start()
    {
        // Defaut scene : SplashScreen
        if (SceneManager.GetSceneByBuildIndex(0).buildIndex == 0)
        {
            StartCoroutine(LoadSpecificScene(delay, newNameScene));
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == ("MainMenu") && Input.GetButtonDown("Submit")) //Enter ou Space
        {
            StartCoroutine(LoadSpecificScene(delay, newNameScene));
        }

        if (SceneManager.GetActiveScene().name == ("LevelBase") && Input.GetButtonDown("Cancel")) // Escape
        {
            StartCoroutine(LoadSpecificScene(delay, newNameScene));
        }
    }

    public IEnumerator LoadSpecificScene(float delay, string newNameScene)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(newNameScene);
    }
}
