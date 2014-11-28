package com.expedia.lux.accountsettingstest.common.pages;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;

/**
 * Created by elluo on 11/13/2014.
 */
public class UserEditPage extends ExpediaPartnerCentralHeader {

    public UserEditPage() {
    }

    public UserEditPage(WebDriver driver) {
        if (!driver.getTitle().contains("Expedia PartnerCentral - Expedia PartnerCentral - My Account")) {
            throw new IllegalStateException("This is not the user edit page.");
        }

        super.driver = driver;
        PageFactory.initElements(driver, this);
    }

    @FindBy(id = "firstName")
    public WebElement FirstNameLabel;

    @FindBy(id = "name_first")
    public WebElement FirstNameInput;

    @FindBy(id = "lastName")
    public WebElement LastNameLabel;

    @FindBy(id = "name_last")
    public WebElement LastNameInput;

}
