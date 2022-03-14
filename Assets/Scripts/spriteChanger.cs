using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XGLabs
{
    public class spriteChanger : MonoBehaviour
    {
        [SerializeField]Sprite score_boost;
        [SerializeField] Sprite speedUpgrade;
        [SerializeField] Sprite enemySlow;
        [SerializeField]SpriteRenderer mySprite;

        // Start is called before the first frame update
        void Start()
        {
            score_boost = Resources.Load<Sprite>("Score_booster");
            speedUpgrade = Resources.Load<Sprite>("Powerup_Icon");
            enemySlow = Resources.Load<Sprite>("Enemy_Slow_Down");
            mySprite = GetComponent<SpriteRenderer>();
            change();
        }
        public void change()
        {
            int num = Random.Range(0,3);
            switch (num) {
                case 0:
                    mySprite.sprite = score_boost;
                    break;
                case 1:
                    mySprite.sprite = speedUpgrade;
                    break;
                case 2:
                    mySprite.sprite = enemySlow;
                    break;
                default:
                    break;
            
            }

        }


    }
}
