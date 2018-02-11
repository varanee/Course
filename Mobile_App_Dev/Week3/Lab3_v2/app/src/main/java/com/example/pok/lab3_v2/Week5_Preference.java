package com.example.pok.lab3_v2;

import android.content.Context;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AbsoluteLayout;
import android.widget.Button;
import android.widget.EditText;
import android.widget.NumberPicker;
import android.widget.Toast;

public class Week5_Preference extends AppCompatActivity {

    EditText inputEdt;
    Button chkScoreBtn;
    AbsoluteLayout layout;
    NumberPicker pickers;

    private static final String myPref = "myPref";
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_week5__preference);

        inputEdt = (EditText)findViewById(R.id.editText);
        chkScoreBtn = (Button)findViewById(R.id.button3);
        layout = (AbsoluteLayout)findViewById(R.id.layout);

        pickers = (NumberPicker)findViewById(R.id.numberPicker);

        final String[] arrayPicker= new String[]{"Red", "Blue", "Green", "Yellow", "Gray"};

        //set min value zero
        pickers.setMinValue(0);

        //set max value from length array string reduced 1
        pickers.setMaxValue(arrayPicker.length - 1);

        //implement array string to number picker
        pickers.setDisplayedValues(arrayPicker);

        //disable soft keyboard
        pickers.setDescendantFocusability(NumberPicker.FOCUS_BLOCK_DESCENDANTS);

        //set wrap true or false, try it you will know the difference
        pickers.setWrapSelectorWheel(false);

        pickers.setOnValueChangedListener(new NumberPicker.OnValueChangeListener() {
            @Override
            public void onValueChange(NumberPicker picker, int oldVal, int newVal) {
                //result.setText(arrayPicker[picker.getValue()]);
                String color = arrayPicker[picker.getValue()];
                if(color.equals("Red"))
                    layout.setBackgroundColor(Color.RED);
                else if(color.equals("Blue"))
                    layout.setBackgroundColor(Color.BLUE);
                else if(color.equals("Green"))
                    layout.setBackgroundColor(Color.GREEN);
                else if(color.equals("Yellow"))
                    layout.setBackgroundColor(Color.YELLOW);
                else if(color.equals("Gray"))
                    layout.setBackgroundColor(Color.GRAY);

            }
        });

        final SharedPreferences shared = getApplicationContext().
                getSharedPreferences(myPref, Context.MODE_PRIVATE);

        chkScoreBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                //Read high score
                int currentHighScore = shared.getInt("highScore",0);
                int newHighScore = Integer.parseInt(inputEdt.getText().toString());
                String output = "";

                if(currentHighScore > newHighScore ){
                    output = "No new high score";
                }
                else{
                    SharedPreferences.Editor editor = shared.edit();
                    editor.putInt("highScore",newHighScore);
                    editor.commit();

                    output = "high score = "+newHighScore;
                }

                Toast.makeText(getApplicationContext(),
                        output + Color.RED,
                        Toast.LENGTH_LONG).show();
            }
        });

    }
}
