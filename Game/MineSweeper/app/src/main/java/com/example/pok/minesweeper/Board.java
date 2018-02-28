package com.example.pok.minesweeper;

import android.graphics.*;
import android.content.*;
import android.view.*;
import java.util.*;

/**
 * Created by IST-MUT on 2/28/2018.
 */

public class Board {
    public Cell[][] grid;
    private GameView gameView;
    private Bitmap cellSpriteSheet;
    private int boardSize;
    private int bombCount;
    private int cellsRevealed;

    public Board(GameView gameView, int boardSize, int bombCount) {
        this.grid = new Cell[boardSize][boardSize];
        this.gameView = gameView;
        this.cellSpriteSheet = BitmapFactory.decodeResource(this.gameView.context.getResources(), R.drawable.cell_spritesheet_md);
        this.boardSize = boardSize;
        this.bombCount = bombCount;
    }

    public void draw(Canvas canvas) {
        for(int i = 0; i < this.grid.length; i++) {
            for(int j = 0; j < this.grid.length; j++) {
                if(this.grid[i][j].isRevealed) {
                    if(this.grid[i][j].isCheat) {
                        this.grid[i][j].onDraw(canvas, 2, 3);
                    } else if(this.grid[i][j].isBomb) {
                        this.grid[i][j].onDraw(canvas, 1, 0);
                    } else {
                        switch(this.grid[i][j].getBombNeighborCount()) {
                            case 0:
                                this.grid[i][j].onDraw(canvas, 2, 0);
                                break;
                            case 1:
                                this.grid[i][j].onDraw(canvas, 0, 1);
                                break;
                            case 2:
                                this.grid[i][j].onDraw(canvas, 1, 1);
                                break;
                            case 3:
                                this.grid[i][j].onDraw(canvas, 2, 1);
                                break;
                            case 4:
                                this.grid[i][j].onDraw(canvas, 0, 2);
                                break;
                            case 5:
                                this.grid[i][j].onDraw(canvas, 1, 2);
                                break;
                            case 6:
                                this.grid[i][j].onDraw(canvas, 2, 2);
                                break;
                            case 7:
                                this.grid[i][j].onDraw(canvas, 0, 3);
                                break;
                            case 8:
                                this.grid[i][j].onDraw(canvas, 1, 3);
                                break;
                            default:
                                this.grid[i][j].onDraw(canvas, 0, 0);
                                break;
                        }
                    }
                } else {
                    this.grid[i][j].onDraw(canvas, 0, 0);
                }
            }
        }
    }

    public void reset() {
        for(int i = 0; i < this.grid.length; i++) {
            for(int j = 0; j < this.grid.length; j++) {
                this.grid[i][j] = new Cell(this.gameView, this.cellSpriteSheet, i, j, false);
            }
        }
        this.shuffleBombs(this.bombCount);
        this.calculateCellNeighbors();
        this.setPositions();
        this.cellsRevealed = 0;
    }

    public void shuffleBombs(int bombCount) {
        boolean spotAvailable = true;
        Random random = new Random();
        int row;
        int column;
        for(int i = 0; i < bombCount; i++) {
            do {
                column = random.nextInt(8);
                row = random.nextInt(8);
                spotAvailable = this.grid[column][row].isBomb;
            } while (spotAvailable);
            this.grid[column][row].isBomb = true;
        }
    }

    public void calculateCellNeighbors() {
        for(int x = 0; x < this.grid.length; x++) {
            for(int y = 0; y < this.grid.length; y++) {
                for(int i = this.grid[x][y].getX() - 1; i <= this.grid[x][y].getX() + 1; i++) {
                    for(int j = this.grid[x][y].getY() - 1; j <= this.grid[x][y].getY() + 1; j++) {
                        if(i >= 0 && i < this.grid.length && j >= 0 && j < this.grid.length) {
                            this.grid[x][y].addNeighbor(this.grid[i][j]);
                        }
                    }
                }
            }
        }
    }

    public void setPositions() {
        int horizontalOffset = (320 - (this.boardSize * 25)) / 2;
        for(int i = 0; i < this.grid.length; i++) {
            for(int j = 0; j < this.grid.length; j++) {
                this.grid[i][j].setX(horizontalOffset + i * 25);
                this.grid[i][j].setY(90 + j * 25);
            }
        }
    }

    public boolean reveal(Cell c) {
        c.reveal();
        if(!c.isBomb) {
            this.cellsRevealed++;
            if(c.getBombNeighborCount() == 0) {
                ArrayList<Cell> neighbors = c.getNeighbors();
                for(int i = 0; i < neighbors.size(); i++) {
                    if(!neighbors.get(i).isRevealed) {
                        reveal(neighbors.get(i));
                    }
                }
            }
        }
        return c.isBomb;
    }

    public void showBombs(Cell c) {
        if(c.isBomb) {
            c.reveal();
        }
    }

    public int getRevealedCount() {
        return this.cellsRevealed;
    }
}
