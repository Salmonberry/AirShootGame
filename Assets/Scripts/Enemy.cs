using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/Enemy")]
public class Enemy : MonoBehaviour
{
    public float m_speed = 1;    // 速度
    public float m_life = 10;       // 生命
    public int m_point = 10;
    protected float m_rotSpeed = 30;  // 旋轉速度
    internal Renderer m_render;  // 模型渲染组件
    internal bool m_isActiv = false;  //是否激活
    public Transform _explosion;

    // Start is called before the first frame update
    void Start()
    {
        this.m_render = GetComponent<Renderer>(); //获取渲染组件

    }

    private void OnBecameVisible() // Unity事件函数，当模型进入可视范围
    {
        Debug.Log("BecameVisible");
    }


    // Update is called once per frame
    void Update() 
    {
        //Debug.Log("update");
        UpdateMove();
        
       if (m_isActiv && !this.m_render.isVisible)  // 如果移动到屏幕
        {
            Debug.Log("dsdsd");
            Destroy(this.gameObject);              // 自我销毁
            Instantiate(_explosion, this.transform.position, Quaternion.identity);
        }

      
    }


    //注意，為了將來擴展功能， UpdateMove是一個虛函數
    protected virtual void UpdateMove()
    {
        // 左右移動
        float rx = Mathf.Sin(Time.time) * Time.deltaTime;

        //前進（向-Z方向）
        transform.Translate(new Vector3(rx, 0, -m_speed * Time.deltaTime));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerRocket")
        {
            Rocket rocket = other.GetComponent<Rocket>(); //获取子弹上的Rocket组件
            
            if (rocket!=null)
            {
                m_life -= rocket.m_power; //减少生命

                if (m_life <= 0)
                {
                    GameManager.Instance.AddScore(m_point); // 更新UI上的分數
                   
                    Instantiate(_explosion, this.transform.position, Quaternion.identity);
                    Destroy(this.gameObject); //自我销毁

                }
            }
        }

        else if (other.tag=="Player") //如果撞到主角
        {

            m_life = 0;
            Destroy(this.gameObject); // 自我毁灭
            Instantiate(_explosion, this.transform.position, Quaternion.identity);
        }
    }




}
