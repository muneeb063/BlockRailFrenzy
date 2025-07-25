using System.Collections.Generic;
using UnityEngine;
using Watermelon.BusStop;

namespace Watermelon
{
    public class EnvironmentBehavior : MonoBehaviour
    {
        [SerializeField] DockBehavior dock;
        public DockBehavior Dock => dock;

        [SerializeField] Transform busSpawnPos;
        [SerializeField] Transform busWaitPos;
        [SerializeField] Transform busCollectPos;
        [SerializeField] Transform busExitPos;
        [SerializeField] Transform CompletebusExitPos;
        public Vector3 BusSpawnPos => busSpawnPos.position;
        public Vector3 BusWaitPos => busWaitPos.position;
        public Vector3 BusCollectPos => busCollectPos.position;
        public Vector3 BusExitPos => busExitPos.position;
        public Vector3 TrainExit=>CompletebusExitPos.position;

        private static Dictionary<LevelElement.Type, PoolGeneric<BusBehavior>> busTypesPoolsDictionary;

        private static List<LevelElement.Type> busTypeQueue = new List<LevelElement.Type>();

        public static BusBehavior WaitingBus { get; private set; }
        public static BusBehavior CollectingBus { get; private set; }

        public static bool IsWaitingPlaceAvailable => WaitingBus == null;
        public static bool IsCollectingPlaceAvailable => CollectingBus == null;

        private static TweenCase secondBusSpawnCase;
        public GameObject[] LockedSlots,unlockedSlots;
        public GameObject DockHand;
        private void OnDestroy()
        {
            secondBusSpawnCase.KillActive();
        }

        #region Initialisation 

        /// <summary>
        /// Creates pools and subscribes to events. Should be called once at the beggining of game session
        /// </summary>
        public static void InitEnvironment()
        {
            if (GameController.Data.ActivateVehicles)
            {
                PopulateBusTypePoolDictionary((BusSkinData)SkinsController.Instance.GetSelectedSkin<BusSkinsDatabase>());

                SkinsController.SkinSelected += OnSkinSelected;
            }
        }
        private void Update()
        {
            if (PlayerPrefs.GetInt("Slot1Status") == 1)
            {
                Slot1Unlock();
            }
            if (PlayerPrefs.GetInt("Slot2Status") == 1)
            {
                Slot2Unlock();
            }
        }
        public void UnlockSlot()
        {
           
            if (LevelController.DisplayLevelNumber+1 >= 50 && PlayerPrefs.GetInt("Slot1Status")==0)
            {
                PlayerPrefs.SetInt("Slot1Status", 1);
                Slot1Unlock();
            }
            if(LevelController.DisplayLevelNumber+1 >= 100 && PlayerPrefs.GetInt("Slot2Status") == 0)
            {
                PlayerPrefs.SetInt("Slot2Status", 1);
                Slot2Unlock();
            }
        }
        public void Slot1Unlock()
        {
            Dock.slot1.SetActive(true);
            Dock.slot1Locked.SetActive(false);
            Dock.slot1.GetComponent<SlotBehavior>().Clear();
            Dock.ResetSlots();
        }
        public void Slot2Unlock()
        {
            Dock.slot2.SetActive(true);
            Dock.slot2Locked.SetActive(false);
            Dock.slot2.GetComponent<SlotBehavior>().Clear();
            Dock.ResetSlots();
        }
        public void LockSlot1()
        {
            Dock.slot1.SetActive(false);
            Dock.slot1Locked.SetActive(true);
            Dock.slot1.GetComponent<SlotBehavior>().Clear();
            
            Dock.ResetSlots();
        }
        public void LockAllSlots()
        {
            for(int i = 0; i < LockedSlots.Length; i++)
            {
                LockedSlots[i].SetActive(true);
                unlockedSlots[i].SetActive(false);
                unlockedSlots[i].GetComponent<SlotBehavior>().Clear();
                Dock.ResetSlots();
            }
           // Dock.slot2.GetComponent<SlotBehavior>().Clear();
            
        }
        public void LockSlot2()
        {
            Dock.slot2.SetActive(false);
            Dock.slot2Locked.SetActive(true);
            Dock.slot2.GetComponent<SlotBehavior>().Clear();
            Dock.ResetSlots();
        }
        public void CheckMatch()
        {
            Dock.CheckBusMatch();
        }
        private static void OnSkinSelected(ISkinData skinData)
        {
            if (skinData is BusSkinData)
            {
                BusSkinData busSkinData = (BusSkinData)skinData;

                UnloadSpawnedBusses();

                foreach (PoolGeneric<BusBehavior> pool in busTypesPoolsDictionary.Values)
                    PoolManager.DestroyPool(pool);

                PopulateBusTypePoolDictionary(busSkinData);

                busTypeQueue = new List<LevelElement.Type>(LevelController.LoadedStageData.BusSpawnQueue);

                SpawnNextBusFromQueue();
                secondBusSpawnCase = Tween.DelayedCall(0.9f, SpawnNextBusFromQueue);
            }
        }

        private static void PopulateBusTypePoolDictionary(BusSkinData busSkinData)
        {
            busTypesPoolsDictionary = new Dictionary<LevelElement.Type, PoolGeneric<BusBehavior>>();

            for (int i = 0; i < busSkinData.BusData.Length; i++)
            {
                PoolGeneric<BusBehavior> pool = new PoolGeneric<BusBehavior>(busSkinData.BusData[i].prefab);

                busTypesPoolsDictionary.Add(busSkinData.BusData[i].type, pool);
            }
        }

        #endregion

        #region Gameplay

        public static void StartSpawningBusses()
        {
            if (!GameController.Data.ActivateVehicles)
                return;

            busTypeQueue = new List<LevelElement.Type>(LevelController.LoadedStageData.BusSpawnQueue);

            SpawnNextBusFromQueue();

            secondBusSpawnCase = Tween.DelayedCall(0.9f, SpawnNextBusFromQueue, false);
        }

        public static void SpawnNextBusFromQueue()
        {
            if (!GameController.Data.ActivateVehicles)
                return;

            if (busTypeQueue.Count > 0)
            {
                var type = busTypeQueue[0];

                busTypeQueue.RemoveAt(0);

                busTypesPoolsDictionary[type].GetPooledComponent().SetType(type);
            }
        }

        public static void AssignWaitingBus(BusBehavior bus)
        {
            WaitingBus = bus;
        }

        public static void AssignCollectingBus(BusBehavior bus)
        {
            CollectingBus = bus;
        }

        /// <summary>
        /// Aslo spawns next bus if there are busses in the queue left
        /// </summary>
        public static void RemoveWaitingBus()
        {
            WaitingBus = null;

            if (busTypeQueue.Count > 0)
                SpawnNextBusFromQueue();
        }

        public static void RemoveCollectingBus()
        {
            CollectingBus = null;
        }

        #endregion

        #region Cleanup

        public static void UnloadSpawnedBusses()
        {
            if (!GameController.Data.ActivateVehicles)
                return;

            if (WaitingBus != null)
                WaitingBus.Clear();
            if (CollectingBus != null)
                CollectingBus.Clear();

            WaitingBus = null;
            CollectingBus = null;

            busTypesPoolsDictionary.ForEachValue((pool) => PoolManager.DestroyPool(pool));

            secondBusSpawnCase.KillActive();
        }

        #endregion
    }
}