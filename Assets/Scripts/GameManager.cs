using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;
    public Transform GetPlayerReference() { return FindObjectOfType<PlayerMovement>().transform; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        //DontDestroyOnLoad(instance);
    }
}
