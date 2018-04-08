package com.example.ekaratrattagan.myapp;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.ListView;
import android.widget.Toast;

import java.util.List;

public class Week8_DBActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_db);

        //Create DB and Table
        DatabaseHandler db = new DatabaseHandler(this);

        Contact person1 = new Contact();
        person1._name = "Ekarat2";
        person1._phone_number = "088123456";

        db.addContact(person1);

        Contact person2 = new Contact();
        person2._name = "AAA2";
        person2._phone_number = "08822222223";

        db.addContact(person2);


        List<Contact> contacts = db.getAllContacts();

        //Toast.makeText(this,contacts.get(1)._name,Toast.LENGTH_LONG).show();

        String[] datas = new String[contacts.size()];
        String[] pn    = new String[contacts.size()];
        for(int i=0; i<datas.length; i++)
        {
            datas[i]= contacts.get(i)._name;
            pn[i] = contacts.get(i)._phone_number;
        }

        CustomAdapter adapter = new CustomAdapter(getApplicationContext(),datas,pn);
        ListView listView = (ListView)findViewById(R.id.listView1);
        listView.setAdapter(adapter);
    }
}
