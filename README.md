# VeryInterestingTask

Test assignment for Unity developer practice.
## Main part
- The movement of the object along a given trajectory.
- It is necessary to create a scene in which the object after user input (pressing a button on the keyboard / mouse) began to move along a certain trajectory. When the end of the trajectory is reached, the object stops.
- The trajectory is set by an array of points of arbitrary (user-defined) size.
- The array of points must be stored in a special configuration file (JSON or plain text file). The file is downloaded when the application is launched.
## Implemented bonuses
- [x] Trajectory closure parameter. The loop parameter is also stored in the file. If it == 1, then at the end of the path, the object continues to move to the beginning of the trajectory and moves along it again.
- [x] The trajectory passage time parameter. The file also stores the travel time of the path, during which the object will travel the entire path.
- [x] The configuration file is stored on the network. The configuration file should be stored on any web resource (for example, dropbox) and when the application starts, it should be downloaded from the network.
- [ ] Make the trajectory smooth (for example Bezier curve)
- [ ] Add an algorithm that generates a random trajectory (array of points) on the plane (X,Z), despite the fact that the trajectory does not intersect itself.
