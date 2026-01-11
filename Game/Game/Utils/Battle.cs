using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Battle
    {
        public int Turn { get; set; }  // 배틀 턴 수
        public int PlayerMaxHP { get; }  // 플레이어 최대 체력
        public int PlayerHP { get; set; }  // 플레이어 현재 체력
        public int PlayerSkillCount { get; set; }  // 스킬 사용 가능 횟수
        public int PlayerGuardCount { get; set; }  // 방어 사용 가능 횟수
        public bool IsFinish { get; set; }  // 전투 종료 여부

        public Monster Monster { get; }

        public BattleAction PlayerAction { get; set; }
        public BattleAction MonsterAction { get; set; }

        public Battle(int playerMaxHP, Monster monster)
        {
            Turn = 1;
            PlayerMaxHP = playerMaxHP;
            PlayerHP = playerMaxHP;
            PlayerSkillCount = 3;
            PlayerGuardCount = 3;
            IsFinish = false;
            Monster = monster;   
        }

        // 플레이어 스킬/방어 사용 가능 횟수 감소
        public void ReducePlayerCount(BattleAction action)
        {
            if(!UsePlayerAction(action)) return;

            if (action == BattleAction.Skill) PlayerSkillCount--;
            else if (action == BattleAction.Guard) PlayerGuardCount--;
        }

        // 플레이어 스킬/방어 사용 가능 여부
        public bool UsePlayerAction(BattleAction action)
        {
            if (action == BattleAction.Skill) return PlayerSkillCount > 0;
            if (action == BattleAction.Guard) return PlayerGuardCount > 0;
            return true;
        }

        public void PlayBattle(BattleAction playerAction)
        {
            PlayerAction = playerAction;
            MonsterAction = Monster.SelectAction();

            // 스킬/방어인 경우 사용 가능 횟수 감소
            ReducePlayerCount(playerAction);
            Monster.ReduceMonsterCount(MonsterAction);

            // 몬스터가 받는 데미지
            int mDamage = MonsterDamage(playerAction, MonsterAction == BattleAction.Guard);
            // 플레이어가 받는 데미지
            int pDamage = PlayerDamage(MonsterAction, playerAction == BattleAction.Guard);

            // 현재 체력 - 받은 데미지
            Monster.HP -= mDamage;
            PlayerHP -= pDamage;

            if (Monster.HP < 0) Monster.HP = 0;
            if (PlayerHP < 0) PlayerHP = 0;

            // 둘 중 하나의 체력이 0이 되면 전투 종료
            if (Monster.HP == 0 || PlayerHP == 0)
            {
                IsFinish = true;
            }
        }

        // 몬스터가 받는 데미지 계산
        public int MonsterDamage(BattleAction action, bool defence)
        {
            int damage;
            if (action == BattleAction.Attack) damage = 1;
            else if (action == BattleAction.Skill) damage = 3;
            else damage = 0;

            return defence ? 0 : damage;
        }

        // 플레이어가 받는 데미지 계산
        public int PlayerDamage(BattleAction action, bool defence)
        {
            return defence ? 0 : Monster.GetDamage(action);
        }
    }
}