using System.ComponentModel.DataAnnotations;

namespace Events.Appointment.Enum
{
	public enum AppointmentStateEnum
	{
		[Display(Name = "Oluşturuldu")]
		Created,
		[Display(Name = "Onaylandı")]
		Approved,
		[Display(Name = "Reddedildi")]
		Denied,
		[Display(Name = "Güncellendi")]
		Updated,
		[Display(Name = "Hizmet Zamanı")]
		InProcess,
		[Display(Name = "Hizmet Tamamlandı")]
		Completed,
		[Display(Name = "İptal Edildi")]
		Cancelled
	}
}
