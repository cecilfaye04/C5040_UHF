
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;
using Android.Media;
using System.Collections; 
namespace UHF
{
	[Activity (Label = "Scan_Fragment")]			
	public class Scan_Fragment : Fragment
	{
		MainActivity mContext;
		RadioButton rdoBtn_singleScan;
		RadioButton rdoBtn_continuous;
		CheckBox chkAnti;
		Spinner sqn_Q;
		Button btnScan;
		TextView tvTotal;
		Button btnClear;
		bool loopFlag=false;
		SoundPool soundPool;
		int soundPoolId;
		UIHand handler;
		private List<IDictionary<string, object>> tagList;
		ListView LvTags;
		SimpleAdapter adapter;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container,Bundle savedInstanceState)
		{
			View view = inflater.Inflate (Resource.Layout.Scan_Fragment,container,false);
			return view;
		}
		public override void OnActivityCreated(Bundle savedInstanceState) {
			base.OnActivityCreated(savedInstanceState);
			mContext = (MainActivity)Activity;
			rdoBtn_singleScan =View.FindViewById<RadioButton> (Resource.Id.rdoBtn_singleScan);
			rdoBtn_continuous = View.FindViewById<RadioButton> (Resource.Id.rdoBtn_continuous);
			rdoBtn_continuous.Checked = true;
			chkAnti = View.FindViewById<CheckBox> (Resource.Id.chkAnti);
			sqn_Q = View.FindViewById<Spinner> (Resource.Id.sqn_Q);
			btnScan = View.FindViewById<Button> (Resource.Id.btnScan);
			LvTags = View.FindViewById<ListView> (Resource.Id.LvTags);
			btnClear = View.FindViewById<Button> (Resource.Id.btnClear);
			tvTotal = View.FindViewById<TextView> (Resource.Id.tvTotal);		
			sqn_Q.SetSelection (3);
			tagList = new List<IDictionary<string, object>> ();
			adapter = new  SimpleAdapter(mContext, tagList, Resource.Layout.listtag_items,
				new String[] { "tagUii", "tagLen", "tagCount", "tagRssi" },
				new int[] { Resource.Id.TvTagUii, Resource.Id.TvTagLen, Resource.Id.TvTagCount,
					Resource.Id.TvTagRssi });
			LvTags.Adapter=adapter;

			btnScan.Click += delegate {
				scan();
			};
			btnClear.Click += delegate {
				Clear();
			};
			soundPool = new SoundPool (10, Stream.Music, 0);
			soundPoolId = soundPool.Load(mContext,Resource.Drawable.beep, 1);
			handler = new UIHand (this);
		}

 
		public override void OnPause() {		 
			base.OnPause();
			StopInventory();
		}

		public void scan()
		{
			if (btnScan.Text == "Stop") 
			{
				StopInventory(); // 停止识别
				return;
			}
			if (!loopFlag) 
			{
				if (rdoBtn_continuous.Checked) 
				{ //连续扫描标签
					byte q = 0;
					byte anti = 0;
					if (chkAnti.Checked)
					{
						q=byte.Parse(sqn_Q.SelectedItemId.ToString());
						anti = 1;
					}

					if (mContext.uhfAPI.StartInventoryTag (anti,q)) {
						loopFlag = true;
						btnScan.Text = "Stop";
						rdoBtn_singleScan.Enabled = false;
						rdoBtn_continuous.Enabled = false;
						chkAnti.Enabled = false;
						sqn_Q.Enabled = false;
						ContinuousRead ();
					}
					else
					{
						Toast.MakeText (mContext,"failuer",ToastLength.Short).Show();
					}
				}
				else 
				{
					//单步扫描标签
					string strUII = mContext.uhfAPI.InventorySingleTag();
					if (!string.IsNullOrEmpty(strUII)) {
						String strEPC = mContext.uhfAPI.ConvertUiiToEPC(strUII);
						AddEPCToList(strEPC,"N/A");
					} else {
						Toast.MakeText (mContext,"failuer",ToastLength.Short);
					}
				}
			}
		}
		private void Clear()
		{
			tvTotal.Text = "0";//.setText("0");
			tagList.Clear ();
			LvTags.Adapter= null;
			adapter.NotifyDataSetChanged ();
			//	adapter.NotifyDataSetChanged();
		}

