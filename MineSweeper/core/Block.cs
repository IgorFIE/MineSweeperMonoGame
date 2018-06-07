using System;
using Microsoft.Xna.Framework;

namespace MineSweeper.core
{
    public class Block
    {
		public int x { get; private set; }
	    public int y { get; private set; }
        
		public BlockType blockType { get; set; }
		public BlockType flag { get; set; }
		public int blockValue { get; set; }

		public Rectangle positionRectangle { get; private set; }
		public Color blockColor;

		public bool isVisible { get; set; }

        public Block(int x, int y)
        {
			this.x = x;
			this.y = y;
			blockType = BlockType.NORMAL;
			blockColor = Color.LightGray;
			positionRectangle = new Rectangle((int)((x * GameProperties.BLOCK_SIZE) * GameProperties.BLOCK_MARGIN),
			                                  (int)((y * GameProperties.BLOCK_SIZE) * GameProperties.BLOCK_MARGIN),
			                                  GameProperties.BLOCK_SIZE, GameProperties.BLOCK_SIZE);
        }
    }
}
