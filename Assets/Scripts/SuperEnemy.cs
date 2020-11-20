using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/SuperEnemy")]
public class SuperEnemy : Enemy
{
  protected override void  UpdateMove()
    {
        ///前進
        transform.Translate(new Vector3(0,0,-m_speed*Time.deltaTime));
    }
}
