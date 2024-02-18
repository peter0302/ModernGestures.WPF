using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Interop;
using System.Windows.Media;
using ICTest;

using ModernGestures.WPF.Internal;

namespace ModernGestures.WPF
{
    public static class Gestures
    {
        #region Public Properties and Accessors/Setters

        #region RegisterWindow

        public static readonly DependencyProperty RegisterWindowProperty =
            DependencyProperty.RegisterAttached(
                "RegisterWindow",
                typeof(bool),
                typeof(Window),
                new FrameworkPropertyMetadata(new PropertyChangedCallback((sender, e) =>
                {
                    if (!(sender is Window wnd))
                        throw new Exception("RegisterWindow can only be set for a Window.");
                    if (!(e.NewValue is bool b) || !b)
                        return;
                    RegisterWindow(wnd);
                })));
        public static bool GetRegisterWindow(DependencyObject obj)
        {
            return (bool)obj.GetValue(RegisterWindowProperty);
        }
        public static void SetRegisterWindow(DependencyObject obj, bool value)
        {
            obj.SetValue(RegisterWindowProperty, value);
        }

        #endregion

        #region ManipulationMode

        public static readonly DependencyProperty ManipulationModeProperty =
            DependencyProperty.RegisterAttached(
                "ManipulationMode",
                typeof(ModernManipulationMode),
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(new PropertyChangedCallback((sender, e) =>
                {
                    if (!(sender is FrameworkElement fe))
                        return;
                    var gestureHandler = fe.GetOrCreateGestureHandler();
                    gestureHandler.ManipulationMode = (ModernManipulationMode)e.NewValue;
                })));
        public static ModernManipulationMode GetManipulationMode(DependencyObject obj)
        {
            return (ModernManipulationMode)obj.GetValue(ManipulationModeProperty);
        }
        public static void SetManipulationMode(DependencyObject obj, ModernManipulationMode value)
        {
            obj.SetValue(ManipulationModeProperty, value);
        }

        #endregion

        #region TapMode

        public static readonly DependencyProperty TapModeProperty =
            DependencyProperty.RegisterAttached(
                "TapMode",
                typeof(TapMode),
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(new PropertyChangedCallback((sender, e) =>
                {
                    if (!(sender is FrameworkElement fe))
                        return;
                    var gestureHandler = fe.GetOrCreateGestureHandler();
                    gestureHandler.TapMode = (TapMode)e.NewValue;                    
                })));
        public static TapMode GetTapMode(DependencyObject obj)
        {
            return (TapMode)obj.GetValue(TapModeProperty);
        }
        public static void SetTapMode(DependencyObject obj, TapMode value)
        {
            obj.SetValue(TapModeProperty, value);
        }

        #endregion

        #endregion

        #region Public Events

        #region Tapped

        public static readonly RoutedEvent TappedEvent = EventManager.RegisterRoutedEvent(
            "Tapped",
            RoutingStrategy.Bubble,
            typeof(TappedEventHandler),
            typeof(FrameworkElement));
        public static void AddTappedHandler(DependencyObject d, TappedEventHandler handler)
        {
            if (!(d is FrameworkElement fe))
                return;
            fe.AddHandler(TappedEvent, handler);
        }
        public static void RemoveTappedHandler(DependencyObject d, TappedEventHandler handler)
        {
            if (!(d is FrameworkElement fe))
                return;
            fe.RemoveHandler(TappedEvent, handler);
        }

        #endregion

        #region RightTapped

        public static readonly RoutedEvent RightTappedEvent = EventManager.RegisterRoutedEvent(
            "RightTapped",
            RoutingStrategy.Bubble,
            typeof(RightTappedEventHandler),
            typeof(FrameworkElement));
        public static void AddRightTappedHandler(DependencyObject d, RightTappedEventHandler handler)
        {
            if (!(d is FrameworkElement fe))
                return;
            fe.AddHandler(RightTappedEvent, handler);
        }
        public static void RemoveRightTappedHandler(DependencyObject d, RightTappedEventHandler handler)
        {
            if (!(d is FrameworkElement fe))
                return;
            fe.RemoveHandler(RightTappedEvent, handler);
        }

