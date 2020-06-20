using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private GameObject WinCanvas;
    [SerializeField] private GameObject LoseCanvas;

    // Start is called before the first frame update
    void Start()
    {
        LoseCanvas.GetComponent<Button>().onClick.AddListener(ReloadScene);
        WinCanvas.GetComponent<Button>().onClick.AddListener(ReloadScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Destroy(WinCanvas);
        //Destroy(LoseCanvas);
    }
}
