using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class Canvas : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject OptionsPanel;
    public GameObject CreditsPanel;
    public GameObject HowToPlayPanel;

    public string NewGameScene;
    void Start()
    {
        clearPanels();
        StartPanel.SetActive(true);
    }
    void newGameOnClick(){
        SceneManager.LoadScene(NewGameScene);
    }
    void optionsOnClick (){
        clearPanels();
        OptionsPanel.SetActive(true);
       
    }
    void backToStartOnClick (){
        clearPanels();
        StartPanel.SetActive(true);
        
    }
    void creditsOnClick (){
        clearPanels();
        CreditsPanel.SetActive(true);
    }
    void howToPlayOnClick(){
         clearPanels();
    }
    void exitOnClick(){
         Application.Quit();
    }

    void clearPanels(){
        StartPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);
    }
    

}
