/**
 *
 * CreateWebDriverHelper.java
 *
 * Copyright 2012 Expedia, Inc. All rights reserved.
 * EXPEDIA PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 */

package com.expedia.lux.accountsettingstest.core;

import java.io.File;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.TimeUnit;

import org.openqa.selenium.Dimension;
import org.openqa.selenium.Point;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.firefox.FirefoxBinary;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.firefox.FirefoxProfile;
import org.openqa.selenium.ie.InternetExplorerDriver;
import org.openqa.selenium.remote.CapabilityType;
import org.openqa.selenium.remote.DesiredCapabilities;
import org.openqa.selenium.remote.RemoteWebDriver;
import org.openqa.selenium.safari.SafariDriver;

/**
 * Create WebDriver by configuration
 * 
 * @author <a href="mailto:v-dluo@expedia.com">v-dluo</a>
 * 
 */
public class CreateWebDriverHelper  {

	private static final String TEST_REMOTE = "com.expedia.luxtest.remote/isRemote";
	private static final String TEST_REMOTE_SITE = "com.expedia.luxtest.remote/remoteServer";
	private static final String TEST_BROWSER = "com.expedia.luxtest.browser/BrowserType";
	private static final String TEST_IE_DRIVER_PATH = "com.expedia.luxtest.browser/IEDriver";
	private static final String TEST_CHROME_DRIVER_PATH = "com.expedia.luxtest.browser/chromeDriver";
	private static final String TEST_FIREFOX_DRIVER_PATH = "com.expedia.luxtest.browser/firefox";
	private static final String TEST_REMOTE_BUILD = "com.expedia.luxtest.remote/build";
	private static final String TEST_TUNNEL_IDENTIFIER = "com.expedia.luxtest.remote/identifier";
	private static final String SAUCE_USER = "amelemolaligne12";
	private static final String SAUCE_KEY = "12ad92a0-2d01-43a4-945e-01cc3f8cc134";

	private static boolean isRemoteTest = TestConfig
			.getConfigValueAsBoolean(TEST_REMOTE);
	private static String identifier = TestConfig
			.getConfigValue(TEST_TUNNEL_IDENTIFIER);
	private static String buildNum = TestConfig
			.getConfigValue(TEST_REMOTE_BUILD);
	private static String testBrowser = TestConfig
			.getConfigValue(TEST_BROWSER);
	private static String remoteSite = TestConfig
			.getConfigValue(TEST_REMOTE_SITE);
	private static String chromeDriverPath = TestConfig
			.getConfigValue(TEST_CHROME_DRIVER_PATH);
	private static String ieDriverPath = TestConfig
			.getConfigValue(TEST_IE_DRIVER_PATH);
	private static String firefoxPath = TestConfig
			.getConfigValue(TEST_FIREFOX_DRIVER_PATH);
	
	/**
	 * Create a WebDriver entry by configuration setting.
	 * 
	 * @Precondition Configuration isRemoteTest, testBrowser provided and
	 *               availed. Remote Site is availed. Remote user can login the
	 *               remote site by remote key.
	 * @Postcondition If you run the case in local, it need your machine
	 *                installed the browser and browser driver application. If
	 *                you run the test in remote, Need you run the agent
	 *                connector in your machine.
	 * @param testName
	 *            Full test name of about test method. It should by
	 *            PackageName.ClassName.MethodName
	 * @return A WebDriver use to execute test case.
	 */
	public static WebDriver createWebDriver(String testName) {
		return createWebDriver(testName, testBrowser);
	}

