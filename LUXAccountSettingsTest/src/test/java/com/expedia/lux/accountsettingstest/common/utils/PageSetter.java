package com.expedia.lux.accountsettingstest.common.utils;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

import java.util.Map;

/**
 * Created by elluo on 11/13/2014.
 */
public class PageSetter {

    private WebDriver driver;

    public PageSetter(WebDriver driver) {
        this.driver = driver;
    }

    public void SetValue(Map<String, String> idValueMap) {
        for (String id : idValueMap.keySet()) {
            WebElement element = driver.findElement(By.id(id));
            element.clear();
            element.sendKeys(idValueMap.get(id));
        }
    }
}
