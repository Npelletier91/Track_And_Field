using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public SceneLoader sceneLoader;

    public void MissileButton()
    {
        sceneLoader.LoadSceneName("MissileStartScreen");
    }

    public void DKButton()
    {
        sceneLoader.LoadSceneName("DK_OpeningScene");
    }

    public void MsPacmanButton()
    {
        sceneLoader.LoadSceneName("Ms-Pacman-OpeningScene");
    }

    public void PacManButton()
    {
        sceneLoader.LoadSceneName("PacMan_TitleScreen");
    }


    public void TrackAndField()
    {
        sceneLoader.LoadSceneName("Track_Game Menu");
    }

    /*
      //Example of how to link your button to your Game menu
      //sceneLoader.LoadSceneName is the preferred method for loading scenes
    
    public void GameButton()   //Name of your Button
    {
       sceneLoader.LoadSceneName("Game_Menu");  // Specify the name of your menu Scene
    }
    */
}
