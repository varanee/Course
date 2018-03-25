#include <MicroGear.h> //Download library from Managed library
#include <ESP8266WiFi.h> //Follow instructions here https://netpie.gitbooks.io/nodemcu-esp8266-on-netpie/content/chapter1.html
#include <DHT.h> //Download library from Managed library

const char* ssid     = "true_home2G_306";
const char* password = "BEXAPNQR";

#define APPID   "PokSmartHome"

//Device keys
#define KEY     "ISCEQt5eYkKvM8g"
#define SECRET  "kBpkHBGIERfCUk1oFZ5sYGprg"

#define ALIAS   "NodeMCU1"
#define TargetWeb "HTML_web"

#define D4 2
#define DHTTYPE DHT11
#define DHTPIN D4
DHT dht(DHTPIN, DHTTYPE, 15);

WiFiClient client;
MicroGear microgear(client);

void onMsghandler(char *topic, uint8_t* msg, unsigned int msglen) 
{
    Serial.print("Incoming message --> ");
    msg[msglen] = '\0';
    Serial.println((char *)msg);
}

void onConnected(char *attribute, uint8_t* msg, unsigned int msglen) 
{
    Serial.println("Connected to NETPIE...");
    microgear.setAlias(ALIAS);
}

void setup() 
{
   /* Event listener */
    microgear.on(MESSAGE,onMsghandler);
    microgear.on(CONNECTED,onConnected);

    dht.begin();
    Serial.begin(115200);
    Serial.println("Starting...");

    WiFi.begin(ssid, password);
    while (WiFi.status() != WL_CONNECTED) 
    {
       delay(250);
       Serial.print(".");
    }

    Serial.println("WiFi connected");  
    Serial.println("IP address: ");
    Serial.println(WiFi.localIP());

    microgear.init(KEY,SECRET,ALIAS);
    microgear.connect(APPID);
}

void loop() 
{
   if (microgear.connected())
    {
       microgear.loop();
       Serial.println("connected");

       float Humidity = dht.readHumidity();
       float Temp = dht.readTemperature();  // Read temperature as Celsius (the default)
       String data = "/" + String(Humidity) + "/" + String(Temp);
       char msg[128];
       data.toCharArray(msg,data.length());
       Serial.println(msg);    

       microgear.chat(TargetWeb , msg);
    }
   else 
   {
    Serial.println("connection lost, reconnect...");
    microgear.connect(APPID);
   }
    delay(1500);
}

