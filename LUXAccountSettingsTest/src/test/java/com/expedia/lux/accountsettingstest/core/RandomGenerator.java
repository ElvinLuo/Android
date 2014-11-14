/**
 *
 * RandomGenerator.java
 *
 * Copyright 2013 Expedia, Inc. All rights reserved.
 * EXPEDIA PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 */
package com.expedia.lux.accountsettingstest.core;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Random;

/**
 * Generates random values
 * 
 * @author pwang
 * 
 */
public class RandomGenerator {

	private static final String AVAILABLE_CHARACTERS = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
	public static final String ENGLISH_CHARACTERS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
	public static final String NON_ENGLISH_CHARACTERS = "1234567890";
	// Using a common Random can make the value more "random"
	private static Random s_random = new Random(new Date().getTime());

	/**
	 * Generates a random number between min and max, inclusive of max
	 * 
	 * @param min
	 * @param max
	 * @return
	 */
	public static Integer getRandomInteger(int min, int max) {
		return s_random.nextInt(1 + max - min) + min;
	}

	/**
	 * Generates a random string
	 * 
	 * @param minLength
	 * @param maxLength
	 * @return
	 */
	public static String getRandomString(int minLength, int maxLength) {
		int length = getRandomInteger(minLength, maxLength);
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < length; i++) {
			int index = getRandomInteger(0, AVAILABLE_CHARACTERS.length() - 1);
			sb.append(AVAILABLE_CHARACTERS.charAt(index));
		}
		return sb.toString();
	}

	/**
	 * Gets random non english characters
	 * 
	 * @param minLength
	 * @param maxLength
	 * @return
	 */
	public static String getRandomNonEnglishString(int minLength, int maxLength) {
		int length = getRandomInteger(minLength, maxLength);
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < length; i++) {
			int index = getRandomInteger(0, NON_ENGLISH_CHARACTERS.length() - 1);
			sb.append(NON_ENGLISH_CHARACTERS.charAt(index));
		}
		return sb.toString();
	}

	/**
	 * Gets random english characters
	 * 
	 * @param minLength
	 * @param maxLength
	 * @return
	 */
	public static String getRandomEnglish(int minLength, int maxLength) {
		int length = getRandomInteger(minLength, maxLength);
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < length; i++) {
			int index = getRandomInteger(0, ENGLISH_CHARACTERS.length() - 1);
			sb.append(ENGLISH_CHARACTERS.charAt(index));
		}
		return sb.toString();
	}

	/**
	 * Generates a random value from a enum
	 * 
	 * @param enumClass
	 * @return
	 */
	public static <T extends Enum<T>> T getRandomEnum(T[] enumValues) {
		int index = getRandomInteger(0, enumValues.length - 1);
		return enumValues[index];
	}

	/**
	 * Generates a random value from a enum, with no overlap
	 * 
	 * @param enumClass
	 * @return
	 */
	public static <T extends Enum<T>> T getRandomEnumNoOverlap(T[] enumValues,
			List<T> usedValues) {
		List<T> valuesOne = new ArrayList<T>();
		List<T> valuesTwo = usedValues;

		for (T t : enumValues) {
			valuesOne.add(t);
		}

		valuesOne.removeAll(valuesTwo);

		int index = getRandomInteger(0, valuesOne.size() - 1);
		return valuesOne.get(index);
	}

	/**
	 * Gets a random value in a list
	 * 
	 * @param list
	 * @return
	 */
	public static String getRandomList(List<String> list) {
		int index = getRandomInteger(0, list.size() - 1);
		return list.get(index);
	}

	/**
	 * Gets a random value in the list that does not exist in usedList
	 * 
	 * @param list
	 * @param usedList
	 * @return
	 */
	public static String getRandomListNoOverlap(List<String> list,
			List<String> usedList) {
		List<String> temp = list;
		temp.removeAll(usedList);
		int index = getRandomInteger(0, temp.size() - 1);
		return temp.get(index);
	}

	/**
	 * Generate random date in scope [startDate, endDate]
	 * 
	 * @param startDate
	 * @param endDate
	 * @return The date generated.
	 */
	public static Date getRandomDate(Integer minFromToday, Integer maxFromToday) {
		Integer daysDifference = RandomGenerator.getRandomInteger(minFromToday,
				maxFromToday);

		Date now = new Date();
		return DateUtils.addDaysToDate(now, daysDifference);
	}

	public static List<Date> getRandomDatesInRange(Integer minFromToday,
			Integer maxFromToday, Integer minConsecutiveDates) {
		List<Date> dates = new ArrayList<Date>();

		Date today = new Date();
		Integer consecutiveStartDate = minFromToday;

		for (int i = minFromToday; i < maxFromToday; i++) {

			Boolean include = RandomGenerator.getRandomBoolean();
			if (include) {
				while (((i - consecutiveStartDate) < minConsecutiveDates)
						&& (i < maxFromToday)) {
					dates.add(DateUtils.addDaysToDate(today, i));
					i++;
				}
				i--;
			} else {
				consecutiveStartDate = i;
			}
		}
		return dates;
	}

	public static List<Date> getRandomDates(Integer startFromToday,
			Integer numberOfDays, Integer minConsecutiveDates, Boolean moreThanHalfBlackout) {
		List<Date> dates = new ArrayList<Date>();

		Date today = new Date();

		int count = startFromToday;
		int totalCount = 0;
		int consecutiveCount = 0;
		int blackoutCount = 0;
		
		while (totalCount < numberOfDays) {

			Boolean include = RandomGenerator.getRandomBoolean();
			if (moreThanHalfBlackout) {
				while (totalCount != 0 &&
						blackoutCount < (totalCount+ 2)) {
					count++;
					blackoutCount++;
				}
			}
					
			if (include) {
				while ((consecutiveCount < minConsecutiveDates)
						&& (totalCount < numberOfDays)) {
					dates.add(DateUtils.addDaysToDate(today, count));
					totalCount++;
					consecutiveCount ++;
					count++;
				}
			} else {
				if (totalCount != 0) {
					blackoutCount++;
				}
				count++;
			}
			consecutiveCount = 0;
		}
		return dates;
	}

	/**
	 * Generate random boolean value
	 * 
	 * @return The boolean value generated
	 */
	public static boolean getRandomBoolean() {
		return (s_random.nextBoolean());
	}

	/**
	 * Generate random double
	 * 
	 * @param minAmount
	 * @param maxAmount
	 * @return
	 */
	public static double getRandomDouble(double minAmount, double maxAmount) {
		return minAmount + s_random.nextDouble() * (maxAmount - minAmount);
	}

	public static double getRandomDoubleDecimalPlaces(double minAmount,
			double maxAmount, int decimalPlaces) {
		Double randomDouble = getRandomDouble(minAmount, maxAmount);
		Double temp = 1.0;
		for (int i = 0; i < decimalPlaces; i++) {
			temp = temp * 10.0;
		}
		Double tempDouble = (randomDouble * temp);
		return tempDouble.longValue() / temp;

	}

	/**
	 * 
	 * @param minAmount
	 * @param maxAmount
	 * @return
	 */
	public static double getRandomDoubleWithDecimal(double minAmount,
			double maxAmount) {
		Double randomOfDouble = getRandomDouble(minAmount, maxAmount);
		String doubleString = randomOfDouble.toString();
		int locationOfPoint = doubleString.indexOf(".");
		return Double.parseDouble(doubleString
				.substring(0, locationOfPoint + 3));
	}

}
