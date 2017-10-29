package com.example.pok.week7_0;


import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import org.w3c.dom.Text;

import java.util.List;

/**
 * Created by pok on 2/15/2017.
 */

public class CustomAdapter extends BaseAdapter {

    Context mContext;
    //List<String> strName;
    String[] strName;

    public CustomAdapter(Context context, String[] strName) //List<String> strName)
    {
        mContext = context;
        this.strName = strName;

    }

    @Override
    public int getCount() {
        return strName.length; // strName.size();
    }

    @Override
    public Object getItem(int i) {
        return null;
    }

    @Override
    public long getItemId(int i) {
        return 0;
    }

    @Override
    public View getView(int position, View view, ViewGroup parent) {
        LayoutInflater mInflater = (LayoutInflater)mContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);

        if(view == null)
            view = mInflater.inflate(R.layout.listview_row, parent, false);

        TextView textView = (TextView)view.findViewById(R.id.textView1);
        textView.setText(strName[position]); //.get(position));

        return view;
    }
}