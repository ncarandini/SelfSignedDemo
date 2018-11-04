# SelfSignedDemo
A simple Xamarin Forms app to test browsing pages (via https) in a server with a self signed certificate.

Browsing a web page via https on a web browser produces a warnig of unsecure page, due to certificate failure. But on the Web browser we can choose to skip the warning and continue the navigation.

On a Xamarin Forms app (Android or iOS) this is not valid on the WebView, and the error on certificate varification process produces to odd effects:

1. The `WebView Navigatin` event is not invoked
2. The `WebView Navigated` event is correctly invoked but the `WebNavigatedEventArgs` instance has a `Result` value of `WebNavigationResult.Success`

For debug and test only purpose I've tried to enable the browsing of https requested pages tha came from a specific self-signed server.

In order to do that, I've followed the instructions found here: [Connecting mobile apps to backends for development with SSL](https://medium.com/@noumaan/ssl-app-dev-a2923d5113c6)

On my Android project, I've done the following changes:
- added `android:networkSecurityConfig="@xml/network_security_config"` property to the `application' tag in the AndroidManifest file:

        <?xml version="1.0" encoding="utf-8"?>
        <manifest ...>
    	    ...
    	    <application android:label="SelfSignedDemo.Android"
                         android:networkSecurityConfig="@xml/network_security_config">
            </application>
            ...
        </manifest>
- Added a xml folder to the Resources folder
- Added a network_security_config.xml file with the following code:

        <?xml version="1.0" encoding="utf-8"?>
        <network-security-config>
          <debug-overrides>
            <trust-anchors>
              <certificates src="user"/>
            </trust-anchors>
          </debug-overrides>
        </network-security-config>

## Issues
After those changes, the solution build just fine, but when debugging the app fails at `base.OnCreate(bundle);` instruction on the `MainActivity` overrided `OnCreate` method. When I remove those changes, everything works fine.
UPDATE: This happens only on Android Emulator, it runs fine on my physical androdid device.

## Environment
I'm using VS 2017 Preview (version 15.9.0 Preview 4.0)
