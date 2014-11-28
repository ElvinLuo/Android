package com.expedia.lux.accountsettingstest.common.services.serviceports.csharp.LodgingInventoryPropertyService.CurrencyThreshold.CurrencyThresholdGetV1;

import com.expedia.lux.accountsettingstest.common.services.serviceports.csharp.LodgingInventoryPropertyService.LodgingInventoryPropertyService;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.client.methods.HttpUriRequest;
import org.apache.http.entity.StringEntity;

import java.io.UnsupportedEncodingException;

/**
 * Created by elluo on 11/27/2014.
 */
public class CurrencyThresholdGetV1RequestBuilder {

    public HttpUriRequest buildRequest(String requestBodyString) throws UnsupportedEncodingException {
        HttpPost request = new HttpPost(LodgingInventoryPropertyService.BaseUriString + LodgingInventoryPropertyService.CurrencyThresholdGetV1.URI);
        request.addHeader("Content-Type", "text/xml");
        StringEntity stringEntity = new StringEntity(requestBodyString);
        request.setEntity(stringEntity);
        return request;
    }

}
