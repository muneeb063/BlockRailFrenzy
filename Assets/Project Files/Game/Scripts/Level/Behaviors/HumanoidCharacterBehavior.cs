using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
namespace Watermelon.BusStop
{
    [RequireComponent(typeof(BoxCollider))]
    public class HumanoidCharacterBehavior : BaseCharacterBehavior
    {
        private static readonly int PARTICLE_POOF = "Poof".GetHashCode();

        private static readonly int ANIMATOR_IDLE_ANIMATION = Animator.StringToHash("Idle");
        private static readonly int ANIMATOR_SITTING_ANIMATION = Animator.StringToHash("Sitting");

        private static readonly int ANIMATOR_MOVEMENT_BOOL = Animator.StringToHash("IsMoving");
        private static readonly int ANIMATOR_SLOTS_BOOL = Animator.StringToHash("Slots");

        [SerializeField] Animator graphicsAnimator;
        [SerializeField] ParticleSystem trailParticleSystem;

        [Header("Movement")]
        [SerializeField] float movementSpeed = 10;

        [Header("Outline")]
        [SerializeField] float outlineActiveWidth = 3.5f;
        [SerializeField] float outlineDisableWidth = 1.5f;

        private TweenCase rotateTweenCase;

        private Coroutine movementCoroutine;

        private MaterialPropertyBlock propertyBlock;
        public GameObject TrailWalkParticle;
        public bool shouldClick = false;
        public ParticleSystem AngryParticle,HappyParticle;
        //Freeze Data
        [Header("Passengers Freeze Info")]
        public bool isFrozen = false;
        public Material originalMaterial;
        public int freezeCounter = 0;
        public TextMeshPro FreezeCounterText;
        public GameObject freezeUI;
        public ParticleSystem SmokeParticle;
        [Header("Passengers Bomb Info")]
        public bool IsBombed = false;
        public GameObject BombHead;
        public float BombTime = 0f;
        public TextMeshPro BombTimerText;
        public GameObject explosionEffectPrefab;    // Particle explosion prefab
        private bool hasExploded = false;
        private float currentTime;
        private bool hasStartedCountdown = false;
        [Header("Passengers Lock Info")]
        public bool isLocked = false;
        public GameObject LockModel;
        public int LockCount = 0;
        public TextMeshPro LockCountText;
        [Header("Passengers Key Info")]
        public bool isKeyPassenger = false;
        public GameObject KeyModel;
        public GameObject Smoke;
        public override void Initialise(LevelElement levelElement, ElementPosition elementPosition)
        {
            base.Initialise(levelElement, elementPosition);

            isMovementActive = false;
            isSubmitted = false;

            trailParticleSystem.Stop();

            graphicsAnimator.SetBool(ANIMATOR_MOVEMENT_BOOL, false);

            propertyBlock = new MaterialPropertyBlock();

            characterRenderer.SetOutlineWidth(propertyBlock, outlineDisableWidth);
        }

        public override void MoveTo(Vector3[] path, bool isSlots, SimpleCallback onCompleted)
        {
            graphicsAnimator.SetBool(ANIMATOR_SLOTS_BOOL, isSlots);
            graphicsAnimator.SetBool(ANIMATOR_MOVEMENT_BOOL, true);

            if (isSlots)
            {
                graphicsAnimator.Play("MovementSlot", -1, Random.Range(0.0f, 1.0f));
              
            }

            if (movementCoroutine != null) StopCoroutine(movementCoroutine);
            movementCoroutine = StartCoroutine(MovementCoroutine(path, movementSpeed, !isSlots, () =>
            {
                graphicsAnimator.SetBool(ANIMATOR_MOVEMENT_BOOL, false);
               // Debug.LogError("sLOT PR HAI");
                trailParticleSystem.Stop();

                onCompleted?.Invoke();
            }));
        }

