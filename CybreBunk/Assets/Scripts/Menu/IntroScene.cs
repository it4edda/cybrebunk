using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    [SerializeField] string   sceneToLoad;
    [SerializeField] Animator animator;
    bool                      canContinue = false;
    void                      Start() { StartCoroutine(Wait()); }
    void Update()
    {
        if (Input.anyKey && canContinue) StartCoroutine(Transition());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        canContinue = true;
    }
    IEnumerator Transition()
    {
        animator.gameObject.SetActive(true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(sceneToLoad);
    }
}
