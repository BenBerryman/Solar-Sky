# Solar-Sky
This application gathers data about the solar system in real time and displays it on an AR Enabled Device. Once a flat surface (or the target surface) is detected, the solar system will display. The first iteration will display an overall space of the system, while pressing the buttons will lead to individual planets or stars.

When it is on an individual planet, a set of statistics, gathered by an API call, will display. The API call is received as JSON data which is then parsed and rendered into the set of statistics readable by the user. The second call will gather a 'fun fact' instead, and display one at random. There are several for each planet.

The app can display animated rotation and orbit of satellites, and moons around certain planets, such as Earth, Mars, and the overall solar system. 

Written in C#.

The app has been deployed to and is supported by Android.
