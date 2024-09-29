using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing.Imaging;
using WindowsInput.Native;
using System.Globalization;
using System.Diagnostics.Eventing.Reader;
using WindowsInput;
//[DllImport("User32.dll")]
//static extern int SetForegroundWindow(IntPtr point);
namespace AutoPilotNet
{
    
    public partial class VisualForm : Form
    {
        
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        //setting debug
        bool testkey = false;
        bool debug = false;
        bool bGetScreen = true;
        bool bSendKey = true;
        bool Helper = true;
        bool optimize = true;
        int reportlevel = 4;
        bool reportkeysend = true;
        bool Pause = false;
        //-------------
        String PathImageToLoad;
        //HUD not touch
        Rectangle RGravidar;
        //Parameters not touch!
        Image<Bgr, byte> Original;
        Image<Bgr, Byte> Visual;
        Image<Bgr, byte> ISystem;
        Image<Bgr, Byte> FindAlign;
        Image<Bgr, Byte> Target;
        Image<Bgr, Byte> NotTarget;
        Image<Bgr, Byte> Gravidar;
        Image<Bgr, Byte> Jumping;
        Image<Bgr, Byte> Jumping2;
        Image<Bgr, Byte> MassLocked;
        Image<Bgr, Byte> AlignedCross;
        Image<Bgr, Byte> Impact;
        Image<Bgr, Byte> IInfo;

        Image<Bgr, Byte> Screen1;
        Image<Bgr, Byte> Screen2;
        Image<Bgr, Byte> Screen3;
        Image<Bgr, Byte> Screen4;
        bool evade = false;
        bool wasinimpact = false;
        bool wasup = false;

        BackgroundWorker bwAutopilot = new BackgroundWorker();
        //Setting

        String sProgram = "EliteDangerous64";
        
