using System.Globalization;

namespace OnlineShop.Classes;

public class Generation
{

    public string PersianDateTime()
    {
        DateTime dateTime = DateTime.Now;
        PersianCalendar persian = new PersianCalendar();

        string persianDate = persian.GetYear(dateTime).ToString("0000") + "/" +
                             persian.GetMonth(dateTime).ToString("00") + "/" +
                             persian.GetDayOfMonth(dateTime).ToString("00");

        var time = dateTime.Hour.ToString("00") + ":" +
                   dateTime.Minute.ToString("00") + ":" +
                   dateTime.Second.ToString("00");

        return String.Format("{0} {1}", persianDate, time);
    }
}
