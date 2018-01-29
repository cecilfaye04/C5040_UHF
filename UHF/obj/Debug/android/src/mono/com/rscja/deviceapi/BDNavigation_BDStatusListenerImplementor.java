package mono.com.rscja.deviceapi;


public class BDNavigation_BDStatusListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.rscja.deviceapi.BDNavigation.BDStatusListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onBDSatelliteChanged:(Ljava/util/ArrayList;)V:GetOnBDSatelliteChanged_Ljava_util_ArrayList_Handler:Com.Rscja.Deviceapi.BDNavigation/IBDStatusListenerInvoker, DeviceAPI_Android\n" +
			"n_onBDSatelliteFIX:(I)V:GetOnBDSatelliteFIX_IHandler:Com.Rscja.Deviceapi.BDNavigation/IBDStatusListenerInvoker, DeviceAPI_Android\n" +
			"n_onBDSatelliteLocating:()V:GetOnBDSatelliteLocatingHandler:Com.Rscja.Deviceapi.BDNavigation/IBDStatusListenerInvoker, DeviceAPI_Android\n" +
			"n_onBDSatelliteUsedChanged:(I)V:GetOnBDSatelliteUsedChanged_IHandler:Com.Rscja.Deviceapi.BDNavigation/IBDStatusListenerInvoker, DeviceAPI_Android\n" +
			"n_onBDSatelliteViewChanged:(I)V:GetOnBDSatelliteViewChanged_IHandler:Com.Rscja.Deviceapi.BDNavigation/IBDStatusListenerInvoker, DeviceAPI_Android\n" +
			"";
		mono.android.Runtime.register ("Com.Rscja.Deviceapi.BDNavigation+IBDStatusListenerImplementor, DeviceAPI_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BDNavigation_BDStatusListenerImplementor.class, __md_methods);
	}


	public BDNavigation_BDStatusListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BDNavigation_BDStatusListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Rscja.Deviceapi.BDNavigation+IBDStatusListenerImplementor, DeviceAPI_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onBDSatelliteChanged (java.util.ArrayList p0)
	{
		n_onBDSatelliteChanged (p0);
	}

	private native void n_onBDSatelliteChanged (java.util.ArrayList p0);


	public void onBDSatelliteFIX (int p0)
	{
		n_onBDSatelliteFIX (p0);
	}

	private native void n_onBDSatelliteFIX (int p0);


	public void onBDSatelliteLocating ()
	{
		n_onBDSatelliteLocating ();
	}

	private native void n_onBDSatelliteLocating ();


	public void onBDSatelliteUsedChanged (int p0)
	{
		n_onBDSatelliteUsedChanged (p0);
	}

	private native void n_onBDSatelliteUsedChanged (int p0);


	public void onBDSatelliteViewChanged (int p0)
	{
		n_onBDSatelliteViewChanged (p0);
	}

	private native void n_onBDSatelliteViewChanged (int p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
