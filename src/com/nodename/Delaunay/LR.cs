namespace Delaunay
{
	namespace LR
	{
		public enum Side
		{
			LEFT = 0,
			RIGHT
		}

		public class SideHelper
		{
			public static Side other (Side leftRight)
			{
				return leftRight == Side.LEFT ? Side.RIGHT : Side.LEFT;
			}
		}

	}
}