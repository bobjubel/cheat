using EFT;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using EFT.HealthSystem;
using System.Runtime.InteropServices;

namespace MumbleOverlay
{
    public class MumbleOverlay : MonoBehaviour
    {
        #region Declaration 
        public MumbleOverlay() { }

        private GameObject holderForGO;

        private IEnumerable<Player> plys;
        private IEnumerable<LootableContainer> cons;
        private IEnumerable<ExfiltrationPoint> exs;
        private IEnumerable<LootItem> li;
        private IEnumerable<Door> doors;

        private List<LootItem> ItemsToShow = new List<LootItem>();
        private List<LootItem> ItemsToShow2 = new List<LootItem>();

        private List<LootItem> ItemsToShowSpecial = new List<LootItem>();
        private List<LootItem> ItemsToShowSpecial2 = new List<LootItem>();

        private List<LootableContainer> ContainersToShow = new List<LootableContainer>();
        private List<LootableContainer> ContainersToShow2 = new List<LootableContainer>();

        private float _startTime;

        private string[] SearchArrayItems = new string[] {
            "6b43",
            "ak",
            "alkali",
            "altyn",
            "analyzer",
            "Antique",
            "gps",
            "beanle",
            "bitcoin",
            "bloodset",
            "book",
            "car",
            "case",
            "gold_chain",
            "condensed",
            "conditioner",
            "counter",
            "cowboy",
            "diary",
            "dollars",
            "dvl",
            "documents",
            "dogtag",
            "epsilion",
            "euros",
            "figurine",
            "filter",
            "firesteel",
            "freeman",
            "gilded",
            "glock",
            "GPhone",
            "graphics",
            "grizzly",
            "powder",
            "gyroscope",
            "headset",
            "ibuprofen",
            "key",
            "keycard",
            "keytool",
            "kit",
            "lab",
            "LEDX",
            "lion",
            "lube",
            "m1a",
            "m2",
            "m4a1",
            "minedetector",
            "morphine",
            "motor",
            "MRE",
            "MS2000",
            "msa",
            "mustache",
            "oil",
            "ophthalmoscope",
            "piligrim",
            "powerbank",
            "propane",
            "roler",
            "rooster",
            "rotor",
            "Roubles",
            "RPK-16",
            "RSASS ",
            "sample",
            "scav",
            "scope",
            "Secure",
            "shattered",
            "Silver",
            "SSD",
            "sv-98",
            "tea",
            "technical",
            "toughbook",
            "transilluminator",
            "tri-zip",
            "usb-flash",
            "uv_lamp",
            "wallet",
            "watch",
            "wilston",
            "beefstew 2",
            "hk",
            "NT-4",
            "ledx",
            "rfid",
            "vpx"
        };
        private string[] SearchArrayContainers = new string[]
        {
            "weapon",
            "box",
            "cover",
            "Medical",
            "Toolbox",
            "safe"
        };

        /*Weapons name switcher for faster method just add mising ones in order*/
        private string[] PissArayo_1 = new string[] {
            "op_sks",
            "Item",
            "toz_sks_762x39",
            "saiga-12k",
            "izhmash_saiga_9_9x19",
            "saiga-9",
            "Saiga",
            "akmns",
            "akmn",
            "akms",
            "akm",
            "pm",
            "molot_vepr_km_vpo_136_762x39",
            "molot_akm_vpo_209",
            "m4a1",
            "P226R",
            "MP-153",
            "MP-133",
            "aks74un",
            "aks74u",
            "ak74n",
            "ak74m",
            "AK74H",
            "ak101",
            "ak102",
            "ak103",
            "ak104",
            "ak105",
            "pb",
            "kedr",
            "klin",
            "mp5",
            "mpx",
            "val",
            "vss",
            "sv",
            "dv",
            "443",
            "rssas",
            "m1a",
            "toz-106",
            "toz_tt_762",
            "pp-19-01",
            "dsa_sa58",
            "glock17",
            "glock18"
        };
        private string[] PissArayo_2 = new string[] {
            "OP SKS",
            "SKS",
            "SKS",
            "12k",
            "Saiga-9",
            "Saiga-9",
            "12K",
            "AKM-NS",
            "AKM-N",
            "AKM-S",
            "AKM",
            "P. Makarov",
            "VeprKM-VPO",
            "AKM-VPO-eco",
            "M4A1",
            "P. P226R",
            "MR-153",
            "MP-133",
            "AKS-74UN",
            "AKSU",
            "AK-74N",
            "AK-74M",
            "AK-74H",
            "AK-101",
            "AK-102",
            "AK-103",
            "AK-104",
            "AK-105",
            "P.T Makarov",
            "KEDR",
            "KLIN",
            "MP-5",
            "MP-X",
            "AS VAL",
            "VSS",
            "SV98",
            "DVL",
            "P. Grach",
            "RSSAS",
            "M1A",
            "TOZ",
            "TT",
            "PP-19",
            "FAL",
            "Glock17",
            "Glock18"
        };

        private string SpecItemNameToSearchStr = "";
        private string searchSpecialItem = "";

        private readonly bool _norPlayer = false;
        private readonly bool _norItems = false;
        private readonly bool _norContainers = false;

        private float _playerUpdateTime;
        private float _exitsUpdateTime;
        private float _itemsUpdateTime;
        private float _itemsSpecUpdateTime;
        private float _containersUpdateTime;

        private float _playersUDPtime = 15f;

        private readonly float _itemUPDtime = 3000f;
        private readonly float _itemSpecUPDtime = 5000f;
        private readonly float _containersUPDTime = 5000f;
        private readonly float _exitUPFtime = 5000f;

        private bool _overlaySwitcher;

        private bool _playersMappingSwitrcher;
        private bool _AIoverlaySwitcher;
        private bool _deadBodiesSwitcher;
        private bool _exsitsMappingSwitcher;
        private bool _itemsMappingSwitcher;
        private bool _itemsSpecMappingSwitcher;
        private bool _containersMappingSwitching;
        private bool _clearSpecItemString;

        private bool _openDoorSwitcher;
        private readonly float _openDoorDistance = 5f;

