package com.expedia.lux.accountsettingstest.common.pages;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;

/**
 * Created by elluo on 11/24/2014.
 */
public class ExpediaPartnerCentralHeader {

    protected WebDriver driver;

    public ExpediaPartnerCentralHeader() {
        this.driver = null;
    }

    public ExpediaPartnerCentralHeader(WebDriver driver) {
        this.driver = driver;
        PageFactory.initElements(driver, this);
    }

    @FindBy(id = "user_name_txt")
    private WebElement LoginUserNameLabel;

    @FindBy(id = "sign_out_link")
    private WebElement SignOutLink;

    public LoginPage logOut() {
        if (LoginUserNameLabel != null && SignOutLink != null) {
            LoginUserNameLabel.click();
            SignOutLink.click();

            return new LoginPage(driver);
        } else {
            return null;
        }
    }
}
