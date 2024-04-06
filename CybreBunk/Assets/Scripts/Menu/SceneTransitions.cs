using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    //[SerializeField] string   sceneToLoad;
    //[SerializeField] Animator animatorOpen;
    //[SerializeField] Animator animatorClose;
    [SerializeField] bool doAnimatorStuff = true;
    bool                  canContinue     = false;
    Animator              animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public IEnumerator Transition(string sceneToLoad)
    {
        if (doAnimatorStuff)
            animator.SetTrigger("Fade");
        
        //yield return new WaitForSeconds(animatorClose ? animatorClose.GetCurrentAnimatorStateInfo(0).length : 1);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(sceneToLoad);
    }
}
