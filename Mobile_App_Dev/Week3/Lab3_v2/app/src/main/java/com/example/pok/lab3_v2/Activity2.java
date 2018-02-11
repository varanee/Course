package com.example.pok.lab3_v2;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;

public class Activity2 extends AppCompatActivity {

    String msg1 = "Lab3";
    String msg2 = "Activity 2 : ";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_2);
        Log.d(msg1, msg2+"onCreate");

        Intent intent=getIntent();
        String[] getMsg = intent.getStringArrayExtra("msgs");
        int input1 = Integer.parseInt(getMsg[0]);
        int input2 = Integer.parseInt(getMsg[1]);
        int answer = Integer.parseInt(getMsg[2]);

        String returnResult = "";
        if((input1 + input2) == answer)
            returnResult = "true";
        else
            returnResult = "false";

        intent.putExtra("result",returnResult);
        setResult(1234,intent);
        finish();//finishing activity
    }

    @Override
    protected void onStart() {
        super.onStart();
        Log.d(msg1, msg2+"onStart");
    }

    @Override
    protected void onResume() {
        super.onResume();
        Log.d(msg1, msg2+"onResume");
    }

    @Override
    protected void onPause() {
        super.onPause();
        Log.d(msg1, msg2+"onPause");
    }

    @Override
    protected void onStop() {
        super.onStop();
        Log.d(msg1, msg2+"onStop");
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        Log.d(msg1, msg2+"onDestroy");
    }

    @Override
    protected void onRestart() {
        super.onRestart();
        Log.d(msg1, msg2+"onRestart");
    }
}
