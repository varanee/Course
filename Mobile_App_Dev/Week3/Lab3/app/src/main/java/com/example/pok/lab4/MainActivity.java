package com.example.pok.lab4;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import org.w3c.dom.Text;

public class MainActivity extends AppCompatActivity {

    Button b;
    TextView t;
    EditText e;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        b = (Button)findViewById(R.id.button2);
        t = (TextView)findViewById(R.id.textView);
        e = (EditText)findViewById(R.id.editText);

        b.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
               try {
                   String myData = "content://contacts/people/";// t.getText().toString();
                   Intent myIntent = new Intent(Intent.ACTION_VIEW,
                           Uri.parse(myData));
                   startActivityForResult(myIntent, 222);

               }catch(Exception e){
                   e.printStackTrace();
               }
            }
        });
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        try{
            switch(requestCode){
                case(222):{
                    if(requestCode== Activity.RESULT_OK){
                        String selectedContact = data.getDataString();
                        Toast.makeText(getApplicationContext(),selectedContact,Toast.LENGTH_LONG).show();
                        t.setText(selectedContact.toString());

                        Intent myAct3 = new Intent(Intent.ACTION_VIEW,
                                Uri.parse(selectedContact));
                        startActivity(myAct3);
                    }
                    else{
                        t.setText("Selection CANCELLED"
                        + requestCode + " " + resultCode);
                    }
                }
            }
        }
        catch(Exception e){}
    }
}
