package com.example.pok.lab3_v2;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.support.annotation.NonNull;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.DisplayMetrics;
import android.widget.ImageView;

import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.firebase.storage.FirebaseStorage;
import com.google.firebase.storage.StorageReference;

import java.io.InputStream;

public class FireBaseActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_fire_base);


        StorageReference gsReference = FirebaseStorage.getInstance().getReferenceFromUrl("gs://lab3v2-2fcb2.appspot.com/aj-pok.jpg");

        // ImageView in your Activity
        //ImageView imageView = (ImageView)findViewById(R.id.imageView);
        final ImageView imgViewer = (ImageView) findViewById(R.id.imageView);

        final long ONE_MEGABYTE = 1024 * 1024;

        gsReference.getBytes(ONE_MEGABYTE).addOnSuccessListener(new OnSuccessListener<byte[]>() {
            @Override
            public void onSuccess(byte[] bytes) {

                Bitmap bm = BitmapFactory.decodeByteArray(bytes, 0, bytes.length);
             /*   DisplayMetrics dm = new DisplayMetrics();
                getWindowManager().getDefaultDisplay().getMetrics(dm);

                imgViewer.setMinimumHeight(dm.heightPixels);
                imgViewer.setMinimumWidth(dm.widthPixels); */
                imgViewer.setScaleX(1f);
                imgViewer.setScaleY(1f);
                imgViewer.setImageBitmap(bm);

            }
        }).addOnFailureListener(new OnFailureListener() {
            @Override
            public void onFailure(@NonNull Exception exception) {
                // Handle any errors
                String success = "";
            }
        });


    }
}

