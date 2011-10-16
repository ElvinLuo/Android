package com.gmail.elvin.luo;

import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;

public class HomeActivity extends Activity {
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.home);

		List<NewsItem> newsList = new ArrayList<NewsItem>();

		try {
			URL url = new URL(getResources().getString(R.string.cnbeta_url));
			HttpURLConnection httpConnection = (HttpURLConnection) url
					.openConnection();

			int responseCode = httpConnection.getResponseCode();
			if (responseCode == HttpURLConnection.HTTP_OK) {
				InputStream in = httpConnection.getInputStream();
				DocumentBuilderFactory dbf = DocumentBuilderFactory
						.newInstance();
				Document dom = dbf.newDocumentBuilder().parse(in);
				Element docEle = dom.getDocumentElement();

				NodeList nl = docEle.getElementsByTagName("a");
				for (int i = 0; i < nl.getLength(); i++) {
					if (nl.item(i).getAttributes().item(0).getNodeValue()
							.contains("marticle.php?sid=")) {
						newsList.add(new NewsItem(nl.item(i).getTextContent(),
								new SimpleDateFormat("yyyy-mm-dd HH:mm:ss")
										.format(new Date()), R.drawable.star));
					}
				}
			}
		} catch (ParserConfigurationException e) {
		} catch (MalformedURLException e) {
		} catch (IOException e) {
		} catch (SAXException e) {
		}

		ListView listView = (ListView) findViewById(R.id.listView);
		listView.setAdapter(new ListViewAdapter(newsList));
	}

	public class NewsItem {

		String title;
		String date;
		int image;

		public NewsItem(String title, String date, int image) {
			this.title = title;
			this.date = date;
			this.image = image;
		}
	}

	public class ListViewAdapter extends BaseAdapter {
		View[] itemViews;

		public ListViewAdapter(List<NewsItem> newsItem) {
			itemViews = new View[newsItem.size()];

			for (int i = 0; i < itemViews.length; i++) {
				itemViews[i] = makeItemView(newsItem.get(i));
			}
		}

		public int getCount() {
			return itemViews.length;
		}

		public View getItem(int position) {
			return itemViews[position];
		}

		public long getItemId(int position) {
			return position;
		}

		private View makeItemView(NewsItem item) {
			LayoutInflater inflater = (LayoutInflater) HomeActivity.this
					.getSystemService(Context.LAYOUT_INFLATER_SERVICE);

			View itemView = inflater.inflate(R.layout.item, null);

			TextView title = (TextView) itemView.findViewById(R.id.itemTitle);
			title.setText(item.title);
			TextView text = (TextView) itemView.findViewById(R.id.itemDate);
			text.setText(item.date);
			ImageView image = (ImageView) itemView.findViewById(R.id.itemImage);
			image.setImageResource(item.image);

			return itemView;
		}

		public View getView(int position, View convertView, ViewGroup parent) {
			if (convertView == null)
				return itemViews[position];
			return convertView;
		}
	}
}
