using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    public string gateName;

    private void Start()
    {
        GameDataManager.SaveGatePosition(gateName, SceneManager.GetActiveScene().name, transform.position);
    }
}