        public override void OnElementClicked(bool isClickAllowed)
        {
           
           // Debug.LogError("Outside Called");
            if (isClickAllowed || shouldClick==true)
            {
                var character = gameObject.GetComponent<HumanoidCharacterBehavior>();

                // If Hammer Powerup is enabled
                if (customManagerScript.instance.isHammerPowerupEnabled)
                {
                    // Only allow click if character is frozen
                    if (character.isFrozen)
                    {
                        character.isClicked = false;
                        customManagerScript.instance.HammerUIState(false);
                        character.freezeCounter = 0;
                        character.Unfreeze();
                        customManagerScript.instance.isHammerPowerupEnabled = false;
                        PUController.ResetBehaviors();
                        // Optionally play a particle or sound here to indicate unfreeze success
                       // AudioController.PlaySound(AudioController.AudioClips.unfreezeSound); // <- replace with your sound
                        HappyParticle.Play();
                        PassengerActionManager.instance.EnableFreezedPassengersColliders(false);
                        
                    }

                    return; // Exit after handling hammer logic
                }
                if (IsBombed == true)
                {
                    DiffuseBomb();
                }
                // Key Unlock Logic
                TryUnlockLockedPassengerWithKey();
               /* if (isKeyPassenger)
                {
                    // Make sure there is a locked character
                    if (PassengerActionManager.instance.LockedCharacters != null && PassengerActionManager.instance.LockedCharacters.Count > 0)
                    {
                        var lockedCharacter = PassengerActionManager.instance.LockedCharacters[0];

                        lockedCharacter.LockCount--;

                        // Update UI
                        if (lockedCharacter.LockCountText != null)
                            lockedCharacter.LockCountText.text = lockedCharacter.LockCount.ToString();

                        if (lockedCharacter.LockCount <= 0)
                        {
                            lockedCharacter.isLocked = false;

                            if (lockedCharacter.LockModel != null)
                                lockedCharacter.LockModel.SetActive(false);

                            var lockedCollider = lockedCharacter.GetComponent<BoxCollider>();
                            if (lockedCollider != null)
                                lockedCollider.enabled = true;

                            PassengerActionManager.instance.LockedCharacters.RemoveAt(0);
                        }
                    }

                    // Remove key status from current passenger
                    isKeyPassenger = false;

                    if (KeyModel != null)
                        KeyModel.SetActive(false);

                    if (PassengerActionManager.instance.KeyPassengers.Contains(this))
                        PassengerActionManager.instance.KeyPassengers.Remove(this);
                }
                ////////*/
                // Debug.LogError("Clicked");
                if (PlayerPrefs.GetInt("UndoPowerUp") == 1 )
                {
                    PlayerPrefs.SetInt("UndoPowerUp", 0);
                    customManagerScript.instance.EnableUndoTutorial();
                }
                if (PassengerActionManager.instance.FreezedPassengers!=null && PassengerActionManager.instance.FreezedPassengers.Count>0)
                {

                    PassengerActionManager.instance.FreezedPassengers[0].DecreaseFreezeCounter();
                }
                customManagerScript.instance.lastcharacter = gameObject.GetComponent<HumanoidCharacterBehavior>();
                customManagerScript.instance.characterBehaviors.Add(gameObject.GetComponent<HumanoidCharacterBehavior>());
                LevelController.OnElementClicked(this, elementPosition);
                HappyParticle.Play();
                AudioController.PlaySound(AudioController.AudioClips.clickSound);
                //Debug.LogError("Inside path "/* + path[0]*/);
                Vector3[] path = MapUtils.CalculatePath(elementPosition.X, elementPosition.Y, LevelController.LevelMap);
                Vector3 rotationVector = path[0] - transform.position;
                rotationVector.z = 0;
                
                trailParticleSystem.Play();

                if (rotationVector != Vector3.zero)
                {
                    rotateTweenCase = new TweenCaseRotateTowards(transform, Quaternion.LookRotation(rotationVector.normalized, Vector3.back), 1200, 0.1f);
                    rotateTweenCase.SetDuration(float.MaxValue);
                    rotateTweenCase.OnComplete(() =>
                    {
                        MoveTo(path, false, () =>
                        {
                            LevelController.OnElementSubmittedToSlot(this, false);
                            
                            shouldClick = false;
                        });
                    });
                    rotateTweenCase.StartTween();
                }
                else
                {
                    MoveTo(path, false, () =>
                    {
                        LevelController.OnElementSubmittedToSlot(this, false);
                      
                        shouldClick = false;
                    });
                }

                LevelController.SubmitElement(this, elementPosition);
            }
            else
            {
               // Debug.LogError("Nahi Click Horha");
                AngryParticle.Play();
            }
        }
        public void DiffuseBomb()
        {
            IsBombed = false;
            BombHead.SetActive(false);
            PassengerActionManager.instance.BombPassengers.Remove(gameObject.GetComponent<HumanoidCharacterBehavior>());
        }
        public override void OnElementSubmittedToBus()
        {
            ParticlesController.PlayParticle(PARTICLE_POOF).SetPosition(transform.position + new Vector3(0, 1, 0));
            AudioController.PlaySound(AudioController.AudioClips.SitSound);
            //trailParticleSystem.Play();
            graphicsAnimator.Play(ANIMATOR_SITTING_ANIMATION, -1, Random.Range(0.0f, 1.0f));
            Invoke(nameof(turnoff), 2f);
        }
        void turnoff()
        {
            TrailWalkParticle.SetActive(false);
        }
        public override void Highlight(bool firstSpawn)
        {
            base.Highlight(firstSpawn);

            graphicsAnimator.Play(ANIMATOR_IDLE_ANIMATION, -1, Random.Range(0.0f, 1.0f));
            gameObject.GetComponent<BoxCollider>().size = new Vector3(0.65f, 3f, 0.7984462f);
            characterRenderer.SetOutlineWidth(propertyBlock, outlineActiveWidth);
        }

