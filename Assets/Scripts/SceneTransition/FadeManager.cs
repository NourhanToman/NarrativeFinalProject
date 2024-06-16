using Google.Protobuf.WellKnownTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AccrossTheEvergate
{
    public class FadeManager : MonoBehaviour
    {
        [Header("Fading Parameters")]
        [SerializeField] CanvasGroup _canvasGroup;
        [SerializeField] float _fadeDuration = 5.0f;

        [Header("Locations")]
        [SerializeField] Transform newPlayerLocation;
        [SerializeField] GameObject playerObj;

        [SerializeField] List<GameObject> Future;
        [SerializeField] List<GameObject> Past;

        [SerializeField] GameObject oldEnviroment;
        [SerializeField] GameObject newEnviroment;

        public static FadeManager instance;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                ServiceLocator.Instance.RegisterService(this);
                DontDestroyOnLoad(gameObject);
            }
        }

        public IEnumerator FadeOut()
        {
            float elapsedTme = 0f;
            while (elapsedTme < _fadeDuration)
            {
                elapsedTme += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, 1f, elapsedTme / _fadeDuration);
                yield return null;
            }
            _canvasGroup.alpha = 1f;
        }

        public IEnumerator FadeIn()
        {
            float elapsedTme = 0f;
            while (elapsedTme < _fadeDuration)
            {
                elapsedTme += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, 0f, elapsedTme / _fadeDuration);
                yield return null;
            }
            _canvasGroup.alpha = 0f;
        }

        public IEnumerator Fade()
        {
            yield return StartCoroutine(FadeOut());

            newEnviroment.gameObject.SetActive(true);
            playerObj.transform.position = newPlayerLocation.position;
            Future.ForEach(x => x.gameObject.SetActive(false));
            Past.ForEach(x => x.gameObject.SetActive(true));
            oldEnviroment.gameObject.SetActive(false);


            yield return StartCoroutine(FadeIn());
        }

 
    }
}
