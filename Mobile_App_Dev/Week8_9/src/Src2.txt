package com.example.pok.thread;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.widget.ImageView;
import android.widget.ProgressBar;

import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;

/**
 * Created by pok on 1/11/2017.
 */

public class LoadImageTask extends AsyncTask<ImageView, Integer, Bitmap> {

    ImageView imageView = null;
    ProgressBar _pb = null;
    String _url = "";

    public LoadImageTask(String url, ProgressBar pb){
        _url = url;
        _pb = pb;
    }

    @Override
    protected Bitmap doInBackground(ImageView... imageViews) {
        this.imageView = imageViews[0];
        Bitmap bmp =null;
        ByteArrayOutputStream byteBuffer = new ByteArrayOutputStream();
        try{
            URL ulrn = new URL(_url);
            HttpURLConnection con = (HttpURLConnection)ulrn.openConnection();
            InputStream is = con.getInputStream();

            int imgSize = con.getContentLength();

            //Progress update
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
            //end update

           // bmp = BitmapFactory.decodeStream(is);
           bmp = BitmapFactory.decodeByteArray(byteBuffer.toByteArray(),0,byteBuffer.size());
            if (null != bmp)
                return bmp;

        }catch(Exception e){}
        return bmp;
    }

    protected void onProgressUpdate(Integer... progress) {

        _pb.setProgress(progress[0]);
    }

    @Override
    protected void onPostExecute(Bitmap result) {
        imageView.setImageBitmap(result);
    }
}
