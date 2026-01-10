using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Grass : GameObject
    {
        public Grass() => Init();
        private void Init()
        {
            Symbol = '/';
        }

        // x, y -> 잔디 시작(왼쪽 위) 좌표 / width 너비 / height 높이
        public static void SetGrassPosition(Tile[,] field, int startX, int startY, int width, int height)
        {
            // x, y 마지막 좌표
            int endX = startX + width - 1;
            int endY = startY + height - 1;

            if (startX < 0) startX = 0;
            if (endX >= field.GetLength(1)) endX = field.GetLength(1) - 1;
            if (startY < 0) startY = 0;
            if (endY >= field.GetLength(0)) endY = field.GetLength(0) - 1;

            for (int i = startY; i <= endY; i++)
            {
                for (int j = startX; j <= endX; j++)
                {
                    if (field[i, j].OnTileObject == null)
                    {
                        field[i, j].HasGrass = true; // 해당위치 잔디로 체크
                        Grass grass = new Grass();
                        grass.Position = new Vector(j, i);
                        field[i, j].OnTileObject = grass;
                    }
                }
            }
        }
    }
}
