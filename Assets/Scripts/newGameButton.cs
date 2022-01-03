using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newGameButton : MonoBehaviour
{
    // Start is called before the first frame update
   public void newgame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
