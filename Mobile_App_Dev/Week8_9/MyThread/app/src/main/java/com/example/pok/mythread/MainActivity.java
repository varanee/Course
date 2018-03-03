package com.example.pok.mythread;


import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.TextView;

import java.net.URL;

public class MainActivity extends AppCompatActivity {

    Button runBtn;
    TextView tv1;
    Button runBtn2;
    TextView tv2;
    String outputStr = "";

    //3
    ProgressBar pb;

    //4
    ImageView imv;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        runBtn = (Button)findViewById(R.id.button1);
        tv1    = (TextView)findViewById(R.id.tv1);

        runBtn2 = (Button)findViewById(R.id.button2);
        tv2    = (TextView)findViewById(R.id.tv2);

        //3
        pb = (ProgressBar)findViewById(R.id.progressBar);

        //4
        imv = (ImageView)findViewById(R.id.imageView);

        //imv.setImageResource(R.drawable.mario);
        //Picasso.with(this).load("http://i.imgur.com/DvpvklR.png").placeholder(R.drawable.mario).into(imv);
        //Picasso.with(this).setLoggingEnabled(true);


        runBtn2.setOnClickListener(new View.OnClickListener() {
                                       @Override
                                       public void onClick(View v) {
                                           tv2.setText("Press");
                                       }
                                   });


               runBtn.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {

                        //1 show sequential
                  /*      outputStr += "\n";
                        for (int i = 0; i < 5000000; i++) {
                            outputStr = "A";
                        }

                        for (int i = 0; i < 5000000; i++) {
                            outputStr = "B";
                        }

                        tv1.setText(outputStr); */


                        //2. show thread, e.g., load image A & B (B not neet to wait A to finish)

  /*              outputStr += "\n";
                new Thread(new Runnable(){
                    public void run() {
                        for (int i = 0; i < 5000; i++){
                            outputStr ="A";
                        }
                    }
                }).start();

                new Thread(new Runnable(){
                    public void run(){
                        for (int i = 0; i < 5000000; i++){
                            outputStr ="B";
                        }
                    }
                }).start();
                tv1.setText(outputStr);
*/



                // 3. Access UI thread from outside the UI.
                // How to update textbox value in delay
                new Thread(new Runnable(){
                    public void run(){
                        for (int i = 1; i <= 100; i++) {
                            final int value = i;

                            runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    tv1.setText(""+value);
                                    pb.setProgress(value);
                                }
                            });

                            try {
                                Thread.sleep(1000);
                            } catch (InterruptedException e) {
                                e.printStackTrace();
                            }
                        }
                    }
                }).start();


               //4. Add progressbar
               /*new Thread(new Runnable(){
                    public void run(){
                        for (int i = 0; i <= 10; i++) {
                            final int value = i*10;
                            doFakeWork();
                            pb.post(new Runnable() {
                                @Override
                                public void run() {
                                    tv1.setText("Updating");
                                    pb.setProgress(value);
                                }
                            });
                        }
                    }
                }).start();
                */


                //5. Basic Asynctask
                //6. Image
                //new LoadImageTask("http://i.imgur.com/DvpvklR.png",pb).execute(imv);
                //new LoadImageTask("https://worldstrides.com/wp-content/uploads/2015/07/12-Chureito-pagoda-and-Mount-Fuji-Japan.jpg",pb).execute(imv);
            new Thread(new Runnable(){
                public void run(){
                    try {

                        URL newurl = new URL("https://api.learn2crack.com/android/images/donut.png");
                        final Bitmap bitmap =
                            BitmapFactory.decodeStream(newurl.openConnection().getInputStream());

                        imv.post(new Runnable() {
                            public void run() {

                                imv.setImageBitmap(bitmap);
                            }
                        });

                    }catch(Exception e){ }
                }
            }).start();

            }
        });
    }

    //3
    private void doFakeWork() {
        try {
            Thread.sleep(1000);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }
}

