using EthosClient.Settings;
using EthosClient.Utils;
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
        float timer = 0.5f;
        float r = 0, g = 0, b = 1;

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

        List<Image> quickmenuStuff = new List<Image>();
        List<Button> quickmenuBtn = new List<Button>();
        List<Renderer> Renderers = new List<Renderer>();

        public override string Description => "";

        public override string Name => "RGB Menu";

        public override string Author => "Four_DJ and Yaekith";

        public override void OnUpdate()
        {
            if (Configuration.GetConfig().MenuRGB)
            {
                try
                {
                    if (timer <= 0)
                    {
                        if (quickmenuStuff.Count == 0 || quickmenuBtn.Count == 0) LoadButtons();
                        if (b > 0 && r <= 0)
                        {
                            b -= 0.025f;
                            g += 0.025f;
                        }
                        else if (g > 0)
                        {
                            g -= 0.025f;
                            r += 0.025f;
                        }
                        else if (r > 0)
                        {
                            r -= 0.025f;
                            b += 0.025f;
                        }
                        Color rainbow = new Color(r, g, b);
                        Color rainbow2 = new Color(r, g, b, 0.6f);
                        foreach (Image btn in quickmenuStuff)
                        {
                            try
                            {
                                btn.color = rainbow2;
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
                                    highlightedColor = rainbow * 1.5f,
                                    normalColor = rainbow / 1.5f,
                                    pressedColor = Color.grey * 1.5f
                                };
                            }
                            catch { }
                        }

                        timer = 0.025f;
                    }

                    timer -= Time.deltaTime;
                }
                catch {}
            }
        }

        private void LoadButtons()
        {
            try
            {
                GameObject UserInterface = GameObject.Find("/UserInterface/MenuContent");
                GameObject VoiceDot = GameObject.Find("/UserInterface/UnscaledUI/HudContent/Hud/VoiceDotParent/VoiceDot");
                GameObject VoiceDotDisabled = GameObject.Find("/UserInterface/UnscaledUI/HudContent/Hud/VoiceDotParent/VoiceDotDisabled");

                foreach (CanvasRenderer btn in UserInterface.GetComponentsInChildren<CanvasRenderer>(true))
                {
                    try
                    {
                        if (btn.GetComponent<Image>())
                        {
                            quickmenuStuff.Add(btn.GetComponent<Image>());
                        }
                    }
                    catch { }
                }

                foreach (CanvasRenderer btn in QuickMenu.prop_QuickMenu_0.GetComponentsInChildren<CanvasRenderer>(true))
                {
                    try
                    {
                        if (btn.GetComponent<Image>())
                        {
                            quickmenuStuff.Add(btn.GetComponent<Image>());
                        }
                    }
                    catch { }
                }

                foreach (Button btn in QuickMenu.prop_QuickMenu_0.GetComponentsInChildren<Button>(true))
                {
                    try
                    {
                        quickmenuBtn.Add(btn);
                        if (btn.GetComponentInChildren<CanvasRenderer>())
                        {
                            foreach (Image img in btn.GetComponentsInChildren<Image>(true))
                            {
                                quickmenuStuff.Add(img);
                            }
                        }
                    }
                    catch { }
                }

                foreach (Button btn in UserInterface.GetComponentsInChildren<Button>(true))
                {
                    try
                    {
                        quickmenuBtn.Add(btn);
                        if (btn.GetComponentInChildren<CanvasRenderer>())
                        {
                            foreach (Image img in btn.GetComponentsInChildren<Image>(true))
                            {
                                quickmenuStuff.Add(img);
                            }
                        }
                    }
                    catch { }
                }

                foreach (CanvasRenderer btn in VoiceDot.GetComponentsInChildren<CanvasRenderer>(true))
                {
                    try
                    {
                        if (btn.GetComponent<Image>())
                        {
                            quickmenuStuff.Add(btn.GetComponent<Image>());
                        }
                    }
                    catch { }
                }
                foreach (CanvasRenderer btn in VoiceDotDisabled.GetComponentsInChildren<CanvasRenderer>(true))
                {
                    try
                    {
                        if (btn.GetComponent<Image>())
                        {
                            quickmenuStuff.Add(btn.GetComponent<Image>());
                        }
                    }
                    catch { }
                }
                foreach(var CanvasRenderer in Resources.FindObjectsOfTypeAll<CanvasRenderer>())
                {
                    if (CanvasRenderer.GetComponent<Image>())
                    {
                        quickmenuStuff.Add(CanvasRenderer.GetComponent<Image>());
                    }
                }
            }
            catch { }
        }
    }
}
