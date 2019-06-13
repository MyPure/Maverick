using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WelcomeControl : MonoBehaviour
{

    /// <summary>
    /// 文本
    /// </summary>
    public Text text;


    /// <summary>
    /// 待播放图片
    /// </summary>
    public List<Image> images;

    /// <summary>
    /// 待播放文字
    /// </summary>
    public List<string> texts;

    /// <summary>
    /// 更新周期
    /// </summary>
    public float cycleTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TextUpdate());
        StartCoroutine(ImageUpadate());
    }

    IEnumerator TextUpdate()
    {
        foreach (var v in texts)
        {
            text.text = v;
            yield return new WaitForSeconds(cycleTime);
        }
    }
    IEnumerator ImageUpadate()
    {
        foreach(var v in images)
        {
            Color c = v.color;
            while(c.a > 0)
            {
                c.a -= 0.01f;
                v.color = c;
                yield return null;
            }

        }

        SceneManager.LoadScene("HomeUI");
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("HomeUI");
    }





}
