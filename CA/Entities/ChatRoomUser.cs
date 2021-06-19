namespace CA.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	public class ChatRoomUser
	{
		[Key]
		public long ChatRoomUser_Id { get; set; }
		[Required]
		[ForeignKey("ChatRoomUser_Room")]
		public long ChatRoomUser_RoomId { get; set; }
		public ChatRoom ChatRoomUser_Room { get; set; }
		[Required]
		[ForeignKey("ChatRoomUser_User")]
		public long ChatRoomUser_UserId { get; set; }
		public ChatUser ChatRoomUser_User { get; set; }
	}
}
