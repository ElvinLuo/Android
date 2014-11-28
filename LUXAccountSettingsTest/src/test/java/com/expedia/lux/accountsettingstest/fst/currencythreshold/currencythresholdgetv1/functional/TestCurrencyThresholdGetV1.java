package com.expedia.lux.accountsettingstest.fst.currencythreshold.currencythresholdgetv1.functional;

import com.expedia.lux.accountsettingstest.common.services.framework.MessageResult;
import com.expedia.lux.accountsettingstest.common.services.serviceports.csharp.LodgingInventoryPropertyService.LodgingInventoryPropertyService;
import org.junit.Test;

import java.io.IOException;

/**
 * Created by elluo on 11/27/2014.
 */
public class TestCurrencyThresholdGetV1 {

    @Test
    public void testCurrencyThresholdGetMessage() throws IOException {
        String requestBody = "<?xml version='1.0'?>" +
                "<CurrencyThresholdGetRQ xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns='com:expedia:lis:lips:messages:propertysettingsget:v1'>" +
                "<BaseRequestIdentity xmlns='com:expedia:lis:Service:messages:BaseTypes:v2'>" +
                "<ClientId>2</ClientId>" +
                "</BaseRequestIdentity>" +
                "<CurrencyCode>USD</CurrencyCode>" +
                "</CurrencyThresholdGetRQ>";
        MessageResult messageResult = LodgingInventoryPropertyService.CurrencyThresholdGetV1.ServicePort.send(requestBody);
    }
}
