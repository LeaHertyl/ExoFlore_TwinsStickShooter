using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private GameObject Loading_Screen;

    public void LoadScene(string SceneToLoad)
    {
        //Debug.Log("Coucou");

        StartCoroutine(Load(SceneToLoad));
    }

    private IEnumerator Load(string SceneToLoad)
    {
        var Loading_ScreenInstance = Instantiate(Loading_Screen);
        DontDestroyOnLoad(Loading_ScreenInstance); 
        var loadingAnimator = Loading_ScreenInstance.GetComponent<Animator>(); 
        var animationTime = loadingAnimator.GetCurrentAnimatorStateInfo(0).length;
        var loading = SceneManager.LoadSceneAsync(SceneToLoad);

        loading.allowSceneActivation = false; 

        while (!loading.isDone)
        {
            //je sais que normalement c'est >= 0.9f mais quand je le met ça bloque à la fin de la première animation, ça ne lance pas la 2e
            //du coup le canvas ne se détruit pas et le jeu se joue quand même derrière
            //donc je laisse ça dans un soucis de lisibilité du jeu quand tu le vas le tester
            //je crois que je viens d'où vient l'erreur mais j'ai le cerveau en compote, donc j'arrive pas du tout à la corriger, je laisse comme ça désolé ^^
            //Mais je crois que j'ai à peu près bien fait tout le reste, merci d'avoir prit de ton temps ajd pour m'aider ^^

            if (loading.progress <= 0.9f)
            {
                loading.allowSceneActivation = true;
                loadingAnimator.SetTrigger("EndLoading");
            }

            yield return new WaitForSeconds(animationTime);
        }

    }
 
}
