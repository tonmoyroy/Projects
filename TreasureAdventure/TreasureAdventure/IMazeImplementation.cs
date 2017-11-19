using AtlasCopco.Integration.Maze;

namespace TreasureAdventure
{

    class IMazeImplementation: IMazeIntegration
    {
        Cube[,] maze;
        int mazeSize;
        public void BuildMaze(int size)
        {

            maze = new Cube[3,3];
            mazeSize = size;
            //Row 1
            Cube cube0 = new Cube();
            cube0.setCube(0,0,1,0,1,false, false, false,true);
            maze[0,0] = cube0;

            Cube cube1 = new Cube();
            cube1.setCube(1, 1, 1, 0, 1, false, false, false, false);
            maze[0,1] = cube1;

            Cube cube2 = new Cube();
            cube2.setCube(2, 1, 0, 0, 1, false, false, false, false);
            maze[0,2] = cube2;


            //Row 2
            Cube cube3 = new Cube();
            cube3.setCube(3, 0, 1, 1, 1, true, false, false, false);
            maze[1,0] = cube3;

            Cube cube4 = new Cube();
            cube4.setCube(4, 1, 1, 1, 1, false, false, false, false);
            maze[1,1] = cube4;

            Cube cube5 = new Cube();
            cube5.setCube(5, 1, 0, 1, 1, false, true, false, false);
            maze[1,2]= cube5;


            //Row 3
            Cube cube6 = new Cube();
            cube6.setCube(6, 0, 1, 1, 0, false, false, false, false);
            maze[2,0] = cube6;

            Cube cube7 = new Cube();
            cube7.setCube(7, 1, 1, 1, 0, false, true, false, false);
            maze[2,1] = cube7;

            Cube cube8 = new Cube();
            cube8.setCube(8, 1, 0, 1, 0, false, false, false, false);
            maze[2,2] = cube8;
        }

        public int GetEntranceRoom()
        {
            maze[0, 0].Visited = true;
            return 0;
        }


        public int? GetRoom(int roomId, char direction)
        {
            if (direction == 'N')
            {
                roomId = roomId - mazeSize;
                if (roomId < 0)
                    return null;

                maze[roomId / mazeSize, roomId % mazeSize].Visited = true;
                return maze[roomId / mazeSize, roomId % mazeSize].Roomid;
            }

            if (direction == 'S')
            {
                roomId = roomId + mazeSize;
                if (roomId >= mazeSize * mazeSize)
                    return null;

                maze[roomId/ mazeSize, roomId % mazeSize].Visited = true;
                return maze[roomId / mazeSize, roomId % mazeSize].Roomid;
            }
   

            if (direction == 'E')
            {
                roomId = roomId + 1;
                if (roomId % mazeSize == 0)
                    return null;

                maze[roomId / mazeSize, roomId % mazeSize].Visited = true;
                return maze[roomId / mazeSize, roomId % mazeSize].Roomid;
            }

            if (direction == 'W')
            {
                roomId = roomId - 1;
                if (roomId % mazeSize == mazeSize-1 || roomId <0)
                    return null;

                maze[roomId / mazeSize, roomId % mazeSize].Visited = true;
                return maze[roomId / mazeSize, roomId % mazeSize].Roomid;
            }

            return null;
        }

        public string GetDescription(int roomId)
        {
            string description=null;
            if (maze[roomId / mazeSize,roomId % mazeSize].Left == 1)
                description = string.Concat(description, "West=Open | ");
            else
                description = string.Concat(description, "West=Close | ");

            if (maze[roomId / mazeSize, roomId % mazeSize].Right == 1)
                description = string.Concat(description, "East=Open | ");
            else
            description = string.Concat(description, "East=Close | ");

            if (maze[roomId / mazeSize, roomId % mazeSize].Top == 1)
                description = string.Concat(description, "North=Open | ");
            else
                description = string.Concat(description, "North=Close | ");

            if (maze[roomId / mazeSize, roomId % mazeSize].Bottom == 1)
                description = string.Concat(description, "South=Open | ");
            else
            description = string.Concat(description, "South=Close | ");

            description = string.Concat(description, "RoomID: "+roomId+"\n");


            if ((roomId% mazeSize) - 1 >= 0)
            {
                if(maze[roomId / mazeSize, (roomId-1) % mazeSize].Visited)
                    description = string.Concat(description, "W: Visited | ");
                else
                    description = string.Concat(description, "W: Unvisited | ");
            }

            if ((roomId % mazeSize) + 1 < mazeSize)
            {
                if (maze[roomId / mazeSize, (roomId+1) % mazeSize].Visited)
                    description = string.Concat(description, "E: Visited | ");
                else
                    description = string.Concat(description, "E: Unvisited | ");
            }

            if (roomId - 3 > 0)
            {
                if (maze[(roomId - 3) / mazeSize, roomId % mazeSize].Visited)
                    description = string.Concat(description, "N: Visited | ");
                else
                    description = string.Concat(description, "N: Unvisited | ");
            }

            if (roomId + 3 < mazeSize* mazeSize)
            {
                if (maze[(roomId + 3) / mazeSize, roomId % mazeSize].Visited)
                    description = string.Concat(description, "S: Visited");
                else
                    description = string.Concat(description, "S: Unvisited");
            }


            return description;
        }

        public bool HasTreasure(int roomId)
        {
            return maze[roomId / mazeSize,roomId % mazeSize].Treasure;
        }

        public bool CausesInjury(int roomId)
        {
            return maze[roomId / mazeSize,roomId % mazeSize].Trap;
        }
    }
}