        private bool _crosshairSwitcher;
        private float _playersDrawDistance = 400f;
        private readonly float _itemsDrawDistanceSpecial = 2000f;
        private float _itemsDrawDistance = 50f;
        private float _containersDrawDistance = 40f;

        private float camUpdateTime = 15f;
        private float lastCameUpdateTime;
        private int sizeOfFont;
        private int sizeOfFont2;
        private Vector3 camPos;

        #endregion

        private void Start()
        {
            Clear();
        }

        private void Clear()
        {
            plys = null;
            exs = null;
            cons = null;
            li = null;
            _containersUpdateTime = 0;
            _itemsUpdateTime = 0;
            _exitsUpdateTime = 0;
            _itemsSpecUpdateTime = 0;
            //GC.Collect();
        }

        public void Load()
        {
            holderForGO = new GameObject();
            holderForGO.AddComponent<MumbleOverlay>();
            DontDestroyOnLoad(holderForGO);
        }

        public void Unload()
        {
            Destroy(holderForGO);
            Resources.UnloadUnusedAssets();
            Destroy(this);
        }

        private void Update()
        {

            //open ESP Menu
            if (Input.GetKeyDown(KeyCode.F11))
            {
                _overlaySwitcher = !_overlaySwitcher;
                _startTime = Time.time;
            }

            if (Time.time + camUpdateTime > lastCameUpdateTime && Camera.main != null)
            {
                try
                {
                    camPos = Camera.main.transform.position;
                    lastCameUpdateTime = Time.time;
                }
                catch (NullReferenceException ex)
                {
                    File.AppendAllText(@"E:\exeptionseft\campos.txt", ex.ToString() + Environment.NewLine);
                    lastCameUpdateTime = Time.time;
                }
            }


            // Unload ESP
            if (Input.GetKeyDown(KeyCode.End))
            {
                Unload();
            }          //clear and disable all before exit
            //dissable all
            if (Input.GetKeyDown(KeyCode.Pause))
            {
                _itemsMappingSwitcher = false;
                _itemsSpecMappingSwitcher = false;
                _playersMappingSwitrcher = false;
                _AIoverlaySwitcher = false;
                _deadBodiesSwitcher = false;
                _containersMappingSwitching = false;
                _exsitsMappingSwitcher = false;
                _crosshairSwitcher = false;
            }
            //Show Players, AI &RIP
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _playersMappingSwitrcher = !_playersMappingSwitrcher;
                _AIoverlaySwitcher = !_AIoverlaySwitcher;

                //_deadBodiesSwitcher = !_deadBodiesSwitcher;
            }
            //Containers ESP Hotkey arrow down
            if (Input.GetKeyDown(KeyCode.DownArrow))
            { _containersMappingSwitching = !_containersMappingSwitching; }
            // Item ESP Hotkey <-
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            { _itemsMappingSwitcher = !_itemsMappingSwitcher; }
            // SpecItem ESP Hotkey <-
            if (Input.GetKeyDown(KeyCode.Insert))
            { _itemsSpecMappingSwitcher = !_itemsSpecMappingSwitcher; }
            // Containers ESP cleat list
            if (_containersMappingSwitching == false)
            {
                ContainersToShow = ContainersToShow2;
                ContainersToShow.Clear();
            }
            // Item ESP cleat list
            if (_itemsMappingSwitcher == false)
            {
                ItemsToShow = ItemsToShow2;
                ItemsToShow.Clear();
            }
            // Item ESP cleat list
            if (_itemsSpecMappingSwitcher == false)
            {
                ItemsToShowSpecial = ItemsToShowSpecial2;
                ItemsToShowSpecial.Clear();
            }
            //Open Doors
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                OpenDoors();
            }
        }

        private void OnGUI()
        {

            // GUI SWITCHER

            if (_overlaySwitcher) { DIM(); }

            //Players ESP Switcher
            if (_playersMappingSwitrcher && Time.time >= _playerUpdateTime)
            {
                if (plys != FindObjectsOfType<Player>())
               {
                    plys = FindObjectsOfType<Player>();
               }
                _playerUpdateTime = Time.time + _playersUDPtime;
            }
            if (_playersMappingSwitrcher)
            { PlayersMapping(); }

            // SPEC Items ESP SWITCHER
            if (_itemsSpecMappingSwitcher && Time.time >= _itemsSpecUpdateTime)
            {
                if (li != FindObjectsOfType<LootItem>())
                {
                    ItemsToShowSpecial.Clear();
                    li = FindObjectsOfType<LootItem>();
                }
                _itemsSpecUpdateTime = Time.time + _itemSpecUPDtime;
            }
            if (_itemsSpecMappingSwitcher)
            { SearchSpecialItem(); }

            // Clear Search string for special items
            if (_clearSpecItemString)
            {
                ClearSpecItemSearchString();
            }

            // Items ESP SWITCHER
            if (_itemsMappingSwitcher && Time.time >= _itemsUpdateTime)
            {
                if (li != FindObjectsOfType<LootItem>())
                {
                    ItemsToShow.Clear();
                    li = FindObjectsOfType<LootItem>();
                }
                _itemsUpdateTime = Time.time + _itemUPDtime;
            }
            if (_itemsMappingSwitcher)
            { ItemsMapping(); }

            //Containers ESP SWITCHER
            if (_containersMappingSwitching && Time.time >= _containersUpdateTime)
            {
               if (cons != FindObjectsOfType<LootableContainer>())
               {
                    ContainersToShow.Clear();
                    cons = FindObjectsOfType<LootableContainer>();
               }
                _containersUpdateTime = Time.time + _containersUPDTime;
            }
            if (_containersMappingSwitching)
            { ContainersMapping(); }

            // Exits ESP SWITCHER
            if (_exsitsMappingSwitcher && Time.time >= _exitsUpdateTime)
            {
                if (exs != FindObjectsOfType<ExfiltrationPoint>())
                {
                    exs = FindObjectsOfType<ExfiltrationPoint>();
                }
                _exitsUpdateTime = Time.time + _exitUPFtime;
            }


            if (_exsitsMappingSwitcher)
            { ExitsMapping(); }

            if (_crosshairSwitcher)
            {
                MumbleOverlayGui.DrawLine(new Vector2((float)Screen.width / 2f - 1f, (float)Screen.height / 2f), new Vector2((float)Screen.width / 2f + 1f, (float)Screen.height / 2f), Color.yellow, 2f); //its an pixel 2x2 center of screen its almost how to draw this box it will be filled with color
            }
        }      

        private void PlayersMapping()

        {
            var enumerator = plys.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var p = enumerator.Current;
                if (p.ProfileId != null)
                {
                    try
                    {
                        Vector3 position = p.Transform.position;
                        Vector3 vector = new Vector3(Camera.main.WorldToScreenPoint(position).x, Camera.main.WorldToScreenPoint(position).y, Camera.main.WorldToScreenPoint(position).z);
                      

                        float distance = gDIS(camPos, position);
                        if (distance < 1f) continue; // dont draw me ;)
                        if (distance <= _playersDrawDistance && vector.z > 0.01)
                        {

                            if (distance <= 30)
                            {
                                sizeOfFont = 14; sizeOfFont2 = 10;
                            }
                            else if (distance < 100 && distance > 30)
                            { sizeOfFont = 12; sizeOfFont2 = 8; }
                            else if (distance > 100 && distance <= 300)
                            { sizeOfFont = 10; sizeOfFont2 = 8; }
                            else if (distance > 300 && distance <= 600)
                            { sizeOfFont = 8; sizeOfFont2 = 6; }
                            else
                            { sizeOfFont = 6; sizeOfFont2 = 4; }

                            var NameStyle = new GUIStyle
                            { fontSize = sizeOfFont };
                            var WeapStyle = new GUIStyle
                            { fontSize = sizeOfFont2 };

                            WeapStyle.normal.textColor = Color.white;

                            if (p.Profile.Info.RegistrationDate <= 0 && _AIoverlaySwitcher && p.HealthController.IsAlive)
                            {
                                Vector3 headPositonVector = p.PlayerBones.Head.position;
                                Vector3 vector2 = new Vector3(Camera.main.WorldToScreenPoint(headPositonVector).x, Camera.main.WorldToScreenPoint(headPositonVector).y, Camera.main.WorldToScreenPoint(headPositonVector).z);

                                Color headColor = Color.cyan;
                                Color headColor1 = Color.yellow;
                                Vector3 look = p.LookDirection;
                                float headPosVector = Camera.main.WorldToScreenPoint(headPositonVector).y + 10f;
                                float headVector = Math.Abs(Camera.main.WorldToScreenPoint(headPositonVector).y - Camera.main.WorldToScreenPoint(position).y) + 10f;
                                float headNewPos = headVector * 0.65f;
                                string weapon;
                                try
                                {
                                    weapon = p.Weapon.Template.ShortName.Contains("Item") ? p.Weapon.Template.Name : p.Weapon.Template.ShortName;
                                }
                                catch
                                {
                                    weapon = "NA.";
                                }
                                string healthstatus = p.HealthStatus.ToString();
                                string playerWeaponText = CurrentWeaponIDtoName($"{weapon}");
                                float playerHealth = p.HealthController.imethod_0(EBodyPart.Common).Current / 435f * 100f;
                                NameStyle.normal.textColor = Color.cyan;
                                string text = string.Format(" [{0}-[{1}] {2}", "BOT", (int)distance, (int)playerHealth);
                                //MumbleOverlayGui.DrawBox(x - num4RAZ / 2f, Screen.height - headPosVector, headPosVector, headVector, playerSideColor);

                                MumbleOverlayGui.DrawLine(new Vector2(vector2.x - 4f, Screen.height - vector2.y), new Vector2(vector2.x + 2f, Screen.height - vector2.y), headColor);
                                MumbleOverlayGui.DrawLine(new Vector2(vector2.x, Screen.height - vector2.y - 2f), new Vector2(vector2.x, Screen.height - vector2.y + 2f), headColor1);


                                GUI.skin.GetStyle(text).CalcSize(new GUIContent(text));

                                Vector2 vector3 = GUI.skin.GetStyle(text).CalcSize(new GUIContent(text));

                                GUI.Label(new Rect(vector.x - vector3.x / 2f, Screen.height - headPosVector - 20f, 300f, 50f), text, NameStyle);
                                GUI.Label(new Rect(vector.x - vector3.x / 2f, Screen.height - headPosVector, 20f, 50f), playerWeaponText, WeapStyle);
                            }
                            else if (p.HealthController.IsAlive && p.Profile.Info.RegistrationDate > 0)
                            {
                                Vector3 headPositonVector = p.PlayerBones.Head.position;
                                Vector3 vector2 = new Vector3(Camera.main.WorldToScreenPoint(headPositonVector).x, Camera.main.WorldToScreenPoint(headPositonVector).y, Camera.main.WorldToScreenPoint(headPositonVector).z);

                                Color headColor = Color.red;
                                float headPosVector = Camera.main.WorldToScreenPoint(headPositonVector).y + 10f;
                                float headVector = Math.Abs(Camera.main.WorldToScreenPoint(headPositonVector).y - Camera.main.WorldToScreenPoint(position).y) + 10f;

                                string weapon;
                                try
                                {
                                    weapon = p.Weapon.Template.ShortName.Contains("Item") ? p.Weapon.Template.Name : p.Weapon.Template.ShortName;
                                }
                                catch
                                {
                                    weapon = "NA.";
                                }
                                float playerHealth = p.HealthController.imethod_0(EBodyPart.Common).Current / 435f * 100f;

                                string playerWeaponText = CurrentWeaponIDtoName($"{weapon}");
                                NameStyle.normal.textColor = Color.red;
                                string text = string.Format("{0} -[{1}] {2}", p.Profile.Info.Nickname, (int)distance, (int)playerHealth);
                                //MumbleOverlayGui.DrawBox(xRAZ - headNewPos / 2f, Screen.height - headPosVector, headVector, headNewPos, playerColorRAZ);

                                MumbleOverlayGui.DrawLine(new Vector2(vector2.x - 4f, Screen.height - vector2.y), new Vector2(vector2.x + 4f, Screen.height - vector2.y), headColor);
                                MumbleOverlayGui.DrawLine(new Vector2(vector2.x, Screen.height - vector2.y - 2f), new Vector2(vector2.x, Screen.height - vector2.y + 2f), headColor);

                                GUI.skin.GetStyle(text).CalcSize(new GUIContent(text));
                                Vector2 vector3 = GUI.skin.GetStyle(text).CalcSize(new GUIContent(text));

                                GUI.Label(new Rect(vector.x - vector3.x / 2f, Screen.height - headPosVector - 20f, 300f, 50f), text, NameStyle);
                                GUI.Label(new Rect(vector.x - vector3.x / 2f, Screen.height - headPosVector, 20f, 50f), playerWeaponText, WeapStyle);
                            }
                            else if (!p.HealthController.IsAlive && _deadBodiesSwitcher)
                            {
                                Vector3 headPositonVector = p.PlayerBones.Head.position;
                                Vector3 vector2 = new Vector3(Camera.main.WorldToScreenPoint(headPositonVector).x, Camera.main.WorldToScreenPoint(headPositonVector).y, Camera.main.WorldToScreenPoint(headPositonVector).z);

                                Color headColor = Color.yellow;
                                NameStyle.normal.textColor = Color.gray;

                                float headPosVector = Camera.main.WorldToScreenPoint(headPositonVector).y + 10f;
                                float headVector = Math.Abs(Camera.main.WorldToScreenPoint(headPositonVector).y - Camera.main.WorldToScreenPoint(position).y) + 10f;

                                float headNewPos = headVector * 0.65f;

                                string weapon;
                                try
                                {
                                    weapon = p.Weapon.Template.ShortName.Contains("Item") ? p.Weapon.Template.Name : p.Weapon.Template.ShortName;
                                }
                                catch
                                {
                                    weapon = "NA.";
                                }

                                string playerWeaponText = CurrentWeaponIDtoName($"{weapon}");
                                NameStyle.normal.textColor = Color.gray;

                                string text = string.Format("{0} [{1}]", "RIP", (int)distance);
                                GUI.skin.GetStyle(text).CalcSize(new GUIContent(text));
                                Vector2 vector3 = GUI.skin.GetStyle(text).CalcSize(new GUIContent(text));

                                GUI.Label(new Rect(vector.x - vector3.x / 2f, Screen.height - headPosVector, 20f, 50f), playerWeaponText, WeapStyle);
                            }

                        }
                    }

                    catch (NullReferenceException ex)
                    {
                        File.AppendAllText(@"E:\exeptionseft\plyslist.txt", ex.ToString() + Environment.NewLine);
                    }
                }
            }
           
        }

        private void SearchSpecialItem()
        {
            if (ItemsToShowSpecial.Count == 0)
            {
                var enumerator = li.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var l = enumerator.Current;
                    if (l != null)
                    {
                        try
                        {
                            if (!_norItems)
                            {
                                int _DoCode = 0;
                                for (int y = 0; y < searchSpecialItem.Length; y++)
                                {
                                    if ((l.name.Length - l.name.Replace(searchSpecialItem, string.Empty).Length) / searchSpecialItem.Length > 0) { _DoCode = 1; }
                                }
                                if (_DoCode == 1)
                                { ItemsToShowSpecial.Add(l); }
                            }
                            else { ItemsToShowSpecial.Add(l); }
                        }
                        catch (NullReferenceException ex)
                        {
                            File.AppendAllText(@"E:\exeptionseft\itemsNotSortSpec.txt", ex.ToString() + Environment.NewLine);
                        }
                    }
                }
            }
            else
            {
                var enumerator = ItemsToShowSpecial.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var l = enumerator.Current;

                    if (l != null)
                    {

                        try
                        {
                            //if (l == null) continue;
                            Vector3 position = l.transform.position;
                            Vector3 vector = new Vector3(Camera.main.WorldToScreenPoint(position).x, Camera.main.WorldToScreenPoint(position).y, Camera.main.WorldToScreenPoint(position).z);

                            var num = gDIS(camPos, position);//new distance method

                            if (num <= _itemsDrawDistanceSpecial && vector.z > 0.01)
                            {
                                sizeOfFont = 14;

                                var NameStyle = new GUIStyle
                                { fontSize = sizeOfFont };


                                string name = l.name;
                                name = name.Replace("item_ammo_box", "").
                                            Replace("_", " ").
                                            Replace("item", "").
                                            Replace("(Clone)", "").
                                            Replace("barter", "[b]").
                                            Replace("weapon", "[w]").
                                            Replace("quest", "[q]").
                                            Replace("valuable", "[v]").
                                            Replace("condensed", "cond").
                                            Replace("loot", "").
                                            Replace("equipment", "").
                                            Replace("household", "").
                                            Replace("handguard", "").
                                            Replace("barrel", "").
                                            Replace("building", "").
                                            Replace("electr", "").
                                            Replace("flam", "").
                                            Replace("medical", "").
                                            Replace("medical", "").
                                            Replace("tools_", "").
                                            Replace("cigarettes", "").
                                            Replace("food", "");

                                if (name != null)
                                {
                                    NameStyle.normal.textColor = Color.yellow;
                                    string text = name + " > " + (int)num;
                                    GUI.Label(new Rect(vector.x - 50f, Screen.height - vector.y, 100f, 50f), text, NameStyle);
                                }

                            }
                        }
                        catch (NullReferenceException ex)
                        {
                            File.AppendAllText(@"E:\exeptionseft\itemsSortSpec.txt", ex.ToString() + Environment.NewLine);
                        }
                    }
                }
            }
        }

        private void ItemsMapping()
        {
            if (ItemsToShow.Count == 0)
            {
                var enumerator = li.GetEnumerator();                
                while (enumerator.MoveNext())
                {
                    var l = enumerator.Current;
                    if (l != null)
                    {
                        try
                        {
                            if (!_norItems)
                            {
                                int _DoCode = 0;

                                for (int y = 0; y < SearchArrayItems.Length; y++)
                                {
                                    if ((l.name.Length - l.name.Replace(SearchArrayItems[y], string.Empty).Length) / SearchArrayItems[y].Length > 0) { _DoCode = 1; }
                                }
                                if (_DoCode == 1)
                                { ItemsToShow.Add(l); }


                            }
                            else { ItemsToShow.Add(l); }

                        }
                        catch (NullReferenceException ex)
                        {
                            File.AppendAllText(@"E:\exeptionseft\itemsNotSort.txt", ex.ToString() + Environment.NewLine);
                        }
                    }
                    
                }
            }
            else 
            {
                var enumerator = ItemsToShow.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    var l = enumerator.Current;
                    if (l.ItemId != null)
                    {
                        try
                        {
                            if (l == null) continue;
                            Vector3 position = l.transform.position;
                            Vector3 vector = new Vector3(Camera.main.WorldToScreenPoint(position).x, Camera.main.WorldToScreenPoint(position).y, Camera.main.WorldToScreenPoint(position).z);

                            var num = gDIS(camPos, position);//new distance method

                            if (num <= _itemsDrawDistance && vector.z > 0.01)
                            {
                                if (num < 20)
                                { sizeOfFont = 14; }
                                else if (num < 100 && num > 20)
                                { sizeOfFont = 12; }
                                else if (num > 100 && num <= 250)
                                { sizeOfFont = 10; }
                                else if (num > 250 && num <= 600)
                                { sizeOfFont = 8; }
                                else
                                { sizeOfFont = 6; }

                                var NameStyle = new GUIStyle
                                { fontSize = sizeOfFont };
                                string name = l.name;
                                name = name.Replace("box", "").
                                            Replace("_", " ").
                                            Replace("item", "").
                                            Replace("(Clone)", "").
                                            Replace("barter", "[b]").
                                            Replace("weapon", "[w]").
                                            Replace("quest", "[q]").
                                            Replace("valuable", "[v]").
                                            Replace("condensed", "cond").
                                            Replace("loot", "").
                                            Replace("equipment", "").
                                            Replace("household", "").
                                            Replace("handguard", "").
                                            Replace("barrel", "").
                                            Replace("building", "").
                                            Replace("electr", "").
                                            Replace("flam", "").
                                            Replace("medical", "").
                                            Replace("medical", "").
                                            Replace("tools_", "").
                                            Replace("cigarettes", "").
                                            Replace("food", "");

                                NameStyle.normal.textColor = Color.magenta;
                                string text = name + " > " + (int)num;
                                GUI.skin.GetStyle(text).CalcSize(new GUIContent(text));
                                Vector2 vector3 = GUI.skin.GetStyle(text).CalcSize(new GUIContent(text));
                                GUI.Label(new Rect(vector.x - vector3.x / 2f, Screen.height - vector.y, 100f, 50f), text, NameStyle);
                            }
                        }

                        catch (NullReferenceException ex)
                        {
                            File.AppendAllText(@"E:\exeptionseft\itemsSort.txt", ex.ToString() + Environment.NewLine);
                        }
                    }
                }
            }
        }

        private void ContainersMapping()
        {
            if (ContainersToShow.Count == 0)
            {
                var enumerator = cons.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var c = enumerator.Current;
                    if (c != null)
                    {
                        try
                        {
                            if (!_norContainers)
                            {
                                int _DoCode = 0;
                                for (int y = 0; y < SearchArrayContainers.Length; y++)
                                {
                                    if ((c.name.Length - c.name.Replace(SearchArrayContainers[y], string.Empty).Length) / SearchArrayContainers[y].Length > 0) { _DoCode = 1; }
                                }
                                if (_DoCode == 1)
                                { ContainersToShow.Add(c); }
                            }
                            else { ContainersToShow.Add(c); }
                        }
                        catch (NullReferenceException ex)
                        {                            
                            File.AppendAllText(@"E:\exeptionseft\containersNotSort.txt", ex.ToString() + Environment.NewLine);
                        }
                    }
                }
            }
            else
            {
                var enumerator = ContainersToShow.GetEnumerator();
                while (enumerator.MoveNext())
                {

                    var c = enumerator.Current;

                    if (c != null)
                    {

                        try
                        {
                            Vector3 position = c.transform.position;
                            Vector3 vector = new Vector3(Camera.main.WorldToScreenPoint(position).x, Camera.main.WorldToScreenPoint(position).y, Camera.main.WorldToScreenPoint(position).z);

                            var num = gDIS(camPos, position);//new distance method

                            if (num <= _containersDrawDistance && vector.z > 0.01)
                            {
                                if (num < 20)
                                { sizeOfFont = 14; }
                                else if (num < 100 && num > 20)
                                { sizeOfFont = 12; }
                                else if (num > 100 && num <= 250)
                                { sizeOfFont = 10; }
                                else if (num > 250 && num <= 600)
                                { sizeOfFont = 8; }
                                else
                                { sizeOfFont = 6; }

                                var NameStyle = new GUIStyle
                                { fontSize = sizeOfFont };

                                string name = c.name;
                                name = name.Replace("weapon", "[W]").
                                            Replace("Medical", "[MD]").
                                            Replace("Toolbox", "[TD]").
                                            Replace("safe", "[S]").
                                            Replace("container", "[CON]");
                                NameStyle.normal.textColor = Color.blue;
                                string text = name + " > " + (int)num;

                                GUI.skin.GetStyle(text).CalcSize(new GUIContent(text));
                                Vector2 vector3 = GUI.skin.GetStyle(text).CalcSize(new GUIContent(text));

                                GUI.Label(new Rect(vector.x - vector3.x / 2f, Screen.height - vector.y, 100f, 50f), text, NameStyle);

                            }
                        }
                        catch (NullReferenceException ex)
                        {
                            File.AppendAllText(@"E:\exeptionseft\containersSort.txt", ex.ToString() + Environment.NewLine);
                        }
                    }
                }
                }
            }        

        private void ExitsMapping()
        {            
            try
            {
                var enumerator = exs.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var e = enumerator.Current;
                        if (e.isActiveAndEnabled)
                        { 
                             Vector3 position = e.transform.position;//faster if inside
                            float dto = gDIS( camPos, position);//new distance method
                            var exfilContainerBoundingVector = new Vector3(
                            Camera.main.WorldToScreenPoint(position).x,
                            Camera.main.WorldToScreenPoint(position).y,
                            Camera.main.WorldToScreenPoint(position).z);
                            if (exfilContainerBoundingVector.z > 0.01)
                            {
                                GUI.color = Color.green;
                                string exfilName = e.name;
                                exfilName = exfilName.Replace("exit", "").
                                       Replace("scav", "S").
                                       Replace("_", "");
                                string boxText = $"{exfilName} - \n{(int)dto}";
                                GUI.Label(new Rect(exfilContainerBoundingVector.x - 50f, Screen.height - exfilContainerBoundingVector.y, 100f, 50f), boxText);
                            }
                        }
                }
            }
            catch (NullReferenceException ex)
            {
                File.AppendAllText(@"E:\exeptionseft\exslist.txt", ex.ToString() + Environment.NewLine);
            }
        }

        #region junk
        public class MjChLGZ
        {
            public void UNRqXSUOuN()
            {
                sbyte kwVY = 71;
                double IWY = 4.868311E-12;
                if (IWY == -2.853652)
                {
                    IWY = Math.Pow(double.MinValue, double.MaxValue);
                }
                float DcjGHMLQQWSh = -2.95692E-18F;
                long mHXsOBad = 34101111309144708;

            }
            public void KwFacW()
            {
                uint QJuKJD = 6699;
                ushort hcEhccgBT = 23644;
                float YzUTKSgCkK = -4.116108E+36F;
                double GuwlYhLppVMD = -251.5446;
                GuwlYhLppVMD = Math.Pow(5, 2);
                Console.Write(GuwlYhLppVMD.ToString());
            }
        }
        public class KcXnJSxcte
        {
            public void hAnWgiKoMZP()
            {
                short AlixPIdX = 20441;
                double wICoaReZet = 4.137437E+34;
                if (wICoaReZet == 2.403572E+07)
                {
                    wICoaReZet = Math.Ceiling(Math.Asin(0.2));
                }
                short Jwce = -12358;
                short pRToUEXcc = 2717;

            }
            public void DRUx()
            {
                byte nNp = 154;
                uint hRzJJxPiNn = 9;
                ulong QztkeMEs = 74929377510506874;
                double SiLzwVoHmtunM = 4.282185E+13;
                SiLzwVoHmtunM = Math.Ceiling(Math.Sin(2));
                Console.ReadLine();
            }
        }
        #endregion

        private void OpenDoors()
        {
            doors = FindObjectsOfType<Door>();      
            try
            {
                var enumerator = doors.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var door = enumerator.Current;
                    if (door == null) continue;
                    if (door != null)
                    {
                        Vector3 doorPosition = door.transform.position;
                        float num = Vector3.Distance( camPos, doorPosition);
                        Vector3 vector = new Vector3(Camera.main.WorldToScreenPoint(doorPosition).x, Camera.main.WorldToScreenPoint(doorPosition).y, Camera.main.WorldToScreenPoint(doorPosition).z);


                        if (num <= _openDoorDistance && vector.z > 0.01)
                        {
 
                            door.enabled = true;
                            door.DoorState = EDoorState.Shut;
                        }
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                File.AppendAllText(@"E:\exeptionseft\doorslist.txt", ex.ToString() + Environment.NewLine);
            }
        }

        private void ClearSpecItemSearchString()
        {
            SpecItemNameToSearchStr = "";
            searchSpecialItem = "";
            ItemsToShowSpecial.Clear();
            ItemsToShowSpecial2.Clear();
            _clearSpecItemString = false;
        }

        #region junk
        public class RXKXceci
        {
            public void CeVzqPpmMwT()
            {
                byte UbG = 4;
                uint MhGWyF = 9701;
                string gXkMdZmZuCsaP = "nnpMcIsbFN";
                ushort WhzTfQnoF = 30370;

            }
            public void ybUPGHYXMt()
            {
                double joAVtHfsyo = -6.040269E-34;
                if (joAVtHfsyo == 8.400989)
                {
                    double dPGSLiSMuHQLmYzoF = Math.IEEERemainder(3, 4);
                    joAVtHfsyo = dPGSLiSMuHQLmYzoF;
                    try
                    {
                        int zfjhEQLdfDwaTUUExONBU = 4012718;
                        if (zfjhEQLdfDwaTUUExONBU == 71456)
                        {
                            zfjhEQLdfDwaTUUExONBU++;
                        }
                        else
                        {
                            zfjhEQLdfDwaTUUExONBU--;
                            Console.Write(zfjhEQLdfDwaTUUExONBU.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                ushort FLh = 56924;
                sbyte OOuDFtL = -108;
                float NOjb = -2.696772E-24F;

            }
        }
        public class MHws
        {
            public void xHmTS()
            {
                string CPV = "xdRpSfPI";
                string gqHCqPuDwmMb = "sNpLCKco";
                ushort nNYzUpGSOowo = 20142;
                int BlHZtJ = 40171871;
                if (BlHZtJ == 557743)
                {
                    BlHZtJ = 543922;
                }
            }
            public void mXbCOAt()
            {
                ulong pBmPwYN = 87546726707994613;
                short iBswZqOkFQmTB = 20999;
                byte gVgMEnPIx = 110;
                sbyte amBGkOpSxXqi = -4;

            }
        }
        #endregion

        private string CurrentWeaponIDtoName(string id)
        {
            //true magic xD
            for (int y = 0; y < PissArayo_1.Length; y++)
            {
                if ((id.Length - id.Replace(PissArayo_1[y], String.Empty).Length) / PissArayo_1[y].Length > 0)
                {
                    return PissArayo_2[y];
                }
            }
            return id;
        
            /*if (id.Contains("p226"))
                return "p226";
            else if (id.Contains("mp443"))
                return "MP443";
            else if (id.Contains("hk"))
                return "HK";
            else if (id.Contains("aks74"))
                return "AKS-74";
            else if (id.Contains("_mr133_"))
                return "MR-133";
            else if (id.Contains("_mr153_"))
                return "MR-153";
            else if (id.Contains("saiga12"))
                return "Saig.12";
            else if (id.Contains("akm"))
                return "AKM";
            else if (id.Contains("ak74n"))
                return "AK-7N";
            else if (id.Contains("ak74m"))
                return "AK-7M";
            else if (id.Contains("mr43e"))
                return "MR-43E";
            else if (id.Contains("model_870"))
                return "M870";
            else if (id.Contains("mosin"))
                return "Mosin";
            else if (id.Contains("_zarya_"))
                return "Zarya";
            else if (id.Contains("_pb_"))
                return "PB9x18PM";
            else if (id.Contains("_izhmeh_pm_"))
                return "Makarov";
            else if (id.Contains("saiga_9"))
                return "SaigaPM";
            else if (id.Contains("_tt_"))
                return "TT";
            else if (id.Contains("pp-9"))
                return "pp91";
            else if (id.Contains("vepr"))
                return "VEPR";
            else if (id.Contains("glock"))
                return "Glock";
            else if (id.Contains("sks"))
                return "SKS";
            else if (id.Contains("akms"))
                return "AKMS";
            else if (id.Contains("akm_vpo"))
                return "AKM VPO";
            else if (id.Contains("vepr_km"))
                return "VEPR KM VPO";
            else if (id.Contains("mp5"))
                return "MP5";
            else if (id.Contains("mp7a1"))
                return "MP7";
            else if (id.Contains("_mpx_"))
                return "MPX";
            else if (id.Contains("molot_aps"))
                return "Molot";
            else if (id.Contains("m1a"))
                return "m1a";
            else if (id.Contains("sa58"))
                return "SA58";
            else if (id.Contains("ak101"))
                return "AK101";
            else if (id.Contains("ak102"))
                return "AK102";
            else if (id.Contains("ak103"))
                return "AK103";
            else if (id.Contains("ak104"))
                return "AK104";
            else if (id.Contains("ak105"))
                return "AK105";
            else if (id.Contains("m4a1"))
                return "M4A1";
            else if (id.Contains("sv-98"))
                return "SV-98";
            else if (id.Contains("_val_"))
                return "ASVAL";
            else if (id.Contains("_vss_"))
                return "VSS";
            else if (id.Contains("rsass"))
                return "RSASS";
            else if (id.Contains("dvl-10"))
                return "DVL10";
            else if (id.Contains("toz"))
                return "TOZ";
            else
                return id;*/

        }

        #region junk
        public class QiAmCGyRU
        {
            public void oEwULKZD()
            {
                byte ksoIINBugiEXm = 254;
                uint hjUAujsYqI = 4167;
                sbyte YPYEtzTSFPf = -114;
                long XsqxydCQIL = 29223023375599341;

            }
            public void wkPZJ()
            {
                int OpFfuqBHaYpWY = 19;
                while (OpFfuqBHaYpWY == 19)
                {
                    OpFfuqBHaYpWY += 41610;
                }
                ushort CCEwbQoB = 10720;
                short QyolkDqjBt = -12720;
                short yfikcJMHmB = -17143;

            }
        }
        public class bKsCkfSGIgB
        {
            public void XWEWSTfljTX()
            {
                double dyBHaDXcy = -3.093073E+27;
                double pFumge = Math.IEEERemainder(3, 4);
                dyBHaDXcy = pFumge;
                try
                {
                }
                catch (Exception ex)
                {
                }
                int NwNa = 75440680;
                while (NwNa == 75440680)
                {
                    NwNa = 598541;
                }
                ulong lgsfgJpp = 76529777316525014;
                int xwnKLwU = 5489;
                while (xwnKLwU == 5489)
                {
                    xwnKLwU = xwnKLwU + 957703;
                }
            }
            public void tZRNJaweS()
            {
                short FZqY = -31946;
                sbyte OyiwnDwXkQ = -97;
                byte UkPbbS = 225;
                string aqEZCgwDq = "XpnByXMn";

            }
        }
        #endregion

        private Color GetPlayerColor(EPlayerSide side)
        {
            switch (side)
            {
                case EPlayerSide.Bear:
                    return Color.red;
                case EPlayerSide.Usec:
                    return Color.blue;
                case EPlayerSide.Savage:
                    return Color.white;
                default:
                    return Color.white;
            }
        }

        private void DIM()
        {            
            float t = (Time.time - _startTime) * 0.7f;
            GUI.color = Color.black; //Bground
            GUI.Box(new Rect(100f, 100f, 300f, 420f), "");
            GUI.color = Color.white;

            _playersMappingSwitrcher =      GUI.Toggle(new Rect(110f, 120f, 160f, 20f), _playersMappingSwitrcher, " ---- PLAYERS ESP");
            _AIoverlaySwitcher =            GUI.Toggle(new Rect(110f, 140f, 160f, 20f), _AIoverlaySwitcher, " ---- BOTS ESP");
            _deadBodiesSwitcher =           GUI.Toggle(new Rect(110f, 160f, 160f, 20f), _deadBodiesSwitcher, " ---- RIP ESP");
            if (_playersMappingSwitrcher)
            {
                                            GUI.Label(new Rect(130f, 180, 150f, 20f), "players distance");
                _playersDrawDistance =      GUI.HorizontalSlider(new Rect(155f, 205f, 200f, 20f), _playersDrawDistance, 200.0F, 1500.0F);

                string showDist = Convert.ToString(_playersDrawDistance);
                                            GUI.Label(new Rect(110f, 205, 40f, 20f), showDist);
            }
            _itemsMappingSwitcher =         GUI.Toggle(new Rect(110f, 220f, 160f, 20f), _itemsMappingSwitcher, " ---- ITEMS");
            if (_itemsMappingSwitcher)
            {
                                            GUI.Label(new Rect(150f, 240f, 150f, 20f), "items distance");
                _itemsDrawDistance =        GUI.HorizontalSlider(new Rect(155f, 265f, 200f, 20f), _itemsDrawDistance, 50.0F, 400.0F);

                string showDist = Convert.ToString(_itemsDrawDistance);

                                            GUI.Label(new Rect(110f, 265f, 40f, 20f), showDist);
                
            }

            _containersMappingSwitching =   GUI.Toggle(new Rect(110f, 280f, 160f, 20f), _containersMappingSwitching, " ---- CONTAINERS");
            if (_containersMappingSwitching)
            {
                                            GUI.Label(new Rect(130f, 300f, 150f, 20f), "container distance");
                _containersDrawDistance =   GUI.HorizontalSlider(new Rect(155f, 325f, 200f, 20f), _containersDrawDistance, 10.0F, 100.0F);

                string showDist = Convert.ToString(_containersDrawDistance);

                                            GUI.Label(new Rect(110f, 325f, 40f, 20f), showDist);
            }   

            _exsitsMappingSwitcher =        GUI.Toggle(new Rect(110f, 360f, 160f, 20f), _exsitsMappingSwitcher, " ---- EXITS");

            _crosshairSwitcher =            GUI.Toggle(new Rect(110f, 380f, 160f, 20f), _crosshairSwitcher, " ---- CROSS");

            _itemsSpecMappingSwitcher =     GUI.Toggle(new Rect(110f, 400f, 160f, 20f), _itemsSpecMappingSwitcher, " ---- ITEMS FINDER");
            if (_itemsSpecMappingSwitcher)
            {
                SpecItemNameToSearchStr =   GUI.TextField(new Rect(150f, 420f, 200f, 20f), SpecItemNameToSearchStr, 80);

                if (GUI.Button(new Rect(150f, 450f, 200f, 20f), "Search"))
                {
                    searchSpecialItem = SpecItemNameToSearchStr;
                }

                if (GUI.Button(new Rect(150f, 480f, 200f, 20f), "Clear String"))
                {
                    _clearSpecItemString = true;
                }
            }

        }

        /*private double gDIS(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2.0) + Math.Pow(y2 - y1, 2.0));
        }*/

        private float gDIS(Vector3 c1, Vector3 c2) { float cx, cy, cz, n; cx = c2.x - c1.x; cy = c2.y - c1.y; cz = c2.z - c1.z; n = (cx * cx + cy * cy + cz * cz); return FSqrt(n); }
        [StructLayout(LayoutKind.Explicit)]
        private struct FloatIntUnion { [FieldOffset(0)] public float f; [FieldOffset(0)] public int tmp; }
        public static float FSqrt(float z)
        { if (z == 0) { return 0; } FloatIntUnion c; c.tmp = 0; c.f = z; c.tmp -= 1 << 23; c.tmp >>= 1; c.tmp += 1 << 29; return c.f; }

        #region junk
        public class LQFYFW
        {
            public void tkpXi()
            {
                byte jyRSobnCI = 126;
                int cGKqep = 544636;
                while (cGKqep == 544636)
                {
                    cGKqep = cGKqep + 366434;
                }
                double mOk = -4.582601E-10;
                while (mOk == 2.868628E+16)
                {
                    mOk = Math.Ceiling(Math.Tan(1));
                    int? TlHGKmwbzUlOBqwfkGqCH = 2116053;
                    TlHGKmwbzUlOBqwfkGqCH += 48123;
                }
                int NYiKDgcimNok = 79;
                if (NYiKDgcimNok == 209604)
                {
                    NYiKDgcimNok = 30366;
                }
            }
            public void xZts()
            {
                ushort TTFDMJRWo = 56929;
                short ZIzbhOazPtT = -10498;
                long jpg = 23692406682142051;
                int dzoQWsJl = 7134;
                while (dzoQWsJl == 7134)
                {
                    dzoQWsJl = dzoQWsJl + 342498;
                }
            }
        }
        public class RAZmULACPxAMe
        {
            public void YkECmXaAEXZZQ()
            {
                ulong MbPnMmjnk = 81730479964113803;
                int xBRpqu = 334993388;
                if (xBRpqu == 262710)
                {
                    xBRpqu += 893232;
                }
                float XduKytJOWs = 4.535222E-06F;
                ushort ZjA = 23336;

            }
            public void AtdVMyT()
            {
                ulong nlXSWUZxZNikI = 50156085437300588;
                ulong VzwpTiJlE = 68683909268611479;
                string jNikSMAxk = "SUECJctBE";
                sbyte qCGHV = -90;

            }
        }
        public class dDuDQPDQMnDF
        {
            public void MyJMLiY()
            {
                string FZVXoS = "EFXEH";
                short iodPYSpfYY = 17187;
                uint SUVMiRMYuISA = 60053701;
                sbyte GbNtPBxHta = -83;

            }
            public void zbW()
            {
                byte YsKJkuPHKOI = 56;
                short mixsX = -31414;
                sbyte syns = -85;
                long AtLJLMmzZu = 75008581991435414;

            }
        }
        public class WTcJZiGTalw
        {
            public void lhXkuXEFCaKy()
            {
                double dwaFKT = -6.529238E-06;
                dwaFKT = Math.Ceiling(Math.Asin(0.2));
                try
                {
                    Console.WriteLine(dwaFKT.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                string KcXHuIgIGPaC = "MNnN";
                float MiKCODFBV = 4.373469E-26F;
                long JXNPAoTZZ = 63531486519011341;

            }
            public void oDsnJaCjdQg()
            {
                byte xfyTjZXOzYFME = 113;
                short SVHQZTfkuoNnp = -18273;
                string OIQgtSAVhYRF = "DeDlkkUBxXD";
                double zVlKz = -6.375615E+29;
                zVlKz = Math.Ceiling(Math.Atan(-5));
                try
                {
                    int jceajqqKn = 404015;
                    if (jceajqqKn == 56503)
                    {
                        jceajqqKn++;
                    }
                    else
                    {
                        jceajqqKn--;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
      

    }
    #endregion
}