        #endregion

        #region DoubleTapped

        public static readonly RoutedEvent DoubleTappedEvent = EventManager.RegisterRoutedEvent(
            "DoubleTapped",
            RoutingStrategy.Bubble,
            typeof(DoubleTappedEventHandler),
            typeof(FrameworkElement));
        public static void AddDoubleTappedHandler(DependencyObject d, DoubleTappedEventHandler handler)
        {
            if (!(d is FrameworkElement fe))
                return;
            fe.AddHandler(DoubleTappedEvent, handler);
        }
        public static void RemoveDoubleTappedHandler(DependencyObject d, DoubleTappedEventHandler handler)
        {
            if (!(d is FrameworkElement fe))
                return;
            fe.RemoveHandler(DoubleTappedEvent, handler);
        }

        #endregion

        #region ManipulationStarted

        public static readonly RoutedEvent ManipulationStartedEvent = EventManager.RegisterRoutedEvent(
            "ManipulationStarted",
            RoutingStrategy.Bubble,
            typeof(ModernManipulationStartedEventHandler),
            typeof(FrameworkElement));
        public static void AddManipulationStartedHandler(DependencyObject d, ModernManipulationStartedEventHandler handler)
        {
            if (!(d is FrameworkElement fe))
                return;
            fe.AddHandler(ManipulationStartedEvent, handler);
        }
        public static void RemoveManipulationStartedHandler(DependencyObject d, ModernManipulationStartedEventHandler handler)
        {
            if (!(d is FrameworkElement fe))
                return;
            fe.RemoveHandler(ManipulationStartedEvent, handler);
        }

        #endregion

        #region ManipulationDelta

        public static readonly RoutedEvent ManipulationDeltaEvent = EventManager.RegisterRoutedEvent(
            "ManipulationDelta",
            RoutingStrategy.Bubble,
            typeof(ModernManipulationDeltaEventHandler),
            typeof(FrameworkElement));
        public static void AddManipulationDeltaHandler(DependencyObject d, ModernManipulationDeltaEventHandler handler)
        {
            if (!(d is FrameworkElement fe))
                return;
            fe.AddHandler(ManipulationDeltaEvent, handler);
        }
        public static void RemoveManipulationDeltaHandler(DependencyObject d, ModernManipulationDeltaEventHandler handler)
        {
            if (!(d is FrameworkElement fe))
                return;
            fe.RemoveHandler(ManipulationDeltaEvent, handler);
        }

        #endregion

        #endregion

        internal static readonly Dictionary<int, WPFGestureHandler> PointerOwners = new Dictionary<int, WPFGestureHandler>();

        internal static readonly DependencyProperty HandlerProperty =
            DependencyProperty.RegisterAttached(
                "Handler",
                typeof(WPFGestureHandler),
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(new PropertyChangedCallback((sender, e) =>
                {
                })));
        internal static bool HandlesGestures(this FrameworkElement fe)
        {
            return fe.GetValue(HandlerProperty) is WPFGestureHandler gh &&
                (gh.TapMode != TapMode.None ||
                 gh.ManipulationMode != ModernManipulationMode.None);
        }        

        internal static WPFGestureHandler GetOrCreateGestureHandler(this FrameworkElement fe)
        {
            var handler = (WPFGestureHandler)fe.GetValue(HandlerProperty);
            if (handler == null)
            {
                handler = new WPFGestureHandler(fe);
                fe.SetValue(HandlerProperty, handler);
                fe.Unloaded += OnElementUnloadedDisposeHandler;
            }
            return handler;
        }

        internal static double GetDPIScale(FrameworkElement fe)
        {
            return VisualTreeHelper.GetDpi(fe).DpiScaleX;
        }

        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (_registeredWindows.TryGetValue(hwnd, out var wnd))
                ProcessPointerEvent(wnd, msg, wParam);

