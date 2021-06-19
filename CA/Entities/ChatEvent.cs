namespace CA.Entities
{
	using CA.Enums;
	using System;
	using System.ComponentModel.DataAnnotations;
	public sealed class ChatEvent
	{
		[Key]
		public long EventId { get; set; }
		[Required]
		public DateTime On { get; private set; }
		[Required]
		public ChatEventEnum EventType { get; private set; }
		[Required]
		public long SourceUserId { get; private set; }
		public long? EventRoomId { get; private set; }
		public long? TargetUserId { get; private set; }
		public string Text { get; private set; }
		public ChatRoom EventRoom { get; private set; }
		public ChatUser SourceUser { get; private set; }
		public ChatUser Targetuser { get; private set; }
		public string Print
		{
			get
			{
				var prefix =
					On.PrintDateTime() +
					Environment.NewLine +
					"\t" +
					SourceUser.Name;
				return EventType switch
				{
					ChatEventEnum.Enter => prefix + " entered",
					ChatEventEnum.Leave => prefix + " left",
					ChatEventEnum.Comment => prefix + " commented: " + Text,
					ChatEventEnum.HighFive => prefix + " highfived " + Targetuser.Name,
					ChatEventEnum.Connect => prefix + " connected",
					_ => throw new NotSupportedException(),
				};
			}
		}
		public override string ToString()
		{
			return Print;
		}
		private ChatEvent() { }
		private ChatEvent
		(
			ChatEventEnum eventType,
			long sourceId
		)
		{
			On = DateTime.UtcNow;
			EventType = eventType;
			SourceUserId = sourceId;
		}
		static public ChatEvent Connect
		(
			long sourceId
		)
		{
			var v =
				new ChatEvent
				(
					ChatEventEnum.Connect,
					sourceId
				);
			return v;
		}
		static public ChatEvent Enter
		(
			long sourceId,
			long roomId
		)
		{
			var v =
				new ChatEvent
				(
					ChatEventEnum.Enter,
					sourceId
				)
				{
					EventRoomId = roomId,
				};
			return v;
		}
		static public ChatEvent Leave
		(
			long sourceId,
			long roomId
		)
		{
			var v =
				new ChatEvent
				(
					ChatEventEnum.Leave,
					sourceId
				)
				{
					EventRoomId = roomId,
				};
			return v;
		}
		static public ChatEvent Comment
		(
			long sourceId,
			long roomId,
			string text
		)
		{
			var v =
				new ChatEvent
				(
					ChatEventEnum.Comment,
					sourceId
				)
				{
					EventRoomId = roomId,
					Text = text,
				};
			return v;
		}
		static public ChatEvent HighFive
		(
			long sourceId,
			long roomId,
			long targetId
		)
		{
			var v =
				new ChatEvent
				(
					ChatEventEnum.HighFive,
					sourceId
				)
				{
					EventRoomId = roomId,
					TargetUserId = targetId
				};
			return v;
		}
	}
}
