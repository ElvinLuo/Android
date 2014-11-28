package com.expedia.lux.accountsettingstest.common.pages;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;

/**
 * Created by elluo on 11/24/2014.
 */
public class LoginPage {

    private final WebDriver driver;

    public LoginPage() {
        this.driver = null;
    }

    public LoginPage(WebDriver driver) {
        if (!driver.getTitle().contains("Expedia PartnerCentral - ")) {
            throw new IllegalStateException("This is not the login page.");
        }

        this.driver = driver;
        PageFactory.initElements(driver, this);
    }

    @FindBy(id = "emailControl")
    public WebElement UserNameInput;

    @FindBy(id = "passwordControl")
    public WebElement PasswordInput;

    @FindBy(id = "signInButton")
    public WebElement LoginButton;

    public UserEditPage login(String userName, String password) {
        if (UserNameInput != null && PasswordInput != null && LoginButton != null) {

            UserNameInput.clear();
            UserNameInput.sendKeys(userName);

            PasswordInput.clear();
            PasswordInput.sendKeys(password);

            LoginButton.click();

            return new UserEditPage(driver);
        } else {
            return null;
        }
    }
}
