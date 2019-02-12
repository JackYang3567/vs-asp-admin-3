using System;
using System.Collections;

namespace Game.Kernel
{
	[Serializable]
	public class Message : IMessage
	{
		private int m_messageID;

		private bool m_success;

		public int MessageID
		{
			get
			{
				return m_messageID;
			}
			set
			{
				m_messageID = value;
				m_success = (m_messageID == 0);
			}
		}

		public bool Success
		{
			get
			{
				return m_success;
			}
			set
			{
				m_success = value;
				if (m_success)
				{
					m_messageID = 0;
				}
				else
				{
					m_messageID = -1;
				}
			}
		}

		public string Content
		{
			get;
			set;
		}

		public ArrayList EntityList
		{
			get;
			set;
		}

		public Message()
		{
			MessageID = 0;
			Success = true;
			Content = string.Empty;
			EntityList = new ArrayList();
		}

		public Message(bool isSuccess)
			: this(isSuccess, "")
		{
		}

		public Message(bool isSuccess, string content)
			: this()
		{
			MessageID = ((!isSuccess) ? (-1) : 0);
			Content = content;
		}

		public Message(int messageID, string content)
			: this()
		{
			MessageID = messageID;
			Content = content;
		}

		public Message(bool isSuccess, string content, ArrayList entityList)
			: this(isSuccess, content)
		{
			EntityList = entityList;
		}

		public Message(int messageID, string content, ArrayList entityList)
			: this(messageID, content)
		{
			EntityList = entityList;
		}

		public void AddEntity(ArrayList entityList)
		{
			EntityList = entityList;
		}

		public void AddEntity(object entity)
		{
			EntityList.Add(entity);
		}

		public void ResetEntityList()
		{
			if (EntityList != null)
			{
				EntityList.Clear();
			}
		}
	}
}
