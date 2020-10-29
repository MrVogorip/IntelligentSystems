namespace Wumpus.Logic
{
    class Room
    {
        public Room[] Neighbors { get; set; }
        public int Number { get; set; }
        public bool IsPit { get; set; }
        public bool IsBats { get; set; }
        public bool IsWumpus { get; set; }
        public Room()
        {
            Neighbors = new Room[3];
            IsBats = false;
            IsPit = false;
            IsWumpus = false;
        }
        public string Info()
        {
            string info = $"You are in room number {Number}.\n" +
                $"nearby rooms is {Neighbors[0].Number}, {Neighbors[1].Number}, {Neighbors[2].Number}.\n";
            if (CheckWind())
                info += "You can hear wind here.\n";
            if (CheckBats())
                info += "You can hear bats here.\n";
            if (CheckWumpus())
                info += "You can hear Wumpus here.\n";
            return info;
        }
        public bool CheckWumpus()
        {
            for (int i = 0; i < Neighbors.Length; i++)
            {
                if (Neighbors[i].IsWumpus)
                    return true;
            }
            return IsWumpus;
        }
        public bool CheckWind()
        {
            for (int i = 0; i < Neighbors.Length; i++)
            {
                if (Neighbors[i].IsPit)
                    return true;
            }
            return IsPit;
        }
        public bool CheckBats()
        {
            for (int i = 0; i < Neighbors.Length; i++)
            {
                if (Neighbors[i].IsBats)
                    return true;
            }
            return IsBats;
        }
    }
}
