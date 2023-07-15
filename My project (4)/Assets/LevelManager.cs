using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string sceneToLoad;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
            SceneManager.LoadScene(sceneToLoad);
    }
}
