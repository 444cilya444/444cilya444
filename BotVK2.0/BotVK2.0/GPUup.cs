using OpenHardwareMonitor.Hardware;
using System;
using System.IO;

namespace BotVK2._0
{
    public class GPUup
    {
        public string gpu()
        {
            Computer myComputer = new Computer { GPUEnabled = true, CPUEnabled = true, RAMEnabled = true };
            myComputer.Open();
            string a = "";
            foreach (var hardwareItem in myComputer.Hardware)
            {
                foreach (var sensor in hardwareItem.Sensors)
                {
                    if (sensor.SensorType == SensorType.Temperature || sensor.SensorType == SensorType.Power || sensor.SensorType == SensorType.Fan || sensor.SensorType == SensorType.Clock || sensor.SensorType == SensorType.Flow)
                        a += sensor.Name + "==" + sensor.Value.ToString() + "\n";
                }
            }
            return a.Trim();
        }
        public void ErrFaile(string errText)
        {
            string str2 = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), str = DateTime.Now.ToString("(yyyy.MM.dd) +HH.mm.ss+");
            if (!Directory.Exists($@"{str2}\BotVK2_0Err"))
                Directory.CreateDirectory($@"{str2}\BotVK2_0Err");   
            StreamWriter file = new StreamWriter($@"{str2}\BotVK2_0Err\{str} BotVK2_0Err.txt");
            file.Write(errText);
            file.Close();
        }
    }
}
