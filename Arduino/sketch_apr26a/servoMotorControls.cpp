// servoMotorControls.cpp
#include "servoMotorControls.h"

servoMotorControls::servoMotorControls(byte servoPin) {
  _servoPin = servoPin;
  myServo.attach(servoPin);
}

void servoMotorControls::servoTurn(int degree) {
  myServo.write(degree);
}
