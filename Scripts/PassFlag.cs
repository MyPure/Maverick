using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassFlag : MonoBehaviour
{
    //编辑器的当前关卡，仅供调试
    public int EditorNowLevel;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("GameController") != null)
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("加载下一个关卡");
            //加载下一个关卡
            if (gameController == null)
                SceneManager.LoadScene("Level " + (EditorNowLevel+1));
            else
                gameController.LoadNext();
        }
    }
}
