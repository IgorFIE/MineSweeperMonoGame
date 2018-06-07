using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

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
					blocksBoards[randomX, randomY].blockColor = Color.Yellow;
					placedMines++;
				}
			}
		}

		private void setMinesNumbers(){
			foreach(Block block in blocksBoards)
			{
				block.blockValue = retrieveNumberOfMines(block);
			}
		}

		private int retrieveNumberOfMines(Block block)
		{
			int countMines = 0;
			for (int x = retrieveLowestNumber(block.x, 1); x < retrieveRightNumber(block.x, 1); x++)
			{
				for (int y = retrieveLowestNumber(block.y, 1); y < retrieveDownNumber(block.y, 1); y++)
				{
					if (blocksBoards[x, y].blockType.Equals(BlockType.MINE))
					{
						countMines++;
					}
				}
			}
			return countMines;
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
  
		public bool setBLockClickVisible(int x, int y){
			Rectangle fakeRectangle = new Rectangle(x, y, 1, 1);
			bool wasMineClicked = false;
			foreach(Block block in blocksBoards){
				if(block.positionRectangle.Intersects(fakeRectangle))
				{
					block.isVisible = true;
					wasMineClicked = checkIfMineWasClicked(block);
					retrieveAreaBlocks(wasMineClicked, block);
					break;
				}
			}
			return wasMineClicked;
		}

		private void retrieveAreaBlocks(bool wasMineClicked, Block block)
		{
			if (!wasMineClicked && block.blockValue == 0)
			{
				List<Block> blocksToCheck = new List<Block>();
                List<Block> blocksChecked = new List<Block>();
				blocksToCheck.Add(block);

				while(blocksToCheck.Count > 0){
					if(blocksToCheck[0].blockValue == 0)
					{
						retrieveBlocksToCheck(blocksToCheck, blocksChecked);
					}
					blocksChecked.Add(blocksToCheck[0]);
					blocksToCheck.RemoveAt(0);
				}
			}
		}

		private void retrieveBlocksToCheck(List<Block> blocksToCheck, List<Block> blocksChecked)
		{
			for (int x = retrieveLowestNumber(blocksToCheck[0].x, 1); x < retrieveRightNumber(blocksToCheck[0].x, 1); x++)
			{
				for (int y = retrieveLowestNumber(blocksToCheck[0].y, 1); y < retrieveDownNumber(blocksToCheck[0].y, 1); y++)
				{
					if ((x != blocksToCheck[0].x || y != blocksToCheck[0].y) &&
					   !blocksToCheck.Contains(blocksBoards[x, y]) && !blocksChecked.Contains(blocksBoards[x, y]))
					{
						blocksBoards[x, y].isVisible = true;
						blocksToCheck.Add(blocksBoards[x, y]);
					}
				}
			}
		}

		private bool checkIfMineWasClicked(Block block)
		{
			if (block.blockType.Equals(BlockType.MINE))
			{
				block.blockColor = Color.Red;
				return true;
			}
			return false;
		}
	}
}
