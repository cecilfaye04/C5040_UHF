using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Rscja.Deviceapi; 
using System.Collections; 
using System.Collections.Generic;

namespace UHF
{
	[Activity (Label = "UHF", MainLauncher = true)]
	public class MainActivity : Activity
	{
		public RFIDWithUHF uhfAPI;

		private ActionBar actionBar=null;
		private Scan_Fragment scan_Fragment=null;
		private Set_Fragment set_Fragment=null;
		private Write_Fragment write_Fragment =null;
		private Read_Fragment read_Fragment =null;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			 
			 if (uhfAPI == null) {
				try
				{
					 uhfAPI=RFIDWithUHF.Instance;
				 	 uhfAPI.StopInventory ();
				}catch
				{
				
				}
			 }
			initTab ();
		}

		protected override void OnResume()
		{
			base.OnResume ();
			if (uhfAPI != null) {
				new InitTask (this).Execute ();
			}
		}
		protected override void  OnPause()
		{
			base.OnPause ();
		}
		public override bool OnKeyDown(Keycode keyCode,KeyEvent e)
		{
			if (keyCode == Keycode.F9) {
				if (e.RepeatCount == 0) {
					if (actionBar.SelectedTab.Tag != null) {
						if (actionBar.SelectedTab.Tag.ToString() == "Scan") {
							scan_Fragment.scan ();
							return true;
						}
					}
				}
			}
			return base.OnKeyDown (keyCode, e);


		}
		void initTab()
		{
			scan_Fragment = new Scan_Fragment ();
			set_Fragment = new Set_Fragment ();
			write_Fragment = new Write_Fragment ();
			read_Fragment = new Read_Fragment ();

			actionBar = ActionBar;
			actionBar.NavigationMode = ActionBarNavigationMode.Tabs;
			actionBar.SetDisplayHomeAsUpEnabled (true);

			ActionBar.Tab tab1 = actionBar.NewTab ();
			tab1.SetTag("Scan");
			tab1.SetText(Resource.String.tab_Scan);
			tab1.TabSelected += (object sender, ActionBar.TabEventArgs e) => {
				e.FragmentTransaction.Replace(Android.Resource.Id.Content,scan_Fragment);
			};   

			ActionBar.Tab tab2 = actionBar.NewTab ();
			tab1.SetTag ("Write");
			tab2.SetText (Resource.String.tab_WriteData);
			tab2.TabSelected += (object sender, ActionBar.TabEventArgs e) => {
				e.FragmentTransaction.Replace(Android.Resource.Id.Content,write_Fragment);
			};
			ActionBar.Tab tab3 = actionBar.NewTab ();
			tab1.SetTag ("Read");
			tab3.SetText (Resource.String.tab_ReadData);
			tab3.TabSelected += (object sender, ActionBar.TabEventArgs e) => {
				e.FragmentTransaction.Replace(Android.Resource.Id.Content,read_Fragment);
			};

			ActionBar.Tab tab4 = actionBar.NewTab ();
			tab1.SetTag ("set");
			tab4.SetText (Resource.String.tab_set);
			tab4.TabSelected += (object sender, ActionBar.TabEventArgs e) => {
				e.FragmentTransaction.Replace(Android.Resource.Id.Content,set_Fragment);
			};
			actionBar.AddTab (tab1);
			actionBar.AddTab (tab2);
			actionBar.AddTab (tab3);
			actionBar.AddTab (tab4);
		}

		private class InitTask: AsyncTask<Java.Lang.Void, Java.Lang.Void, string[]>{

			MainActivity mainActivity;
			ProgressDialog  proDialg=null;

			public InitTask(MainActivity _mainActivity)
			{
				mainActivity=_mainActivity;
			}
			protected override string[] RunInBackground (params Java.Lang.Void[] @params)
			{
				//throw new NotImplementedException ();
				return null;
			}

			//后台要执行的任务
			protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
			{
				string result = string.Empty;
				if  (mainActivity.uhfAPI!=null)  
				{
					if (mainActivity.uhfAPI.Init()) 
					{
						result = "OK";
					}
				}
				return result;
			}
			protected override void OnPostExecute(Java.Lang.Object result)
			{
				proDialg.Cancel();
				if (result.ToString() != "OK")
					Toast.MakeText (mainActivity,"Init failure!",ToastLength.Short);
			}

			//开始执行任务
			protected override void OnPreExecute()
			{
				proDialg=new ProgressDialog(mainActivity);
				proDialg.SetMessage("init.....");
				proDialg.Show();
			}
		}


	}
}


