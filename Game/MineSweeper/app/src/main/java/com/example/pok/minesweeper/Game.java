package com.example.pok.minesweeper;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.view.MotionEvent;
import android.widget.Toast;

/**
 * Created by IST-MUT on 2/28/2018.
 */

public class Game {
    private GameView gameView;
    public Board gameBoard;
    private Bitmap hudSpriteSheet;
    private int boardSize;
    private int bombCount;
    private boolean isGameOver;
    private int score;
    public HUD[] hud;

    public Game(GameView gameView, int boardSize, int bombCount) {
        this.gameView = gameView;
        this.boardSize = boardSize;
        this.bombCount = bombCount;
        this.gameBoard = new Board(gameView, boardSize, bombCount);
        this.hud = new HUD[3];
        this.hudSpriteSheet = BitmapFactory.decodeResource(this.gameView.context.getResources(), R.drawable.hud_spritesheet_md);
        this.hud[0] = new HUD(this.gameView, this.hudSpriteSheet, 0, 0);
        this.hud[1] = new HUD(this.gameView, this.hudSpriteSheet, 160, 0);
        this.hud[2] = new HUD(this.gameView, this.hudSpriteSheet, 80, 40);
    }

    public void start() {
        this.isGameOver = false;
        this.score = 0;
        this.gameBoard.reset();
    }

    public void cheat() {
        outerLoop:
        for(int i = 0; i < this.boardSize; i++) {
            for(int j = 0; j < this.boardSize; j++) {
                if(!this.gameBoard.grid[i][j].isRevealed && this.gameBoard.grid[i][j].isBomb) {
                    this.gameBoard.grid[i][j].isCheat = true;
                    this.gameBoard.grid[i][j].reveal();
                    break outerLoop;
                }
            }
        }
    }

    public void gameOver() {
        this.isGameOver = true;
        for(int i = 0; i < this.boardSize; i++) {
            for(int j = 0; j < this.boardSize; j++) {
                this.gameBoard.showBombs(this.gameBoard.grid[i][j]);
            }
        }
        Toast.makeText(this.gameView.context, "Game over!", Toast.LENGTH_LONG).show();
    }

    public void gameFinished() {
        this.isGameOver = true;
        Toast.makeText(this.gameView.context, "You've beat the game!", Toast.LENGTH_LONG).show();
    }

    public void validate() {
        if(this.score == (this.boardSize * this.boardSize) - this.bombCount) {
            this.gameFinished();
        } else {
            this.gameOver();
        }
    }

    public void draw(Canvas canvas) {
        this.hud[0].onDraw(canvas, 0, 0);
        this.hud[1].onDraw(canvas, 0, 1);
        this.hud[2].onDraw(canvas, 0, 2);
    }

    public void registerTouch(MotionEvent event) {
        if(this.hud[0].hasCollided(event.getX(), event.getY())) {
            this.start();
        }
        if(this.hud[1].hasCollided(event.getX(), event.getY())) {
            this.cheat();
        }
        if(this.hud[2].hasCollided(event.getX(), event.getY())) {
            this.validate();
        }
        if(!this.isGameOver) {
            for(int i = 0; i < this.boardSize; i++) {
                for(int j = 0; j < this.boardSize; j++) {
                    if(this.gameBoard.grid[i][j].hasCollided(event.getX(), event.getY())) {
                        if(this.gameBoard.reveal(this.gameBoard.grid[i][j])) {
                            this.gameOver();
                        } else {
                            this.score = this.gameBoard.getRevealedCount();
                            if(this.score == (this.boardSize * this.boardSize) - this.bombCount) {
                                this.gameFinished();
                            }
                        }
                        break;
                    }
                }
            }
        }
    }
}
