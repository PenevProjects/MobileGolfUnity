using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("_Scenes/End");
    }
}
