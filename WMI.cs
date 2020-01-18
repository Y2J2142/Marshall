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
public UInt64 GetTotalCPUUsage() {
    return getTotalCPUUsage(wmi); 
} 

public string GetTotalMemory() {
    return getTotalMemory(wmi);    
}

public string GetAvailableMemory() {
    return getAvailableMemory(wmi);    
}

public UInt32 GetMemClockSpeed() {
    return getMemClockSpeed(wmi);    
}


public Int32 GetMemType() {
    return getMemType(wmi);    
}

public Int32 GetMemVoltage() {
    return getMemVoltage(wmi);    
}




}