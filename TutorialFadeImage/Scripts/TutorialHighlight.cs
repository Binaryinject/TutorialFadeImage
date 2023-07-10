namespace Abu
{
    using UnityEngine;

    /// <summary>
    /// Highlights RectTransform or Renderer. Shouldn't be added to game object without RectTransform or Renderer.
    /// </summary>
    [ExecuteInEditMode]
    [AddComponentMenu("UI/Tutorial Highlight")]
    public class TutorialHighlight : MonoBehaviour
    {
        // /// <summary>
        // /// TutorialFadeImage.InstanceImage to render hole.
        // /// Note: "Automatically finds TutorialFadeImage.InstanceImage in active scene. So it can't be null if there is any TutorialFadeImage.InstanceImage in the scene. To stop rendering hole use component's enabled flag.
        // /// </summary>
        // [SerializeField] 
        // [Tooltip("Automatically finds TutorialFadeImage.InstanceImage in active scene. So it can't be null if there is any TutorialFadeImage.InstanceImage in the scene. To stop rendering hole use component's enabled flag.")] 
        // TutorialFadeImage tutorialFade;
        
        public TutorialHole hole;

        /// <summary>
        /// Tutorial hole.
        /// </summary>
        TutorialHole Hole
        {
            get
            {
                if (hole == null)
                {
                    if(TryGetComponent(out RectTransform rectTransform))
                        hole = new RectTransformTutorialHole(rectTransform);
                    else if (TryGetComponent(out Renderer rendererComponent) && TutorialFadeImage.Instance)
                        hole = new RendererTutorialHole(rendererComponent, TutorialFadeImage.Instance);
                }

                return hole;
            }
        }

        void OnEnable() {
            if (TutorialFadeImage.Instance) TutorialFadeImage.Instance.AddHole(Hole);
        }

        void OnDisable() {
            if (TutorialFadeImage.Instance) TutorialFadeImage.Instance.RemoveHole(Hole);
        }

        void OnDestroy() {
            if (TutorialFadeImage.Instance) TutorialFadeImage.Instance.RemoveHole(Hole);
        }

#if UNITY_EDITOR

        void OnValidate()
        {
            if (TutorialFadeImage.Instance == null)
                TutorialFadeImage.Instance = FindObjectOfType<TutorialFadeImage>();
            
            if(isActiveAndEnabled && TutorialFadeImage.Instance != null)
                TutorialFadeImage.Instance.AddHole(Hole);
        }
#endif
    }

}