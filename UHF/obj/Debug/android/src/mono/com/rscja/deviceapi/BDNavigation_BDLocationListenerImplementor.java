package mono.com.rscja.deviceapi;


public class BDNavigation_BDLocationListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.rscja.deviceapi.BDNavigation.BDLocationListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDataResult:(Ljava/lang/String;)V:GetOnDataResult_Ljava_lang_String_Handler:Com.Rscja.Deviceapi.BDNavigation/IBDLocationListenerInvoker, DeviceAPI_Android\n" +
			"n_onLocationChanged:(Lcom/rscja/deviceapi/entity/BDLocation;)V:GetOnLocationChanged_Lcom_rscja_deviceapi_entity_BDLocation_Handler:Com.Rscja.Deviceapi.BDNavigation/IBDLocationListenerInvoker, DeviceAPI_Android\n" +
			"";
		mono.android.Runtime.register ("Com.Rscja.Deviceapi.BDNavigation+IBDLocationListenerImplementor, DeviceAPI_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BDNavigation_BDLocationListenerImplementor.class, __md_methods);
	}


	public BDNavigation_BDLocationListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BDNavigation_BDLocationListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Rscja.Deviceapi.BDNavigation+IBDLocationListenerImplementor, DeviceAPI_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onDataResult (java.lang.String p0)
	{
		n_onDataResult (p0);
	}

	private native void n_onDataResult (java.lang.String p0);


	public void onLocationChanged (com.rscja.deviceapi.entity.BDLocation p0)
	{
		n_onLocationChanged (p0);
	}

	private native void n_onLocationChanged (com.rscja.deviceapi.entity.BDLocation p0);

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
