package com.example.pok.lab3_v2;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Toast;

public class Week10_Graphics extends AppCompatActivity {

    DrawableView house;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_week10__graphics);

        house = findViewById(R.id.house);

        house.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Toast.makeText(getApplicationContext(),"clicked house",Toast.LENGTH_SHORT)
                        .show();
            }
        });
    }
}
