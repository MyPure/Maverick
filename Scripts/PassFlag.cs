using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassFlag : MonoBehaviour
{
    //编辑器的当前关卡，仅供调试
    public int EditorNowLevel;

    public GameObject passUI;

    private GameController gameController;

    private CollectionManager collectionManager;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("GameController") != null)
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        }
        if (GameObject.Find("CollectionManager") != null)
        {
            collectionManager = GameObject.Find("CollectionManager").GetComponent<CollectionManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().enabled = false;
            Debug.Log($"通过第{gameController.nowLevel}关！");
            if (gameController.passLevel < gameController.nowLevel)
            {
                gameController.passLevel = gameController.nowLevel;
            }
            Instantiate(passUI);

            //通关后的数据处理
            collectionManager.Pass();
            gameController.SaveGame();
        }
    }
}