        //Setting Optimizer
        int msRefresh = 150;//+ msrefresh + slow autopilot - msrefresh + load cpu
        bool bElaborateImage = false;
        //Thread TAutoPilot = new Thread(AutoPilot);
        public VisualForm()
        {
            InitializeComponent();
            cbOptimize.Enabled = false;
            cbPause.Enabled = false;
            //TestInputSimulator();
            if (msRefresh<0)
            {
                msRefresh = 1;
            }
            LoadImage();
           

        }
        private void Bot_Helper_ED()
        {
            while (true)
            {
                while (!Pause && autoscan)
                {
                    Thread.Sleep(msRefresh);
                    sw.Restart();
                    GetScreen();
                    if (true && wasfocus)
                    {
                        if (debug)
                        {

                            Report("havepressj:" + havepressj);
                            Report("dateTravel.AddMilliseconds(jumpingtime*2) > DateTime.Now:" + (dateTravel.AddMilliseconds(jumpingtime * 3) > DateTime.Now));
                            //10+10 > 30 

                        }
                        bool bMassLocked = !FindTemplate(Screen3, MassLocked, Visual, 0.85).IsEmpty;
                        //todo mining.
                        if (_jkeypress)
                        {
                            Tryed = 0;
                            Report("In charging jumping");
                            //chargingjumping
                            Thread.Sleep((int)(jumpingtime*0.9));
                            PrecJumping = 0.45;
                            _jkeypress = false;
                            havepressj = true;
                            dateTravel = DateTime.Now;
                        }
                        //else if(!havepressj)
                        //    Thread.Sleep(jumpingtime/4);
                        if (bMassLocked)
                        {
                            Report("In MassLocked AP suspend");
                            //if (RGravidar.IsEmpty)
                            //    RGravidar = GetRectangle(Screen4, Gravidar, 0.65);
                            sw.Stop();
                            Thread.Sleep(5000);
                            sw.Start();
                            continue;
                        }
                        else if (havepressj
                            && (!FindTemplate(Screen4, Jumping, PrecJumping, true, true).IsEmpty || !FindTemplate(Screen4, Jumping2, PrecJumping, true, true).IsEmpty))
                        { //se è in salto non fare niente controllare per quando esce!
                            Report("In jumping");
                            PrecJumping = 0.6;
                            sw.Stop();
                            Thread.Sleep((int)(timetravel / 2));
                            if (!bJumped)
                                Thread.Sleep((int)(timetravel / 2));
                            sw.Start();
                            bJumped = true;
                        }
                        else
                        {

                            if (bJumped)
                            {
                                Report("Leave by jumping");
                                bJumped = false;
                                havepressj = false;
                                ZeroThrust();
                                if (reportlevel > 0)
                                    Report("Auto scan system run");
                                SendKeys(VirtualKeyCode.OEM_MINUS, 30 * 6, true, true, true);
                                int TimeForNextJump = (int)((jumpingtime + jumpcooldown) * 0.75);
                                sw.Stop();
                                Thread.Sleep(TimeForNextJump);
                                sw.Start();
                                //manovra d'evasione
                            }
                            else
                            {//sistema ricorsivo di controllo.

                                //Thread.Sleep(msRefresh);
                                if (Tryed > LimitTry)//troppi tentativi
                                {
                                    havepressj = false;
                                    Tryed = 0;
                                    Thread.Sleep(msRefresh * 12);
                                }
                                if (havepressj)
                                {
                                    Thread.Sleep(msRefresh);
                                    Tryed++;
                                    if (DateTime.Now > dateTravel.AddMilliseconds(jumpingtime * 3))//troppo tempo
                                    {
                                        bJumped = false;
                                        havepressj = false;
                                        Thread.Sleep(msRefresh * 12);
                                    }
                                }
                            }

                            //if (debug)
                            //    Report("Check if not want jump");
                            //if (false && !bJumped && FindTemplate(Screen4, ISystem, 0.51,false,debug).IsEmpty)
                            //{
                            //    Report("Not want jump in eco mod for " + (timetravel + jumpingtime / 2)/1000 + " Seconds");
                            //    sw.Stop();
                            //    Thread.Sleep(timetravel + jumpingtime/2);
                            //    sw.Start();
                            //}

                        }

                    }
                    sw.Stop();
                    if (msAverage == -1)
                        msAverage = (int)sw.ElapsedMilliseconds;
                    else
                        msAverage = ((int)sw.ElapsedMilliseconds + msAverage) / 2;
                    lTime.Invoke(new Action(() => lTime.Text = msAverage.ToString()));
                }
                Thread.Sleep(5000);
                if (debug)
                {
                    if (DateTime.Now > pausetime.AddSeconds(5))
                    {
                        Report("botHelper enable");

                        Pause = false;
                    }
                }
                else if (DateTime.Now > pausetime.AddMinutes(15))
                {
                    Report("botHelper enable");
                    if (cbPause.InvokeRequired)
                    {
                        cbPause.Invoke(new Action(() => cbPause.Checked = false));
                    }
                    else
                        cbPause.Checked = false;
                    Pause = false;
                }
            }
        }
        public void LoadImage()
        {
            Report("Initializing Image");
            evade = false;
            Original = new Image<Bgr, byte>("ImageFinder/NotAligned.bmp");//non modificare in alcun modo
            Visual = Original.Copy();
            FindAlign = new Image<Bgr, byte>("ImageFinder/Iconalign.bmp");
            Target = new Image<Bgr, byte>("ImageFinder/Target.bmp");
            NotTarget = new Image<Bgr, byte>("ImageFinder/NotTarget2.bmp");
            //Gravidar = new Image<Bgr, byte>("ImageFinder/Iconalign.bmp");
            Gravidar = new Image<Bgr, byte>("ImageFinder/Gravidar.bmp");
            ISystem = new Image<Bgr, byte>("ImageFinder/system2.bmp");
            Jumping = new Image<Bgr, byte>("ImageFinder/Jumping.bmp");

            Jumping2 = new Image<Bgr, byte>("ImageFinder/Jumping2.bmp");
            MassLocked = new Image<Bgr, byte>("ImageFinder/MassLockedSmall.bmp");
            AlignedCross = new Image<Bgr, byte>("ImageFinder/AlignedCross.bmp");
            Impact = new Image<Bgr, byte>("ImageFinder/Impact.bmp");
            IInfo = new Image<Bgr, byte>("ImageFinder/Info.bmp");
            if (!optimize)
                ibVisual.Image = Visual;
        }
        int sleepepic = 100;
        private void InitVariables()
        {
            aColor= new double[] { 0,25,50,75,100,125,150,175,200,225,255 };
            cbOptimize.Checked = optimize;
            Report("value optimize : " + optimize.ToString());
            Thread.Sleep(sleepepic);
            Report("value debug : " + debug.ToString());
            Thread.Sleep(sleepepic);
            Report("value testkey : " + testkey.ToString());
            Thread.Sleep(sleepepic);
            Report("value bGetScreen : " + bGetScreen.ToString());
            Thread.Sleep(sleepepic);
            Report("value bSendKey : " + bSendKey.ToString());
            Thread.Sleep(sleepepic);
            Report("value Helper : " + Helper.ToString());
            Thread.Sleep(sleepepic);
            cbOptimize.Enabled = true;
            Report("value reportkeysend : " + reportkeysend.ToString());
            Thread.Sleep(sleepepic);
            Report("value Pause : " + Pause.ToString());
            cbPause.Checked = Pause;
            cbPause.Enabled = true;
            Thread.Sleep(sleepepic);
            Report("value refresh in ms : " + msRefresh.ToString());
            txtrefresh.Text = msRefresh.ToString();
            Thread.Sleep(sleepepic);
            autoscan = true;
            Report("autoscan : " + autoscan.ToString());
            cbAutoScan.Checked = autoscan;
        }
        private void LoadHud()
        {
            //while (true)
            //{
            GetScreen();
            RGravidar = GetRectangle(Screen4, Gravidar, 0.65);
            //Thread.Sleep(1000);

            //}
        }
        bool bJumped = false;
        int msAverage = -1;

        bool _jkeypress = false;

