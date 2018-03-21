package com.example.pok.mythread;

import android.os.AsyncTask;
import android.widget.TextView;

/**
 * Created by pok on 10/3/2561.
 */
//Do in background, onPregressUpdate, onPostExecute
public class MyTask extends AsyncTask <String,Integer,String> {

    TextView tv1;

    MyTask(TextView tv){ tv1 = tv; }

    /*@Override
    protected void onPreExecute() { }*/

    //Implement progressbar by using AsyncTask
    @Override
    protected String doInBackground(String... params) {

       // String myString = params[0];
        for (int i = 1; i <= 10; i++) {
            publishProgress(i);
            doSleepFor();
        }
        return "End loop";
    }

    private void doSleepFor() {
        try {
            Thread.sleep(1000);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    @Override
    protected void onProgressUpdate(Integer... values) {
        tv1.setText(values[0].toString());
    }

    @Override
    protected void onPostExecute(String s) {
        tv1.setText(s);  }
}