package com.expedia.lux.accountsettingstest.core;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.Locale;
import java.util.TimeZone;

import javax.xml.datatype.DatatypeConstants;
import javax.xml.datatype.DatatypeFactory;
import javax.xml.datatype.XMLGregorianCalendar;

import org.joda.time.DateTime;
import org.joda.time.DateTimeZone;
import org.joda.time.Days;
import org.joda.time.Months;

public class DateUtils {

	/**
	 * Get the local time in a given time zone. E.g. The given time zone is
	 * "America/Los_Angeles", then this method will return current date time in
	 * "America/Los_Angeles" time zone.
	 * 
	 * @param zone
	 *            The time zone which current date time in.
	 * @return Current date time in the given time zone.
	 */
	public static Date getLocalTime(TimeZone zone) {
		Calendar srcCal = Calendar.getInstance(zone);

		TimeZone defaultTimeZone = TimeZone.getDefault();
		Calendar tarCal = convertDateTime(srcCal, defaultTimeZone);

		return tarCal.getTime();
	}

	/**
	 * Convert one date to another date in target time zone.
	 * 
	 * @param srcDate
	 *            The source date.
	 * @param targetTimeZone
	 *            The target time zone.
	 * @return A calendar which the source date is converted to.
	 */
	private static Calendar convertDateTime(Calendar srcDate,
			TimeZone targetTimeZone) {

		Calendar targetDate = Calendar.getInstance(targetTimeZone);
		targetDate.set(Calendar.YEAR, srcDate.get(Calendar.YEAR));
		targetDate.set(Calendar.MONTH, srcDate.get(Calendar.MONTH));
		targetDate.set(Calendar.DAY_OF_MONTH,
				srcDate.get(Calendar.DAY_OF_MONTH));
		targetDate.set(Calendar.HOUR_OF_DAY, srcDate.get(Calendar.HOUR_OF_DAY));
		targetDate.set(Calendar.MINUTE, srcDate.get(Calendar.MINUTE));
		targetDate.set(Calendar.SECOND, srcDate.get(Calendar.SECOND));
		targetDate.set(Calendar.MILLISECOND, srcDate.get(Calendar.MILLISECOND));

		return targetDate;
	}

	/**
	 * Get different date string with different format string.
	 * 
	 * @return date string
	 */
	public static String getDateStr(String format, Date date) {
		SimpleDateFormat dateFormat = new SimpleDateFormat(format, new Locale(
				"en"));
		return dateFormat.format(date);
	}

	/**
	 * Parse the giving date string which's in giving pattern and giving time
	 * zone, the parsed date will be still in its time zone. .
	 * 
	 * @param formatStr
	 * @param dateString
	 * @return dateInItsTimeZone
	 * @throws ParseException
	 */
	public static Date parseStringToDate(String formatStr, String dateString)
			throws ParseException {
		SimpleDateFormat format = new SimpleDateFormat(formatStr, new Locale(
				"en"));
		Date date = format.parse(dateString);
		return date;

	}

	/**
	 * Get the last date of next month
	 * 
	 * @param date
	 * @return
	 */
	public static Date getNextMonthLastDate(Date date) {
		Calendar c = Calendar.getInstance();
		c.setTime(date);
		int dd = c.get(Calendar.DAY_OF_MONTH);

		c.add(Calendar.MONTH, 2);
		c.add(Calendar.DAY_OF_MONTH, -dd);
		return c.getTime();
	}
	
	@Deprecated
	public static Date addDaysToDate(Date date, int days) {
		
		// CAST:
		Calendar c = Calendar.getInstance();
		c.setTime(date);
		c.add(Calendar.DAY_OF_MONTH, days);
		
		// AND RETURN:
		return c.getTime();
	}

	/**
	 * Add specific days to giving date.
	 * 
	 * @param date - not null
	 * @param days
	 * @param zone
	 * @return date
	 */
	public static Date addDaysToDate(Date date, int days, TimeZone zone) {
		if(null == zone){
			zone = TimeZone.getDefault();
		}
		
		DateTimeZone jodaZone = DateTimeZone.forID(zone.getID());
		DateTime dateTime = new DateTime(date, jodaZone);
		
		dateTime = dateTime.plusDays(days);
		
		return dateTime.toDate();
	}
	
