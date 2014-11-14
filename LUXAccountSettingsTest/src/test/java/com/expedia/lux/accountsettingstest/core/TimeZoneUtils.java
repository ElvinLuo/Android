/**
 *
 * TimeZoneUtil.java
 *
 * Copyright 2012 Expedia, Inc. All rights reserved.
 * EXPEDIA PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 */

package com.expedia.lux.accountsettingstest.core;

import java.util.Calendar;
import java.util.Map;
import java.util.TimeZone;
import java.util.concurrent.ConcurrentHashMap;


/**
 * The TimeZoneUtil class provides methods to process time zone relevant operations.
 * 
 * @author <a href="mailto:v-diqiu@expedia.com">v-diqiu</a>
 * 
 */
public class TimeZoneUtils
{
	/**
	 *  The time zone map hold the mapping.
	 *  The ConcurrentHashMap is applied to make it as a thread safe map.
	 */
	private static Map<Integer, String> s_timeZoneMap = new ConcurrentHashMap<Integer, String>();

	/**
	 * Private constructor
	 */
	private TimeZoneUtils() {	
	}
	
	/**
	 * Get timeZoneOffSet by timeZone ID.
	 * 
	 * @param timeZoneId
	 * @return timeZoneOffSet
	 * @throws NullPointerException
	 */
	public static int getTimeZoneOffSet(int timeZoneId) {
		// Get timeZone by timeZone ID.
		TimeZone timeZone = TimeZoneUtils.getTimeZoneById(timeZoneId);

		if (null == timeZone) {
			throw new IllegalArgumentException("The giving time zone ID is invalid!");
		}

		Calendar calendar = Calendar.getInstance(timeZone);
		int timeZoneOffSet = -(calendar.get(Calendar.DST_OFFSET) + calendar.get(Calendar.ZONE_OFFSET)) / (60 * 1000);

		return timeZoneOffSet;
	}
	
	/**
	 * Get a time zone by a given time zone id.
	 * 
	 * @param timeZoneId
	 *            The time zone id stored in DB, allowed value is one of the
	 *            integers from 1 to 75.
	 * @return	A TimeZone object of java. 
	 * 			Null if the parameter timeZoneId is invalid.
	 */
	public static TimeZone getTimeZoneById(int timeZoneId)
	{
		String timeZoneStr = s_timeZoneMap.get(timeZoneId);
		if(timeZoneStr == null)
		{
			// return null if time zone cannot be reached from the map by this timeZoneId.
			System.out.println("The time zone id " + timeZoneId 
					+ " is invalid, it should be one of the integers from 1 to 75");
			
			return null;
		}
		TimeZone timeZone = TimeZone.getTimeZone(timeZoneStr);
		
		return timeZone;
	}
	
	/**
	 * Get timeZone by giving time zone ID, return null if time zone ID equlas
	 * to 0.
	 * 
	 * @param timeZoneId
	 * @return timeZone
	 * @throws Exception
	 */
	public static TimeZone getTimeZone(int timeZoneId){

		if (0 != timeZoneId) {
			TimeZone timeZone = getTimeZoneById(timeZoneId);

			return timeZone;
		}

		return null;
	}

