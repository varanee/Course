package com.example.pok.mythread;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.widget.ImageView;
import android.widget.ProgressBar;

import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;


public class LoadImageTask extends AsyncTask<String, Integer, Bitmap> {

   private ImageView _imgv = null;
   private ProgressBar _pb = null;

    public LoadImageTask(ImageView imgv, ProgressBar pb){
        _imgv = imgv;
        _pb = pb;
    }

    @Override
    protected Bitmap doInBackground(String... params) {
        Bitmap bmp = null;
        ByteArrayOutputStream byteBuffer = new ByteArrayOutputStream();
        try
        {
            URL url = new URL(params[0]);
            HttpURLConnection con = (HttpURLConnection)url.openConnection();
            int imgSize = con.getContentLength();
            InputStream is = con.getInputStream();
            byte[] buffer = new byte[1024];
            int len = 0;
            int sum = 0;
            while ((len = is.read(buffer)) > 0)//)
            {
                byteBuffer.write(buffer,0,len);
                sum += len;
                float percent = (sum*100.0f)/imgSize;
                publishProgress((int)percent);
            }
           // bmp =  BitmapFactory.decodeStream(url.openConnection().getInputStream());
           bmp = BitmapFactory.decodeByteArray(byteBuffer.toByteArray(),0,byteBuffer.size());

        }catch(Exception e){}
        return bmp;
    }

    protected void onProgressUpdate(Integer... progress) {

        _pb.setProgress(progress[0]);
    }

    @Override
    protected void onPostExecute(Bitmap result) {
        _imgv.setImageBitmap(result);

    }
}