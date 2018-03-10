package com.example.pok.lab3_v2;

import android.content.Intent;
import android.database.Cursor;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.provider.MediaStore;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;

public class Week8_DBAdd extends AppCompatActivity {

    private static int RESULT_LOAD_IMAGE = 1;

    Button insertBtn;
    Button browseImgBtn;
    EditText nameEdt;
    EditText phoneEdt;
    TextView imgPathTxt;
    String imgPath = "";


    DatabaseHandler db;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_week8__dbadd);


        insertBtn = (Button)findViewById(R.id.insertBtn);
        browseImgBtn = (Button)findViewById(R.id.browseImgBtnID);
        nameEdt = (EditText)findViewById(R.id.nameEdt);
        phoneEdt = (EditText)findViewById(R.id.phoneEdt);
        imgPathTxt = (TextView)findViewById(R.id.imgTxtID);

        //Create DB and Table
        db = new DatabaseHandler(this);

        insertBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                Contact person = new Contact();
                person._name = nameEdt.getText().toString();
                person._phone_number = phoneEdt.getText().toString();
                person._img_path = imgPath;

                db.updateOrInsert(person);
            }
        });

        browseImgBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v)
            {
                Intent i = new Intent(
                        Intent.ACTION_PICK,
                        android.provider.MediaStore.Images.Media.EXTERNAL_CONTENT_URI
                );

                startActivityForResult(i, RESULT_LOAD_IMAGE);
            }
        });
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);

        if (requestCode == RESULT_LOAD_IMAGE && resultCode == RESULT_OK && null != data) {

            Uri selectedImage = data.getData();

            String[] filePathColumn = { MediaStore.Images.Media.DATA };

            Cursor cursor = getContentResolver().query(selectedImage,
                    filePathColumn, null, null, null);
            cursor.moveToFirst();

            int columnIndex = cursor.getColumnIndex(filePathColumn[0]);

            //picturePath is the path to save in DB
            imgPath = cursor.getString(columnIndex);
            imgPathTxt.setText(imgPathTxt.getText() + imgPath);
            cursor.close();

            ImageView imageView = (ImageView) findViewById(R.id.imageView3);
            imageView.setImageBitmap(BitmapFactory.decodeFile(imgPath));

        }
    }
}
