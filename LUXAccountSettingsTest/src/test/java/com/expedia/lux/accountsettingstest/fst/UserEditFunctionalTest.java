package com.expedia.lux.accountsettingstest.fst;

import com.expedia.lux.accountsettingstest.common.utils.ExcelReader;
import com.expedia.lux.accountsettingstest.common.utils.PageSetter;
import com.expedia.lux.accountsettingstest.core.SingleDriverBaseTest;
import junit.framework.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.Parameterized;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

import java.util.*;

/**
 * Created by v-dalv on 11/12/2014.
 */
@RunWith(Parameterized.class)
public class UserEditFunctionalTest extends SingleDriverBaseTest {

    private Map idValueMap;

    public UserEditFunctionalTest(ArrayList<Object> args) {

        List<String> idList = Arrays.asList(new String[]{
                "name_first", "name_last"
        });

        idValueMap = new HashMap<String, String>();

        for (int i = 0; i < idList.size(); i++) {
            idValueMap.put(idList.get(i).toString(), args.get(i + 1).toString());
        }
    }

    @Override
    public void targetTestSetUp() {

    }

    @Override
    public void targetTestTearDown() {

    }

    @Test
    public void testUserEditPage() {
        WebDriver driver = getDriver();
        driver.get("https://lux.expediapartnercentral.com.lisqa7.sb.karmalab.net:8443/PartnerCentral/AccountSettings.html");

        if (driver.getTitle().contains("Expedia PartnerCentral - Sign In")) {

            WebElement emailControl = driver.findElement(By.id("emailControl"));
            emailControl.sendKeys("admin");

            WebElement passwordControl = driver.findElement(By.id("passwordControl"));
            passwordControl.sendKeys("mttpower");

            WebElement signInButton = driver.findElement(By.id("signInButton"));
            signInButton.click();
        }

        new PageSetter(driver).SetValue(idValueMap);

    }

    @Test
    public void testGoogleSearch() throws Exception {

        WebDriver driver = getDriver();
        driver.get("https://www.google.com");

        WebElement searchBox = driver.findElement(By.xpath("//input[@type='text']"));
        searchBox.sendKeys("expedia");

        WebElement button = driver.findElement(By.name("btnG"));
        button.click();

        Thread.sleep(2000);

        WebElement resultDiv = driver.findElement(By.id("ires"));
        WebElement firstResult = resultDiv.findElement(By.xpath("ol/li/div/div/h3/a"));

        Assert.assertEquals("Expedia Travel: Vacations, Cheap Flights, Airline Tickets ...", firstResult.getText());
    }

    @Parameterized.Parameters
    public static Collection<Object[]> data() {
        ExcelReader excelReader = new ExcelReader("fileName", "sheetName");
        return excelReader.ReadSheet();
    }
}
