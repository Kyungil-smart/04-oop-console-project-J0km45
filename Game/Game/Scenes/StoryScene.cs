using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class StoryScene : Scene
    {
        private PlayerCharacter _player;
        private Battle _battle;
        private MenuList _battleMenu;  // 플레이어 행동 메뉴
        private bool _turnFinish;  // 턴 종료 여부
        
        private Random rand = new Random();

        public StoryScene(PlayerCharacter player) => Init(player);

        public void Init(PlayerCharacter player)
        {
            _player = player;
        }

        public override void Enter()
        {
            int percent = rand.Next(0, 100);
            if (percent < 50) _battle = new Battle(15, new Slime()); // 50%
            else if (percent < 80) _battle = new Battle(15, new Goblin()); // 30%
            else _battle = new Battle(15, new Golem()); // 20%

            _turnFinish = false;
            SetActionMenu();
        }

        public override void Update()
        {
            // 전투 종료 시 Enter누르면 이동
            if (_battle.IsFinish)
            {
                if (InputManager.GetKey(ConsoleKey.Enter))
                {
                    if (_battle.Monster.HP == 0 && _battle.PlayerHP > 0) // 승리
                    {
                        _player.AddGold(5); // +5 Gold
                        SceneManager.Change("Town");
                    }
                    else if (_battle.PlayerHP == 0 && _battle.Monster.HP > 0) // 패배
                    {
                        _player.Health.Value--; // -1 Health
                        if (_player.Health.Value > 0) SceneManager.Change("Town");
                        else SceneManager.Change("GameOver");
                    }
                    else SceneManager.Change("Town"); // 무승부
                }
                return;
            }

            if (_turnFinish)
            {
                if (InputManager.GetKey(ConsoleKey.Enter))
                {
                    _battle.Turn++;
                    _turnFinish = false;
                    SetActionMenu();
                }
                return;
            }

            if (InputManager.GetKey(ConsoleKey.UpArrow))
            {
                _battleMenu.SelectUp();
            }

            if (InputManager.GetKey(ConsoleKey.DownArrow))
            {
                _battleMenu.SelectDown();
            }

            if (InputManager.GetKey(ConsoleKey.Enter))
            {
                _battleMenu.Select();
            }
        }

        public override void Render()
        {
            // 플레이어 체력
            "플레이어 ".Print();
            for (int i = 0; i < _battle.PlayerMaxHP; i++)
            {
                if (i < _battle.PlayerHP) "■".Print(ConsoleColor.Green);
                else "□".Print(ConsoleColor.Green);
            }
            $" {_battle.PlayerHP} / {_battle.PlayerMaxHP}".Print();
            Console.WriteLine();

            // 몬스터 체력
            $"{_battle.Monster.Name} ".Print();
            for (int i = 0; i < _battle.Monster.MaxHP; i++)
            {
                if (i < _battle.Monster.HP) "■".Print(ConsoleColor.Red);
                else "□".Print(ConsoleColor.Red);
            }
            $" {_battle.Monster.HP} / {_battle.Monster.MaxHP}\n".Print();
            Console.WriteLine();

            if (!_turnFinish)
            {
                // 선택 화면
                Console.WriteLine($"[턴 {_battle.Turn}]\n");

                _battleMenu.Render(0, 5);

                Console.SetCursorPosition(0, 12);
                Console.WriteLine("행동을 선택하세요.");
            }
            else
            {
                Console.WriteLine($"[턴 {_battle.Turn}]\n");
                PrintPlayerAction(_battle.PlayerAction);
                PrintMonsterAction(_battle.MonsterAction);
                Console.WriteLine();

                if (_battle.IsFinish)
                {
                    Console.WriteLine("\n전투가 종료되었습니다.\n");
                    if (_battle.Monster.HP == 0 && _battle.PlayerHP > 0)  // 승리
                    {
                        "승리! ".Print(ConsoleColor.Cyan);
                        Console.WriteLine("Enter를 누르면 5 Gold를 받고 마을로 돌아갑니다.");
                    }
                    else if (_battle.PlayerHP == 0 && _battle.Monster.HP > 0)  // 패배
                    {
                        if (_player.Health.Value - 1 > 0)
                        {
                            "패배.. ".Print(ConsoleColor.Red);
                            Console.WriteLine("Enter를 누르면 체력이 1 감소하고 마을로 돌아갑니다.");
                        }
                        else  // 게임 종료
                        {
                            "패배.. ".Print(ConsoleColor.Red);
                            Console.WriteLine("체력이 0이 되었습니다.");
                            Console.WriteLine("Enter를 누르면 종료 화면으로 이동합니다.");
                        }
                    }
                    else  // 무승부
                    {
                        "무승부. ".Print(ConsoleColor.White);
                        Console.WriteLine("Enter를 누르면 마을로 돌아갑니다.");
                    }
                }
                else
                {
                    Console.WriteLine("\nEnter를 누르면 다음 턴을 진행합니다.");
                }
            }
        }

        public override void Exit()
        {
            
        }

        // 행동 메뉴 생성
        public void SetActionMenu()
        {
            _battleMenu = new MenuList();

            _battleMenu.Add("공격", Attack);
            _battleMenu.Add($"스킬 (남은 횟수: {_battle.PlayerSkillCount})", Skill);
            _battleMenu.Add($"방어 (남은 횟수: {_battle.PlayerGuardCount})", Guard);

            // _currentIndex 초기화
            _battleMenu.Reset();
        }

        public void Attack()
        {
            _battle.PlayBattle(BattleAction.Attack);
            _turnFinish = true;
        }

        public void Skill()
        {
            if (!_battle.UsePlayerAction(BattleAction.Skill)) return;

            _battle.PlayBattle(BattleAction.Skill);
            _turnFinish = true;
        }

        public void Guard()
        {
            if (!_battle.UsePlayerAction(BattleAction.Guard)) return;

            _battle.PlayBattle(BattleAction.Guard);
            _turnFinish = true;
        }

        // 플레이어 행동 출력
        public void PrintPlayerAction(BattleAction action)
        {
            if (action == BattleAction.Attack)
            {
                Console.WriteLine("플레이어가 공격했습니다. (1 Damage)");
            }
            else if (action == BattleAction.Skill)
            {
                Console.WriteLine("플레이어가 강타를 사용했습니다. (3 Damage)");
            }
            else Console.WriteLine("플레이어가 방어했습니다.");
        }

        // 몬스터 행동 출력
        public void PrintMonsterAction(BattleAction action)
        {
            if (action == BattleAction.Attack)
            {
                _battle.Monster.Attack();
            }
            else if (action == BattleAction.Skill)
            {
                _battle.Monster.Skill();
            }
            else _battle.Monster.Guard();
        }
    }
}