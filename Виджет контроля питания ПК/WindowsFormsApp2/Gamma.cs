using System;
using System.Runtime.InteropServices;

namespace PowerOff
{
    class Gamma
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceGammaRamp(IntPtr hDC, ref RAMP lpRamp);

        [DllImport("gdi32.dll")]
        public static extern int SetDeviceGammaRamp(IntPtr hDC, ref RAMP lpRamp);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct RAMP
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public UInt16[] Red;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public UInt16[] Green;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public UInt16[] Blue;
        }
        public int Value
        {
            get
            {
                return _Value;
            }

            set
            {
                this.SetGamma(value);
                _Value = value;
            }
        }

        private int _Value = 128;

        private void SetGamma(int Value)
        {
            IntPtr DC = GetDC(GetDesktopWindow());

            if (DC != null)
            {

                RAMP _Rp = new RAMP();

                _Rp.Blue = new ushort[256];
                _Rp.Green = new ushort[256];
                _Rp.Red = new ushort[256];

                for (int i = 1; i < 256; i++)
                {
                    int value = i * (Value + 128);

                    if (value > 65535)
                        value = 65535;

                    _Rp.Red[i] = _Rp.Green[i] = _Rp.Blue[i] = Convert.ToUInt16(value);
                }

                SetDeviceGammaRamp(DC, ref _Rp);
            }
        }
    }
}
