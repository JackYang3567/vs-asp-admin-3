using System.Collections;

namespace Game.Kernel
{
	public interface IMessage
	{
		string Content
		{
			get;
			set;
		}

		ArrayList EntityList
		{
			get;
			set;
		}

		int MessageID
		{
			get;
			set;
		}

		bool Success
		{
			get;
			set;
		}

		void AddEntity(ArrayList entityList);

		void AddEntity(object entity);

		void ResetEntityList();
	}
}