        //EliteDangerous64
        //notepad
        Process pEliteDangerous = Process.GetProcessesByName("EliteDangerous64").FirstOrDefault();
        Process pnotepad = Process.GetProcessesByName("notepad").FirstOrDefault();
        Process p = Process.GetProcessesByName("Offworld").FirstOrDefault();
        /// <summary>
        /// Simulate input info https://archive.codeplex.com/?p=inputsimulator
        /// </summary>
        /// 

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        
        private static extern IntPtr GetForegroundWindow();
        private void SendMouse(String tsm, int TimesDown = 3)
        {
            var activatedHandle = GetForegroundWindow();
            if (!CheckEliteDangerousInForeGround())
            {
                return;
            }
            if (!bGetScreen)
                return;

            WindowsInput.InputSimulator isKeySend = new WindowsInput.InputSimulator();
            WindowsInput.MouseSimulator mousesim = new WindowsInput.MouseSimulator(isKeySend);
            Thread.Sleep(10);
            if (TypeSendMouse.RightMouse == tsm)
            {
                if (debug) Report("Begin send mouse");
                mousesim.RightButtonDown().Sleep(34* TimesDown);
            }
            else if (TypeSendMouse.LeftMouse == tsm)
            {
                mousesim.LeftButtonDown().Sleep(34 * TimesDown);
            }
            if (TypeSendMouse.RightMouse == tsm)
            {
                if (debug) Report("Begin send mouse");
                mousesim.RightButtonUp();
            }
            else if (TypeSendMouse.LeftMouse == tsm)
            {
                mousesim.LeftButtonUp();
            }
        }
        private void SendKeys(VirtualKeyCode VKC, int TimesDown = 3, bool _SendKey = false, bool keyup = true, bool scanmouse = false, bool KeyPress = false, bool experimental = true)
        {
            if (TimesDown < 3)
                TimesDown = 3;
            if (_SendKey == false || scanmouse)
                _SendKey = bSendKey;
            
            if(!_SendKey) 
            {
                Report("Can't send key for option bSendKEy");
                return;
            }
            

            //var procId = Process.GetCurrentProcess().Id;
            var activatedHandle = GetForegroundWindow();
            if (!CheckEliteDangerousInForeGround())
            {
                return;
            }
            if (!bGetScreen)
                return;
            //Report("inizio timing");
            
            var isKeySend = new InputSimulator();
            
            var isKeyboard = new KeyboardSimulator(isKeySend);
            bool UseMouse = scanmouse;
            WindowsInput.MouseSimulator mousesim = new WindowsInput.MouseSimulator(isKeySend);
            if (debug) Report("Begin send key: " + VKC.ToString());
            bool fakehuman = false;
            if(!scanmouse) Thread.Sleep(1000);
             
            //if (!UseMouse && !fakehuman)
            //{
            //    isKeyboard.KeyPress(VKC);
            //    fakehuman = true;
            //}
            if(KeyPress)
            {
                keyup = false;
                isKeySend.Keyboard.KeyPress(VKC);
            }
            for (int i = 0; i < TimesDown; i++)
            {
                if (UseMouse)
                {
                    if (debug) Report("Begin send mouse");
                    mousesim.RightButtonDown();
                }
                else if(!KeyPress)
                    isKeyboard.KeyDown(VKC);
                Thread.Sleep(34);
            }
            if (UseMouse)
            {
                mousesim.RightButtonUp();
            }
            else if (keyup)
                isKeyboard.KeyUp(VKC);
            if(debug)Report("end send key");
            bwAutoFire.Dispose();
        }

        public void WorkOnGravidar(Image<Bgr, byte> ScreenGravidar)
        {
            //Image<Bgr, byte> ScreenGravidarOriginal = ScreenGravidar.Copy();
            Rectangle RTarget = new Rectangle();

            Image<Bgr, byte> TrueGravidar = GetTemplate(ScreenGravidar, Gravidar, 0.65,true);
            if (TrueGravidar == null)
            {
                Report("Not found Gravidar in ScreenGravidar");
                RGravidar = GetRectangle(Screen4, Gravidar, 0.65);
                return;
             }
            RTarget = FindTemplate(TrueGravidar, Target, 0.75);
            bool isBack = false;
            
            if (evade)
            {//DA GESTIRE
                ZeroThrust();
            }
            else
            {
                if (RTarget.IsEmpty)
                {
                    isBack = true;
                    Debug.Write("Target is back?");
                    RTarget = FindTemplate(TrueGravidar, NotTarget, 0.65);
                    if (RTarget.IsEmpty)
                    {
                        Report("I not found target back, now Check if exist gravidar");
                        if (!FindTemplate(ScreenGravidar, Gravidar, 0.75, true).IsEmpty)
                        {
                            Report("Exist gravidar, target back but not founded!");
                            
                        }
                        else
                        {
                            isBack = false;
                            Report("Not Exist gravidar.");
                            return;
                        }
                    }
                }
                else
                {
                    Report("Target found");
                }
                Point PositionTarget = RTarget.Location;
                //Report("Grandezza gravidar:" + ScreenGravidarOriginal.Size.ToString());
                //Report("Target location:" + PositionTarget);
                
                int WG = TrueGravidar.Width;
                int IgnoreWidth = 1;//2
                int HG = TrueGravidar.Height;
                int IgnoreHeight = 1;//2
                int XT = PositionTarget.X;
                int YT = PositionTarget.Y;
                if (XT != 0)
                {
                    if (((WG / 2) + IgnoreWidth) < XT)
                    {//1
                        if (YT < (HG / 2))
                        {
                            Report("Cerco di allinearmi di roll right a " + (Math.Abs((WG / 2 - IgnoreWidth) - XT)).ToString());
                            Rollright(Math.Abs((WG / 2 - IgnoreWidth) - XT));
                        }
                        else
                        {
                            Report("Cerco di allinearmi di RollLeft a " + (Math.Abs((WG / 2 - IgnoreWidth) - XT)).ToString());
                            RollLeft(Math.Abs((WG / 2 - IgnoreWidth) - XT));

                        }


                    }
                    else if (((WG / 2) - IgnoreWidth) > XT)
                    {//2
                        if (YT < (HG / 2))
                        {
                            Report("Cerco di allinearmi di RollRight a " + (Math.Abs((WG / 2 - IgnoreWidth) - XT)).ToString());
                            Rollright(Math.Abs((WG/2 - IgnoreWidth) - XT));
                        }
                        else
                        {
                            Report("Cerco di allinearmi di RollLeft a " + (Math.Abs((WG / 2 - IgnoreWidth) - XT)).ToString());
                            RollLeft(Math.Abs((WG/2 - IgnoreWidth) - XT));
                            
                        }

                    }
                }
                if (isBack)
                {
                    Report("Cerco di invertire la nave");
                    Up(90);
                }
                else
                {
                    if ((HG / 2 + IgnoreHeight) < YT)
                    {//3
                        Report("Pitchup at " + (Math.Abs((WG/2 - IgnoreWidth) - XT)).ToString());
                        if(isBack)
                            Down(Math.Abs((WG / 2 - IgnoreWidth) - XT)*2);
                        else
                            Up(Math.Abs((WG / 2 - IgnoreWidth) - XT)*2);
                    }
                    else if (((HG / 2) - IgnoreHeight) > YT)
                    {//4
                        Report("Pitchdown at " + (Math.Abs((WG/2 - IgnoreWidth) - XT)).ToString());
                        if (isBack)
                            Up(Math.Abs((WG / 2 - IgnoreWidth) - XT)*2);
                        else
                            Down(Math.Abs((WG / 2 - IgnoreWidth) - XT)*2);
                    }
                }

            }


        }
        private void Salto()
        {
            Report("Salto");
            SendKeys(VirtualKeyCode.VK_9);
            SendKeys(VirtualKeyCode.VK_J);
            Thread.Sleep(10000);
        }
        private void Rollright(int TimesDown = 15)
        { //e
            SendKeys(VirtualKeyCode.VK_E, TimesDown);
        }
        private void RollLeft(int TimesDown = 15)
        { //q
            SendKeys(VirtualKeyCode.VK_Q, TimesDown);
        }
        private void Up(int TimesDown = 15)
        { //8
            wasup = true;
            SendKeys(VirtualKeyCode.VK_8, TimesDown);
        }
        private void Down(int TimesDown = 15)
        { //2
            wasup = false;
            SendKeys(VirtualKeyCode.VK_0, TimesDown);

        }
        private void ZeroThrust()
        { //0
            Report("Zero trust");
            SendKeys(VirtualKeyCode.VK_X, 15);
        }
        private void MaxThrust()
        {
            SendKeys(VirtualKeyCode.VK_9, 15);
        }

       
        //ROBA PER PRENDERE UNO SCREEN

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc,
            int nWidth, int nHeight);

