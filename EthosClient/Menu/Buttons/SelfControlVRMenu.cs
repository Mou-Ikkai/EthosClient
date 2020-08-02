using EthosClient.Utils;
using EthosClient.Wrappers;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EthosClient.Menu.Buttons
{
    public class SelfControlVRMenu : QMNestedButton
    {
        public SelfControlVRMenu(QMNestedButton parent) : base(parent, 1, 1, "Self\nControl", "Controls stuff on your own player.", Color.red, Color.white, Color.red, Color.white)
        {
            new QMSingleButton(this, 0, 0, "Increase\nUSpeaker Bitrate", delegate
            {
                if (!GeneralUtils.VoiceMod) return;

                if (PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentVoice>().field_Private_USpeaker_0.CurrentBitrate != EnumPublicSealedvaBi15BiBiBiBiBiBiBiUnique.BitRate_512k)
                    PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentVoice>().field_Private_USpeaker_0.CurrentBitrate = PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentVoice>().field_Private_USpeaker_0.CurrentBitrate + 1;

            }, "Increase your own uspeaker's bitrate", Color.red, Color.white);

            new QMSingleButton(this, 0, 1, "Decrease\nUSpeaker Bitrate", delegate
            {
                if (!GeneralUtils.VoiceMod) return;

                if (PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentVoice>().field_Private_USpeaker_0.CurrentBitrate != EnumPublicSealedvaBi15BiBiBiBiBiBiBiUnique.BitRate_8K)
                    PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentVoice>().field_Private_USpeaker_0.CurrentBitrate = PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentVoice>().field_Private_USpeaker_0.CurrentBitrate - 1;

            }, "Decrease your own uspeaker's bitrate", Color.red, Color.white);

            new QMToggleButton(this, 4, 2, "Enable\nVoice Mod", delegate
            {
                if (!PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentVoice>())
                    PlayerWrappers.GetCurrentPlayer().GetVRC_Player().gameObject.AddComponent<PlayerModComponentVoice>();

                GeneralUtils.VoiceMod = true;
            }, "Disable\nVoice Mod", delegate
            {
                GeneralUtils.VoiceMod = false;
            }, "Enable/Disable Voice Modification, allowing you to increase the bitrate and volume of your microphone at please.", Color.red, Color.white).setToggleState(GeneralUtils.VoiceMod);

            new QMToggleButton(this, 4, 1, "Go\nAutistic", new Action(() =>
            {
                GeneralUtils.Autism = true;
            }), "Revert\nYour Autism", new Action(() =>
            {
                GeneralUtils.Autism = false;
            }), "Do some crazy shit idk", Color.red, Color.white).setToggleState(GeneralUtils.Autism);

            new QMToggleButton(this, 4, 0, "Enable\nSpin Bot", delegate
            {
                GeneralUtils.SpinBot = true;
            }, "Disable\nSpin Bot", delegate
            {
                GeneralUtils.SpinBot = false;
            }, "Toggle the spin bot and go zooming in circles lol", Color.red, Color.white).setToggleState(GeneralUtils.SpinBot);

            new QMSingleButton(this, 3, 2, "Add\nJump", delegate
            {
                PlayerWrappers.GetCurrentPlayer().GetVRC_Player().gameObject.AddComponent<PlayerModComponentJump>();
            }, "Adds the jump component to your player if it doesn't exist.", Color.red, Color.white);

            new QMToggleButton(this, 5, 0, "Enable\nFlight", delegate
            {
                Physics.gravity = Vector3.zero;
                GeneralUtils.Flight = true;
                GeneralUtils.ToggleColliders(!GeneralUtils.Flight);
            }, "Disable\nFlight", delegate
            {
                Physics.gravity = GeneralUtils.SavedGravity;
                GeneralUtils.Flight = false;
                GeneralUtils.ToggleColliders(!GeneralUtils.Flight);
            }, "Toggle Flight and move around within the air with ease!", Color.red, Color.white).setToggleState(GeneralUtils.Flight);

            new QMSingleButton(this, 5, 1, "Reset\nOptions", delegate
            {
                GeneralUtils.InfiniteJump = false;
                GeneralUtils.VoiceMod = false;
                GeneralUtils.Flight = false;
                GeneralUtils.Autism = false;
                GeneralUtils.SpinBot = false;
                GeneralUtils.SpeedHax = false;
                PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>().field_Private_LocomotionInputController_0.walkSpeed = GeneralUtils.WalkSpeed;
                PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>().field_Private_LocomotionInputController_0.runSpeed = GeneralUtils.RunSpeed;
                PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>().field_Private_LocomotionInputController_0.strafeSpeed = GeneralUtils.StrafeSpeed;
                GeneralUtils.ToggleColliders(!GeneralUtils.Flight);
                Physics.gravity = GeneralUtils.SavedGravity;

            }, "Resets all options on here to default.", Color.red, Color.white);

            new QMToggleButton(this, 1, 2, "Infinite\nJump", delegate
            {
                GeneralUtils.InfiniteJump = true;
            }, "Finite\nJump", delegate
            {
                GeneralUtils.InfiniteJump = false;
                PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<VRCMotionState>().field_Private_Boolean_0 = false;
            }, "Enable/Disable Infinite jumping, when this is enabled, it allows you to jump as much as possible >lol", Color.red, Color.white).setToggleState(GeneralUtils.InfiniteJump);

            new QMToggleButton(this, 2, 2, "Enable\nSpeed Hax", delegate
            {
                GeneralUtils.SpeedHax = true;
                if (!PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>())
                    PlayerWrappers.GetCurrentPlayer().GetVRC_Player().gameObject.AddComponent<PlayerModComponentSpeed>();
            }, "Disable\nSpeed Hax", delegate
            {
                GeneralUtils.SpeedHax = false;
                if (!PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>())
                    PlayerWrappers.GetCurrentPlayer().GetVRC_Player().gameObject.AddComponent<PlayerModComponentSpeed>();

                PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>().field_Private_LocomotionInputController_0.walkSpeed = GeneralUtils.WalkSpeed;
                PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>().field_Private_LocomotionInputController_0.runSpeed = GeneralUtils.RunSpeed;
                PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>().field_Private_LocomotionInputController_0.strafeSpeed = GeneralUtils.StrafeSpeed;
            }, "Enables/Disable Speed Hax on your own player, basically makes you move faster ok.", Color.red, Color.white).setToggleState(GeneralUtils.SpeedHax);

            new QMSingleButton(this, 1, 0, "Increase\nWalk Speed (+2)", delegate
            {
                if (!GeneralUtils.SpeedHax) return;
                PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>().field_Private_LocomotionInputController_0.walkSpeed = PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>().field_Private_LocomotionInputController_0.walkSpeed + 2f;
            }, "Increases your own player's walk speed, only works when Speed Hax is enabled.", Color.red, Color.white);

            new QMSingleButton(this, 1, 1, "Decrease\nWalk Speed (-2)", delegate
            {
                if (!GeneralUtils.SpeedHax) return;
                PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>().field_Private_LocomotionInputController_0.walkSpeed = PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>().field_Private_LocomotionInputController_0.walkSpeed - 2f;
            }, "Decreases your own player's walk speed, only works when Speed Hax is enabled.", Color.red, Color.white);

            new QMSingleButton(this, 2, 0, "Increase\nRun Speed (+2)", delegate
            {
                if (!GeneralUtils.SpeedHax) return;
                PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>().field_Private_LocomotionInputController_0.runSpeed = PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>().field_Private_LocomotionInputController_0.runSpeed + 2f;
            }, "Increases your own player's run speed, only works when Speed Hax is enabled.", Color.red, Color.white);

            new QMSingleButton(this, 2, 1, "Decrease\nRun Speed (-2)", delegate
            {
                if (!GeneralUtils.SpeedHax) return;
                PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>().field_Private_LocomotionInputController_0.runSpeed = PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>().field_Private_LocomotionInputController_0.runSpeed - 2f;
            }, "Decreases your own player's run speed, only works when Speed Hax is enabled.", Color.red, Color.white);

            new QMSingleButton(this, 3, 0, "Increase\nStrafe Speed (+2)", delegate
            {
                if (!GeneralUtils.SpeedHax) return;
                PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>().field_Private_LocomotionInputController_0.strafeSpeed = PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>().field_Private_LocomotionInputController_0.strafeSpeed + 2f;
            }, "Increases your own player's strafe speed, only works when Speed Hax is enabled.", Color.red, Color.white);

            new QMSingleButton(this, 3, 1, "Decrease\nStrafe Speed (-2)", delegate
            {
                if (!GeneralUtils.SpeedHax) return;
                PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>().field_Private_LocomotionInputController_0.strafeSpeed = PlayerWrappers.GetCurrentPlayer().GetVRC_Player().GetComponent<PlayerModComponentSpeed>().field_Private_LocomotionInputController_0.strafeSpeed - 2f;
            }, "Decreases your own player's strafe speed, only works when Speed Hax is enabled.", Color.red, Color.white);
        }
    }
}
