using System;

namespace Statement_SwitchCase_And_Enum
{
    // enum 열거형 사용자정의 자료형
    // 기본적으로 System.int32 자료형
    enum PlayerStates
    {
        Idle,
        Move,
        Jump,
        Dash,
        Attack,
        Hurt,
        Die
    }

    // Flags 속성은 enum 타입의 ToString() 변환시에 각 비트별로 string 변환해주는 속성이다.
    [System.Flags]
    enum Layer
    {
        None     = 0 << 0,  // == ...00000000
        Player   = 1 << 0,  // == ...00000001
        Enemy    = 1 << 1,  // == ...00000010
        Obstacle = 1 << 2,  // == ...00000100
        NPC      = 1 << 3,  // == ...00001000
        Wall     = 1 << 4   // == ...00010000
    }

    class Program
    {
        static void Main(string[] args)
        {
            #region How to use swtich-case
            int state = 3;

            switch (state)
            {
                case 0:
                    Console.WriteLine("상태가 0이다");
                    break; // break 분기문 : 현재 흐름에서 벗어남 (상위 문법에서 빠져나옴)
                case 1:
                    Console.WriteLine("상태가 1이다");
                    break;
                case 2:
                    Console.WriteLine("상태가 2이다");
                    break;
                default:
                    break;
            }
            #endregion

            Player other = new Player();
            Enemy enemy = new Enemy();
            Obstacle obstacle = new Obstacle();
            NPC npc = new NPC();
            Wall wall = new Wall();


            Player player = new Player();

            Console.WriteLine(player.targetLayer);

            player.state = PlayerStates.Attack;
            switch (player.state)
            {
                case PlayerStates.Idle:
                    Console.WriteLine("플레이어는 아무것도 안하고있다");
                    break;
                case PlayerStates.Move:
                    Console.WriteLine("플레이어는 움직이고있다");
                    break;
                case PlayerStates.Jump:
                    Console.WriteLine("플레이어는 점프하고있다");
                    break;
                case PlayerStates.Dash:
                    Console.WriteLine("플레이어는 돌진하고있다");
                    break;
                case PlayerStates.Attack:
                    {
                        Console.WriteLine("플레이어는 공격하고있다");

                        if ((other.layer & player.targetLayer) == other.layer)
                            Console.WriteLine($"{player} 는 {other} 를 공격한다");

                        if ((enemy.layer & player.targetLayer) == enemy.layer)
                            Console.WriteLine($"{player} 는 {enemy} 를 공격한다");

                        if ((obstacle.layer & player.targetLayer) == obstacle.layer)
                            Console.WriteLine($"{player} 는 {obstacle} 를 공격한다");

                        if ((npc.layer & player.targetLayer) == npc.layer)
                            Console.WriteLine($"{player} 는 {npc} 를 공격한다");

                        if ((wall.layer & player.targetLayer) == wall.layer)
                            Console.WriteLine($"{player} 는 {wall} 를 공격한다");

                    }
                    break;
                case PlayerStates.Hurt:
                    Console.WriteLine("플레이어는 공격받고있다");
                    break;
                case PlayerStates.Die:
                    Console.WriteLine("플레이어는 죽어간다");
                    break;
                default:
                    break;
            }
        }
    }

    class Player
    {
        public Layer layer = Layer.Player;
        public Layer targetLayer = Layer.Player | Layer.Enemy | Layer.Obstacle;
        public PlayerStates state;
        public void Attack()
        {

        }
    }
    class Enemy
    {
        public Layer layer = Layer.Enemy;
    }

    class Obstacle
    {
        public Layer layer = Layer.Obstacle;
    }

    class NPC
    {
        public Layer layer = Layer.NPC;
    }

    class Wall
    {
        public Layer layer = Layer.Wall;
    }
}
