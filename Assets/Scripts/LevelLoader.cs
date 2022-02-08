using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class LevelLoader : MonoBehaviour
{
    public readonly int FirstGameLevel = 1;

    static LevelLoader _loader;
    private Slider slider;

    Coroutine _loadingCoroutine;

    private void Awake()
    {
        //Dont destroy non singleton instance. LoadLevel will be executed on the singleton this way without reassigning references.
        if (!_loader)
        {
            _loader = this;
              
            DontDestroyOnLoad(this);
            slider = GetComponentInChildren<Slider>();
        }

    }

    public void LoadLevel(int index)
    {
        if (_loader == this)
        {
            if (_loadingCoroutine != null)
            {
                StopCoroutine(_loadingCoroutine);
            }

            _loadingCoroutine = StartCoroutine(LoadAsync(index));
        }
        else
        {

            _loader.LoadLevel(index);
        
        }
    }

    IEnumerator LoadAsync(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        DOTween.KillAll();

        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / .9f);

            if (slider)
                slider.value = progress;

            Debug.Log(progress);

            yield return null;
        }

        if (slider)
            slider.value = 0f;
    }

}
