#include <OneWire.h>

#include <OneWire.h>
#include <DallasTemperature.h>
#define ONE_WIRE_BUS 2
 
OneWire oneWire(ONE_WIRE_BUS);
DallasTemperature sensors(&oneWire);
 
void setup(void)
{
  
  Serial.begin(9600);
  Serial.println("Dallas Temperature IC Control Library Demo");
  sensors.begin();
  pinMode(13,OUTPUT);
}
 
 
void loop(void)
{
  Serial.print(" Requesting temperatures...");
  sensors.requestTemperatures();
  Serial.println("DONE");

  Serial.print("Temperature for Device 1 is: ");
  int t = sensors.getTempCByIndex(0);
  Serial.print(t);
  if(t>29){
    digitalWrite(13,HIGH);
  }
  else{
    digitalWrite(13,LOW);
  }
  delay(2000);

}
