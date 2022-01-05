# TUIO Client for Unity


## Overview

A TUIO Client package to enable the easy creation of apps and games that interface with TUIO capable hardware and software for tangible input.

## Package contents

The Editor and Runtime folders contain key scripts for the Unity editor and runtime respectively. The Samples~ folder contains a simple reference implementation for TUIO 1.1 or TUIO 2.0 that visualizes the cursors and objects as coloured squares.

## Installation instructions

Open the Package Manager window and click the plus icon followed by "Add package from git URL...". Enter https://bitbucket.org/interactive-scape/tuio_client_unity.git and click "Add".

## Requirements

You will require a TUIO source, such as a Scape Tangible display connected to your device.

This package is built to run from Unity 2020.3 upwards.

## Limitations

TUIO 1.1 support is currently limited to 2Dobj, 2Dcur and 2Dblb messages. 25Dobj, 25Dcur, 25Dblb, 3Dobj, 3Dcur, 3Dblb and custom messages are all ignored.

TUIO 2.0 support is currently limited to FRM, ALV, TOK, PTR, BND and SYM messages. T3D, P3D, B3D, CHG, OCG, ICG, SKG, S3D, SVG, ARG, RAW, CTL, DAT, SIG, ALA, COA, LIA, LLA, LTA and custom messages are all ignored.

This package has not been tested outside of Windows, however should be compatible across all platforms.

## Workflows

Create a TUIO 1.1 Manager or TUIO 2.0 Manager in your scene using GameObject > TUIO in the main Unity window or Right Click > TUIO in the Hierarchy. Create a TUIO Manager Settings object in your Assets folder using Right Click > TUIO in the Project window. Reference the created TUIO Manager Settings asset from the TUIO 1.1 Manager or TUIO 2.0 Manager in the Hierarchy.

Set the desired TUIO Manager Settings (see Reference).

Create scripts that implement the Tuio11Listener or Tuio20Listener interface and subscribe them to the manager using Tuio11Manager.Instance.AddTuio11Listener(this) or Tuio20Manager.Instance.AddTuio20Listener(this). Use the appropriate callbacks to implement your own TUIO application.

## Advanced topics

Callbacks for TUIO objects can be either handled individually, using AddTuioCursor, UpdateTuioCursor, RemoveTuioCursor etc. or all together once per frame using Refresh then retrieving the full list of objects e.g. using Tuio11Manager.Instance.tuio11Client.GetTuioCursors().

TUIO objects passed to the TUIO Listener are guaranteed to persist, that is the same object will be passed for the same session ID and will not be garbage collected. Thus a TUIO object may be passed to a Unity MonoBehaviour on a GameObject on creation and then polled in the Update or LateUpdate method to update the state of the app without needing to be passed further callbacks. This powerful pattern is demonstrated in the Samples.

## Reference

| **Field** | **Format** | **Description** |
|--|--|--|
|Tuio Connection Type | Websocket / UDP | The connection type to use
| Udp Port | 0 - 9999 | The local port to receive UDP messages on |
| Websocket Address | IPv4 address   | The remote address to connect to websocket server
| Websocket Port | 0 - 9999 | The remote port to connect to the websocket server |


## Samples



## Tutorials

