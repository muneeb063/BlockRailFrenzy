using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon.BusStop;

namespace Watermelon
{
    public class BusBehavior : MonoBehaviour
    {
        [SerializeField] Transform enterPosition;

        [SerializeField] Animator animator;

        public List<Transform> seats = new List<Transform>();

        public LevelElement.Type Type { get; private set; }

        private BusStateMachine stateMachine;

        public List<BaseCharacterBehavior> passengers = new List<BaseCharacterBehavior>();

        public int PassengersCount => passengers.Count;
        public bool HasAvailableSit => passengers.Count < 3;
        public bool IsAvailableToEnter { get; private set; }
        public GameObject DetectionPoint;
        public GameObject BusHead;
        public GameObject Doors;
        private void Awake()
        {
            stateMachine = GetComponent<BusStateMachine>();
            stateMachine.Init(this);
        }

        private void OnEnable()
        {
            stateMachine.StartMachine();
            if (PlayerPrefs.GetInt("BusHead") == 0)
            {
                PlayerPrefs.SetInt("BusHead", 1);
                BusHead.SetActive(true);
                AudioController.PlaySound(AudioController.AudioClips.TrainEntrance);
            }
        }

        private void OnDisable()
        {
            stateMachine.StopMachine();
            if (PlayerPrefs.GetInt("BusHead") == 1)
            {
                PlayerPrefs.SetInt("BusHead", 0);
                BusHead.SetActive(false);
               // AudioController.PlaySound(AudioController.AudioClips.TrainEntrance);
            }
        }

        public void SetType(LevelElement.Type type)
        {
            Type = type;
        }

        public void Spawn()
        {
            if (PlayerPrefs.GetInt("BusHead") == 0)
            {
                PlayerPrefs.SetInt("BusHead", 1);
                BusHead.SetActive(true);
                AudioController.PlaySound(AudioController.AudioClips.TrainEntrance);
            }
            transform.position = LevelController.Environment.BusSpawnPos;

            passengers.Clear();
        }

        public void MoveToWaitingPos()
        {
            EnvironmentBehavior.AssignWaitingBus(this);
            Move(LevelController.Environment.BusWaitPos);
        }
        
        public void MoveToCollectingPos()
        {
            AudioController.PlaySound(AudioController.AudioClips.TrainMove);
            EnvironmentBehavior.AssignCollectingBus(this);
            //Debug.LogError("Collecting Bus Came");
            DetectionPoint.SetActive(true);
            if (EnvironmentBehavior.WaitingBus == this)
                EnvironmentBehavior.RemoveWaitingBus();

            var duration = 1;

            if (Vector3.Distance(transform.position, LevelController.Environment.BusSpawnPos) < 0.1f)
            {
                duration = 2;
            }
            Move(LevelController.Environment.BusCollectPos, duration, MakeAvailable);
        }
        public void PlayDoorAnim()
        {
            Doors.GetComponent<Animation>().Play("DoorAnim");
        }
        public void Collect(BaseCharacterBehavior passenger)
        {
            customManagerScript.instance.AnimationCounter = 10;
            passengers.Add(passenger);
            var sitIndex = passengers.Count - 1;
          //  customManagerScript.instance.comboSpriteManager.RegisterAction();
            passenger.MoveTo(new Vector3[] { enterPosition.transform.position }, false, () =>
            {
                var sit = seats[sitIndex];
                passenger.transform.SetParent(sit);
                passenger.transform.position = sit.position;
                passenger.transform.rotation = Quaternion.Euler(0, 90, 0);
                passenger.PlaySpawnAnimation();
                GameObject particle= Instantiate(customManagerScript.instance.SitParticle, sit.position, Quaternion.identity);
               // Destroy(particle, 2f);
                passenger.OnElementSubmittedToBus();
                if (!HasAvailableSit)
                    IsAvailableToEnter = false;
            });
        }

        public void CollectInstant(BaseCharacterBehavior passenger)
        {
            passengers.Add(passenger);
            var sitIndex = passengers.Count - 1;
            if (passenger.GetComponent<HumanoidCharacterBehavior>().IsBombed == true)
            {
                passenger.GetComponent<HumanoidCharacterBehavior>().DiffuseBomb();
            }
            if (passenger.GetComponent<HumanoidCharacterBehavior>().isKeyPassenger == true)
            {
                passenger.GetComponent<HumanoidCharacterBehavior>().TryUnlockLockedPassengerWithKey();
            }
            if (passenger.GetComponent<HumanoidCharacterBehavior>().isLocked == true)
            {
                passenger.GetComponent<HumanoidCharacterBehavior>().TurnOffLock();
            }
            var sit = seats[sitIndex];
            passenger.transform.SetParent(sit);
            passenger.transform.position = sit.position;
            passenger.transform.rotation = Quaternion.Euler(0, 90, 0);
            passenger.PlaySpawnAnimation();
            passenger.OnElementSubmittedToBus();
            if (!HasAvailableSit)
                IsAvailableToEnter = false;
        }

        private void MakeAvailable()
        {
            IsAvailableToEnter = true;

            if (EnvironmentBehavior.WaitingBus == null)
                EnvironmentBehavior.SpawnNextBusFromQueue();
        }

        public void MoveToExit()
        {
           // Debug.LogError("Gone");
          /*  LevelController.OnMatchComplete();
            customManagerScript.instance.comboSpriteManager.ShowCombo();*///Test
            EnvironmentBehavior.RemoveCollectingBus();
            if (GameController.RemoveTrain == true)
            {
                GameController.RemoveTrain = false;
                Move(LevelController.Environment.TrainExit, 1, Clear);
                AudioController.PlaySound(AudioController.AudioClips.TrainMove);
            }
            else
            {
                Move(LevelController.Environment.BusExitPos, 1, Clear);
            }
            
        }

        private TweenCaseCollection moveCase;

        public void Move(Vector3 position, float duration = 1, SimpleCallback onReached = null)
        {
            
            if (moveCase != null && !moveCase.IsComplete())
                moveCase.Kill();

            moveCase = Tween.BeginTweenCaseCollection();

            if (animator != null)
                animator.SetTrigger("Start");
            transform.DOMove(position, duration).OnComplete(onReached).SetEasing(Ease.Type.QuadOutIn);
            if (animator != null)
                Tween.DelayedCall(duration - 0.1f, () => animator.SetTrigger("Break"));

            Tween.EndTweenCaseCollection();
        }

        public void Clear()
        {
            if (moveCase != null && !moveCase.IsComplete())
                moveCase.Kill();
            IsAvailableToEnter = false;

            /* for (int i = 0; i < passengers.Count; i++)
             {
                 passengers[i].transform.SetParent(null);
                 passengers[i].gameObject.SetActive(false);
             }*/
            Invoke(nameof(DelayForPassangerExit), 2f);
            passengers.Clear();

            stateMachine.StopMachine();

           // gameObject.SetActive(false);//Bus band hona band krdi
        }
        void DelayForPassangerExit()
        {
            for (int i = 0; i < passengers.Count; i++)
            {
                passengers[i].transform.SetParent(null);
               // passengers[i].gameObject.SetActive(false);
            }
        }
    }
}