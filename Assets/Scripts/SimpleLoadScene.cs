using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleLoadScene : MonoBehaviour
{

    static int sceneNumber = 0;//this variable is used to call various scenes using the LoadScene function
    bool cursor = true;
//=======
    //static int sceneNumber = 1;
    
//>>>>>>> 1bd6ec46e339b34439c90c81f89822d575f0f92e:Assets/Scripts/SimpleLoadScene.cs
    // Start is called before the first frame update

    private void Start()
    {   // This to unlock the cursor when it enters the scene so that player can click on the button.
        Cursor.lockState = CursorLockMode.None;
    }
    public void LoadNextLevel()//used in the win screen to load the next scene in the game
    {

        sceneNumber++;//adds 1 to the sceneNumber variable when called
        SceneManager.LoadScene(sceneNumber);//loads new scene
        

        if (sceneNumber >= 3)//this causes the game to loop so that after the player beats level 3 they will be taken back to level 1
        {
            sceneNumber = 0;
        }
    }

    public void LoadPreviousLevel()//used in the game over scene to load the level the player was just playing
    {
        SceneManager.LoadScene(sceneNumber);
//<<<<<<< HEAD:Assets/SimpleLoadScene.cs

//=======
        Time.timeScale = 1.0f;//this prevents the game from staying paused if the player chooses to restart the level from the pause menu
    }

    public void QuitGame()//used to quit game
    {
        Application.Quit();
//>>>>>>> 1bd6ec46e339b34439c90c81f89822d575f0f92e:Assets/Scripts/SimpleLoadScene.cs
    }

    public void LoadLevel1()//used to load level 1 from the settings menu
    {
        sceneNumber = 1;
        SceneManager.LoadScene(sceneNumber);
    }
    public void LoadLevel2()//used to load level 2 from the settings menu
    {
        sceneNumber = 2;
        SceneManager.LoadScene(sceneNumber);
    }
    public void LoadLevel3()//used to load level 3 from the settings menu
    {
        sceneNumber = 3;
        SceneManager.LoadScene(sceneNumber);
    }

    public void loadMainMenu()//used to bring the player to the main menu
    {
        sceneNumber = 0;
        SceneManager.LoadScene(sceneNumber);
        Time.timeScale = 1.0f;
    }

    public void loadSettings()//used to load the settings menu
    {
        SceneManager.LoadScene(6);
        Time.timeScale = 1.0f;
    }

    public void loadFinalWin()//used to load final win screen
    {
        SceneManager.LoadScene(7);
    }

}
