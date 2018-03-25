package com.example.pok.lab3_v2;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.RectF;
import android.os.Handler;
import android.support.annotation.Nullable;
import android.util.AttributeSet;
import android.view.MotionEvent;
import android.view.View;

/**
 * Created by pok on 17/3/2561.
 */

public class DrawableView extends View {

    Handler h;
    public DrawableView(Context context) {
        super(context);
    }

    public DrawableView(Context context, @Nullable AttributeSet attrs) {
        super(context, attrs);
        h = new Handler();
    }

    Runnable r = new Runnable() {
        @Override
        public void run() {
            invalidate();
        }
    };
    int x = 0;

    @Override
    protected void onDraw(Canvas canvas) {
        super.onDraw(canvas);
        Paint paint = new Paint();
        int whichColor = (int)(Math.random()*4);
        if(whichColor == 0) paint.setColor(Color.RED);
        else if(whichColor == 1) paint.setColor(Color.GREEN);
        else if(whichColor == 2) paint.setColor(Color.BLUE);
        else paint.setColor(Color.YELLOW);

        //canvas.scale(0.5f,0.5f,0,0);
        //canvas.rotate(30,0,0);
        canvas.save();
        //canvas.rotate(30,0,0);
        canvas.scale(5f,1f,50f,50f);
        canvas.drawRect(20,20,80,80,paint);
        canvas.restore();

        canvas.drawRect(20,100,80,160,paint);
        //x+=10;
        //h.postDelayed(r,50);

        // paint.setColor(Color.RED);

      /*  canvas.drawRect(40,20,90,80,paint);

        canvas.drawArc(50,300,200,400,0,90,true,paint);

        // draw Rectangle with corners at (40, 20) and (90, 80)
        canvas.drawRect(40, 20, 90, 80, paint); */



        // change the color
       /* paint.setColor(Color.BLUE);

        // set a shadow
        paint.setShadowLayer(10, 10, 10, Color.GREEN);
        this.setLayerType(View.LAYER_TYPE_SOFTWARE, null);

        // create a “bounding rectangle”
        RectF rect = new RectF(0, 0, 100, 100);

        // draw an oval in the bounding rectangle
        canvas.drawOval(rect, paint); */
    }

    @Override
    public boolean onTouchEvent(MotionEvent event) {

        if(event.getAction() == MotionEvent.ACTION_UP){

            float x = event.getX();
            float y = event.getY();

            if(x >= 40 && x <= 90 && y >= 20 && y <= 80){

                invalidate();

            }
        }

        return true; //super.onTouchEvent(event);
    }
}




/*
        c.drawArc(...); - draw a partial ellipse
        c.drawCircle(centerX, centerY, r, paint); - draw a circle
        c.drawLine(x1, y1, x2, y2, paint); - draw a line segment
        c.drawPoint(x, y, paint); - color a single pixel
        c.drawRect(x1, y1, x2, y2, paint); * (requires Android 5.0)
        c.drawRoundRect(x1, y1, x2, y2, rx, ry, paint); * (requires Android 5.0)
        c.drawText("str", x, y, paint); - draw a text string
        c.getWidth(), c.getHeight() - get dimensions of drawing are

*/


/*
setShadowLayer() is only supported on text when hardware acceleration is on.
Hardware acceleration is on by default when targetSdk=14 or higher.
An easy workaround is to put your View in a software layer:
myView.setLayerType(View.LAYER_TYPE_SOFTWARE, null).
 */