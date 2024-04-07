using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    [SerializeField] string   sceneToLoad;
    [SerializeField] Animator animator;
    [SerializeField] bool     doAnimatorStuff = true;
    bool                      canContinue     = false;
    void                      Start() { StartCoroutine(Wait()); }
    void Update()
    {
        if (Input.anyKey && canContinue) StartCoroutine(Transition());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        canContinue = true;
    }
    public IEnumerator Transition()
    {
        if (doAnimatorStuff)
        animator.gameObject.SetActive(true);
        yield return new WaitForSeconds(animator ? animator.GetCurrentAnimatorStateInfo(0).length : 1);
        SceneManager.LoadScene(sceneToLoad);
    }
}
