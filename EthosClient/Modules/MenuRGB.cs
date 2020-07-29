using Harmony;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace EthosClient.Modules
{
    public class RGBMenu : VRCMod
    {
        float timer = 0.5f, changeColourTime = 2.0f;
        int currentIndex = 0, nextIndex = 1;

        public List<Color> colors = new List<Color>()
        {
            Color.red,
            Color.magenta,
            Color.blue,
            Color.black,
            Color.green,
            Color.yellow,
            Color.white,
            Color.cyan,
            Color.gray,
        };

        CanvasRenderer[] quickmenuStuff;
        Button[] quickmenuBtn;

        public override string Description => "";

        public override string Name => "RGB Menu";

        public override string Author => "Four_DJ and Yaekith";

        public override void OnUpdate()
        {
            try
            {
                timer -= Time.deltaTime;

                if (timer < 0)
                {
                    if (nextIndex > colors.Count)
                    {
                        currentIndex = 0;
                        nextIndex = 1;
                    }
                    currentIndex++;
                    nextIndex++;
                    Color rainbow = Color.Lerp(colors[currentIndex], colors[nextIndex], timer / changeColourTime);
                    Color rainbow2 = Color.Lerp(colors[currentIndex], colors[nextIndex], timer / changeColourTime);
                    foreach (CanvasRenderer btn in quickmenuStuff)
                    {
                        try
                        {
                            if (btn.GetComponent<Image>())
                            {
                                btn.GetComponent<Image>().color = rainbow;
                            }
                        }
                        catch { }
                    }
                    foreach (Button btn in quickmenuBtn)
                    {
                        try
                        {
                            btn.colors = new ColorBlock()
                            {
                                colorMultiplier = 1f,
                                disabledColor = Color.grey,
                                highlightedColor = rainbow2 * 1.5f,
                                normalColor = rainbow2 / 1.5f,
                                pressedColor = Color.grey * 1.5f
                            };

                            if (btn.GetComponentInChildren<CanvasRenderer>())
                            {
                                foreach (Image img in btn.GetComponentsInChildren<Image>(true))
                                {
                                    img.color = rainbow2;
                                }
                            }
                        }
                        catch { }
                    }
                    timer = 0.3f;
                }
            }
            catch { }
        }

        public override void OnUiLoad()
        {
            try
            {
                GameObject UserInterface = GameObject.Find("/UserInterface/MenuContent");
                quickmenuStuff = QuickMenu.prop_QuickMenu_0.GetComponentsInChildren<CanvasRenderer>(true);
                quickmenuStuff.AddRangeToArray(UserInterface.GetComponentsInChildren<CanvasRenderer>(true));
                quickmenuBtn = QuickMenu.prop_QuickMenu_0.GetComponentsInChildren<Button>(true);
                quickmenuBtn.AddRangeToArray(UserInterface.GetComponentsInChildren<Button>(true));
            } catch { }
        }
    }
}
