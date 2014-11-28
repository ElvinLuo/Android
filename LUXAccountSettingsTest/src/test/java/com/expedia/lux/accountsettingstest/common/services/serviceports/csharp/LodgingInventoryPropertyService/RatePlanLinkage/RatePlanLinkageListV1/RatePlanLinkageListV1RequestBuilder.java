package com.expedia.lux.accountsettingstest.common.services.serviceports.csharp.LodgingInventoryPropertyService.RatePlanLinkage.RatePlanLinkageListV1;

import com.expedia.lux.accountsettingstest.common.services.serviceports.csharp.LodgingInventoryPropertyService.LodgingInventoryPropertyService;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpUriRequest;

/**
 * Created by elluo on 11/27/2014.
 */
public class RatePlanLinkageListV1RequestBuilder {

    public static HttpUriRequest buildRequest(int hotelId) {
        HttpGet request = new HttpGet(String.format(LodgingInventoryPropertyService.BaseUriString + LodgingInventoryPropertyService.RatePlanLinkageListV1.URI, hotelId));
        request.addHeader("Client-ID", "1");
        request.addHeader("Content-Type", "application/json");
        return request;
    }

}
