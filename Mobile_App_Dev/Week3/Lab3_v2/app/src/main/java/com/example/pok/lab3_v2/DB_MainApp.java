package com.example.pok.lab3_v2;

import android.content.Context;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

public class DB_MainApp extends AppCompatActivity {

    Button addBtn;
    Button viewBtn;
    Context context;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_db__main_app);

        addBtn = (Button)findViewById(R.id.addBtnID);
        viewBtn = (Button)findViewById(R.id.viewBtnID);

        context = this.getApplicationContext();

        addBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                Intent intent1 = new Intent(context,Week8_DBAdd.class);

                startActivity(intent1);

            }
        });


        viewBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                Intent intent1 = new Intent(context,Week8_DBView.class);

                startActivity(intent1);

            }
        });

    }
}
