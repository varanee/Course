package com.example.pok.week7_0;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;

import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        final DatabaseHandler db = new DatabaseHandler(this);

        db.addContact(new Contact("Ekarat","0899999999"));

        final List<Contact> contacts = db.getAllContacts();

        String[] datas = new String[contacts.size()];
        for(int i=0; i<datas.length; i++)
        {
            datas[i]= contacts.get(i)._name;
        }
        CustomAdapter adapter = new CustomAdapter(getApplicationContext(), datas);
        ListView listView = (ListView)findViewById(R.id.listView1);
        listView.setAdapter(adapter);
    }
}
