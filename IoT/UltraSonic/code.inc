int trig = 3; //Trig
int echo = 2;   //Echo

 
void setup() {
  Serial.begin(9600);
  pinMode(13,OUTPUT);
}
 
void loop()
{
  long duration, cm;
 
  pinMode(trig, OUTPUT);
 

  digitalWrite(trig, LOW);
  delayMicroseconds(2);
  digitalWrite(trig, HIGH);
  delayMicroseconds(5);
  digitalWrite(trig, LOW);
  pinMode(echo, INPUT);
  duration = pulseIn(echo, HIGH);

  cm = microsecondsToCentimeters(duration);

  if(cm < 30)
    digitalWrite(13,HIGH);
  else
    digitalWrite(13,LOW);

  Serial.print(cm);
  Serial.print("cm");
  Serial.println();     
  delay(100);
}
 
long microsecondsToCentimeters(long microseconds)
{
  // The speed of sound is 340 m/s or 29 microseconds per centimeter.
  // The ping travels out and back, so to find the distance of the
  // object we take half of the distance travelled.
  return microseconds / 29 / 2;
}
