package com.example.pok.minesweeper;

import android.graphics.*;

/**
 * Created by IST-MUT on 2/28/2018.
 */

public class Sprite {

    private int x, y;
    private GameView gameView;
    private Bitmap sheet;
    private int width, height;

    public Sprite(GameView gameView, Bitmap bmp, int x, int y, int columns, int rows) {
        this.x = x;
        this.y = y;
        this.gameView = gameView;
        this.sheet = bmp;
        this.width = bmp.getWidth()/columns;
        this.height = bmp.getHeight()/rows;
    }

    public void onDraw(Canvas canvas, int spriteColumn, int spriteRow){
        Rect src = new Rect((spriteColumn * this.width), (spriteRow * this.height), (spriteColumn * this.width) + width, (spriteRow * this.height) + height);
        Rect dst = new Rect(this.x, this.y, this.x + this.width, this.y + this.height);
        canvas.drawBitmap(this.sheet, src, dst, null);
    }

    public boolean hasCollided(float otherX, float otherY) {
        return this.x < otherX && this.y < otherY && this.x + this.width > otherX && this.y + this.height > otherY;
    }

    public void setX(int x) {
        this.x = x;
    }

    public void setY(int y) {
        this.y = y;
    }

    public int getX() {
        return this.x;
    }

    public int getY() {
        return this.y;
    }

    public int getWidth() {
        return this.width;
    }

    public int getHeight() {
        return this.height;
    }
}
