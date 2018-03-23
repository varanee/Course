#include "LedControl.h"
LedControl lc=LedControl(12,11,10,1); 

void setup()
{
  lc.shutdown(0,false);
  lc.setIntensity(0,8);
  lc.clearDisplay(0);
} 

void loop()
{
       
    lc.setLed(0,0,0,true); // turns on LED at col, row
    lc.setLed(0,0,1,true); // turns on LED at col, row
}
