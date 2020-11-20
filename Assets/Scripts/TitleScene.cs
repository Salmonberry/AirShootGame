using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("MyGame/TitleScreen")]
public class TitleScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //響應遊戲開始按鈕事件
    public void OnButtonGameStart()
    {
        SceneManager.LoadScene("level1");  //讀取關卡1
    }
}
