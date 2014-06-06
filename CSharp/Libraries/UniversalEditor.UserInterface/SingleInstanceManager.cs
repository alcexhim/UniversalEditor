using System;
using System.Security.Permissions;
using System.Threading;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Diagnostics;
using System.Runtime.Remoting.Channels.Ipc;

internal static class SingleInstanceManager
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [Serializable]
    private class InstanceProxy : MarshalByRefObject
    {
        public static bool IsFirstInstance
        {
            get;
            internal set;
        }
        public static string[] CommandLineArgs
        {
            get;
            internal set;
        }
        public void SetCommandLineArgs(bool isFirstInstance, string[] commandLineArgs)
        {
            SingleInstanceManager.InstanceProxy.IsFirstInstance = isFirstInstance;
            SingleInstanceManager.InstanceProxy.CommandLineArgs = commandLineArgs;
        }
    }
    public class InstanceCallbackEventArgs : EventArgs
    {
        public bool IsFirstInstance
        {
            get;
            private set;
        }
        public string[] CommandLineArgs
        {
            get;
            private set;
        }
        internal InstanceCallbackEventArgs(bool isFirstInstance, string[] commandLineArgs)
        {
            this.IsFirstInstance = isFirstInstance;
            this.CommandLineArgs = commandLineArgs;
        }
    }
    public static bool CreateSingleInstance(string name, EventHandler<SingleInstanceManager.InstanceCallbackEventArgs> callback)
    {
        bool result;
        switch (Environment.OSVersion.Platform)
        {
            case PlatformID.Unix:
            case PlatformID.Xbox:
            case PlatformID.MacOSX:
                result = true;
                return result;
        }
        name = name.Replace("\\", "_");
        name = name.Replace("/", "_");
        EventWaitHandle eventWaitHandle = null;
        string name2 = string.Format("{0}-{1}", Environment.MachineName, name);
        SingleInstanceManager.InstanceProxy.IsFirstInstance = false;
        SingleInstanceManager.InstanceProxy.CommandLineArgs = Environment.GetCommandLineArgs();
        try
        {
            eventWaitHandle = EventWaitHandle.OpenExisting(name2);
        }
        catch
        {
            SingleInstanceManager.InstanceProxy.IsFirstInstance = true;
        }
        if (SingleInstanceManager.InstanceProxy.IsFirstInstance)
        {
            eventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, name2);
            ThreadPool.RegisterWaitForSingleObject(eventWaitHandle, new WaitOrTimerCallback(SingleInstanceManager.WaitOrTimerCallback), callback, -1, false);
            eventWaitHandle.Close();
            SingleInstanceManager.RegisterRemoteType(name);
        }
        else
        {
            SingleInstanceManager.UpdateRemoteObject(name);
            if (eventWaitHandle != null)
            {
                eventWaitHandle.Set();
            }
            Environment.Exit(0);
        }
        result = SingleInstanceManager.InstanceProxy.IsFirstInstance;
        return result;
    }
    private static void UpdateRemoteObject(string uri)
    {
        IpcClientChannel chnl = new IpcClientChannel();
        ChannelServices.RegisterChannel(chnl, true);
        SingleInstanceManager.InstanceProxy instanceProxy = Activator.GetObject(typeof(SingleInstanceManager.InstanceProxy), string.Format("ipc://{0}{1}/{1}", Environment.MachineName, uri)) as SingleInstanceManager.InstanceProxy;
        if (instanceProxy != null)
        {
            instanceProxy.SetCommandLineArgs(SingleInstanceManager.InstanceProxy.IsFirstInstance, SingleInstanceManager.InstanceProxy.CommandLineArgs);
        }
        ChannelServices.UnregisterChannel(chnl);
    }
    private static void RegisterRemoteType(string uri)
    {
        IpcServerChannel serverChannel = new IpcServerChannel(Environment.MachineName + uri);
        ChannelServices.RegisterChannel(serverChannel, true);
        RemotingConfiguration.RegisterWellKnownServiceType(typeof(SingleInstanceManager.InstanceProxy), uri, WellKnownObjectMode.Singleton);
        Process currentProcess = Process.GetCurrentProcess();
        currentProcess.Exited += delegate
        {
            ChannelServices.UnregisterChannel(serverChannel);
        };
    }
    private static void WaitOrTimerCallback(object state, bool timedOut)
    {
        EventHandler<SingleInstanceManager.InstanceCallbackEventArgs> eventHandler = state as EventHandler<SingleInstanceManager.InstanceCallbackEventArgs>;
        if (eventHandler != null)
        {
            eventHandler(state, new SingleInstanceManager.InstanceCallbackEventArgs(SingleInstanceManager.InstanceProxy.IsFirstInstance, SingleInstanceManager.InstanceProxy.CommandLineArgs));
        }
    }
}