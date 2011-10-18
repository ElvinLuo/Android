package com.gmail.elvin.luo;

import android.app.TabActivity;
import android.content.Intent;
import android.content.res.Resources;
import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TabHost;

public class CBReaderTabActivity extends TabActivity {

	Button btnTab1, btnTab2;
	EditText edtTab1, edtTab2;

	/** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.main);

		Resources res = getResources();
		TabHost tabHost = getTabHost();
		tabHost.getTabWidget().setDividerDrawable(R.drawable.tab_bottom_left);
		TabHost.TabSpec spec;
		Intent intent;

		intent = new Intent().setClass(this, HomeActivity.class);
		spec = tabHost.newTabSpec("home")
				.setIndicator("", res.getDrawable(R.drawable.eclipse))
				.setContent(intent);
		tabHost.addTab(spec);

		intent = new Intent().setClass(this, CommentActivity.class);
		spec = tabHost.newTabSpec("comment")
				.setIndicator("", res.getDrawable(R.drawable.star))
				.setContent(intent);
		tabHost.addTab(spec);

		intent = new Intent().setClass(this, SettingActivity.class);
		spec = tabHost.newTabSpec("setting")
				.setIndicator("", res.getDrawable(R.drawable.loved))
				.setContent(intent);
		tabHost.addTab(spec);

		intent = new Intent().setClass(this, DiggActivity.class);
		spec = tabHost.newTabSpec("digg")
				.setIndicator("", res.getDrawable(R.drawable.itunes))
				.setContent(intent);
		tabHost.addTab(spec);

		intent = new Intent().setClass(this, SoftwareActivity.class);
		spec = tabHost.newTabSpec("software")
				.setIndicator("", res.getDrawable(R.drawable.anki))
				.setContent(intent);
		tabHost.addTab(spec);

		tabHost.setCurrentTab(0);
	}
}