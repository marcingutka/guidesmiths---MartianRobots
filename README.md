<b>1. Overview</b>  
   This is repository for analyizing Martians Robots, which are sent to discover Red Planet (rectangular one).

<b>2. Input</b>   
   It is a .txt file in following format:  
   X Y  
   I J O  
   CCCCCCCCCC  
   
   First line assigns Mars size (X => max horizontal coordinate and Y => max vertical coordinate) separated by single space.
   Second line describes robot initial position (I => horizontal coordinate, J => vertical coordinate, O - orientation) separated by single spaces
   Third line contains commands, which robot is supposed to execute (without spaces).
   
   Allowed values for orientation are uppercase: "N", "E", "S", "W" -> which correspond to North, East, South, West
   Allowed values for commands are uppercase: "F", "L", "R" -> which correspond to Forward, Left, Right
   
   Max value for X, Y, I, J is 50 and they cannot be less than 0.
   There could be up to 100 commands per robot,
   
   You can create more than one robot by inserting next lines similar to lines no. 2 and 3.
   
   Example:  
   5 3  
   1 1 E  
   RFRFRFRF  
   3 2 N  
   FRRFLLFFRRFLL  
   0 3 W  
   LLFFFRFLFL  
   
<b>3. Output</b>   
   The output is .txt file, which contains last robots position and if robot is lost it is marked as "LOST". Each line represents one robot in following format:  
   1 1 E  
   3 3 N LOST  
   4 2 N  
   
   It means that first robot finished its job at position 1,1 and was orientated to East, second one is lost and its last known position is 3,3 and was orientated to North and
   the third one finished at 4,2 orientated to North.
   
   The output can be downloaded from the application by button: Get Result File
   
<b>4. Rules</b>   
   Most of the names are self-explaining like Forward or North, but there is one important rule according to being lost. The robot is considered to be lost when it is outside the Mard grid,
   which mean that its position is less than 0 or greater than X for horizontal coordinate or less than 0 and greater than Y for vertical coordinate. When robot is lost it left the hint for another robot,
   which prevent the other robots to go off grid. In this case if some robot is supposed to be outside the grid, it ommits this command and execute next one. Each robot waits for another to finish his movement.

<b>5. Analitics features</b>   
   This application provides insights into each run. It displays:  
   - number of robots,
   - lost robot,
   - total grid area,
   - discovered area (abs),
   - dicovered area (%)
   
   The grid is also displayed and it is shown which grid points were visited by robots (green points) and if you hoover that field, you will receive information how many robots were on it.
   The red point indicates points, where robots were lost.
   
   It is possible to dig even deeper and check each individual robot step, just choose the robot from Robots dropdown list.
   
<b>6. Run application</b>   
   Requirements:  
   - Windows 10,
   - installed Windows Docker,
   - have stable Internet connection,
   - newest browser e.g. Mozilla Firefox v.96.0.3,

   Steps:   
   a) clone or download this repository (guidesmiths---MartianRobots) into your local machine,  
   b) go to the local repository main directory and open command line and type 
   ```sh
   docker compose up
   ```
   this will create docker images and run them in containers. It can take up to 10 minutes,  
   c) after the container creation is completed, open your browser and go to http://localhost:3000  
   d) upload first .txt file and start analyzing.  
  
  
