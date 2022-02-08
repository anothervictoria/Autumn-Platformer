using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        int numOfGScenePersists = FindObjectsOfType<ScenePersist>().Length;
        if (numOfGScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void RestartPersist()
    {
        Destroy(gameObject);
    }
}