            return IntPtr.Zero;
        }

        private static void ProcessPointerEvent(UIElement wnd, int msg, IntPtr wParam)
        {
            switch (msg)
            {
                case Win32.WM_POINTERDOWN:
                case Win32.WM_POINTERUP:
                case Win32.WM_POINTERUPDATE:
                case Win32.WM_POINTERCAPTURECHANGED:
                    break;
                default:
                    return;
            }

            int pointerID = Win32.GET_POINTER_ID(wParam);
            Win32.POINTER_INFO pi = new Win32.POINTER_INFO();
            if (!Win32.GetPointerInfo(pointerID, ref pi))
                Win32.CheckLastError();

            WPFGestureHandler? handler;

            if (msg == Win32.WM_POINTERDOWN)
            {
                var pt = wnd.PointFromScreen(
                    new Point(
                        pi.PtPixelLocation.X,
                        pi.PtPixelLocation.Y));
                FrameworkElement? target = FindPointerTarget(wnd, pt);
                while (target != null)
                {
                    if (target.HandlesGestures())
                        break;
                    target = VisualTreeHelper.GetParent(target) as FrameworkElement;
                }
                if (target == null)
                    return;
                handler = target.GetOrCreateGestureHandler();
                PointerOwners[pointerID] = handler;
            }
            else if (!PointerOwners.TryGetValue(pointerID, out handler))
                return;

            handler.HandleWindowsPointerMessage(
                msg,
                pointerID,
                pi);

            if (msg == Win32.WM_POINTERUP ||
                msg == Win32.WM_POINTERCAPTURECHANGED)
                PointerOwners.Remove(pointerID);
        }

        private static FrameworkElement? FindPointerTarget(Visual visual, Point pt)
        {
            var result = VisualTreeHelper.HitTest(
                visual,
                pt);
            return result.VisualHit as FrameworkElement;
        }

        private static void RegisterWindow(Window window)
        {
            OperatingSystem os = Environment.OSVersion;
            if (os.Version.Major == 6 && os.Version.Minor < 2 ||
                os.Version.Major < 6)
                throw new Exception("Modern Gestures requires Windows 8 or higher.");

            if (!window.IsLoaded)
                window.Loaded += OnWindowLoaded;
            else
                OnWindowLoaded(window, null);
            window.Unloaded += OnWindowUnloaded;
        }

        private static void OnElementUnloadedDisposeHandler(object sender, RoutedEventArgs e)
        {
            if (!(sender is FrameworkElement fe))
                return;
            var handler = (WPFGestureHandler)fe.GetValue(HandlerProperty);
            handler.Dispose();
            fe.SetValue(HandlerProperty, null);
            fe.Unloaded -= OnElementUnloadedDisposeHandler;
        }

        private static void OnWindowLoaded(object sender, RoutedEventArgs? e)
        {
            if (!(sender is Window window))
                return;

            HwndSource? source = PresentationSource.FromVisual(window) as HwndSource;
            if (source == null)
                throw new Exception("Failed to obtain HwndSource from window");

            _registeredWindows[source.Handle] = window;
            source.AddHook(WndProc);
            RegisterTouchWindow(source.Handle, 0);
            if (!_mipEnabled)
            {
                Win32.EnableMouseInPointer(true);
                _mipEnabled = true;
            }
        }

        private static void OnWindowUnloaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is Window window))
                return;
            HwndSource? source = PresentationSource.FromVisual(window) as HwndSource;
            if (source == null)
                return;
            _registeredWindows.Remove(source.Handle);
            window.Unloaded -= OnWindowUnloaded;
            source.RemoveHook(WndProc);
        }

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean RegisterTouchWindow(IntPtr hWnd, uint ulFlags);
       
        private static Dictionary<IntPtr, Window> _registeredWindows = new Dictionary<IntPtr, Window>();
        private static bool _mipEnabled = false;        
    }
}