        public override void Unhighlight()
        {
            base.Unhighlight();

            if (isSubmitted) return;

            characterRenderer.SetOutlineWidth(propertyBlock, outlineDisableWidth);
        }

        public override void Unload()
        {
            base.Unload();

            rotateTweenCase.KillActive();
        }

        private void Reset()
        {
#if UNITY_EDITOR
            characterRenderer = transform.GetComponentInChildren<Renderer>();
            graphicsAnimator = transform.GetComponentInChildren<Animator>();
            trailParticleSystem = transform.GetComponentInChildren<ParticleSystem>();
            if (trailParticleSystem == null)
            {
                if (!UnityEditor.PrefabUtility.IsPartOfAnyPrefab(gameObject))
                {
                    GameObject dustParticle = RuntimeEditorUtils.GetAssetByName<GameObject>("Running Dust");
                    if (dustParticle != null)
                    {
                        GameObject particleInstance = GameObject.Instantiate(dustParticle, transform);
                        particleInstance.name = dustParticle.name;

                        trailParticleSystem = particleInstance.GetComponent<ParticleSystem>();
                    }
                }
            }

            movementSpeed = 10;

            BoxCollider boxCollider = transform.GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                boxCollider.isTrigger = true;
            }
#endif
        }
        public void ResetData()
        {
            //
        }
        public void ResetCharacter(LevelElement levelElement, ElementPosition elementPosition)
        {
            Initialise(levelElement, elementPosition);
        }

        #region Freeze

        public void DecreaseFreezeCounter()
        {
            if (!isFrozen)
                return;
            freezeCounter--;

            if (freezeCounter <= 0)
                Unfreeze();
            else
                UpdateFreezeUI(true);
        }

        public void Unfreeze()
        {
            if (characterRenderer != null && originalMaterial != null)
                characterRenderer.material = originalMaterial;

            GetComponent<BoxCollider>().enabled = true;
            this.enabled = true;
            isFrozen = false;
            SmokeParticle.Play();
            PassengerActionManager.instance.FreezedPassengers.RemoveAt(0);
            UpdateFreezeUI(false);
        }

