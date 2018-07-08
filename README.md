# Maze-creator
Creating mazes based on photograps

Nearly one year ago i got a gift. It was a T-shirt. T-shirt with map of Manhattan on it. But not classic one - it was deformed to shape a woman's face. And i finally saw it for the first time after several months of wearing that. As you can assume, i was suprised. And it got me an idea.

Several days after my discovery i started to create program in c# whose job was to create a maze based on any picture.

I wasn't sure at the beginning how i want to achieve that, but finally these are the rules on which mazes are created:
* first channel of texture given to the program is height map i calculate gradient of, and along the normalized graphical projection on screen surface i draw maze tunnels(or perpendicularly to it). >> in simpler words i found angle at which for each pixel slope is steepest by looking at this channel and draw maze tunnels along it(or perpendicular to it).
* second channel is just length of the tunnels at specific points.
* tunnels cannot ever cross.

Without further ado: these are few examples on what i created using this program.

* Based on Robert Mapplethorpe picture of Deborah Harry
![texture without red channel, only density is afected](/Expamles/Robert-Mapplethorpe-Deborah-Harry.png)
![Maze from it](/Expamles/bigone.png)

* Based on Vincent van Gogh painting of his room
![both channels are nearly the same](/Expamles/Vincent.jpg)
![Maze from it](/Expamles/VincentMaze.png)

* Based on Barack Obama photo
![both channels usage](/Expamles/obama.png)
![Maze from it](/Expamles/obamawuynik.png)

- - - -

When using this program:
* it takes only Bitmap(.bmp) files - size of maze is size of texture input
* blur size is radius around each pixel in which i find slope - bigger the value, slower program will run (5 is OK in most cases)
* MinStepSize and MaxStepSize are minimum and maximum lengths of simplest tunnel - lower minimum value is longer it will take to compute that

Thats all i have, future improvements are coming :)
