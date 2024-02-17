using ICTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ModernGestures.WPF.Internal
{
    internal class WPFGestureHandler : BaseHandler
    {
        FrameworkElement _fe;

        public WPFGestureHandler(FrameworkElement fe) 
        { 
            _fe = fe;
        }

        ModernManipulationMode _ManipulationMode;
        internal ModernManipulationMode ManipulationMode
        {
            get => _ManipulationMode;
            set
            {
                _ManipulationMode = value;
                UpdateInteractionConfiguration();
            }
        }

        TapMode _TapMode;
        internal TapMode TapMode
        {
            get => _TapMode;
            set
            {
                _TapMode = value;
                UpdateInteractionConfiguration();
            }
        }

        internal override void ProcessEvent(InteractionOutput output)
        {
            var screenCoords = new Point(output.Data.X, output.Data.Y);
            switch (output.Data.Interaction)
            {
                case Win32.INTERACTION.TAP:
                    GestureEventArgs args;                    
                    if (output.Data.Tap.Count > 1)
                        args = new DoubleTappedEventArgs(_fe, screenCoords);
                    else
                        args = new TappedEventArgs(_fe, screenCoords);                    
                    this._fe.RaiseEvent(args);
                    break;
                case Win32.INTERACTION.MANIPULATION:
                    break;
            }
        }

        internal void HandleWindowsPointerMessage(
            int msg,
            int pointerID,
            Win32.POINTER_INFO pointerInfo)
        {
            switch (msg)
            {
                case Win32.WM_POINTERDOWN:
                    this.AddPointer(pointerID);
                    this.ProcessPointerFrames(pointerID, pointerInfo.FrameID);
                    break;
                case Win32.WM_POINTERUP:
                    this.ProcessPointerFrames(pointerID, pointerInfo.FrameID);
                    this.RemovePointer(pointerID);
                    break;
                case Win32.WM_POINTERUPDATE:
                    this.ProcessPointerFrames(pointerID, pointerInfo.FrameID);
                    break;
                case Win32.WM_POINTERCAPTURECHANGED:
                    foreach (var pointer in this.ActivePointers)
                        Gestures.PointerOwners.Remove(pointer);
                    this.StopProcessing();
                    break;
            }
        }

        private void UpdateInteractionConfiguration()
        {
            List<Win32.INTERACTION_CONTEXT_CONFIGURATION> cfg =
                new List<Win32.INTERACTION_CONTEXT_CONFIGURATION>();
            cfg.Add(new Win32.INTERACTION_CONTEXT_CONFIGURATION(
                Win32.INTERACTION.MANIPULATION,
                (Win32.INTERACTION_CONFIGURATION_FLAGS)this.ManipulationMode));

            ushort primaryFlags = (ushort)((uint)this.TapMode & 0xFFFF);
            if (primaryFlags > 0)
            {
                cfg.Add(new Win32.INTERACTION_CONTEXT_CONFIGURATION(
                    Win32.INTERACTION.TAP,
                    (Win32.INTERACTION_CONFIGURATION_FLAGS)primaryFlags));
            }

            ushort secondaryFlags = (ushort)((uint)this.TapMode >> 16);
            if (secondaryFlags > 0)
            {
                cfg.Add(new Win32.INTERACTION_CONTEXT_CONFIGURATION(
                    Win32.INTERACTION.TAP,
                    (Win32.INTERACTION_CONFIGURATION_FLAGS)secondaryFlags));
            }

            Win32.SetInteractionConfigurationInteractionContext(
                Context,
                cfg.Count,
                cfg.ToArray());
        }
    }
}