        private void UpdateFreezeUI(bool show)
        {
            if (freezeUI != null)
                freezeUI.SetActive(show);

            if (FreezeCounterText!= null)
                FreezeCounterText.text = freezeCounter.ToString();
        }
        #endregion
        #region Bomb
        void Update()
        {
           /* Debug.LogError("IsBombed " + IsBombed);
            Debug.LogError("hasExploded " + hasExploded);
            Debug.LogError("hasStartedCountdown " + hasStartedCountdown);
            Debug.LogError("isLevelPaused " + customManagerScript.instance.isLevelPaused);*/
            if (IsBombed==false || hasExploded==true || hasStartedCountdown ==false || customManagerScript.instance.isLevelPaused==true/*||customManagerScript.instance.isLevelFailed==false*/)
                return;

            currentTime -= Time.deltaTime;
            currentTime = Mathf.Max(0, currentTime);

            if (BombTimerText != null)
                BombTimerText.text = Mathf.CeilToInt(currentTime).ToString();

            if (currentTime <= 0)
            {
                Explode();
            }
        }

        public void StartBombTimer()
        {
            
            if (!IsBombed || hasStartedCountdown)
                return;

            BombHead.SetActive(true);              // Show bomb if not already shown
            currentTime = BombTime;                // Reset timer
            hasStartedCountdown = true;            // Begin countdown
          
        }
        void Explode()
        {
            PassengerActionManager.instance.BombPassengers.Remove(gameObject.GetComponent<HumanoidCharacterBehavior>());
            hasExploded = true;
            customManagerScript.instance.isLevelFailed = true;
            // Play explosion particles
            if (explosionEffectPrefab !=null)
            {
                explosionEffectPrefab.GetComponent<ParticleSystem>().Play();
            }
            GameController.LoseGame();
            BombHead.SetActive(false);
        }
        public void ResetBombTimer()
        {
            IsBombed = true;
            hasExploded = false;
            hasStartedCountdown = true;
            currentTime = BombTime;
            BombHead.SetActive(true);
        }
        #endregion
        #region Lock Passenger
        public void TryUnlockLockedPassengerWithKey()
        {
            if (!isKeyPassenger)
                return;

            var lockedList = PassengerActionManager.instance.LockedCharacters;
            if (lockedList == null || lockedList.Count == 0)
                return;

            var targetLockedCharacter = lockedList[0];
            Vector3 start = transform.position + Vector3.up * 1.5f; // Above the key passenger
            Vector3 end = targetLockedCharacter.transform.position + Vector3.up * 1.5f;

            // Spawn and animate key
            GameObject keyObj = Instantiate(PassengerActionManager.instance.KeyPrefab, start, Quaternion.identity);

            // Optional: Set parent to something like UI canvas or main container
            // keyObj.transform.SetParent(transform.parent, true);

            // Animate with DOTween
            keyObj.transform.DOMove(end, 0.6f).SetEasing(Ease.Type.Linear).OnComplete(() =>
            {
                // Unlock logic
                targetLockedCharacter.LockCount--;

                if (targetLockedCharacter.LockCountText != null)
                    targetLockedCharacter.LockCountText.text = targetLockedCharacter.LockCount.ToString();

                if (targetLockedCharacter.LockCount <= 0)
                {
                    targetLockedCharacter.isLocked = false;
                    targetLockedCharacter.Smoke.GetComponent<ParticleSystem>().Play();
                    if (targetLockedCharacter.LockModel != null)
                        targetLockedCharacter.LockModel.SetActive(false);

                    var collider = targetLockedCharacter.GetComponent<BoxCollider>();
                    if (collider != null)
                        collider.enabled = true;

                    lockedList.RemoveAt(0);
                }

                // Disable Key Passenger Status
                isKeyPassenger = false;

                if (KeyModel != null)
                    KeyModel.SetActive(false);

                if (PassengerActionManager.instance.KeyPassengers.Contains(this))
                    PassengerActionManager.instance.KeyPassengers.Remove(this);

                // Destroy key prefab
                Destroy(keyObj);
            });
        }
        public void TurnOffLock()
        {
            var lockedList = PassengerActionManager.instance.LockedCharacters;
            if (lockedList == null || lockedList.Count == 0)
                return;

            var targetLockedCharacter = lockedList[0];
            targetLockedCharacter.isLocked = false;
            if (targetLockedCharacter.LockModel != null)
                targetLockedCharacter.LockModel.SetActive(false);
            var collider = targetLockedCharacter.GetComponent<BoxCollider>();
            if (collider != null)
                collider.enabled = true;

            lockedList.RemoveAt(0);
        }
        #endregion
    }
}
