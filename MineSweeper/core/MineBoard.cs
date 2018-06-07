using System;
namespace MineSweeper.core
{
    public class MineBoard
    {
		public Block[,] blocksBoards { get; set; }

        public MineBoard(int sizeX, int sizeY, int numberOfMines)
        {
			blocksBoards = createBoard(sizeX, sizeY);
			setMinesOnMineBoard(numberOfMines);
			setMinesNumbers();
        }

		private Block[,] createBoard(int sizeX, int sizeY)
		{
			Block[,] newBoard = new Block[sizeX, sizeY];
			for (int x = 0; x < sizeX; x++){
				for (int y = 0; y < sizeY; y++)
                {
					newBoard[x, y] = new Block(x, y);
                }	
			}
			return newBoard;
		}

		private void setMinesOnMineBoard(int numberOfMines){
			int placedMines = 0, randomX, randomY;
			Random random = new Random();
			while(placedMines < numberOfMines){
				randomX = random.Next(0, blocksBoards.GetLength(0));
				randomY = random.Next(0, blocksBoards.GetLength(1));

				if(blocksBoards[randomX,randomY].blockType.Equals(BlockType.NORMAL)){
					blocksBoards[randomX, randomY].blockType = BlockType.MINE;
					placedMines++;
				}
			}
		}

		private void setMinesNumbers(){
			int countMines;
			foreach(Block block in blocksBoards){
				countMines = 0;
				for (int x = retrieveLowestNumber(block.x, 1); x < retrieveRightNumber(block.x, 1); x++ ){
					for (int y = retrieveLowestNumber(block.y, 1); y < retrieveDownNumber(block.y, 1); y++)
                    {
						if(blocksBoards[x,y].blockType.Equals(BlockType.MINE)){
							countMines++;
						}
                    }	
				}
				block.blockValue = countMines;
			}
		}

		private int retrieveLowestNumber(int currentValue, int amount){
			currentValue -= amount;
			if(currentValue < 0){
				return 0;
			}
			return currentValue;
		}

		private int retrieveRightNumber(int currentValue, int amount)
        {
			currentValue += (amount + 1);
            if (currentValue > blocksBoards.GetLength(0))
            {
				return blocksBoards.GetLength(0);
            }
            return currentValue;
        }

        private int retrieveDownNumber(int currentValue, int amount)
        {
            currentValue += (amount + 1);
            if (currentValue > blocksBoards.GetLength(1))
            {
                return blocksBoards.GetLength(1);
            }
            return currentValue;
        }
	}
}
