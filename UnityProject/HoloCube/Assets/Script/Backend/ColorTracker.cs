namespace Backend
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public sealed class ColorTracker
    {
        private static volatile ColorTracker instance;
        private static object syncRoot = new Object();
        public Dictionary<int, List<Color>> tempColors;
        public Dictionary<string, List<Color>> permColors;
        public Dictionary<string, Color> PlaneIDsAndColors;
        private bool hasTempChanged;
        private Translator ts;

        private ColorTracker() { }


        public static ColorTracker Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ColorTracker();
                    }
                }

                return instance;
            }
        }
    

    public void Start()
        {
            Instance.permColors = new Dictionary<string, List<Color>>();
            Instance.tempColors = new Dictionary<int, List<Color>>();
            Instance.PlaneIDsAndColors = new Dictionary<string, Color>();
        }

        public void addToPerm(List<Color> list, string color)
        {
            if (ValidListChecker(list, color))
            {
                permColors.Add(color, list);
            }
        }

        public void addToTemp(List<Color> list)
        {
            if (ValidListChecker(list))
            {
                if (tempColors.ContainsKey(permColors.Count) && tempColors[permColors.Count].Equals(list))
                {
                }
                else
                {
                    tempColors[permColors.Count] = list;
                    setTempListBool(true);
                }
            }
        }

        public bool hasTempListChanged()
        {
            return hasTempChanged;
        }

        public void setTempListBool(bool b)
        {
            hasTempChanged = b;
        }


        public bool saveTempColorsToPerm(string s)
        {
            if (tempColors.ContainsKey(permColors.Count)) { 
            if (ValidListChecker(Instance.tempColors[permColors.Count]))
            {
                    Debug.Log("permColors[i] = " + permColors.Count);
                    permColors[s] = tempColors[permColors.Count];


                }
                return true;
            }
            else return false;
        }

        public bool ValidListChecker(List<Color> list, string color)
        {
            return true;
        }

        public bool ValidListChecker(List<Color> list)
        {
            if (list != null && list.Count == 9)
            {
         //       Debug.Log("list pass");
                return true;
            }
            else return false;
        }

        public void SavePlaneIDsAndCorrespondingColor(Dictionary<string, Color> di)
        {
            Instance.PlaneIDsAndColors = di;
        }

        public void SendToTranslate()
        {
            ts = new Translator();
            Debug.Log(ts.TranslateFromDictToString(Instance.permColors));
        }


        public List<Color> GetTempList()
        {
            if (tempColors == null) return null;
            if (tempColors.ContainsKey(permColors.Count)) return tempColors[permColors.Count];
            else return null;
        }

        public List<Color> getPermList(string s)
        {
            return permColors[s];
        }

        public int getProgress()
        {
            if (permColors == null) return 0;
             else return permColors.Count;
        }

        public void Update()
        {

        }
    }



    static public class MethodExtensionForMonoBehaviourTransform
    {
        /// <summary>
        /// Gets or add a component. Usage example:
        /// BoxCollider boxCollider = transform.GetOrAddComponent<BoxCollider>();
        /// </summary>
        static public T GetOrAddComponent<T>(this Component child) where T : Component
        {
            T result = child.GetComponent<T>();
            if (result == null)
            {
                result = child.gameObject.AddComponent<T>();
            }
            return result;
        }
    }
}