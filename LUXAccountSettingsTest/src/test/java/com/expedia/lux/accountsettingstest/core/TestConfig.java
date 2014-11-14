/**
 *
 * TestConfig.java
 *
 * Copyright 2013 Expedia, Inc. All rights reserved.
 * EXPEDIA PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 */

package com.expedia.lux.accountsettingstest.core;

import java.io.FileInputStream;
import java.text.MessageFormat;
import java.util.Map;
import java.util.Properties;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;

/**
 * Read test required data form configuration file.
 * 
 * @author <a href="mailto:v-dluo@expedia.com">v-dluo</a>
 * 
 */
public class TestConfig {
	private static Map<String, String> props;
	private final String BASE_PROPERITIES_PATH = "./configurations/base.properties";
	private final String FARM_PROPERITIES_PATH = "./configurations/{0}.properties";
	private final String TEST_ENVIRONMENT = "com.expedia.luxtest.environment/environment";

	/**
	 * Read the property files and store in m_props.
	 * 
	 */
	private TestConfig() {
		System.out.println("Reading configuations");
		try {
			props = new ConcurrentHashMap<String, String>();
			Set<Object> keys;

			// Read properties from base
			Properties baseProperies = new Properties();
			FileInputStream fis = new FileInputStream(BASE_PROPERITIES_PATH);
			baseProperies.load(fis);
			keys = baseProperies.keySet();
			for (Object key : keys) {
				String stringKey = (String) key;
				if (stringKey.contains("BrowserType")) {
					props.put("BrowserType", baseProperies.getProperty(stringKey));
				} else {
					props.put(stringKey, baseProperies.getProperty(stringKey));
				}
			}

			// read properties from farm
			String configFile = System.getProperty("configFile");
			if ((configFile == null) || (configFile.equals(""))) {
				// read properties of environment
				String env = System.getProperty("env");
				if ((env != null) && (!env.equals(""))) {
					switch (env.toUpperCase()) {
						case "LOCAL":{
							props.put(TEST_ENVIRONMENT, "local");
							break;
						}
						case "MAUI":{
							props.put(TEST_ENVIRONMENT, "maui");
							break;
						}
						case "PROD-CH":{
							props.put(TEST_ENVIRONMENT, "prod-ch");
							break;
						}
						case "PROD-PH":{
							props.put(TEST_ENVIRONMENT, "prod-ph");
							break;
						}
						case "QA504":{
							props.put(TEST_ENVIRONMENT, "qa504");
							break;
						}
                        case "QA8REGRESSION":{
                            props.put(TEST_ENVIRONMENT, "qa8Regression");
                            break;
                        }
                        case "MauiREGRESSION":{
                            props.put(TEST_ENVIRONMENT, "mauiRegression");
                            break;
                        }
						default:{
							props.put(TEST_ENVIRONMENT, "qa704");
							break;
						}
					}
				}  
					
				configFile =  props.get(TEST_ENVIRONMENT);
			}
			
			// read properties of browser
			String browser = System.getProperty("browser");
			if ((browser != null) && (!browser.equals(""))) {
				switch (browser.toUpperCase()) {
					case "IE7":{
						props.remove("BrowserType");
						props.put("com.expedia.luxtest.browser/BrowserType", "ie7");
						break;
					}
					case "IE8":{
						props.remove("BrowserType");
						props.put("com.expedia.luxtest.browser/BrowserType", "ie8");
						break;
					}
					case "IE9":{
						props.remove("BrowserType");
						props.put("com.expedia.luxtest.browser/BrowserType", "ie9");
						break;
					}
					case "IE10":{
						props.remove("BrowserType");
						props.put("com.expedia.luxtest.browser/BrowserType", "ie10");
						break;
					}
					case "IE11":{
						props.remove("BrowserType");
						props.put("com.expedia.luxtest.browser/BrowserType", "ie11");
						break;
					}
					case "FF":{
						props.remove("BrowserType");
						props.put("com.expedia.luxtest.browser/BrowserType", "ff");
						break;
					}
					case "SF":{
						props.remove("BrowserType");
						props.put("com.expedia.luxtest.browser/BrowserType", "sf");
						break;
					}
					case "OP":{
						props.remove("BrowserType");
						props.put("com.expedia.luxtest.browser/BrowserType", "op");
						break;
					}
					case "IOS":{
						props.remove("BrowserType");
						props.put("com.expedia.luxtest.browser/BrowserType", "ios");
						break;
					}
					case "AND":{
						props.remove("BrowserType");
						props.put("com.expedia.luxtest.browser/BrowserType", "and");
						break;
					}
					case "RAND":{
						props.remove("BrowserType");
						props.put("com.expedia.luxtest.browser/BrowserType", "rand");
						break;
					}
					default:{
						props.remove("BrowserType");
						props.put("com.expedia.luxtest.browser/BrowserType", "cr");
						break;
					}
				}
			} else {
				String browserType = props.get("BrowserType");
				props.remove("BrowserType");
				if (browserType.isEmpty()) {
					props.put("com.expedia.luxtest.browser/BrowserType", "rand");
				} else {
					props.put("com.expedia.luxtest.browser/BrowserType", browserType);
				}
			}
			
			// read properties of remote
            String isRemote = System.getProperty("remote");
            if(isRemote != null && (!isRemote.equals(""))){
                props.remove("com.expedia.luxtest.remote/isRemote");
                props.put("com.expedia.luxtest.remote/isRemote", isRemote);
            }
			
			String farmPath = MessageFormat.format(FARM_PROPERITIES_PATH, configFile);
			Properties farmsProperies = new Properties();
			fis = new FileInputStream(farmPath);
			farmsProperies.load(fis);
			keys = farmsProperies.keySet();
            String score = System.getProperty("SCORE");
            for (Object key : keys) {
				String stringKey = (String) key;
                if(stringKey.equals("com.expedia.lux.test.test/SCORE") && score != null && !score.equals("")) {
                    props.put(stringKey, score);
                } else {
                    props.put(stringKey, farmsProperies.getProperty(stringKey));
                }
			}
		} catch (Throwable e) {
			System.out.println("Properties file not found");
			e.printStackTrace();
		}
	}

