using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/Player")]
public class Player : MonoBehaviour
{
    public float m_speed = 1;
    private Transform m_transform;


    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
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
    }
}
