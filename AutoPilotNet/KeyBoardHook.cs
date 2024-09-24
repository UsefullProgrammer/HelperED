using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPilotNet
{
    //from https://stackoverflow.com/questions/13998131/get-key-from-any-process
    public delegate IntPtr KeyboardProcess(int nCode, IntPtr wParam, IntPtr lParam);

    public sealed class KeyboardHook
    {
        public static event EventHandler<KeyPressedEventArgs> KeyPressed;
        private const int WH_KEYBOARD = 13;
        private const int WM_KEYDOWN = (int)Keys.OemMinus;
        private static KeyboardProcess keyboardProc = HookCallback;
        private static IntPtr hookID = IntPtr.Zero;
        private static int process = 0;
        
        public static void CreateHook(int processelite)
        {
           process = processelite;
           hookID = SetHook(keyboardProc);
        }

        public static void DisposeHook()
        {
            UnhookWindowsHookEx(hookID);
        }

        private static IntPtr SetHook(KeyboardProcess keyboardProc)
        {
            using (Process currentProcess = Process.GetCurrentProcess())
            using (ProcessModule currentProcessModule = currentProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD, keyboardProc, GetModuleHandle(currentProcessModule.ModuleName), 0);
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            Debug.WriteLine("Code" + nCode + "wparam" + wParam + "lParam" + lParam + "VMKeyDown" );
            //2571 is release
            if (wParam.ToInt32() == 257 && CheckeliteKeys())
            {
                //if (CheckeliteKeys())
                //{&& 
                int vkCode = Marshal.ReadInt32(lParam);
                /*if (vkCode == WM_KEYDOWN)
                {*/
                    if (KeyPressed != null)
                        KeyPressed(null, new KeyPressedEventArgs((Keys)vkCode));
                //}
                //}
            }
            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }
        
        static bool CheckeliteKeys()
        {
            var activatedHandle = GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero)
            {
                return false;
            }


            uint idPactivehandle;
            GetWindowThreadProcessId(activatedHandle, out idPactivehandle);
            var idPED = process;
            if (idPactivehandle == idPED)
            {
                return true;
            }
            return false;
        }
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]

        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, KeyboardProcess lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    }
    public class KeyPressedEventArgs : EventArgs
    {
        public Keys KeyCode { get; set; }
        public KeyPressedEventArgs(Keys Key)
        {
            
            KeyCode = Key;
        }
        
    }
    

}
