using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PoofAnimation : MonoBehaviour
{
    BoxCollider2D poofCollider;
    Animator poofAnimator;
    void Start()
    {
        poofCollider = GetComponent<BoxCollider2D>();
        poofAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void poofAnimate()
    {
       
        
        if (poofCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            //return;
             poofAnimator.SetTrigger("poofAnimate");
        // poofAnimator.SetBool("poofAnimate", true);
        }
       // if (!poofCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
         //   poofAnimator.SetBool("poofAnimate", false);
        }
    }
}
