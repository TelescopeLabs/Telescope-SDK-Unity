
# Install Telescope SDK

To integrate Telescope Labs analytics SDK, you can use one of the two methods: by using the _Unity Package Manager_ (recommended) or by manually importing the _unitypackage_.

## Unity Package Manager

1. Open the Package Manager (Window → Package Manager), click + in the top left corner and select _Add package from git URL_.    
  ![](https://drive.google.com/uc?id=1k3Tvk3UM65eZKxoCsjkU4q3uwYhyCBjb)
2. Copy the repository URL <https://github.com/telescopelabs/sdk-unity.git> to the input box and click _Add_.  
   ![](https://drive.google.com/uc?id=1ezybSCFZrlrywK5zNEmLjZa7i5xmpbCV)

3. Wait for the Unity Package Manager to download the package.

## Import Unity Package

1. Download Telescoplabs.unitypackage from [link](link).
2. In the Unity Editor menu, open Assets → Import Package → Custom Package.
3. Select the _Telescopelabs.unitypackage_ that you have downloaded.

# Initializing SDK

You don't need to manually initialize the sdk. The SDK generates a game object called _Telescope_ once game started.

## SDK Settings

1. You will need your game api key for initializing your library. You can get your api key from [link](link).
2. To initialize the library, first open the unity project settings menu for Telescope. (Edit → Project Settings → Telescope) Then, enter your game api key into the _Api Key_ input fields within the inspector.  
   ![](https://drive.google.com/uc?id=1GJGJ23UKfZUegirwphz6SjtabEg4Zp5x)

After these steps are done, you are ready to view your game data.

> 🚧 Warning
> 
> The SDK won't send game data if you don't enter an api key. However, It will store events and send data when enabled.

# SDK Features

## Show Debug

It is a feature in project settings menu for Telescope. It will print all logs coming from the SDK to console with prefix _[Telescope]_ if enabled.

## Enabled

It is a feature in project settings menu for Telescope. The SDK won't send game data if disabled. However, It will store events and send data when enabled.

## Flush Interval
The SDK uses the flush interval to determine when to send buffered events to server. Its default value is 60 seconds. You may need to set a appropriate timeout value that fits your game or application's use case, to avoid ending sessions prematurely.
> 🚧 Warning
> 
> You can't set less than 15 seconds or bigger than 300 seconds due to optimization.

## Session Timeout

The SDK uses the timeout to determine when to end a session. If there has been a period of inactivity, such as the game running in the background or paused for too long, the SDK will end the session. Its default value is 10 minutes and minimum value is 60 seconds. You may need to set a appropriate timeout value that fits your game or application's use case, to avoid ending sessions prematurely.

> 📘 Info
> 
> This feature will be added in the next update.

## Custom User ID

- The SDK will automatically generate a user id.
- It is useful to set a custom user id when you match the users with collected data from different sources.

If you want to use custom user id, you should call the SetCustomUserID method and pass your desired user id as parameter. This will ensure that all events including the auto-triggered events will use the custom user id that you have set. Here's the code to set a custom user id:

```csharp
Telescope.SetCustomUserID("my_custom_user_id");
```

This will set "my_custom_user_id" as the user id for all events including the auto-triggered events.

> 📖 Note
> 
> You have to ensure that the SetCustomUserId is called in awake method. The SDK will be initialized and will generate a user id with some events between awake and start methods.

## Buffering

The SDK has a buffer structure that allows it to collect and store events locally before sending them to the server. The SDK periodically sends the buffered events to the server. If for any reason the SDK is unable to send the events to the server, it will retain the buffered events and try to send them again later.

This buffer structure serves several important purposes:

- **Reliability**: By buffering events locally before sending them to the server, the SDK ensures that events are not lost if the server is unavailable or if there is a temporary network issue. This improves the reliability of event tracking and ensures that all events are eventually sent to the server.
- **Performance**: Sending events to the server one at a time can be resource-intensive, especially if the application generates a large number of events. By buffering events locally, the SDK can send them to the server in batches, which improves the performance and reduces the load on both the client and the server.
- **Network efficiency**: If a device is in an area with weak or unstable network connection, sending events one by one would be problematic, as it would take too long for the events to reach the server. By buffering events, the SDK can wait for a stable connection and send events in a batch, reducing the chances of data loss and increasing network efficiency.
- **Data Integrity**: By buffering events, the SDK can ensure that the data is not lost if the application crashes or if there is a problem with the device before the data is sent. This ensures the integrity of the data being sent to the server.

Overall, the buffer structure is an important feature of the SDK that improves the reliability, performance, network efficiency, and data integrity of event tracking.

## Session Handling

The SDK includes a session handling feature that allows it to track and manage the lifecycle of a user's session within the game. The feature includes several key components:

- **Session Start Event**: When the game is first started, the SDK immediately sends a "Session Start" event to the server. This event includes information about the start of the session, such as the user ID, session ID, and timestamp.
- **Game Running Event**: The SDK sends a "Game Running" event periodically, such as a heartbeat. This event is used to detect if a session has reached a timeout and therefore needs to end. It also helps to detect if the game has crashed, and end the session accordingly.
- **Timeout Detection**: The SDK checks session periodically to detect if a session has reached a timeout. If there has been a period of inactivity, such as the game running in the background or paused for too long, the SDK will end the session.
- **End Session Event**: When the session ends, the SDK sends an "End Session" event to the server. This event includes information about the end of the session, such as the user ID, session ID, and timestamp.

This session handling feature is useful for tracking user engagement and understanding how users interact with the game. By sending session start and end events, the SDK allows you to track session duration and analyze user behavior within the game. The "Game Running" event serves as a heartbeat, allowing you to detect if a session has reached a timeout or if the game has crashed, and end the session accordingly.

Events related to session are all internal, auto-triggered events, which are automatically sent by the SDK to the server at the appropriate times.

> 📖 Note
> 
> It's important to note that the SDK uses the timeout to determine when to end a session. It's important to set a appropriate timeout value that fits your game or application's use case, to avoid ending sessions prematurely.

## Network Status
When the SDK detects that there is no network connection, it will continue to store events locally in a buffer. Once a connection is re-established, the SDK will send the buffered events to the server. This feature improves the reliability of event tracking, as it ensures that all events are eventually sent to the server, even if the player is in an area with poor or unstable network connectivity.

# Event Tracking

The SDK provides a Telescope class with several static methods that developers can use to track events and send values.

> 📘 Info
> 
> Note that the SDK adds tracks to the buffer and sends them as a batch with intervals you set _Flush Interval_ in Telescope Settings.

## Generic Track

```csharp
Track(string entityName, Dictionary<string,  object> value)
```



method is a convenient way for developers to track events with specific entity name and a dictionary of values. The method takes in two arguments:

- `entityName` is a string that represents the name of the event that is being tracked.
- `value` is a dictionary containing key-value pairs of the data that you want to track. The keys are strings and the values can be of any type that netwonsoft.json can [serialize](https://www.newtonsoft.com/json/help/html/SerializationGuide.htm).

This method creates an instance of the _TelescopeGenericTrack_ class and populate it with the provided _entityName_ (event name) and _value_ and then call the Track method to send the event to the server.  
Here's an example of how to use this method:

```csharp
Dictionary<string,  object> values = new Dictionary<string,  object>();
values.Add("score", 500);
values.Add("level", 3);
Telescope.Track("level_complete", values);
```



This will track an event named "level_complete" and send the values of score:500 and level:3 to the server.

By using this method, you can easily track events with entity name and values you defined.  
Also, you can use any of these two methods:

```csharp
//This method is used to track a single TelescopeGenericTrack object.
Track(TelescopeGenericTrack tgt)
//This method is used to track a list of TelescopeGenericTrack objects.
Track(List<TelescopeGenericTrack> tgts)
```



> 🚧 Warning
> 
> Don't forget to do mapping in _Data Settings_ after you send an event for the first time. You can use preset events if you don't want to do the mapping.

## Events

### AdImpressions
```csharp
Telescope.AdImpressions();
```
### IngamePurchases
```csharp
Telescope.IngamePurchases();
```
### LevelUps
```csharp
Telescope.LevelUps();
```
### LevelUpsCurrencies
```csharp
Telescope.LevelUpsCurrencies();
```
### Payments
```csharp
Telescope.Payments();
```
### Progressions
```csharp
Telescope.Progressions();
```
### PushClicked
```csharp
Telescope.PushClicked();
```
### PushSent
```csharp
Telescope.PushSent();
```
### Subscriptions
```csharp
Telescope.Subscriptions();
```
### Tutorials
```csharp
Telescope.Tutorials();
```
### CustomOffChainEvents
```csharp
Telescope.CustomOffChainEvents();
```