        [DllImport("user32.dll", EntryPoint = "GetDC")]
        public static extern IntPtr GetDC(IntPtr ptr);

        [DllImport("user32.dll", EntryPoint = "GetWindowDC")]
        public static extern IntPtr GetWindowDC(Int32 ptr);

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        //https://stackoverflow.com/questions/3223159/is-there-any-function-in-opencv-or-emgu-cv-desktop-capture-options-addons-lib
        bool modecapturescreenshotbyDesktop = true;
        public Bitmap CaptureApplication(string procName)
        {
            var proc = Process.GetProcessesByName(procName)[0];
            IntPtr hDC = proc.MainWindowHandle;
            RECT rc;
            if (hDC == null)
            {
                pEliteDangerous = Process.GetProcessesByName(sProgram).FirstOrDefault();
                Report("Need Run process " + sProgram + " Retry to 10 seconds");
                Thread.Sleep(10000);
                return null;
            }
            GetWindowRect(hDC, out rc);
            //We create a compatible bitmap of the screen size and using
            //the screen device context.
            if (rc.Width == 0 || rc.Height == 0)
            {
                Report("Game in loading is impossible screen, Retry to 5 seconds");
                Thread.Sleep(5000);
                return null;
            }
            int width = rc.Width;
            int height = rc.Height;
            int posx = 0;
            int posy = 0;

            Bitmap ScreenShot = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(ScreenShot);
            graphics.CopyFromScreen(posx, posy, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
            return ScreenShot;
        }

        bool wasfocus = false;
        void restorprocess()
        {
            if (pEliteDangerous == null)
            {
                pEliteDangerous = Process.GetProcessesByName(sProgram).FirstOrDefault();
            }
            int ntime = 0;
            while (pEliteDangerous == null && ntime < 10)
            {
                ntime++;
                pEliteDangerous = Process.GetProcessesByName(sProgram).FirstOrDefault();
                Thread.Sleep(60000);
            }
            if(ntime == 10 && pEliteDangerous == null)
            {
                this.Close();
            }
        }
        bool CheckEliteDangerousInForeGround()
        {
            
            var activatedHandle = GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero || pEliteDangerous == null)
            {
                Report("No window is currently activated ");
                return false;
            }
            if (pEliteDangerous.HasExited)
            {
                wasfocus = false;
                Report("Need Run process Retry to 60 seconds");
                Thread.Sleep(1000);
                pEliteDangerous = Process.GetProcessesByName(sProgram).FirstOrDefault();
                restorprocess();
                return false;
            }

            uint idPactivehandle;
            GetWindowThreadProcessId(activatedHandle, out idPactivehandle);
            var idPED = pEliteDangerous.Id;
            if (idPactivehandle != idPED)
            {
                if (wasfocus)
                    Report(pEliteDangerous.ProcessName + " isn't focus");
                wasfocus = false;
                
                return false;
            }
            else if (!wasfocus)
            {
                wasfocus = true;
                Report(pEliteDangerous.ProcessName + " is focus");
            }
            return true;
        }
        public void GetScreen()
        {
            if (bGetScreen)
            {             //In size variable we shall keep the size of the screen.
                Size size;
                Bitmap ScreenShot = null;
                if (!CheckEliteDangerousInForeGround())
                {
                    return;
                }
                if (!modecapturescreenshotbyDesktop)
                {
                    //Variable to keep the handle to bitmap.
                    IntPtr hBitmap;

                    //Here we get the handle to the desktop device context.

                    //IntPtr hDC = GetDC(pEliteDangerous.MainWindowHandle); 
                    IntPtr hDC = pEliteDangerous.MainWindowHandle;
                    //System.Threading.Thread.Sleep(500);
                    //Here we make a compatible device context in memory for screen
                    //device context.
                    //IntPtr hMemDC = CreateCompatibleDC(hDC);

                    //We pass SM_CXSCREEN constant to GetSystemMetrics to get the
                    //X coordinates of the screen.

                    //int x = GetSystemMetrics
                    //  (0);
                    RECT rc;
                    if (hDC == null)
                    {
                        pEliteDangerous = Process.GetProcessesByName(sProgram).FirstOrDefault();
                        Report("Need Run process " + sProgram + " Retry to 10 seconds");
                        Thread.Sleep(10000);
                        return;
                    }
                    GetWindowRect(hDC, out rc);
                    //We pass SM_CYSCREEN constant to GetSystemMetrics to get the
                    //Y coordinates of the screen.
                    //int y = GetSystemMetrics
                    //        (1);

                    //We create a compatible bitmap of the screen size and using
                    //the screen device context.
                    if (rc.Width == 0 || rc.Height == 0)
                    {
                        Report("Game in loading is impossible screen, Retry to 5 seconds");
                        Thread.Sleep(5000);
                        return;
                    }
                    rc.Width = rc.Width;
                    rc.Height = rc.Height;
                    Bitmap Screen = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
                    //hBitmap = CreateCompatibleBitmap
                    // (hDC, 1920, 1080);


                    //As hBitmap is IntPtr, we cannot check it against null.
                    //For this purpose, IntPtr.Zero is used.
                    if (Screen != null)
                    {
                        //I don't know how work...
                        Graphics graphics = Graphics.FromImage(Screen);
                        IntPtr hdcBitmap = graphics.GetHdc();

                        bool SuccefullyPrintWindows = PrintWindow(hDC, hdcBitmap, 0);
                        if (!SuccefullyPrintWindows)
                            Report("Can't get screen");

                        graphics.ReleaseHdc(hdcBitmap);
                        graphics.Dispose();
                        ScreenShot = Screen;


                    }
                }
                else
                {
                    ScreenShot = CaptureApplication(sProgram);
                }
                if (ScreenShot != null)
                {
                    Original = new Image<Bgr, Byte>(ScreenShot);
                    Visual = Original.Copy();
                    if (!optimize)
                        ibVisual.Image = Visual;//DA GESTIRE forse si può togliere
                }
                else
                    Report("Can't get screen");

            }

            int ioneW = Original.Size.Width / 2;
            int ioneH = Original.Size.Height / 2;
            Screen1 = Original.Copy(new Rectangle(Original.Size.Width / 4, Original.Size.Height / 4, ioneW/*+ ioneW/2*/, ioneH/*+ ioneH/2*/));

            Screen2 = Original.Copy(new Rectangle(ioneW, 0, ioneW, ioneH));

            Screen3 = Original.Copy(new Rectangle(ioneW+ ioneW/2, ioneH+ ioneH/2, ioneW/2, ioneH/2));

            Screen4 = Original.Copy(new Rectangle(0, ioneH, ioneW, ioneH));
            if (!optimize)
            {
                IBScreen1.Image = Screen1;
                IBScreen2.Image = Screen2;
                IBScreen3.Image = Screen3;
                IBScreen4.Image = Screen4;
            }
        }
        public Rectangle GetRectangle(Image<Bgr, byte> OriginalSource, Image<Bgr, byte> Find, double Precision = 0.9)
        {
            Rectangle match = new Rectangle();
            //Checking
            if (OriginalSource == null)
            {
                Report("Error image is null");
                return match;
            }
            //--------
            Image<Bgr, byte> source = ElaborateImage(OriginalSource); // Immagine analizzata
            Image<Bgr, byte> template = ElaborateImage(Find); // immagina da trovare
            //source._GammaCorrect(Gammacorrect);
            //template._GammaCorrect(Gammacorrect);
            //imageToShow._GammaCorrect(Gammacorrect);
            if (source.Size.Width >= template.Size.Width && source.Size.Height >= template.Size.Height)
            {
                using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))//ricerco immagine con coefficenti normalizzati forse eccessiva?
                {//gray scala di grigi? potrebbe essere problematica
                    double[] minValues, maxValues;
                    Point[] minLocations, maxLocations;
                    result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                    // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                    if (maxValues[0] > Precision)//primo trovato
                    {
                        //estrapolo l'immagine
                        match = new Rectangle(maxLocations[0], template.Size);
                        match = new Rectangle(match.X - 10, match.Y - 10, match.Width + 20, match.Height + 20);
                    }
                }
            }
            else
                Report("MatchTemplate Size Image < template");
            return match;
        }
        public Image<Bgr, byte> GetTemplateByRettangle(Image<Bgr, byte> OriginalSource, Rectangle Find)
        {
            //Checking
            if (OriginalSource == null)
            {
                Report("Error image is null");
                return null;
            }
            //--------
            Image<Bgr, byte> match = null;
            if (!Find.IsEmpty)//primo trovato
            {
                //estrapolo l'immagine
                match = OriginalSource.Copy(Find);
            }
            return match;
        }
        public Image<Bgr, byte> GetTemplate(Image<Bgr, byte> OriginalSource, Image<Bgr, byte> Find, double Precision = 0.9, bool report = false)
        {
            Image<Bgr, byte> match = null;
            //Checking
            if (OriginalSource == null)
            {
                Report("Error image is null");
                return match;
            }
            //--------
            byte bset1 = 0;
            byte bset2 = 0;
            if (report)
            { bset1 = 1; bset2 = 2; }
            Image<Bgr, byte> source = ElaborateImage(OriginalSource, bset1); // Immagine analizzata
            Image<Bgr, byte> template = ElaborateImage(Find, bset2); // immagina da trovare
            //source._GammaCorrect(Gammacorrect);
            //template._GammaCorrect(Gammacorrect);
            
            using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))//ricerco immagine con coefficenti normalizzati forse eccessiva?
            {//gray scala di grigi? potrebbe essere problematica
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                if (maxValues[0] > Precision)//primo trovato
                {
                    //estrapolo l'immagine
                    match = OriginalSource.Copy(new Rectangle(maxLocations[0], template.Size));
                }
            }
            return match;
        }
        public Rectangle FindTemplate(Image<Bgr, byte> OriginalSource,Image<Bgr, byte> Find, Image<Bgr, byte> VisualSouce, double Precision = 0.9, bool report = false)
        {
            //funziona sorprendentemente...
            //https://stackoverflow.com/questions/16406958/emgu-finding-image-a-in-image-b
            Rectangle match = new Rectangle();
            //Checking
            if (OriginalSource == null)
            {
                Report("Error image is null");
                return match;
            }
            //--------
            byte bset1 = 0;
            byte bset2 = 0;
            if (report)
            { bset1 = 1; bset2 = 2; }
            bool draw = true;
            using (Image<Bgr, byte> source = ElaborateImage(OriginalSource, bset1) )
            {
                using (Image<Bgr, byte> template = ElaborateImage(Find, bset2))
                {
                    Image<Bgr, byte> imageToShow = ElaborateImage(VisualSouce);
                        try
                        {
                            if (source.Width > 15)
                            {
                                using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))//ricerco immagine con coefficenti normalizzati forse eccessiva?
                                {//gray scala di grigi? potrebbe essere problematica
                                    double[] minValues, maxValues;
                                    Point[] minLocations, maxLocations;
                                    result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                                if (maxValues[0] > Precision)//primo trovato
                                {
                                    
                                    match = new Rectangle(maxLocations[0], template.Size);
                                    if (draw)
                                    {
                                        //dopo che lo trovo ricavo un retangolo della sua posizione e
                                        
                                        //lo disegno per debug
                                        imageToShow.Draw(result.ROI, new Bgr(Color.Red), 3);
                                    }
                                    if (!optimize)
                                        IMTEMP.Image = template.Copy();



                                    }
                                }
                            }
                            else
                            {
                                Report("Screen corrupt");
                                Thread.Sleep(10);
                            }
                        }
                        catch (Exception ex)
                        {
                            Report("Eccezione " + ex.ToString());
                            Thread.Sleep(10);
                        }
                        if(!optimize)
                            ibVisual.Image = imageToShow;
                    
                }
            }
            return match;
        }
        double[] aColor;
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        private Image<Bgr, byte> ElaborateImage(Image<Bgr, byte> source,byte bset = 0)
        {
            //Image<Bgr, byte> Ireturn = source.Convert<byte>(delegate (byte b) 
            //{
            //    return (Byte)(255 - b);
            //});
            
            Image<Bgr, byte> Ireturn = source.Copy();
            if (bElaborateImage)
            {
                int x = 0; int y = 0;
                Bgr bgrData;
                double _Blue, _Red, _Green;
                //sw.Reset();
                //sw.Start();
                int Height = Ireturn.Height;
                int Width = Ireturn.Width;
                while (x < Height)
                {
                    while (y < Width)
                    {
                        bgrData = new Bgr();
                        bgrData = Ireturn[x, y];

                        _Blue = bgrData.Blue;
                        _Red = bgrData.Red;
                        _Green = bgrData.Green;
                        if (_Blue < 30 && _Red < 30 && _Green < 30)
                        {
                            //homogeinizzo il nero
                            _Blue = 0;
                            _Red = 0;
                            _Green = 0;

                        }
                        else
                        {
                            _Blue = aColor[(int)(_Blue / 25)];
                            _Green = aColor[(int)(_Green / 25)];
                            _Red = aColor[(int)(_Red / 25)];
                        }
                        bgrData.Blue = _Blue;
                        bgrData.Red = _Red;
                        bgrData.Green = _Green;

                        Ireturn[x, y] = bgrData;
                        y++;
                    }
                    y = 0;
                    x++;
                }
                //sw.Stop();
                //Report("tempo elabiorazione omogenità immagine: " + sw.Elapsed.ToString());
                if (!optimize)
                {
                    if (bset == 1)
                        IBSource.Image = Ireturn;
                    else if (bset == 2)
                        IBTemplate.Image = Ireturn;
                }
            }
            return Ireturn;
        }
        public Rectangle FindTemplate(Image<Bgr, byte> OriginalSource, Image<Bgr, byte> Find, double Precision = 0.9, bool report = false, bool writereport = false)
        {
            //funziona sorprendentemente...
            //https://stackoverflow.com/questions/16406958/emgu-finding-image-a-in-image-b

            Rectangle match = new Rectangle();
            //Checking
            if (OriginalSource == null)
            {
                Report("Error image is null");
                return match;
            }
            //--------
            byte bset1 = 0;
            byte bset2 = 0;
            if (report)
            { bset1 = 1;bset2 = 2; }

            using (Image<Bgr, byte> source = ElaborateImage(OriginalSource, bset1))
            {// Immagine analizzata
                using (Image<Bgr, byte> template = ElaborateImage(Find, bset2))
                {
                    // immagina da trovare
                    try
                    {

                        using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))//ricerco immagine con coefficenti normalizzati forse eccessiva?
                        {//gray scala di grigi? potrebbe essere problematica
                            double[] minValues, maxValues;
                            Point[] minLocations, maxLocations;
                            result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);
                            if(writereport && reportlevel > 3)
                                Report("Risultato trovato: " + maxValues[0]);
                            // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                            if (maxValues[0] > Precision)//primo trovato
                            {
                                //dopo che lo trovo ricavo un retangolo della sua posizione e
                                match = new Rectangle(maxLocations[0], template.Size);
                                //lo disegno per debug
                                //imageToShow.Draw(match, new Bgr(Color.Red), 3);
                                IMTEMP.Image = template;
                                //ibVisual.Image = imageToShow;



                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Report("problema con il matching");
                        if (debug)
                        {
                            Report("Eccezione " + ex.ToString());
                            Thread.Sleep(25);
                        }
                        else
                            Thread.Sleep(1000);
                    }
                }
            }
            
            return match;
        }

        private void imageBox2_Click(object sender, EventArgs e)
        {

        }

        private void VisualForm_Load(object sender, EventArgs e)
        {
           
        }
        private void VisualForm_Shown(object sender, EventArgs e)
        {
            InitVariables();
            if (testkey)
            {
                Report("TEST Send key for scan system");
                SendKeys(VirtualKeyCode.OEM_MINUS, 30 * 6, true);
            }
            do
            {
                
                pEliteDangerous = Process.GetProcessesByName(sProgram).FirstOrDefault();
                if (pEliteDangerous != null || bGetScreen == false)
                {
                    Report("Process necessary are in running");
                    Report("You can play in 3 seconds");
                    Report("Se è la prima volta togli ottimizzazione e metti la finestra in un altro mmonitor e guarda se vede giusto!");
                    Thread.Sleep(2990);
                    if (bGetScreen)
                        SetForegroundWindow(pEliteDangerous.MainWindowHandle);
                    Report("Initializing Screen");
                    Thread.Sleep(sleepepic);
                    GetScreen();
                    Report("Initializing Hud");
                    Thread.Sleep(sleepepic);
                    LoadHud();

                    if (Helper)
                    {
                        Report("Initializing Complete, start auhelper");
                        StartBotHelper();
                    }
                    else
                        StartAutoPilot();
                }
                else
                {
                    Report("Need Run process " + sProgram + " Retry to 3 seconds");
                    Thread.Sleep(3000);
                }
            } while (pEliteDangerous == null && !debug);
        }
        private void StartBotHelper()
        {
            if (pEliteDangerous != null)
            {
                KeyboardHook.CreateHook(pEliteDangerous.Id);
            }
            KeyboardHook.KeyPressed += KeyboardHook_KeyPressed;
            
            // This allows our worker to report progress during work
            bwAutopilot.WorkerSupportsCancellation = true;
            // What to do in the background thread
            bwAutopilot.DoWork += new DoWorkEventHandler(
            delegate (object o, DoWorkEventArgs args)
            {
                
                Bot_Helper_ED();

            });
            //Now in start
            bwAutopilot.RunWorkerAsync();
        }
        
        private void StartAutoPilot()
        {


            // this allows our worker to report progress during work
            //bwAutopilot.WorkerReportsProgress = true;
            bwAutopilot.WorkerSupportsCancellation = true;
            // what to do in the background thread
            bwAutopilot.DoWork += new DoWorkEventHandler(
            delegate (object o, DoWorkEventArgs args)
            {
               // AutoPilot();
            //    BackgroundWorker b = o as BackgroundWorker;

            //// do some simple processing for 10 seconds
            //for (int i = 1; i <= 10; i++)
            //    {
            //    // report the progress in percent
            //    b.ReportProgress(i * 10);
            //        Thread.Sleep(1000);
            //    }

            });

            // what to do when progress changed (update the progress bar for example)
            //bw.ProgressChanged += new ProgressChangedEventHandler(
            //delegate (object o, ProgressChangedEventArgs args)
            //{
            //    label1.Text = string.Format("{0}% Completed", args.ProgressPercentage);
            //});

            // what to do when worker completes its task (notify the user)
            //bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            //delegate (object o, RunWorkerCompletedEventArgs args)
            //{
            //    label1.Text = "Finished!";
            //});

            bwAutopilot.RunWorkerAsync();
        }
        int timetravel   = 15000;
        int jumpingtime  = 20000;
        int jumpcooldown = 10000;
        KeyboardHook keyboardFromProcessing;
        DateTime pausetime = DateTime.Now;
        double PrecJumping = 0.6;
        bool havepressj = false;
        DateTime dateTravel = DateTime.Now;
        bool autoscan = true;
        byte Tryed = 0;
        byte LimitTry = 17;
        
        private void VisualForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bwAutopilot.CancelAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog dFile = new OpenFileDialog();
            dFile.Multiselect = false;
            dFile.ShowDialog();
            PathImageToLoad = dFile.FileName;
            if (System.IO.Path.GetExtension(dFile.FileName) == ".bmp")
            {
                PathImageToLoad = dFile.FileName;
                Original = new Image<Bgr, byte>(PathImageToLoad);//non modificare in alcun modo
                Visual = Original.Copy();
            }
            else
                Report("Estensione file non valida");
        }
        private void Report(String s)
        {
            String[] atext = txtinfo.Text.Split("\r\n".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);

            String stext = "";
            if(atext.Length != 0)
                stext = atext[atext.Length - 1];
            stext.Replace("\r\n", "");
            if (stext == "" || !s.Contains(stext) || s.ToUpper().Contains("RETRY"))
            {
                if (txtinfo.InvokeRequired)
                {
                    txtinfo.Invoke(new Action(() => txtinfo.AppendText(s + "\r\n")));
                }
                else
                    txtinfo.AppendText(s + "\r\n");
            }

        }

        private void VisualForm_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
        bool autofireon = true;
        bool fire = false;
        
        BackgroundWorker bwAutoFire = new BackgroundWorker();
        bool bwAutoFireBusy = false;
        void KeyboardHook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (!bwAutoFireBusy)
            {
                bwAutoFire.Dispose();
                bwAutoFire = new BackgroundWorker();
                if (e.KeyCode == Keys.RControlKey)
                {
                    if (Pause)
                    {

                        Pause = false;
                        Report("botHelper enable");
                    }
                    else
                    {
                        pausetime = DateTime.Now;
                        Pause = true;
                        Report("botHelper disable he return on in 15 minutes");
                    }
                    cbPause.Checked = Pause;
                    if (debug)
                    {
                        if (fire || autofireon)
                        { autofireon = false; fire = false; Report("Autofire off"); }
                        else { autofireon = true; Report("Autofire on"); }
                    }
                }
                if (e.KeyCode == Keys.Divide)
                {
                    cbAutoScan.Checked = !cbAutoScan.Checked;
                }
                if (Keys.Oem7 == e.KeyCode)//à
                {
                    //if (!bwAutoFire.IsBusy)
                    //{
                    Report("botHelper left click for  30 * 6");
                    bwAutoFire.DoWork += new DoWorkEventHandler(

                    delegate (object o, DoWorkEventArgs args)
                    {
                        bwAutoFireBusy = true;
                        SendMouse(TypeSendMouse.LeftMouse, 30 * 6);
                        bwAutoFireBusy = false;
                    });
                    bwAutoFire.RunWorkerAsync();
                    //}
                }
                if (Keys.Oemplus == e.KeyCode)//+
                {
                    //if (!bwAutoFire.IsBusy)
                    //{
                    Report("botHelper right click for  30 * 9");
                    bwAutoFire.DoWork += new DoWorkEventHandler(
                    delegate (object o, DoWorkEventArgs args)
                    {
                        bwAutoFireBusy = true;
                        SendMouse(TypeSendMouse.RightMouse, 30 * 10);
                        bwAutoFireBusy = false;
                    });
                    bwAutoFire.RunWorkerAsync();
                    //}
                }
                if (Keys.OemQuestion == e.KeyCode)//ù
                {
                    //if (!bwAutoFire.IsBusy)
                    //{
                    Report("botHelper right click for  30 * 6");
                    bwAutoFire.DoWork += new DoWorkEventHandler(
                    delegate (object o, DoWorkEventArgs args)
                    {
                        bwAutoFireBusy = true;
                        SendMouse(TypeSendMouse.RightMouse, 30 * 6);
                        bwAutoFireBusy = false;
                    });
                    bwAutoFire.RunWorkerAsync();
                    //}
                }
                if (debug && Keys.Insert == e.KeyCode)
                {
                    SendKeys(VirtualKeyCode.DOWN, 2, false, false, false, true);
                    SendKeys(VirtualKeyCode.VK_S, 30, false, false);

                }
                //if(Keys.)
                if (Keys.OemMinus == e.KeyCode)
                {

                    if (autofireon && !fire)//se è in modalità automatica e non sta sparando
                    {
                        Report("Autoscan: fire with second fire");
                        bwAutoFire.DoWork += new DoWorkEventHandler(
                        delegate (object o, DoWorkEventArgs args)
                        {
                            bwAutoFireBusy = true;
                            SendKeys(VirtualKeyCode.OEM_MINUS, 30 * 6, false, false);
                            bwAutoFireBusy = false;
                            //bool bStopfire = false;


                            //fire = true;
                            //while (!bStopfire)
                            //{
                            //    if (autofireon)
                            //        SendKeys(VirtualKeyCode.OEM_MINUS, 3, false, false);
                            //    else
                            //    {
                            //        SendKeys(VirtualKeyCode.OEM_MINUS);
                            //        bStopfire = true;
                            //    }
                            //}
                        });
                        bwAutoFire.RunWorkerAsync();
                    }
                    //else if (fire)//sennò lo fermo semplicemente, facendo decadere il backgroundworker
                    //{
                    //    fire = false;
                    //}
                }
            }
            if (Keys.J == e.KeyCode)
            {
                _jkeypress = true;
            }
            if(reportkeysend)
                Report("User press: " + e.KeyCode);
        }
        Point txtinfopoint;
        Size txtinfoSize;
        private void cbOptimize_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                optimize = ((CheckBox)sender).Checked;
                if (txtinfopoint.X == 0)
                {
                    txtinfopoint = txtinfo.Location;
                    txtinfoSize = txtinfo.Size;
                }
                if (optimize)
                {
                    txtinfo.Location = new Point(2, 3);
                    txtinfo.Size = new Size(1200, 590);
                }
                else
                {
                    txtinfo.Location = txtinfopoint;
                    txtinfo.Size = txtinfoSize;
                }
            }
            catch(Exception ex)
            {
                Report("errore: " + ex.ToString());
                cbOptimize.Checked = optimize;
            }
        }

        private void cbPause_CheckedChanged(object sender, EventArgs e)
        {
            
            Pause = ((CheckBox)sender).Checked;
            if(Pause) Report("botHelper disable he return on in 15 minutes");
        }

        private void txtrefresh_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permetti solo cifre (da 0 a 9) e tasti di controllo come Backspace #chatgpt
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Impedisce l'inserimento del carattere non valido
            }
            else if(txtrefresh.TextLength > 2)
                msRefresh = int.Parse(txtrefresh.Text);
            if (msRefresh < 150)
                msRefresh = 150;
        }

        private void cbAutoScan_CheckedChanged(object sender, EventArgs e)
        {
            autoscan = ((CheckBox)sender).Checked;
            if (!autoscan) Report("botHelper not start autoscan in autonomy");
        }
    }
}

