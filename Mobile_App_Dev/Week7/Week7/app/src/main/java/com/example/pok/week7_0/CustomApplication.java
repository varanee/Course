package com.example.pok.week7_0;

import android.database.sqlite.SQLiteOpenHelper;

import com.clough.android.androiddbviewer.ADBVApplication;
/**
 * Created by pok on 2/16/2017.
 */

public class CustomApplication extends ADBVApplication {
    @Override
    public SQLiteOpenHelper getDataBase() {
        return new DatabaseHandler(getApplicationContext());
    }
}
