package com.example.pok.mythread;


import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.TextView;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

public class MainActivity extends AppCompatActivity {

    Button runBtn;
    TextView tv1;
    Button runBtn2;
    TextView tv2;
    String outputStr = "";
    ProgressBar pb;
    ImageView imv;
    ByteArrayOutputStream byteBuffer = new ByteArrayOutputStream();
    int i = 1;
    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        runBtn = findViewById(R.id.button1);
        tv1 = findViewById(R.id.tv1);
        pb = findViewById(R.id.progressBar);
        imv = findViewById(R.id.imageView);

        //imv.setImageResource(R.drawable.mario);
        //Picasso.with(this).load("http://i.imgur.com/DvpvklR.png").placeholder(R.drawable.mario).into(imv);
        //Picasso.with(this).setLoggingEnabled(true);

        runBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                outputStr += "\n";





                //example1();
                //example2();
                //example3();
                //example4();
                example5();
                //example6();
                //example7();
                            }
        });
    }

    private void example1() {

        //1 show sequential

        for (int i = 0; i < 5; i++) {
            outputStr += "A";
        }

        for (int i = 0; i < 5; i++) {
            outputStr += "B";
        }

        tv1.setText(outputStr);
    }

    private void example7() {

        String img2 = "https://static.pexels.com/photos/103567/pexels-photo-103567.jpeg";
        new LoadImageTask(imv,pb).executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR,img2);
        // new LoadImageTask(imv,pb).execute(img2);

    }

    private void example6() {
        new MyTask(tv1).execute("My string parameter");
    }

    private void example5() {
        new Thread(new Runnable() {
            public void run() {
                try {
                    URL newurl = new URL("https://api.learn2crack.com/android/images/donut.png");
                    //final Bitmap bitmap = BitmapFactory.
                    //        decodeStream(newurl.openConnection().getInputStream());
                    HttpURLConnection con = (HttpURLConnection)newurl.openConnection();
                    InputStream is = con.getInputStream();
                    int imgSize = con.getContentLength();
                    byte[] buffer = new byte[1024];
                    int len = 0;
                    int sum = 0;
                    while ((len = is.read(buffer)) > 0)//)
                    {
                        byteBuffer.write(buffer,0,len);
                        sum += len;
                        float percent = (sum*100.0f)/imgSize;
                        pb.setProgress((int)percent);
                        //tv1.setText((int)percent);
                    }
                     //runOnUiThread();
                    imv.post(new Runnable() {
                        public void run() {
                            Bitmap bmp = BitmapFactory.decodeByteArray(byteBuffer.toByteArray(),0,byteBuffer.size());
                            imv.setImageBitmap(bmp);
                        }
                    });
                }
                catch (Exception e)
                {
                }

            }
        }).start();
    }

    private void example4() {
        //4. Add progressbar
        new Thread(new Runnable() {
            public void run() {
                for (int i = 0; i <= 100; i++) {
                    final int value = i;
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
    }

    private void example3() {
        // 3. Access UI thread from outside the UI.
        // How to update textbox value in delay
        new Thread(new Runnable() {
            public void run() {
                for (int i = 1; i <= 100; i++) {
                    final int value = i;

                    runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            tv1.setText("" + value);
                        }
                    });
                    doFakeWork();
                }
            }
        }).start();
    }

    private void example2() {
        //2. show thread, e.g., load image A & B (B not neet to wait A to finish)


        new Thread(new Runnable() {
            public void run() {
                for (int i = 0; i < 5; i++) {
                    outputStr += "A";
                }
            }
        }).start();

        new Thread(new Runnable() {
            public void run() {
                for (int i = 0; i < 5; i++) {
                    outputStr += "B";
                }
            }
        }).start();

        tv1.setText(outputStr);
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

