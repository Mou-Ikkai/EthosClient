using EthosClient.Utils;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EthosClient.Menu.Buttons
{
    public class AudioVRMenu : QMNestedButton
    {
        public AudioVRMenu(QMNestedButton parent) : base(parent, 1, 0, "Audio\nControl", "Control audio devices on your computer and easily adjust your current music, volume, etc.", Color.red, Color.white, Color.red, Color.white)
        {
            new QMSingleButton(this, 1, 0, "Volume\nUp", delegate
            {
                AudioHelper.VolumeUp();
            }, "Turns up the volume on your computer.", Color.red, Color.white);
            new QMSingleButton(this, 1, 1, "Volume\nDown", delegate
            {
                AudioHelper.VolumeDown();
            }, "Turns down the volume on your computer.", Color.red, Color.white);
            new QMToggleButton(this, 1, 2, "Mute\nAudio", delegate
            {
                AudioHelper.MuteOrUnmute();
            }, "Unmute\nAudio", delegate
            {
                AudioHelper.MuteOrUnmute();
            }, "Mute/Unmute your computer's audio.", Color.red, Color.white);
            new QMToggleButton(this, 2, 2, "Play\nAudio", delegate
            {
                AudioHelper.PlayOrPause();
            }, "Pause\nAudio", delegate
            {
                AudioHelper.PlayOrPause();
            }, "Play/Pause your current playing audio.", Color.red, Color.white);
            new QMSingleButton(this, 3, 2, "Previous\nAudio", delegate
            {
                AudioHelper.Back();
            }, "Go back and play the previous audio on your computer.", Color.red, Color.white);
            new QMSingleButton(this, 4, 2, "Next\nAudio", delegate
            {
                AudioHelper.Next();
            }, "Skip to the next track and play the next inline audio.", Color.red, Color.white);
        }
    }
}
