package com.example.pok.lab3_v2;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.ListView;
import android.widget.Toast;

import java.util.List;

public class Week8_DBView extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_db);

        //Create DB and Table
        DatabaseHandler db = new DatabaseHandler(this);

        List<Contact> contacts = db.getAllContacts();

        String[] datas = new String[contacts.size()];
        String[] pn    = new String[contacts.size()];
        String[] imgPaths = new String[contacts.size()];

        for(int i=0; i<datas.length; i++)
        {
            datas[i]= contacts.get(i)._name;
            pn[i] = contacts.get(i)._phone_number;
            imgPaths[i] = contacts.get(i)._img_path;
        }

        CustomAdapter adapter = new CustomAdapter(getApplicationContext(),datas,pn,imgPaths);
        ListView listView = (ListView)findViewById(R.id.listView1);
        listView.setAdapter(adapter);
    }
}
