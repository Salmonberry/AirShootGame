using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/Player")]
public class Player : MonoBehaviour
{
    public float m_speed = 1;
    public float m_life = 3;
    public Transform m_rocket;
    private Transform m_transform;
    private AudioSource _audioSource;
    public AudioClip _audioClip;
    public Transform _explsion;
    protected Vector3 m_targetPos; // 目標位置
    public LayerMask m_inputMask;  // 鼠標射線碰撞層
    //發射速度
    public float m_rocketTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;

        _audioSource = this.GetComponent<AudioSource>();

        m_targetPos = this.m_transform.position;// 添加代碼 初始化目標點位置
    }

    void MoveTo()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 ms = Input.mousePosition;  //獲取鼠標的屏幕位置
            Ray ray = Camera.main.ScreenPointToRay(ms);//將屏幕位置轉化為射線
            RaycastHit hitinfo; // 用來紀錄射線的碰撞信息
            bool iscast = Physics.Raycast(ray, out hitinfo, 1000, m_inputMask);
            if (iscast)
            {
                //如果射中目標 紀錄射線的碰撞點
                m_targetPos = hitinfo.point;
            }
        }


        // 使用 Vector3 提供的MoveTowards函數 獲得朝目標移動的位置
        Vector3 pos = Vector3.MoveTowards(m_transform.position, m_targetPos, m_speed * Time.deltaTime);

        //更新當前位置
        this.m_transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        //縱向移動距離
        float movev = 0;

        //水平移動距離
        float moveh = 0;

      

        //按上鍵Z方向遞增
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movev += m_speed * Time.deltaTime;
        }

        //按下鍵Z方向遞減
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movev -= m_speed * Time.deltaTime;
        }

        //按左鍵X方向遞減
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveh -= m_speed * Time.deltaTime;
        }

        //按右鍵X方向遞增
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveh += m_speed * Time.deltaTime;
        }

        //移動
        m_transform.Translate(new Vector3(moveh, 0, movev));



        //按空格鍵或鼠標左鍵發射子彈
        m_rocketTimer -= Time.deltaTime;
        if (m_rocketTimer <= 0)
        {
            m_rocketTimer = 0.1f;

            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                Instantiate(m_rocket, m_transform.position, m_transform.rotation);

                if (_audioSource.isPlaying)
                {
                    _audioSource.Stop();
                }
                _audioSource.PlayOneShot(_audioClip);
            }
        }

        MoveTo();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "PlayerRocket")
        {
            m_life -= 1;    //  减少生命
            GameManager.Instance.ChangeLife(m_life);

            if (m_life <= 0) // 当生命为0时，
            {
                Destroy(this.gameObject); // 自我销毁
                Instantiate(_explsion, this.transform.position, Quaternion.identity);

            }
        }
    }





}
