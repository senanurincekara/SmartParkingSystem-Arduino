// servoMotorControls.h
#ifndef SERVO_MOTOR_CONTROLS_H
#define SERVO_MOTOR_CONTROLS_H

#include <ESP32Servo.h>

class servoMotorControls {
  public:
    servoMotorControls(byte servoPin);
    void servoTurn(int degree);

  private:
    Servo myServo;
    byte _servoPin;
};

#endif