	/**
	 * Make the configuration entry is singleton
	 * 
	 * @return TestConfig instance
	 */
	public static TestConfig getInstance() {
		return TestConfigHolder.s_instance;
	}

	private static class TestConfigHolder {
		static TestConfig s_instance = new TestConfig();
	}

	/**
	 * Get a configuration value by a key.
	 * 
	 * @param key
	 * @return Value of key.
	 */
	public static String getConfigValue(String key) {

		TestConfig.getInstance();
		String configValue = (String) TestConfig.props.get(key);

		if (null == configValue) {
			return null;
		}

		return configValue.trim();
	}

	/**
	 * Add a configuration key - value pair. NOTE: Don't add a key that already
	 * exists in configuration files, the original value will be covered.
	 * 
	 * @param key
	 * @param The
	 *            value from Configuration settings
	 */
	public void addConfiguration(String key, String value) {

		props.put(key, value);

	}

	/**
	 * This method does a lookup and returns the Configuration setting for the
	 * supplied 'key' as an integer. If no such key exists or the value cannot
	 * be retrieved, the method will return 0 [zero].
	 * 
	 * @param key
	 * 
	 * @return integer - The value from Configuration settings as an integer
	 * 
	 */
	public static int getConfigValueAsInteger(String key) {

		return Integer.parseInt(TestConfig.getConfigValue(key));

	}

	/**
	 * 
	 * This method does a lookup and returns the Configuration setting for the
	 * supplied 'key' as a boolean. If no such key exists, the value cannot be
	 * retrieved, or the value is a string other than 'true', the method will
	 * return false.
	 * 
	 * @param key
	 * 
	 * @return boolean - The value from Configuration settings as a boolean
	 * 
	 */
	public static boolean getConfigValueAsBoolean(String key) {

		String value = TestConfig.getConfigValue(key);
		return (value == null) ? false : ("true").equals(value);

	}

	public static String[] getStringArrayConfigValue(String key) {
		String configValue = getConfigValue(key);

		if (null == configValue || configValue.trim().isEmpty()) {
			return null;
		}

		return configValue.split(",");
	}

	public static boolean[] getBooleanArrayConfigValue(String key) {
		String[] configValues = getStringArrayConfigValue(key);

		if (null == configValues) {
			return null;
		}

		return convertToBooleanArray(configValues);
	}

	private static boolean[] convertToBooleanArray(String[] configValues) {
		boolean[] config = new boolean[configValues.length];
		
		for(int i = 0; i < configValues.length; i++) {
			config[i] = configValues[i].equals("true");
		}
		
		return config;
	}
}