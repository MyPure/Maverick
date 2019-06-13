using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WelcomeControl : MonoBehaviour
{
    /// <summary>
    /// 图片
    /// </summary>
    public Image image;

    /// <summary>
    /// 文本
    /// </summary>
    public Text text;


    /// <summary>
    /// 待播放图片
    /// </summary>
    public List<Sprite> sprites;

    /// <summary>
    /// 待播放文字
    /// </summary>
    public List<string> texts;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        if(sprites.Count != texts.Count)
        {
            Debug.Log("请保证贴图数量和文字数量一致");
            yield return null;
        }
        else
        {
            for(int i = 0;i < sprites.Count;i++)
            {
                image.sprite = sprites[i];
                text.text = texts[i];
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("HomeUI");
        }
    }
}