	/**
	 * Create webdriver entry
	 * 
	 * @param testName
	 * @param browserType
	 *            1 is chrome, 2 is firefox, 0 is internet explorer, 3 is safari
	 * @param logging
	 * @return
	 */
	public static WebDriver createWebDriver(String testName, String testBrowser) {
		WebDriver driver = null;
		String isCloud = String.valueOf(isRemoteTest);
		
		if (!isRemoteTest) {
			DesiredCapabilities capabillities = new DesiredCapabilities();
			capabillities.setCapability(CapabilityType.SUPPORTS_WEB_STORAGE,
					false);
			switch (testBrowser.toUpperCase()) {
			case "IE":
				// Need set iedriver.exe file path
				System.setProperty("webdriver.ie.driver", ieDriverPath);
				driver = new InternetExplorerDriver(capabillities);
				break;
			case "FF":
				// TODO: Not stable
				capabillities.setCapability(FirefoxDriver.BINARY,
						new FirefoxBinary(new File(firefoxPath)));
				FirefoxProfile profile = new FirefoxProfile();
				profile.setAcceptUntrustedCertificates(true);
				capabillities.setCapability(FirefoxDriver.PROFILE, profile);
				driver = new FirefoxDriver(capabillities);
				break;
			case "SF":
				// TODO: No one's machine has safari installed in SZ
				driver = new SafariDriver(capabillities);
				break;
			default:
				// Need set the chromedriver.exe file path.
				System.setProperty("webdriver.chrome.driver", chromeDriverPath);
				driver = new ChromeDriver(capabillities);
				break;
			}
		} else {
			DesiredCapabilities capabillities;
			switch (testBrowser.toUpperCase()) {
			case "IE9":
				capabillities = DesiredCapabilities.internetExplorer();
				capabillities.setCapability(CapabilityType.VERSION, "9");
				break;
			case "FF":
				capabillities = DesiredCapabilities.firefox();
				break;
			case "SF":
				capabillities = DesiredCapabilities.safari();
				capabillities.setCapability("platform", "OS X 10.6");
				capabillities.setCapability("version", "5");
				capabillities.setCapability("disable-popup-handler", true);
				break;
			case "IE7":
				capabillities = DesiredCapabilities.internetExplorer();
				capabillities.setCapability(CapabilityType.VERSION, "7");
				break;
			case "IE8":
				capabillities = DesiredCapabilities.internetExplorer();
				capabillities.setCapability(CapabilityType.VERSION, "8");
				break;
			case "IE10":
				capabillities = DesiredCapabilities.internetExplorer();
				capabillities.setCapability(CapabilityType.VERSION, "10");
				break;
			case "IE11":
				capabillities = DesiredCapabilities.internetExplorer();
				capabillities.setCapability("version", "11");
				break;
			case "OP":
				capabillities = DesiredCapabilities.opera();
				capabillities.setCapability("version", "12");
				break;
			//Note: may not support HTTPS via sauceConnect with mobile platform yet
			case "IOS":
				capabillities = DesiredCapabilities.iphone();
				capabillities.setCapability("version", "6.1");
				capabillities.setCapability("platform", "OS X 10.8");
				capabillities.setCapability("device-orientation", "portrait");
				capabillities.setCapability("disable-popup-handler", true);
				break;
			case "AND":
				capabillities = DesiredCapabilities.android();
				capabillities.setCapability("version", "4.0");
				capabillities.setCapability("platform", "Linux");
				capabillities.setCapability("device-type", "tablet");
				capabillities.setCapability("device-orientation", "portrait");
				break;
			case "SAUCE":
				return createWebDriverSaucelab(testName + " (SauceLabs)");
			default:
				capabillities = DesiredCapabilities.chrome();
				capabillities.setBrowserName("chrome");
				break;
			}
			
			try {
				driver = new RemoteWebDriver(new URL("http://10.208.52.16:5555/wd/hub"), capabillities);
			}
			catch (MalformedURLException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		
		fillBrowserName(testBrowser, isCloud);
		return driver;
	}

	public static WebDriver createWebDriverSaucelab(String testName) {
		DesiredCapabilities capabillities = new DesiredCapabilities();
		// Set test name, use to show the test case name on remote site
		List tags = new ArrayList<String>();
		tags.add("LUX5");
		tags.add("PromotionsTest");
		capabillities.setCapability("tags", tags);
		capabillities.setCapability("build", buildNum);
		capabillities.setCapability("idle-timeout", 120);
		capabillities.setCapability("disable-popup-handler", true);
		capabillities.setCapability("record-video", false);
		capabillities.setCapability("video-upload-on-pass", false);
		capabillities.setCapability("capture-html", true);
		capabillities.setCapability("webdriver.remote.quietExceptions", false);
		capabillities.setCapability("sauce-advisor", false);
		// Set test use and password for remote site
		capabillities.setCapability("username", SAUCE_USER);
		capabillities.setCapability("accessKey", SAUCE_KEY);
		capabillities.setCapability("tunnel-identifier", identifier);
		capabillities.setCapability("name", testName);
		capabillities = DesiredCapabilities.chrome();
		capabillities.setBrowserName("chrome");

		try {
			WebDriver driver = new RemoteWebDriver(new URL(remoteSite), capabillities);
			// Set driver implicit time to wait for element to appear
			driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
			return driver;
		} catch (MalformedURLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return null;
	}

	/**
	 * fills the browser name for logging purposes
	 * 
	 * @param logging
	 */
	public static void fillBrowserName(String browserName, String isCloud) {
		System.out.println("Sauce connection is " + isCloud);
		switch (browserName.toUpperCase()) {
			case "IE7":
				System.out.println("Running with IE7 on PC...");
				break;
			case "IE8":
				System.out.println("Running with IE8 on PC...");
				break;
			case "IE9":
				System.out.println("Running with IE9 on PC...");
				break;
			case "IE10":
				System.out.println("Running with IE10 on PC...");
				break;
			case "IE11":
				System.out.println("Running with IE11 on PC...");
				break;
			case "FF":
				System.out.println("Running with Firefox on PC...");
				break;
			case "SF":
				System.out.println("Running with Safari on Mac...");
				break;
			case "OP":
				System.out.println("Running with Opera on Linux...");
				break;
			case "IOS":
				System.out.println("Running with Safari on IOS device...");
				break;
			case "AND":
				System.out.println("Running with Chrome on Android device...");
				break;
			default:
				System.out.println("Running with Chrome on PC...");
				break;
		}
	}

	public static void setBrowserSize(WebDriver driver, Integer width,
			Integer height) {
		driver.manage().window().setPosition(new Point(0, 0));
		driver.manage().window().setSize(new Dimension(width, height));
	}
}