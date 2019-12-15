using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenBackground : MonoBehaviour
{

  void Start()
  {
    Resize();
  }



  void Resize()
  {
    SpriteRenderer sr = GetComponent<SpriteRenderer>();
    if (sr == null) return;

    transform.localScale = new Vector3(1, 1, 1);

    float width = sr.sprite.bounds.size.x;
    float height = sr.sprite.bounds.size.y;

    double variable = Camera.main.orthographicSize * 4.0;
    float worldScreenHeight = (float)variable;
    float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

    transform.localScale = new Vector2(worldScreenWidth / width, worldScreenHeight / height);

  }
}