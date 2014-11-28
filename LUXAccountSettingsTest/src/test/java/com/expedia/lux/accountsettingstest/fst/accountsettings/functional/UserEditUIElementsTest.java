package com.expedia.lux.accountsettingstest.fst;

import com.expedia.lux.accountsettingstest.core.SingleDriverBaseTest;
import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.Parameterized;
import org.openqa.selenium.WebDriver;

import java.util.Arrays;
import java.util.Collection;

@RunWith(Parameterized.class)
public class UserEditUIElementsTest extends SingleDriverBaseTest {

    private String userName;
    private String passWord;

    public UserEditUIElementsTest(String testName, String userName, String passWord) {
        super.setTestName(testName);
        this.userName = userName;
        this.passWord = passWord;
    }

    /**
     * @throws Exception
     */
    @BeforeClass
    public static void setUpBeforeClass() throws Exception {
        System.out.println("Starting test class : " + UserEditUIElementsTest.class.getName());
    }

    /**
     * @throws Exception
     */
    @AfterClass
    public static void tearDownAfterClass() throws Exception {
        System.out.println("Ending test class : " + UserEditUIElementsTest.class.getName());
    }

    @Override
    public void targetTestSetUp() {
        System.out.println("Starting test...................");
    }

    @Override
    public void targetTestTearDown() {
        System.out.println("Ending test...................");
    }

    @Parameterized.Parameters
    public static Collection<Object[]> testData() {
        return Arrays.asList(new Object[][]{
                {"", "hotautoxn", "hottest"},
                {"", "v-dalv", "123456"}
        });
    }

    @Test
    public void CheckUIElementsFromOption() throws Exception {
        WebDriver driver = SingleDriverBaseTest.getDriver(); // initial driver
    }

    private boolean CheckAllUIElementsPresent(WebDriver driver) throws Exception {
        boolean pass = true;

        return pass;
    }
}
