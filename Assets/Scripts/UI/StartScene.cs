using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    private Button _startBtn;

    private void Awake()
    {
        _startBtn = GetComponent<Button>();
    }

    private void Start()
    {
        _startBtn.onClick.AddListener(StartBtn);
    }

    public void StartBtn()
    {
        
        SceneManager.LoadScene("SampleScene");
    }
}