	/**
	 * Initialize the time zone map.
	 */
	static
	{
		s_timeZoneMap.put(1, "Asia/Kabul");
		s_timeZoneMap.put(2, "America/Anchorage");
		s_timeZoneMap.put(3, "Asia/Riyadh");
		s_timeZoneMap.put(4, "Asia/Muscat");
		s_timeZoneMap.put(5, "Asia/Baghdad");
		s_timeZoneMap.put(6, "America/Halifax");
		s_timeZoneMap.put(7, "Australia/Darwin");
		s_timeZoneMap.put(8, "Australia/Sydney");
		s_timeZoneMap.put(9, "Atlantic/Azores");
		s_timeZoneMap.put(10, "America/Regina");

		s_timeZoneMap.put(11, "Atlantic/Cape_Verde");
		s_timeZoneMap.put(12, "Asia/Tbilisi");
		s_timeZoneMap.put(13, "Australia/Adelaide");
		s_timeZoneMap.put(14, "America/Managua");
		s_timeZoneMap.put(15, "Asia/Dhaka");
		s_timeZoneMap.put(16, "Europe/Belgrade");
		s_timeZoneMap.put(17, "Europe/Sarajevo");
		s_timeZoneMap.put(18, "Asia/Magadan");
		s_timeZoneMap.put(19, "America/Chicago");
		s_timeZoneMap.put(20, "Asia/Hong_Kong");

		s_timeZoneMap.put(21, "Etc/GMT+12");
		s_timeZoneMap.put(22, "Africa/Nairobi");
		s_timeZoneMap.put(23, "Australia/Brisbane");
		s_timeZoneMap.put(24, "Europe/Bucharest");
		s_timeZoneMap.put(25, "America/Sao_Paulo");
		s_timeZoneMap.put(26, "America/New_York");
		s_timeZoneMap.put(27, "Africa/Cairo");
		s_timeZoneMap.put(28, "Asia/Yekaterinburg");
		s_timeZoneMap.put(29, "Pacific/Fiji");
		s_timeZoneMap.put(30, "Europe/Helsinki");

		s_timeZoneMap.put(31, "Europe/London");
		s_timeZoneMap.put(32, "America/Godthab");
		s_timeZoneMap.put(33, "Africa/Casablanca");
		s_timeZoneMap.put(34, "Europe/Istanbul");
		s_timeZoneMap.put(35, "Pacific/Honolulu");
		s_timeZoneMap.put(36, "Asia/Calcutta");
		s_timeZoneMap.put(37, "Asia/Tehran");
		s_timeZoneMap.put(38, "Asia/Jerusalem");
		s_timeZoneMap.put(39, "Asia/Seoul");
		s_timeZoneMap.put(40, "America/Mexico_City");

		s_timeZoneMap.put(41, "America/Noronha");
		s_timeZoneMap.put(42, "America/Denver");
		s_timeZoneMap.put(43, "Asia/Rangoon");
		s_timeZoneMap.put(44, "Asia/Novosibirsk");
		s_timeZoneMap.put(45, "Asia/Katmandu");
		s_timeZoneMap.put(46, "Pacific/Auckland");
		s_timeZoneMap.put(47, "America/St_Johns");
		s_timeZoneMap.put(48, "Asia/Irkutsk");
		s_timeZoneMap.put(49, "Asia/Krasnoyarsk");
		s_timeZoneMap.put(50, "America/Santiago");

		s_timeZoneMap.put(51, "America/Los_Angeles");
		s_timeZoneMap.put(52, "Europe/Paris");
		s_timeZoneMap.put(53, "Europe/Moscow");
		s_timeZoneMap.put(54, "America/Buenos_Aires");
		s_timeZoneMap.put(55, "America/Bogota");
		s_timeZoneMap.put(56, "America/Caracas");
		s_timeZoneMap.put(57, "Pacific/Apia");
		s_timeZoneMap.put(58, "Asia/Bangkok");
		s_timeZoneMap.put(59, "Asia/Singapore");
		s_timeZoneMap.put(60, "Africa/Johannesburg");

		s_timeZoneMap.put(61, "Asia/Colombo");
		s_timeZoneMap.put(62, "Asia/Taipei");
		s_timeZoneMap.put(63, "Australia/Hobart");
		s_timeZoneMap.put(64, "Asia/Tokyo");
		s_timeZoneMap.put(65, "Pacific/Tongatapu");
		s_timeZoneMap.put(66, "America/Indianapolis");
		s_timeZoneMap.put(67, "America/Phoenix");
		s_timeZoneMap.put(68, "Asia/Vladivostok");
		s_timeZoneMap.put(69, "Australia/Perth");
		s_timeZoneMap.put(70, "Africa/Lagos");

		s_timeZoneMap.put(71, "Europe/Berlin");
		s_timeZoneMap.put(72, "Asia/Karachi");
		s_timeZoneMap.put(73, "Pacific/Guam");
		s_timeZoneMap.put(74, "Asia/Yakutsk");
		s_timeZoneMap.put(75, "America/Chihuahua");
	}
}
