using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        // TODO will be edited soon.
        
        private CanvasGroup canvasGroup;
        private Coroutine activeFadeRoutine = null;
        
        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public IEnumerator FadeOutIn()
        {
            yield return FadeOut(3f);
            yield return FadeIn(1f);
        }
        
        public void FadeOutInstantly()
        {
            canvasGroup.alpha = 1;
        }
        
        public IEnumerator Fade(float target,float time)
        {
            if (activeFadeRoutine != null)
            {
                StopCoroutine(activeFadeRoutine);
            }
            
            activeFadeRoutine = StartCoroutine(FadeRoutine(target,time));
            yield return activeFadeRoutine;

        }

        IEnumerator FadeRoutine(float target,float time)
        {
            while (!Mathf.Approximately(canvasGroup.alpha,target))
            {
                Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        {
            return Fade(0, time);
        }

        public IEnumerator FadeOut(float time)
        {
            return Fade(1, time);
        }

       

        
    }    
}

