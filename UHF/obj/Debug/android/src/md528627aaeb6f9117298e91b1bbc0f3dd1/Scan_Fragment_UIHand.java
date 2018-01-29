package md528627aaeb6f9117298e91b1bbc0f3dd1;


public class Scan_Fragment_UIHand
	extends android.os.Handler
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_handleMessage:(Landroid/os/Message;)V:GetHandleMessage_Landroid_os_Message_Handler\n" +
			"";
		mono.android.Runtime.register ("UHF.Scan_Fragment+UIHand, UHF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Scan_Fragment_UIHand.class, __md_methods);
	}


	public Scan_Fragment_UIHand () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Scan_Fragment_UIHand.class)
			mono.android.TypeManager.Activate ("UHF.Scan_Fragment+UIHand, UHF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public Scan_Fragment_UIHand (android.os.Handler.Callback p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == Scan_Fragment_UIHand.class)
			mono.android.TypeManager.Activate ("UHF.Scan_Fragment+UIHand, UHF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.OS.Handler+ICallback, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public Scan_Fragment_UIHand (android.os.Looper p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == Scan_Fragment_UIHand.class)
			mono.android.TypeManager.Activate ("UHF.Scan_Fragment+UIHand, UHF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.OS.Looper, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public Scan_Fragment_UIHand (android.os.Looper p0, android.os.Handler.Callback p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == Scan_Fragment_UIHand.class)
			mono.android.TypeManager.Activate ("UHF.Scan_Fragment+UIHand, UHF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.OS.Looper, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.OS.Handler+ICallback, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}

	public Scan_Fragment_UIHand (md528627aaeb6f9117298e91b1bbc0f3dd1.Scan_Fragment p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == Scan_Fragment_UIHand.class)
			mono.android.TypeManager.Activate ("UHF.Scan_Fragment+UIHand, UHF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "UHF.Scan_Fragment, UHF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public void handleMessage (android.os.Message p0)
	{
		n_handleMessage (p0);
	}

	private native void n_handleMessage (android.os.Message p0);

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
