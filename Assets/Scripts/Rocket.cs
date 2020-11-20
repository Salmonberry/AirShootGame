using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/Rocket")]
public class Rocket : MonoBehaviour
{
    public float m_speed = 10;   // 子彈飛行速度
    public float m_power = 1.0f; // 威力

    private void OnBecameInvisible()  // 當離開屏幕 無法被渲染時 自動調用
    {
        if (this.enabled)  //通過判斷當前子彈是否處於激活狀態 防止重複刪除
        {
            Destroy(this.gameObject); // 刪除子彈
        }
    }

    private void OnBecameVisible()
    {
        Debug.Log("我是子彈君");
    }

    private void Update()
    {
        //向前（Z方向）移动
        transform.Translate(new Vector3(0, 0, m_speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy")
            return;
        Destroy(this.gameObject);
    }
}
