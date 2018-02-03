package com.example.pok.lab3_v2;

import android.content.Intent;
import android.net.Uri;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import java.net.URI;

public class MainActivity extends AppCompatActivity {

    String msg1 = "Lab3";
    String msg2 = "Activity 1 : ";

    //EditText edt;
    float input1 = 0;
    float input2 = 0;

    Button sumBtn;
    TextView result;
    EditText num1, num2, ans;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        //edt = (EditText)findViewById(R.id.location);
        sumBtn = (Button)findViewById(R.id.button);
        num1 = (EditText)findViewById(R.id.input1);
        num2 = (EditText)findViewById(R.id.input2);
        ans = (EditText)findViewById(R.id.input3);
        result = (TextView)findViewById(R.id.textView1);

        sumBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

              String input1 = num1.getText().toString();
              String input2 = num2.getText().toString();
              String answer = ans.getText().toString();
              String[] msgs = {input1,input2,answer};

              Intent intent = new Intent(getApplicationContext(),Activity2.class);
              intent.putExtra("msgs",msgs);
              startActivityForResult(intent,1234);

              // Intent intent = new Intent(getApplicationContext(),Activity2.class);
              // startActivity(intent);


              //String address = edt.getText().toString();
              //address = address.replace(' ','+');
              //Intent intent = new Intent(Intent.ACTION_VIEW, Uri.parse("geo:0,0?q="+address));
              /*String  geoCode = "google.streetview:cbll=13.752366, 100.492577&cbp=0,145,0,5,-15";
              Intent intent = new Intent(Intent.ACTION_VIEW, Uri.parse(geoCode));
              startActivity(intent);*/


            }
        });
        Log.d(msg1, msg2+"onCreate");
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if(requestCode==1234)
        {
            String message=data.getStringExtra("result");
            result.setText("Result = "+message);
        }
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
