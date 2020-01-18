using System.Runtime.InteropServices;
using System;
class WMI {
#region  importedFunctions
[DllImport("WMILib.dll", CallingConvention = CallingConvention.Cdecl)]
private static extern IntPtr getWMIAPI();

[DllImport("WMILib.dll", CallingConvention = CallingConvention.Cdecl)]
[return:MarshalAs(UnmanagedType.I1)]
private static extern bool InitializeWMIAPI(IntPtr wmi);

[DllImport("WMILib.dll", CallingConvention = CallingConvention.Cdecl)]
private static extern UInt64 getTotalCPUUsage(IntPtr wmi);

[DllImport("WMILib.dll", CallingConvention = CallingConvention.Cdecl)]
[return: MarshalAs(UnmanagedType.BStr)]
private static extern string getTotalMemory(IntPtr wmi);

[DllImport("WMILib.dll", CallingConvention = CallingConvention.Cdecl)]
[return: MarshalAs(UnmanagedType.BStr)]
private static extern string getAvailableMemory(IntPtr wmi);

[DllImport("WMILib.dll", CallingConvention = CallingConvention.Cdecl)]
[return: MarshalAs(UnmanagedType.BStr)]
private static extern string getOSName(IntPtr wmi);

[DllImport("WMILib.dll", CallingConvention = CallingConvention.Cdecl)]
private static extern UInt32 getMemClockSpeed(IntPtr wmi);

[DllImport("WMILib.dll", CallingConvention = CallingConvention.Cdecl)]
private static extern Int32 getMemType(IntPtr wmi);

[DllImport("WMILib.dll", CallingConvention = CallingConvention.Cdecl)]
private static extern Int32 getMemVoltage(IntPtr wmi);

[DllImport("WMILib.dll", CallingConvention = CallingConvention.Cdecl)]
private static extern void uninitializeWMIAPI(IntPtr wmi);



#endregion

~WMI() {
    if(wmi != IntPtr.Zero)
        uninitializeWMIAPI(wmi);
}
private IntPtr wmi;

public WMI() {
    wmi = getWMIAPI();
    var initialized = InitializeWMIAPI(wmi);
    if(!initialized) throw new Exception("Init failed");
}

public string GetOS() {
    return getOSName(wmi);
}

public ulong GetTotalCPUUsage() {
    Console.WriteLine(wmi);
    return getTotalCPUUsage(wmi); 
} 








}