using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystem : MonoBehaviour
{
    [System.Obsolete]
    public void PlayAgain()
    {
        Application.LoadLevel("Level");
    }
}
