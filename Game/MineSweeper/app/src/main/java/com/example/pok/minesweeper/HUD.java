package com.example.pok.minesweeper;

import android.graphics.Bitmap;

/**
 * Created by IST-MUT on 2/28/2018.
 */

public class HUD extends Sprite {

    public HUD(GameView gameView, Bitmap spriteSheet, int x, int y){
        super(gameView,spriteSheet,x,y,1,3);
    }
}