		private void ContinuousRead()
		{
			Thread th = new Thread (new ThreadStart (delegate {
				while(loopFlag)
				{
					string[] res = mContext.uhfAPI.ReadTagFormBuffer();
					if(res!=null)
					{
						Message msg = handler.ObtainMessage();
						string strEPC;
						string strTid="";
						StringBuilder sb=new StringBuilder();
						if (res[0]!="0000000000000000")
						{
							strTid = "TID:" + res[0]+"\r\n";
						}
						strEPC= "EPC:"+ mContext.uhfAPI.ConvertUiiToEPC(res[1])+"@";
						sb.Append(strTid);
						sb.Append(strEPC);
						sb.Append(res[2]);

						msg.Obj =sb.ToString();
						handler.SendMessage(msg);
				   }
				}
			}));
			th.Start ();
		}
		private void StopInventory() 
		{
			if (loopFlag) 
			{
				mContext.uhfAPI.StopInventory ();
				loopFlag=false;
				btnScan.Text = "Scan";
				rdoBtn_singleScan.Enabled = true;
				rdoBtn_continuous.Enabled = true;
				chkAnti.Enabled = true;
				sqn_Q.Enabled = true;
				 
			}
		}

		private class UIHand:Handler
		{
			Scan_Fragment scanFragment;
			public UIHand(Scan_Fragment _scanFragment)
			{
				scanFragment=_scanFragment;
			}
			public override void HandleMessage(Message msg)
			{
				try
				{
					string result = msg.Obj + "";
					string[] strs = result.Split ('@');
					scanFragment.AddEPCToList (strs [0], strs [1]);
				}
				catch(Exception) {

				}

			}
		}
	
	 
 
		private void AddEPCToList(String epc,String rssi) {
			if (!string.IsNullOrEmpty(epc)) {
				 
				int index = checkIsExist(epc);
				if (index == -1)
				{
					JavaDictionary<string,object> map = new JavaDictionary<string,object> ();
					map.Add ("tagUii", epc);
					map.Add ("tagCount", "1");
					map.Add ("tagRssi", rssi);
					tagList.Add (map);
					adapter = new  SimpleAdapter(mContext, tagList, Resource.Layout.listtag_items,
						new String[] { "tagUii", "tagLen", "tagCount", "tagRssi" },
						new int[] { Resource.Id.TvTagUii, Resource.Id.TvTagLen, Resource.Id.TvTagCount,
							Resource.Id.TvTagRssi });
					LvTags.Adapter=adapter;
				}
				else 
				{
					int	tagcount =int.Parse(tagList [index] ["tagCount"].ToString ())+1;  
					tagList [index]["tagCount"] = tagcount.ToString(); 
					adapter.NotifyDataSetChanged();
				}
				Sound ();
				tvTotal.Text= adapter.Count.ToString();			    
			}
		}
		public int checkIsExist(string strEPC) {
			int existFlag = -1;
			if (string.IsNullOrEmpty(strEPC)) {
				return existFlag;
			}

			String tempStr = "";
			for (int i = 0; i < tagList.Count; i++) {
				tempStr = tagList[i]["tagUii"].ToString();
				if (strEPC==tempStr) {
					existFlag = i;
					break;
				}
			}
			return existFlag;
		}

		 
		private void Sound()
		{
			//第一个参数为id
			//第二个和第三个参数为左右声道的音量控制
			//第四个参数为优先级，由于只有这一个声音，因此优先级在这里并不重要

			//第五个参数为是否循环播放，0为不循环，-1为循环
			//
			//最后一个参数为播放比率，从0.5到2，一般为1，表示正常播放。
			soundPool.Play(soundPoolId,1, 1, 0, 0, 1);


		}
	}
}

