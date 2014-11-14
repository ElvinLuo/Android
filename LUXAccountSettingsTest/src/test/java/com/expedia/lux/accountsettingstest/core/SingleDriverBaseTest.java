/**
 *
 * SuperTest.java
 *
 * Copyright 2012 Expedia, Inc. All rights reserved.
 * EXPEDIA PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 */

package com.expedia.lux.accountsettingstest.core;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.openqa.selenium.WebDriver;

/**
 * Prepare test web driver for test case.
 *
 * @author <a href="mailto:v-dluo@expedia.com">v-dluo</a>
 *
 */
public abstract class SingleDriverBaseTest {

    private static WebDriver driver;
    private String testName;

    /**
     * Initial WebDriver by configuration.
     *
     * @throws Exception
     */
    @Before
    final public void setUp() throws Exception {

        targetTestSetUp();

    }

    /**
     * Subclass should override this method to set test data. Test data would
     * like create a reservation, update some setting, or load some inventory.
     *
     */
    abstract public void targetTestSetUp();

    /**
     * Subclass should override this method to clean test data. Test data would
     * like delete a reservation, update some setting, or load some inventory.
     *
     */
    abstract public void targetTestTearDown();

    /**
     * Release WebDriver
     *
     * @throws Exception
     */
    @After
    public void tearDown() {
        targetTestTearDown();
    }

    @AfterClass
    public static void DriverQuit() {
        if (driver != null)
            driver.quit();
        driver = null;
    }

    protected static WebDriver getDriver() {
        if(SingleDriverBaseTest.driver == null) {
            SingleDriverBaseTest.driver = CreateWebDriverHelper.createWebDriver("");
        }
        return SingleDriverBaseTest.driver;
    }

    protected void setTestName(String testName) {
        this.testName = testName;
    }

    protected String getTestName() {
        return this.testName;
    }
}