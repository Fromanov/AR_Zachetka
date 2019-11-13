using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;
using System.IO;

namespace Michsky.UI.CCUI
{
    public class UMACustomizer : MonoBehaviour
    {
        [Header("UMA AVATAR")]
        public DynamicCharacterAvatar avatar;

        [Header("PRESETS")]
        public SharedColorTable HairColor;
        public SharedColorTable SkinColor;
        public SharedColorTable EyesColor;
        public SharedColorTable ClothingColor;
        public SharedColorTable SkinColors;
        public SharedColorTable HairColors;

        [Header("HAIR / HEAD")]
        public List<string> hairModelsMale = new List<string>();
        private int currentHairMale;
        public List<string> hairModelsFemale = new List<string>();
        private int currentHairFemale;

        [Header("EARS / EARRING")]
        public List<string> earsModelsMale = new List<string>();
        private int currentEarsMale;
        public List<string> earsModelsFemale = new List<string>();
        private int currentEarsFemale;

        [Header("EYES")]
        public List<string> eyeModelsMale = new List<string>();
        private int currentEyeMale;
        public List<string> eyeModelsFemale = new List<string>();
        private int currentEyeFemale;

        [Header("EYEBROWS")]
        public List<string> eyebrowsModelsMale = new List<string>();
        private int currentEyebrowsMale;
        public List<string> eyebrowsModelsFemale = new List<string>();
        private int currentEyebrowsFemale;

        [Header("BEARD")]
        public List<string> beardModelsMale = new List<string>();
        private int currentBeardMale;
        public List<string> beardModelsFemale = new List<string>();
        private int currentBeardFemale;

        [Header("TORSO")]
        public List<string> torsoModelsMale = new List<string>();
        private int currentTorsoMale;
        public List<string> torsoModelsFemale = new List<string>();
        private int currentTorsoFemale;

        [Header("BOTTOM")]
        public List<string> bottomModelsMale = new List<string>();
        private int currentBottomMale;
        public List<string> bottomModelsFemale = new List<string>();
        private int currentBottomFemale;

        [Header("HANDS")]
        public List<string> handsModelsMale = new List<string>();
        private int currentHandsMale;
        public List<string> handsModelsFemale = new List<string>();
        private int currentHandsFemale;

        [Header("FEET")]
        public List<string> feetModelsMale = new List<string>();
        private int currentFeetMale;
        public List<string> feetModelsFemale = new List<string>();
        private int currentFeetFemale;

        [Header("SETTINGS")]
        public string myRecipe;
        private string saveFilePath;
        public string saveName = "Cool Character";

        public Dictionary<string, DnaSetter> dna;

		private void Awake()
		{
			
		}

		void OnEnable()
        {
            avatar.CharacterUpdated.AddListener(Updated);
        }

        void OnDisable()
        {
            avatar.CharacterUpdated.RemoveListener(Updated);
        }

        public void SwitchGender(bool male)
        {
            if (male && avatar.activeRace.name != "HumanMaleDCS")
                avatar.ChangeRace("HumanMaleDCS");

            if (!male && avatar.activeRace.name != "HumanFemaleDCS")
                avatar.ChangeRace("HumanFemaleDCS");
        }

        void Updated(UMAData data)
        {
            dna = avatar.GetDNA();
        }

        public void ResetClick()
        {
            avatar.ClearSlots();
            SwitchGender(false);
            SwitchGender(true);
            avatar.BuildCharacter();
        }

        public void RandomizeClick()
        {
            RandomizeAvatar(avatar);
        }

        private void RandomizeAvatar(DynamicCharacterAvatar RandomAvatar)
        {
            Dictionary<string, List<UMATextRecipe>> recipes = RandomAvatar.AvailableRecipes;

            // Set random wardrobe slots.
            foreach (string SlotName in recipes.Keys)
            {
                int cnt = recipes[SlotName].Count;
                if (cnt > 0)
                {
                    //Get a random recipe from the slot, and apply it
                    int min = -1;
                    if (SlotName == "Legs") min = 0; // Don't allow pants removal in random test
                    int rnd = Random.Range(min, cnt);
                    if (rnd == -1)
                    {
                        RandomAvatar.ClearSlot(SlotName);
                    }
                    else
                    {
                        RandomAvatar.SetSlot(recipes[SlotName][rnd]);
                    }
                }
            }

            // Set Random DNA 
            Dictionary<string, DnaSetter> setters = RandomAvatar.GetDNA();
            foreach (KeyValuePair<string, DnaSetter> dna in setters)
            {
                dna.Value.Set(0.35f + (Random.value * 0.3f));
            }

            // Set Random Colors for Skin and Hair
            int RandHair = Random.Range(0, HairColors.colors.Length);
            int RandSkin = Random.Range(0, SkinColors.colors.Length);

            RandomAvatar.SetColor("Hair", HairColors.colors[RandHair]);
            RandomAvatar.SetColor("Skin", SkinColors.colors[RandSkin]);
            RandomAvatar.BuildCharacter(true);
            RandomAvatar.ForceUpdate(true, true, true);
        }

