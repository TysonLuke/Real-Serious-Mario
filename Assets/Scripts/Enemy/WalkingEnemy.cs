using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSM
{
    namespace Enemy
    {
        public class WalkingEnemy : MonoBehaviour
        {
            private int direction = 1;

            private float speed = 1f;

            private void Update()
            {

                // Work out some values so these don't look like an absolute mess.
                float movement = speed * Time.deltaTime * direction;
                Vector3 newLocation = transform.position + new Vector3(movement * 2, 0, 0);
                BoxCollider2D myCol = GetComponent<BoxCollider2D>();
                Vector2 collisionSize = new Vector2(myCol.size.x, myCol.size.y * 0.8f);
                
                // Check if the enemy can move to the side.
                if (Physics2D.OverlapBox(newLocation, collisionSize, 0))
                {
                    // Check which objects we will be colliding with if we move there.
                    Collider2D[] hits = Physics2D.OverlapBoxAll(newLocation, collisionSize, 0);

                    foreach (Collider2D hit in hits)
                    {
                        // If the enemy hits something it can't pass, flip back the other way.
                        if (hit.tag == "Solid" || hit.tag == "Blocking")
                        {
                            direction *= -1;
                            movement *= -1f;
                            break;
                        }
                    }
                }

                transform.Translate(new Vector3(movement, 0 ,0));
            }

            private void OnCollisionEnter2D(Collision2D col)
            {
                // If hit the player, we kill them.
                if (col.gameObject.tag == "Player")
                {
                    RSM.Player.PlayerController.Instance.LoseLife();
                }
            }
        }
    }
}
