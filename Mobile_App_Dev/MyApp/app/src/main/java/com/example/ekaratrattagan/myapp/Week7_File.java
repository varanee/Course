package com.example.ekaratrattagan.myapp;

import android.graphics.Color;
//import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import org.w3c.dom.Text;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;


public class Week7_File extends AppCompatActivity {

    EditText addText;
    TextView showText;
    Button saveBtn, readBtn;
    View rootView;

        @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_week7__file);

        addText = (EditText)findViewById(R.id.editText2);
        showText = (TextView)findViewById(R.id.textView4);
        saveBtn = (Button)findViewById(R.id.button4);
        readBtn = (Button)findViewById(R.id.button5);
        rootView = (View)findViewById(R.id.rootView);

        //Create file
        final String fileName = "myFile.txt";
        //final String data = "HelloWorld";
        File file = new File(this.getFilesDir(), fileName);

        saveBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                //Write
                try {

                    String data = addText.getText().toString();
                    FileOutputStream fos = openFileOutput(fileName, MODE_PRIVATE);
                    fos.write(data.getBytes());
                    fos.close();

                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });


        readBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //Read
                try {

                    InputStream ins = getApplicationContext().openFileInput(fileName);
                    InputStreamReader insr = new InputStreamReader(ins);
                    BufferedReader br = new BufferedReader(insr);

                    String receiveString = "";

                    StringBuilder stringBuilder = new StringBuilder();

                    while((receiveString = br.readLine())!=null){

                        stringBuilder.append(receiveString);

                    }

                    ins.close();

                    String readString = stringBuilder.toString();

                    showText.setText(readString);


























                   // Toast.makeText(getApplicationContext(),
                   //         readString,Toast.LENGTH_LONG).show();

                   // Snackbar.make(rootView,readString,Snackbar.LENGTH_LONG)
                   //         .setActionTextColor(Color.YELLOW).show();



                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });




    }
}