        public void ChangeHair(int hairIndex)
        {
            if (avatar.activeRace.name == "HumanMaleDCS")
            {
                currentHairMale = hairIndex;

                if (hairModelsMale[currentHairMale] == "None")
                    avatar.ClearSlot("Hair");
                else
                    avatar.SetSlot("Hair", hairModelsMale[currentHairMale]);
            }

            if (avatar.activeRace.name == "HumanFemaleDCS")
            {
                currentHairFemale = hairIndex;

                if (hairModelsFemale[currentHairFemale] == "None")
                    avatar.ClearSlot("Hair");
                else
                    avatar.SetSlot("Hair", hairModelsFemale[currentHairFemale]);
            }
            avatar.BuildCharacter();
        }


        public void ChangeEars(int earsIndex)
        {
            if (avatar.activeRace.name == "HumanMaleDCS")
            {
                currentEarsMale = earsIndex;

                if (earsModelsMale[currentEarsMale] == "None")
                    avatar.ClearSlot("Ears");
                else
                    avatar.SetSlot("Ears", earsModelsMale[currentEarsMale]);
            }

            if (avatar.activeRace.name == "HumanFemaleDCS")
            {
                currentEarsFemale = earsIndex;

                if (earsModelsFemale[currentEarsFemale] == "None")
                    avatar.ClearSlot("Ears");
                else
                    avatar.SetSlot("Ears", earsModelsFemale[currentEarsFemale]);
            }
            avatar.BuildCharacter();
        }

        public void ChangeEyes(int eyeIndex)
        {
            if (avatar.activeRace.name == "HumanMaleDCS")
            {
                currentEyeMale = eyeIndex;

                if (eyeModelsMale[currentEyeMale] == "None")
                    avatar.ClearSlot("Eyes");
                else
                    avatar.SetSlot("Eyes", eyeModelsMale[currentEyeMale]);
            }

            if (avatar.activeRace.name == "HumanFemaleDCS")
            {
                currentEyeFemale = eyeIndex;

                if (eyeModelsFemale[currentEyeFemale] == "None")
                    avatar.ClearSlot("Eyes");
                else
                    avatar.SetSlot("Eyes", eyeModelsFemale[currentEyeFemale]);
            }
            avatar.BuildCharacter();
        }

        public void ChangeEyebrows(int eyebrowsIndex)
        {
            if (avatar.activeRace.name == "HumanMaleDCS")
            {
                currentEyebrowsMale = eyebrowsIndex;

                if (eyebrowsModelsMale[currentEyebrowsMale] == "None")
                    avatar.ClearSlot("Eyebrows");
                else
                    avatar.SetSlot("Eyebrows", eyebrowsModelsMale[currentEyebrowsMale]);
            }

            if (avatar.activeRace.name == "HumanFemaleDCS")
            {
                currentEyebrowsFemale = eyebrowsIndex;

                if (eyebrowsModelsFemale[currentEyebrowsFemale] == "None")
                    avatar.ClearSlot("Eyebrows");
                else
                    avatar.SetSlot("Eyebrows", eyebrowsModelsFemale[currentEyebrowsFemale]);
            }
            avatar.BuildCharacter();
        }

        public void ChangeBeard(int beardIndex)
        {
            if (avatar.activeRace.name == "HumanMaleDCS")
            {
                currentBeardMale = beardIndex;

                if (beardModelsMale[currentBeardMale] == "None")
                    avatar.ClearSlot("Beard");
                else
                    avatar.SetSlot("Beard", beardModelsMale[currentBeardMale]);
            }

            if (avatar.activeRace.name == "HumanFemaleDCS")
            {
                currentBeardFemale = beardIndex;

                if (beardModelsFemale[currentBeardFemale] == "None")
                    avatar.ClearSlot("Beard");
                else
                    avatar.SetSlot("Beard", beardModelsFemale[currentBeardFemale]);
            }
            avatar.BuildCharacter();
        }

