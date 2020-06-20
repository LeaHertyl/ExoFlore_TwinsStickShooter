using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RestartButton : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Button>().onClick.AddListener(ReloadScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
