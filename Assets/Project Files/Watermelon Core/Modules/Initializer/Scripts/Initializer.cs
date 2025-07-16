#pragma warning disable 0649

using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Watermelon
{
    [DefaultExecutionOrder(-999)]
    public class Initializer : MonoBehaviour
    {
        [SerializeField] ProjectInitSettings initSettings;
        [SerializeField] EventSystem eventSystem;

        public static GameObject GameObject { get; private set; }
        public static Transform Transform { get; private set; }

        public static ProjectInitSettings InitSettings { get; private set; }

        public static bool IsInititalized { get; private set; }
        public static bool IsStartInitialized { get; private set; }
        public GameObject Splash;
        public void Awake()
        {
            if (!IsInititalized)
            {
                IsInititalized = true;

                InitSettings = initSettings;

                GameObject = gameObject;
                Transform = transform;

#if MODULE_INPUT_SYSTEM
                eventSystem.gameObject.AddComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
#else
                eventSystem.gameObject.AddComponent<StandaloneInputModule>();
#endif

                DontDestroyOnLoad(gameObject);

                initSettings.Init(this);
            }
        }

        public void Start()
        {
            //Init(true);
           
            StartCoroutine(LoadNextScene());
        }
        IEnumerator LoadNextScene()
        {
            Splash.SetActive(true);
            //yield return new WaitForSeconds(4.2f);
            //Splash.SetActive(false);
            yield return new WaitForSeconds(6.7f);
            Init(true);
        }
        public void Init(bool loadingScene)
        {
            if (!IsStartInitialized)
            {
                IsStartInitialized = true;

                if (loadingScene)
                {
                    GameLoading.LoadGameScene();
                }
                else
                {
                    GameLoading.SimpleLoad();
                }
            }
        }

        public static bool IsModuleInitialized(Type moduleType)
        {
            ProjectInitSettings projectInitSettings = InitSettings;

            InitModule[] initModules = null;

#if UNITY_EDITOR
            if (!IsInititalized)
            {
                projectInitSettings = RuntimeEditorUtils.GetAssetByName<ProjectInitSettings>();
            }
#endif

            if (projectInitSettings != null)
            {
                initModules = projectInitSettings.Modules;
            }

            for (int i = 0; i < initModules.Length; i++)
            {
                if (initModules[i].GetType() == moduleType)
                {
                    return true;
                }
            }

            return false;
        }

        private void OnDestroy()
        {
            IsInititalized = false;
        }
    }
}
