#include <Arduino.h>
const int btnPin = 13;
const int potPin = A0;
const int tmpPin = A1;
bool alarm;
void setup() {
  // put your setup code here, to run once:
  pinMode(btnPin,INPUT_PULLUP);
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  
  listen();
  delay(10);
}

void listen(){
  char recieveVal;
  if(Serial.available()> 0){
    recieveVal = Serial.read();

    if(recieveVal == '1'){
      sendValues();
    }
    else{
      //Serial.println("??");
    }
  }
}
void sendValues(){
  float voltage;
  bool btn = digitalRead(btnPin);
  int pot = analogRead(potPin);
  float temp = analogRead(tmpPin);
  voltage = (temp/1024)*5.0;
  temp = (voltage- .5)*100;
  Serial.print(temp);
  Serial.print(";");
  Serial.print(pot);
  Serial.print(";");
  Serial.println(btn);
}
