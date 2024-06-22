#include <Wire.h>  // I2C haberleşme protokolünü kullanmak için Wire kütüphanesini dahil eder.
#include "servoMotorControls.h"  // Servo motor kontrol kütüphanesini projeye dahil eder.

#define led_pin_red 18  // Kırmızı LED'in pin numarasını 18 olarak tanımlar.
#define led_pin_green 19  // Yeşil LED'in pin numarasını 19 olarak tanımlar.

#define echoPin 2  // İlk mesafe sensörünün echo pini için pin numarasını 2 olarak tanımlar.
#define trigPin 4  // İlk mesafe sensörünün trig pini için pin numarasını 4 olarak tanımlar.

#define echoPin2 33  // İkinci mesafe sensörünün echo pini için pin numarasını 33 olarak tanımlar.
#define trigPin2 32  // İkinci mesafe sensörünün trig pini için pin numarasını 32 olarak tanımlar.

const byte servoPin = 12;  // Servo motorun bağlı olduğu pin numarasını 12 olarak tanımlar.

servoMotorControls myServoControl(servoPin);  // Servo motor kontrol nesnesini oluşturur ve servo pinini parametre olarak geçirir.

long duration, distance, duration2, distance2;  // Mesafe sensörlerinden alınan süre ve mesafe değerlerini saklamak için değişkenler tanımlar.
int notEmptyPlace = 3;  // Otoparkta dolu yer sayısını saklamak için bir değişken tanımlar ve başlangıç değerini 3 olarak atar.

unsigned long servoActivatedTime = 0;  // Servo motorun son aktive edildiği zamanı saklamak için bir değişken tanımlar ve başlangıç değerini sıfır olarak atar.
bool isServoActive = false;  // Servo motorun aktif olup olmadığını saklamak için bir bayrak tanımlar ve başlangıç değerini false olarak atar.
bool ArabaGirdiMi = false;  // Arabanın içeri girdiği kontrolünü saklamak için bir bayrak tanımlar ve başlangıç değerini false olarak atar.

unsigned long previousMillis = 0;  // Önceki millis süresini saklamak için bir değişken tanımlar ve başlangıç değerini sıfır olarak atar.
const long interval = 1000;  // Her bir okuma arasındaki zaman aralığını saklamak için bir değişken tanımlar ve başlangıç değerini 1000 (1 saniye) olarak atar.


void setup() {
  Serial.begin(9600);  // Seri haberleşmeyi başlatır ve baud hızını 9600 olarak ayarlar.
  pinMode(led_pin_red, OUTPUT);  // Kırmızı LED pinini çıkış olarak ayarlar.
  pinMode(led_pin_green, OUTPUT);  // Yeşil LED pinini çıkış olarak ayarlar.
  pinMode(trigPin, OUTPUT);  // İlk mesafe sensörünün trig pinini çıkış olarak ayarlar.
  pinMode(echoPin, INPUT);  // İlk mesafe sensörünün echo pinini giriş olarak ayarlar.
  pinMode(trigPin2, OUTPUT);  // İkinci mesafe sensörünün trig pinini çıkış olarak ayarlar.
  pinMode(echoPin2, INPUT);  // İkinci mesafe sensörünün echo pinini giriş olarak ayarlar.
}

void loop() {
  unsigned long currentMillis = millis();  // Şu anki millis zamanını alır.

  if (notEmptyPlace > 5) {
    digitalWrite(led_pin_red, HIGH);  // Kırmızı LED'i yakar.
    digitalWrite(led_pin_green, LOW);  // Yeşil LED'i söndürür.
  } else {
    digitalWrite(led_pin_green, HIGH);  // Yeşil LED'i yakar.
    digitalWrite(led_pin_red, LOW);  // Kırmızı LED'i söndürür.
  }

  if (currentMillis - previousMillis >= interval) {
    previousMillis = currentMillis;  // Önceki millis zamanını günceller.

    // Mesafe sensörü 1
    digitalWrite(trigPin, LOW);
    delayMicroseconds(2);
    digitalWrite(trigPin, HIGH);
    delayMicroseconds(10);
    digitalWrite(trigPin, LOW);
    duration = pulseIn(echoPin, HIGH);
    distance = duration / 58.2;

    // Mesafe sensörü 2
    digitalWrite(trigPin2, LOW);
    delayMicroseconds(2);
    digitalWrite(trigPin2, HIGH);
    delayMicroseconds(10);
    digitalWrite(trigPin2, LOW);
    duration2 = pulseIn(echoPin2, HIGH);
    distance2 = duration2 / 58.2;
  }

  // Seri porttan veri okunur
  if (Serial.available() > 0) {
    String data = Serial.readStringUntil('\n');  // Seri porttan gelen veriyi okur.
    Serial.println(data);  // Okunan veriyi seri porta yazdırır.
    int delimiterIndex = data.indexOf(':');  // Veri içerisindeki ":" karakterinin indeksini bulur.
    if (delimiterIndex != -1) {
      String valueStr = data.substring(delimiterIndex + 1);  // ":" karakterinden sonraki kısmı alır.
      notEmptyPlace = valueStr.toInt();  // Alınan kısmı integer'a dönüştürür ve dolu park yeri sayısını günceller.
    } else {
      Serial.println("Delimiter not found!");  // Eğer ":" karakteri bulunamazsa hata mesajı yazdırır.
    }
  }

  // Servo kontrolü
  if (distance < 7 && notEmptyPlace < 6 && !isServoActive) {
    myServoControl.servoTurn(180);  // Servo motoru 180 derece döndürür.
    servoActivatedTime = currentMillis;  // Servo motorun aktive edildiği zamanı kaydeder.
    isServoActive = true;  // Servo motorun aktif olduğunu işaretler.
    ArabaGirdiMi = false;  // Arabanın içeri girdiği kontrolünü sıfırlar.
  }

  // Servo motoru 5 saniye sonra kapar
  if (isServoActive && currentMillis - servoActivatedTime >= 5000) {
    myServoControl.servoTurn(90);  // Servo motoru 90 derece döndürür.
    isServoActive = false;  // Servo motorunun aktif olduğunu işaretler.
  }

  if (distance2 > 19 && distance2 < 24 && isServoActive && !ArabaGirdiMi) {
    ArabaGirdiMi = true;  // Arabanın içeri girdiğini işaretler.
    Serial.println("1");  // Seri porta "1" yazdırır.
    while (distance2 > 19 && distance2 < 24) {
      digitalWrite(trigPin2, LOW);
      delayMicroseconds(2);
      digitalWrite(trigPin2, HIGH);
      delayMicroseconds(10);
      digitalWrite(trigPin2, LOW);
      duration2 = pulseIn(echoPin2, HIGH);
      distance2 = duration2 / 58.2;
    }
  } else if (distance2 < 8) {
    Serial.println("2");  // Seri porta "2" yazdırır.
    while (distance2 < 8) {
      digitalWrite(trigPin2, LOW);
      delayMicroseconds(2);
      digitalWrite(trigPin2, HIGH);
      delayMicroseconds(10);
      digitalWrite(trigPin2, LOW);
      duration2 = pulseIn(echoPin2, HIGH);
      distance2 = duration2 / 58.2;
    }
   
  }
}
