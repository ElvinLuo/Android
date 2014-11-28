package com.expedia.lux.accountsettingstest.common.services.serviceports.csharp.LodgingInventoryPropertyService.CurrencyThreshold.CurrencyThresholdGetV1;

import com.expedia.lux.accountsettingstest.common.services.framework.MessageFacade;
import com.expedia.lux.accountsettingstest.common.services.framework.MessageResult;
import com.expedia.lux.accountsettingstest.common.services.serviceports.csharp.LodgingInventoryPropertyService.LodgingInventoryPropertyService;
import org.apache.http.client.methods.HttpUriRequest;

import java.io.IOException;

/**
 * Created by elluo on 11/27/2014.
 */
public class CurrencyThresholdGetV1ServicePort {

    public MessageResult send(String requestBodyString) throws IOException {
        HttpUriRequest request = LodgingInventoryPropertyService.CurrencyThresholdGetV1.RequestBuilder.buildRequest(requestBodyString);
        return MessageFacade.send(null, request);
    }

}
