
namespace TreasureAdventure
{
    class Cube
    {
        public int Roomid { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        public int Top { get; set; }
        public int Bottom { get; set; }
        public bool Treasure { get; set; }
        public bool Trap { get; set; }
        public bool Visited { get; set; }
        public bool Entrance { get; set; }




        public void setCube(int roomid, int left,int right, int top, int bottom, bool treasure, bool trap, bool visited, bool entrance)
        {
            this.Roomid = roomid;
            this.Left = left;
            this.Right = right;
            this.Top = top;
            this.Bottom = bottom;
            this.Treasure = treasure;
            this.Trap = trap;
            this.Visited = visited;
            this.Entrance = entrance;
        }

    }
}