	/**
	 * Add specify hours to specific date.
	 * 
	 * @param date - not null
	 * @param hours
	 * @param zone
	 * @return date
	 */
	public static Date addHoursToDate(Date date, int hours, TimeZone zone){
		if(null == zone){
			zone = TimeZone.getDefault();
		}
		
		DateTimeZone jodaZone = DateTimeZone.forID(zone.getID());
		DateTime dateTime = new DateTime(date, jodaZone);
		
		dateTime = dateTime.plusHours(hours);
		
		return dateTime.toDate();
	}

	public static Date trim(Date date) {
		Calendar cal = Calendar.getInstance();
		cal.clear(); // as per BalusC comment.
		cal.setTime(date);
		cal.set(Calendar.HOUR_OF_DAY, 0);
		cal.set(Calendar.MINUTE, 0);
		cal.set(Calendar.SECOND, 0);
		cal.set(Calendar.MILLISECOND, 0);
		return cal.getTime();
	}

	public static Date createDate(Integer year, Integer month, Integer day,
			Integer hour, Integer minute, Integer second) {
		Calendar cal = Calendar.getInstance();
		cal.clear(); // as per BalusC comment.
		cal.setTime(new Date());
		cal.set(Calendar.YEAR, year);
		cal.set(Calendar.MONTH, month);
		cal.set(Calendar.DATE, day);
		cal.set(Calendar.HOUR_OF_DAY, hour);
		cal.set(Calendar.MINUTE, minute);
		cal.set(Calendar.SECOND, second);
		cal.set(Calendar.MILLISECOND, 0);
		return cal.getTime();
	}

	public static Integer daysDifference(Date smaller, Date larger) {
		DateTime dt1 = new DateTime(trim(smaller));
		DateTime dt2 = new DateTime(trim(larger));
		return Days.daysBetween(dt1, dt2).getDays();
	}

	public static Integer monthsDifference(Date smaller, Date larger) {
		// trim the dates first
		Date smallerMonth = createDate(getYear(smaller), getMonth(smaller), 1,
				0, 0, 0);
		Date largerMonth = createDate(getYear(larger), getMonth(larger), 1, 0,
				0, 0);

		DateTime dt1 = new DateTime(smallerMonth);
		DateTime dt2 = new DateTime(largerMonth);
		return Months.monthsBetween(dt1, dt2).getMonths();
	}

	public static Integer getYear(Date date) {
		Calendar cal = Calendar.getInstance();
		cal.setTime(date);

		return cal.get(Calendar.YEAR);
	}

	public static Integer getMonth(Date date) {
		Calendar cal = Calendar.getInstance();
		cal.setTime(date);

		return cal.get(Calendar.MONTH);
	}

	public static Integer getDate(Date date) {
		Calendar cal = Calendar.getInstance();
		cal.setTime(date);

		return cal.get(Calendar.DATE);
	}
	
	/**
	 * Convert java.util.Date to javax.xml.datatype.XMLGregorianCalendar.
	 * 
	 * @param date
	 * @param includeTime
	 * 
	 * @return instant of XMLGregorianCalendar
	 */
	public static XMLGregorianCalendar convertDateToXMLGregorianCalendar(Date date, boolean includeTime) {
		if (null == date) {
			return null;
		}
		
		XMLGregorianCalendar xDate = null;

		GregorianCalendar cal = new GregorianCalendar();
		cal.setTime(date);
		try {
			xDate = DatatypeFactory.newInstance().newXMLGregorianCalendar(cal);
			xDate.setTimezone(DatatypeConstants.FIELD_UNDEFINED);
			if(!includeTime){
				xDate.setTime(DatatypeConstants.FIELD_UNDEFINED, DatatypeConstants.FIELD_UNDEFINED, 
						DatatypeConstants.FIELD_UNDEFINED, DatatypeConstants.FIELD_UNDEFINED);
			}

		} catch (Exception e) {
			return null;
		}

		return xDate;
	}
}
