package com.example.pok.lab3_v2;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.widget.Toast;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by pok on 24/2/2561.
 */

public class DatabaseHandler extends SQLiteOpenHelper {

    private static final String DATABASE_NAME = "contactsManager";
    private static final int DATABASE_VERSION = 3;
    private static final String TABLE_CONTACTS = "contacts";
    // Contacts Table Columns names
    private static final String KEY_ID = "id";
    private static final String KEY_NAME = "name";
    private static final String KEY_PH_NO = "phone_number";
    private static final String KEY_IMG_PATH = "image_path";
    SQLiteDatabase db;

    Context _context;

    public DatabaseHandler(Context context){
        super(context,DATABASE_NAME, null, DATABASE_VERSION);
        db = getWritableDatabase();
        _context = context;
    }

    //If db exist, it not create.
    @Override
    public void onCreate(SQLiteDatabase db)
    {
        String CREATE_CONTACTS_TABLE = "CREATE TABLE " + TABLE_CONTACTS + " ("
                + KEY_ID + " INTEGER PRIMARY KEY,"
                + KEY_NAME + " TEXT NOT NULL UNIQUE,"
                + KEY_PH_NO + " TEXT NOT NULL UNIQUE" + ")";
        db.execSQL(CREATE_CONTACTS_TABLE);
    }

    private static final String DATABASE_ALTER_CONTACT_1 = "ALTER TABLE "
            + TABLE_CONTACTS + " ADD COLUMN " + KEY_IMG_PATH + " TEXT;";

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
    {
        if (oldVersion < 3) {
            db.execSQL(DATABASE_ALTER_CONTACT_1);
        }
    }

    public void addContact(Contact contact)
    {
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put(KEY_NAME, contact._name);
        values.put(KEY_PH_NO,contact._phone_number);
        long insert = db.insert(TABLE_CONTACTS, null, values);
        db.close();
    }

    public void updateOrInsert(Contact contact){

        SQLiteDatabase db = this.getWritableDatabase();

        ContentValues values = new ContentValues();
        values.put(KEY_NAME, contact._name);
        values.put(KEY_PH_NO,contact._phone_number);
        values.put(KEY_IMG_PATH,contact._img_path);

        String queryStr = KEY_NAME + " = '" + contact._name + "' AND "+
                KEY_PH_NO+" = '"+contact._phone_number +"' AND "+
                KEY_IMG_PATH+" = '"+contact._img_path+"'";

        int rows = db.update(TABLE_CONTACTS, values, queryStr, null);

        String toastStr = "";

        if(rows == 0)
        {
            long insert = db.insert(TABLE_CONTACTS, null, values);

            if (insert != -1)
            {
                toastStr = "Inserted data already";
            }
            else
            {
                toastStr = "Error to insert data";
            }
        }
        else
        {
            toastStr = "Data already exist";
        }

        Toast.makeText(_context,toastStr, Toast.LENGTH_LONG).show();

        db.close();
    }

    public List<Contact> getAllContacts()
    {
        List<Contact> contactList = new ArrayList<Contact>();
        String selectQuery = "SELECT * FROM " + TABLE_CONTACTS;
        SQLiteDatabase db = this.getWritableDatabase();
        Cursor cursor = db.rawQuery(selectQuery, null);
        // looping through all rows and adding to list
        if (cursor.moveToFirst()) {
            do {
                Contact contact = new Contact();
                contact._id = Integer.parseInt(cursor.getString(0));
                contact._name = cursor.getString(1);
                contact._phone_number = cursor.getString(2);
                contact._img_path = cursor.getString(3);
                contactList.add(contact);
            } while (cursor.moveToNext());
        }
        // return contact list
        return contactList;
    }

    public Contact getTheContactFromName(String name)
    {
        String selectQuery = "SELECT * FROM " + TABLE_CONTACTS + " WHERE "+
                KEY_NAME + " = '" + name + "'";

        SQLiteDatabase db = this.getWritableDatabase();
        Cursor cursor = db.rawQuery(selectQuery, null);

        Contact _contact = new Contact();
        if (cursor.moveToFirst()) {
            do {
                _contact._id = Integer.parseInt(cursor.getString(0));
                _contact._name = cursor.getString(1);
                _contact._phone_number = cursor.getString(2);
            } while (cursor.moveToNext());
        }

        //Don't close it yet
        //db.close();

        return _contact;
    }
}
