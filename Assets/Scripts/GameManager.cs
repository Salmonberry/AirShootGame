using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[AddComponentMenu("MyGame/GameManager")]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //靜態實例

    public Transform m_canvas_main;  // 顯示分數的UI界面
    public Transform m_canvas_gameover; // 遊戲失敗UI界面
    protected Text text_score;  // 得分UI文字
    protected Text text_best;   // 最高分Ui文字
    protected Text text_life;   // 生命值Ui文字

    protected int m_score = 0; // 得分數值
    public static int m_hiscore = 0; //最高分數值
    protected Player m_player; //主角實例

    public AudioClip m_musicClip;  // 背景音樂 音樂文件
    protected AudioSource m_Audio; // 聲音源 播放器

    private void Start()
    {
        Instance = this;

        m_Audio = this.gameObject.AddComponent<AudioSource>(); //使用代碼添加聲音源組件
        m_Audio.clip = m_musicClip; //指定背景音樂
        m_Audio.loop = true; //開啟循環播放
        m_Audio.Play();   //開啟播放

        //通過tag找主角
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        // 獲取Ui控件
        text_score = m_canvas_main.transform.Find("Text_score").GetComponent<Text>();
        text_best = m_canvas_main.transform.Find("Text_best").GetComponent<Text>();
        text_life = m_canvas_main.transform.Find("Text_life").GetComponent<Text>();

        //初始化分數
        text_score.text = string.Format("分數: {0}", m_score);
        text_best.text = string.Format("最高分: {0}", m_hiscore);
        text_life.text = string.Format("生命值: {0}", m_player.m_life);

        //獲取重啟遊戲按鈕
        var restart_button = m_canvas_gameover.transform.Find("Button_restart").GetComponent<Button>();

        restart_button.onClick.AddListener(
            /// 委託 設置重新開始遊戲按鈕事件回調
            delegate ()
        {
            // 設置重新開始當前遊戲關卡
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        });

        m_canvas_gameover.gameObject.SetActive(false); //默認隱藏遊戲失敗UI
    }


    // 增加分數函數
    public void AddScore(int point)
    {
        m_score += point;

        if (m_hiscore < m_score)
        {
            m_hiscore = m_score;
            text_score.text = string.Format("分數 {0}", m_score);
            text_best.text = string.Format("最高分 {0}", m_hiscore);
        }
    }

    //改變生命值UI顯示
    public void ChangeLife(float life)
    {
        text_life.text = string.Format("生命 {0}", life); // 更新UI

        if (life < 0)
        {
            m_canvas_gameover.gameObject.SetActive(true); //如果生命為0 顯示遊戲失敗Ui
        }
    }

}
