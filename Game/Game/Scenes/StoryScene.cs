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
            if (percent < 50) _battle = new Battle(15, new Slime());
            else if (percent < 80) _battle = new Battle(15, new Goblin());
            else _battle = new Battle(15, new Golem());

            _turnFinish = false;
            SetActionMenu();
        }

        public override void Update()
        {
            // 전투 종료 시 Enter누르면 Town으로 이동
            if (_battle.IsFinish)
            {
                if (InputManager.GetKey(ConsoleKey.Enter))
                {
                    SceneManager.Change("Town");
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
            Console.WriteLine($"플레이어 HP : {_battle.PlayerHP} / {_battle.PlayerMaxHP}");
            Console.WriteLine($"{_battle.Monster.Name} HP : {_battle.Monster.HP} / {_battle.Monster.MaxHP}\n");

            if (!_turnFinish)
            {
                // 선택 화면
                Console.WriteLine($"[턴 {_battle.Turn}]\n");

                _battleMenu.Render(0, 5);

                Console.SetCursorPosition(0, 12);
                Console.WriteLine("행동을 선택하세요");
            }
            else
            {
                Console.WriteLine($"[턴 {_battle.Turn}]\n");
                PrintPlayerAction(_battle.PlayerAction);
                PrintMonsterAction(_battle.MonsterAction);
                Console.WriteLine();

                if (_battle.IsFinish)
                {
                    Console.WriteLine("전투가 종료되었습니다.");
                    Console.WriteLine("Enter를 누르면 Town으로 돌아갑니다.");
                }
                else
                {
                    Console.WriteLine("Enter를 누르면 다음 턴을 진행합니다.");
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
            else Console.WriteLine("플레이어가 방어했습니다");
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