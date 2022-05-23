using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SceneMgr : NetworkBehaviour
{
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

   
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width/2,Screen.height/2,100,100),"跳转到大厅"))
        {
            FindObjectOfType<NetworkManager>().ServerChangeScene(sceneName);
        }
    }
}
