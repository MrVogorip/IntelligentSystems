using System;
using System.Linq;

namespace Wumpus.Logic
{
    class Game
    {
        private readonly Room[] _rooms;
        private readonly Random _random;
        private string _status;
        public bool IsLose { get; set; }
        public bool IsWin { get; set; }
        public int CurrentRoom { get; set; } = 1;
        public Game()
        {
            _rooms = new Room[21];
            _random = new Random();
            SetupRooms();
        }
        void SetupRooms()
        {
            for (int i = 0; i < 21; i++)
            {
                _rooms[i] = new Room { Number = i };
            }
            SetNeighbors(1, 2, 5, 8);
            SetNeighbors(2, 1, 3, 10);
            SetNeighbors(3, 2, 4, 12);
            SetNeighbors(4, 3, 5, 14);
            SetNeighbors(5, 1, 4, 6);
            SetNeighbors(6, 5, 7, 15);
            SetNeighbors(7, 6, 8, 17);
            SetNeighbors(8, 1, 7, 9);
            SetNeighbors(9, 8, 10, 18);
            SetNeighbors(10, 2, 9, 11);
            SetNeighbors(11, 10, 12, 19);
            SetNeighbors(12, 3, 11, 13);
            SetNeighbors(13, 12, 14, 20);
            SetNeighbors(14, 4, 13, 15);
            SetNeighbors(15, 6, 14, 16);
            SetNeighbors(16, 15, 17, 20);
            SetNeighbors(17, 7, 16, 18);
            SetNeighbors(18, 9, 17, 19);
            SetNeighbors(19, 11, 18, 20);
            SetNeighbors(20, 13, 16, 19);
            int num = _random.Next(1, 20);
            _rooms[num].IsWumpus = true;
            num = _random.Next(1, 20);
            _rooms[num].IsPit = true;
            num = _random.Next(1, 20);
            _rooms[num].IsPit = true;
            num = _random.Next(1, 20);
            _rooms[num].IsBats = true;
            num = _random.Next(1, 20);
            _rooms[num].IsBats = true;
        }
        void SetNeighbors(int roomIndex, int neighbor1, int neighbor2, int neighbor3)
        {
            _rooms[roomIndex].Neighbors[0] = _rooms[neighbor1];
            _rooms[roomIndex].Neighbors[1] = _rooms[neighbor2];
            _rooms[roomIndex].Neighbors[2] = _rooms[neighbor3];
        }
        public void Move(int newRoom)
        {
            _status = "";
            CurrentRoom = newRoom;
            if (_rooms[CurrentRoom].IsWumpus)
            {
                _status += "You were killed Wumpus!\n";
                IsLose = true;
            }
            else if (_rooms[CurrentRoom].IsPit)
            {
                _status += "You fell into a pit and died.\n";
                IsLose = true;
            }
            else if (_rooms[CurrentRoom].IsBats)
            {
                _status += "Bats fly you to another place in the cave.\n";
                int move = _random.Next(1, 20);
                Move(move);
            }
        }
        public void Shoot(int newRoom)
        {
            _status = "";
            if (IsNeighbor(newRoom))
            {
                if (_rooms[newRoom].IsWumpus)
                {
                    _status += "You have killed Wumpus!\n";
                    IsWin = true;
                }
                else
                {
                    _status += "Your only arrow hits the nearest cave harmlessly.\n";
                    IsLose = true;
                }
            }
        }
        bool IsNeighbor(int num)
        {
            for (int i = 0; i < _rooms[CurrentRoom].Neighbors.Length; i++)
            {
                if (_rooms[CurrentRoom].Neighbors[i].Number == num)
                    return true;
            }
            return false;
        }
        public string GetStatus()
        {
            if (IsLose)
                _status += "You lose!\n";
            else
                _status += _rooms[CurrentRoom].Info();
            return _status;
        }
        public int[] GetNeighbors()
        {
            return _rooms[CurrentRoom].Neighbors.Select(x => x.Number).ToArray();
        }
    }
}