        public void ChangeTorso(int torsoIndex)
        {
            if (avatar.activeRace.name == "HumanMaleDCS")
            {
                currentTorsoMale = torsoIndex;

                if (torsoModelsMale[currentTorsoMale] == "None")
                    avatar.ClearSlot("Chest");
                else
                    avatar.SetSlot("Chest", torsoModelsMale[currentTorsoMale]);
            }

            if (avatar.activeRace.name == "HumanFemaleDCS")
            {
                currentTorsoFemale = torsoIndex;

                if (torsoModelsFemale[currentTorsoFemale] == "None")
                    avatar.ClearSlot("Chest");
                else
                    avatar.SetSlot("Chest", torsoModelsFemale[currentTorsoFemale]);
            }
            avatar.BuildCharacter();
        }

        public void ChangeBottom(int bottomIndex)
        {
            if (avatar.activeRace.name == "HumanMaleDCS")
            {
                currentBottomMale = bottomIndex;

                if (bottomModelsMale[currentBottomMale] == "None")
                    avatar.ClearSlot("Legs");
                else
                    avatar.SetSlot("Legs", bottomModelsMale[currentBottomMale]);
            }

            if (avatar.activeRace.name == "HumanFemaleDCS")
            {
                currentBottomFemale = bottomIndex;

                if (bottomModelsFemale[currentBottomFemale] == "None")
                    avatar.ClearSlot("Legs");
                else
                    avatar.SetSlot("Legs", bottomModelsFemale[currentBottomFemale]);
            }
            avatar.BuildCharacter();
        }

        public void ChangeHands(int handsIndex)
        {
            if (avatar.activeRace.name == "HumanMaleDCS")
            {
                currentHandsMale = handsIndex;

                if (handsModelsMale[currentHandsMale] == "None")
                    avatar.ClearSlot("Hands");
                else
                    avatar.SetSlot("Hands", handsModelsMale[currentHandsMale]);
            }

            if (avatar.activeRace.name == "HumanFemaleDCS")
            {
                currentHandsFemale = handsIndex;

                if (handsModelsFemale[currentHandsFemale] == "None")
                    avatar.ClearSlot("Hands");
                else
                    avatar.SetSlot("Hands", handsModelsFemale[currentHandsFemale]);
            }
            avatar.BuildCharacter();
        }

        public void ChangeFeet(int feetIndex)
        {
            if (avatar.activeRace.name == "HumanMaleDCS")
            {
                currentFeetMale = feetIndex;

                if (feetModelsMale[currentFeetMale] == "None")
                    avatar.ClearSlot("Feet");
                else
                    avatar.SetSlot("Feet", feetModelsMale[currentFeetMale]);
            }

            if (avatar.activeRace.name == "HumanFemaleDCS")
            {
                currentFeetFemale = feetIndex;

                if (feetModelsFemale[currentFeetFemale] == "None")
                    avatar.ClearSlot("Feet");
                else
                    avatar.SetSlot("Feet", feetModelsFemale[currentFeetFemale]);
            }
            avatar.BuildCharacter();
        }

        public void SaveRecipe()
        {
			
			if (saveFilePath != null)
            {
                myRecipe = avatar.GetCurrentRecipe();
                File.WriteAllText(saveFilePath + "\\" + saveName + ".txt", myRecipe);
                Debug.Log("Recipe saved to: " + saveFilePath + "\\" + saveName + ".txt");
            }
            else
            {
                myRecipe = avatar.GetCurrentRecipe();
                File.WriteAllText(Application.persistentDataPath + "/CharacterRecipes/" + saveName + ".txt", myRecipe);
                Debug.Log("Recipe saved to: " + Application.persistentDataPath + "/CharacterRecipes/" + saveName + ".txt");
            }
            
        }

        public void LoadRecipe()
        {
			
            myRecipe = File.ReadAllText(Application.persistentDataPath + "/CharacterRecipes/" + saveName + ".txt");
            avatar.ClearSlots();
            avatar.LoadFromRecipeString(myRecipe);
            Debug.Log("Recipe loaded from: " + Application.persistentDataPath + "/CharacterRecipes/" + saveName + ".txt");
        }
    }
}