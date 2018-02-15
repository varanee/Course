package com.example.pok.lab3_v2;

import android.support.test.rule.ActivityTestRule;
import android.support.test.runner.AndroidJUnit4;

import org.junit.Rule;
import org.junit.Test;
import org.junit.runner.RunWith;

import static android.support.test.espresso.Espresso.onView;
import static android.support.test.espresso.action.ViewActions.click;
import static android.support.test.espresso.action.ViewActions.closeSoftKeyboard;
import static android.support.test.espresso.action.ViewActions.typeText;
import static android.support.test.espresso.assertion.ViewAssertions.matches;

import static android.support.test.espresso.matcher.ViewMatchers.withId;
import static android.support.test.espresso.matcher.ViewMatchers.withText;


/**
 * Created by pok on 14/2/2561.
 */

@RunWith(AndroidJUnit4.class)
public class Week5Test {

    @Rule
    public ActivityTestRule<Week5_Preference> mActivityRule =
            new ActivityTestRule<Week5_Preference>(Week5_Preference.class);

    @Test
    public void ensureHighScoreInput(){

        onView(withId(R.id.editText)).perform(typeText("123"),
                closeSoftKeyboard());

        onView(withId(R.id.button3)).perform(click());

    }

}
