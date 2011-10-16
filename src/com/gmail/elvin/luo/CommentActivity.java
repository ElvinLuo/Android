package com.gmail.elvin.luo;

import android.app.Activity;
import android.os.Bundle;
import android.widget.TextView;

public class CommentActivity extends Activity {
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		TextView textview = new TextView(this);
		textview.setText("This is the comment tab");
		setContentView(textview);
	}
}
