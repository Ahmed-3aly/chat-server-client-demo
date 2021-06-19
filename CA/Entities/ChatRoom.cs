namespace CA.Entities
{
	using CA.DTOs;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;
	public class ChatRoom
	{
		[Key]
		public long RoomId { get; set; }
		[StringLength(Constants.MaxLen, MinimumLength = Constants.MinLen)]
		public string Name { get; set; }
		public virtual ICollection<ChatRoomUser> UsersLog { get; set; }
		public virtual ICollection<ChatEvent> RealtimeLog { get; set; }
		[NotMapped]
		public List<HourlyStats> HourlyLog
		{
			get
			{
				var que = RealtimeLog.GroupBy(_ => _.On.DateHour()).ToList();
				var result = que
					.Select(_ => {
						var append = new HourlyStats(_.Key);
						append.AddRange(_.OrderBy(__ => __.On).ToList());
						return append;
					}).ToList();
				return result;
			}
		}
	}
}
